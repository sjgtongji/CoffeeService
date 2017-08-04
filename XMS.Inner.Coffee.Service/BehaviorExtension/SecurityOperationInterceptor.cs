using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using System.Reflection;
using System.ServiceModel.Web;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Security.Cryptography;
using System.IO;
using System.Linq;

using XMS.Core;
using XMS.Core.Web;
using XMS.Core.Logging;
using XMS.Core.WCF;

namespace XMS.Inner.Coffee.Service
{
    public class SecurityOperationInterceptor : OperationInterceptor
    {
        private const string key = "P4MwFCLS";

        private static Dictionary<string, long> dicVisited = new Dictionary<string, long>();
        private static Dictionary<string, int> dicPermanentBlocked = new Dictionary<string, int>();

        private static DateTime tLastReleaseTime = System.DateTime.Now;
        private static object objLock = new object();

        private bool needVerify = false;
        private bool needToken = false;

        public SecurityOperationInterceptor(OperationDescription operationDescription, IOperationInvoker invoker, bool showExceptionDetailToClient, bool needVerify, bool needToken)
            : base(operationDescription, invoker, showExceptionDetailToClient)
        {
            this.needVerify = needVerify;
            this.needToken = needToken;
        }

        /// <summary>
        /// 在对方法进行调用前执行。
        /// </summary>
        /// <param name="instance">要调用的对象。</param>
        /// <param name="operationDescription">要调用对象的方法的说明。</param>
        /// <param name="inputs">方法的输入。</param>
        protected override void OnInvoke(object instance, OperationDescription operationDescription, object[] inputs)
        {
            this.EnsureRequest(operationDescription);
        }

        private Dictionary<string, DateTime> deviceLastRequestTimes = new Dictionary<string, DateTime>();

        /// <summary>
        /// 验证请求是否合法
        /// </summary>
        private void EnsureRequest(OperationDescription operationDescription)
        {
            OperationContext operationContext = OperationContext.Current;

            HttpContext httpContext = HttpContext.Current;

            string restecname = null;
            string token = null;

            #region 计算传入 Restecname 和 token 头
            if (operationContext != null)
            {
                int headerIndex = operationContext.IncomingMessageHeaders.FindHeader("Restecname", String.Empty);
                if (headerIndex >= 0)
                {
                    restecname = operationContext.IncomingMessageHeaders.GetHeader<string>(headerIndex);
                }
                else
                {
                    if (operationContext.IncomingMessageProperties.ContainsKey(HttpRequestMessageProperty.Name))
                    {
                        HttpRequestMessageProperty requestMessageProperty = operationContext.IncomingMessageProperties[HttpRequestMessageProperty.Name] as HttpRequestMessageProperty;
                        if (requestMessageProperty != null)
                        {
                            restecname = requestMessageProperty.Headers.Get("Restecname");
                        }
                    }
                }

                headerIndex = operationContext.IncomingMessageHeaders.FindHeader("token", String.Empty);
                if (headerIndex >= 0)
                {
                    token = operationContext.IncomingMessageHeaders.GetHeader<string>(headerIndex);
                }
                else
                {
                    if (operationContext.IncomingMessageProperties.ContainsKey(HttpRequestMessageProperty.Name))
                    {
                        HttpRequestMessageProperty requestMessageProperty = operationContext.IncomingMessageProperties[HttpRequestMessageProperty.Name] as HttpRequestMessageProperty;
                        if (requestMessageProperty != null)
                        {
                            token = requestMessageProperty.Headers.Get("token");
                        }
                    }
                }
            }
            else if (httpContext != null)
            {
                System.Web.HttpRequest httpRequest = httpContext.TryGetRequest();

                if (httpRequest != null)
                {
                    // 从查询参数中获取代理并用完整模式解析
                    restecname = httpRequest["Restecname"];

                    token = httpRequest["token"];
                }
            }
            #endregion
        }

        #region UploadLog

        private void SyncClientUploadLog(string regionName, string deviceNo)
        {
            if (regionName == "Sync_Client_ShouldUploadLog")
            {
                if (Container.CacheService.RemoteCache.GetItem("Sync_Client_ShouldUploadLog", deviceNo) != null)
                {
                    DateTime time = Container.CacheService.RemoteCache.GetItem("Sync_Client_ShouldUploadLog", deviceNo).ConvertTo<DateTime>(DateTime.Now);
                    Container.CacheService.RemoteCache.RemoveItem("Sync_Client_ShouldUploadLog", deviceNo);

                    XMS.Core.Logging.LogUtil.Warn("sync_uploadLogCommand.log",
                        String.Format("通知设备 {0} 需要上传日志", deviceNo),
                        "Sync", null);

                    // 510，一种特殊的错误，该错误说明客户端发生了异常行为，要求客户端提交运行日志到服务器指定位置以进行分析
                    throw new RequestException(510, time.ToMilliSecondsFrom1970L().ToString(), String.Format("通知设备 {0} 需要上传日志", deviceNo), null);
                }
            }
            else if (regionName == "Sync_Client_ShouldUploadOperateLog")
            {
                if (Container.CacheService.RemoteCache.GetItem("Sync_Client_ShouldUploadOperateLog", deviceNo) != null)
                {
                    DateTime time = Container.CacheService.RemoteCache.GetItem("Sync_Client_ShouldUploadOperateLog", deviceNo).ConvertTo<DateTime>(DateTime.Now);
                    Container.CacheService.RemoteCache.RemoveItem("Sync_Client_ShouldUploadOperateLog", deviceNo);

                    XMS.Core.Logging.LogUtil.Warn("sync_uploadLogCommand.log",
                        String.Format("通知设备 {0} 需要上传操作异常日志", deviceNo),
                        "Sync", null);

                    throw new RequestException(511, time.ToMilliSecondsFrom1970L().ToString(), String.Format("通知设备 {0} 需要上传操作异常日志", deviceNo), null);
                }
            }
            else if (regionName == "Sync_Client_ShouldUploadBusinessLog")
            {
                if (Container.CacheService.RemoteCache.GetItem("Sync_Client_ShouldUploadBusinessLog", deviceNo) != null)
                {
                    DateTime time = Container.CacheService.RemoteCache.GetItem("Sync_Client_ShouldUploadBusinessLog", deviceNo).ConvertTo<DateTime>(DateTime.Now);
                    Container.CacheService.RemoteCache.RemoveItem("Sync_Client_ShouldUploadBusinessLog", deviceNo);

                    XMS.Core.Logging.LogUtil.Warn("sync_uploadLogCommand.log",
                        String.Format("通知设备 {0} 需要上传业务异常日志", deviceNo),
                        "Sync", null);

                    throw new RequestException(512, time.ToMilliSecondsFrom1970L().ToString(), String.Format("通知设备 {0} 需要上传业务异常日志", deviceNo), null);
                }
            }
            else if (regionName == "Sync_Client_ShouldUploadDataBaseLog")
            {
                if (Container.CacheService.RemoteCache.GetItem("Sync_Client_ShouldUploadDataBaseLog", deviceNo) != null)
                {
                    DateTime time = Container.CacheService.RemoteCache.GetItem("Sync_Client_ShouldUploadDataBaseLog", deviceNo).ConvertTo<DateTime>(DateTime.Now);
                    Container.CacheService.RemoteCache.RemoveItem("Sync_Client_ShouldUploadDataBaseLog", deviceNo);

                    XMS.Core.Logging.LogUtil.Warn("sync_uploadLogCommand.log",
                        String.Format("通知设备 {0} 需要上传数据库", deviceNo),
                        "Sync", null);

                    throw new RequestException(513, time.ToMilliSecondsFrom1970L().ToString(), String.Format("通知设备 {0} 需要上传数据库", deviceNo), null);
                }
            }
            else if (regionName == "Sync_Client_ShouldUploadOtherLog")
            {
                if (Container.CacheService.RemoteCache.GetItem("Sync_Client_ShouldUploadOtherLog", deviceNo) != null)
                {
                    DateTime time = Container.CacheService.RemoteCache.GetItem("Sync_Client_ShouldUploadOtherLog", deviceNo).ConvertTo<DateTime>(DateTime.Now);
                    Container.CacheService.RemoteCache.RemoveItem("Sync_Client_ShouldUploadOtherLog", deviceNo);

                    XMS.Core.Logging.LogUtil.Warn("sync_uploadLogCommand.log",
                        String.Format("通知设备 {0} 需要上传其它信息", deviceNo),
                        "Sync", null);

                    throw new RequestException(514, time.ToMilliSecondsFrom1970L().ToString(), String.Format("通知设备 {0} 需要上传其它信息", deviceNo), null);
                }
            }
            else if (regionName == "Sync_Restaurant_SMS_NotSufficientFunds")
            {
                if (Container.CacheService.RemoteCache.GetItem("Sync_Restaurant_SMS_NotSufficientFunds", deviceNo) != null)
                {
                    Container.CacheService.RemoteCache.RemoveItem("Sync_Restaurant_SMS_NotSufficientFunds", deviceNo);

                    XMS.Core.Logging.LogUtil.Warn("sync_uploadLogCommand.log",
                        String.Format("通知设备 {0} 餐厅短信余额不足", deviceNo),
                        "Sync", null);

                    throw new RequestException(888, "短信余额不足，请去充值", String.Format("通知设备 {0} 餐厅短信余额不足", deviceNo), null);
                }
            }
        }

        #endregion
    }
}
