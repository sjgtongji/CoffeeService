using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using XMS.Core;

namespace XMS.Inner.Coffee.Business
{
    // 定义当前工程配置在 conf/App.config 中的所有简单配置项
    // 系统中用到的所有配置在 conf/App.config 的内容必须都通过本类以配置的方式进行读取
    // 除了 XMS.Core 中内置的 AppName、AppVersion 和 Windows服务宿主安装所需要的 ServiceName、DisplayName 等配置项外，
    // 不允许在 .net 自带的 Web.config 或 app.config 的 AppSettings 配置节中定义配置项。
    internal class AppSettingHelper
    {
        /// <summary>
        /// ProductId
        /// </summary>
        public static string ProductId
        {
            get
            {
                return Container.ConfigService.GetAppSetting<string>("ProductId", "wx");
            }
        }

        public static string WaterPicFilePath
        {
            get { return Container.ConfigService.GetAppSetting("WaterPicFilePath", ""); }
        }

        private static readonly Random r = new Random();
        /// <summary>
        /// 文件服务器散列化地址
        /// </summary>
        public static string StaticUploadUrl
        {
            get
            {
                string sUrl = XMS.Core.Container.ConfigService.GetAppSetting("StaticUploadUrl", "");
                if (string.IsNullOrEmpty(sUrl))
                {
                    return string.Empty;
                }
                sUrl = string.Format(sUrl, r.Next(1, 5));
                return sUrl.TrimEnd('/') + "/";
            }
        }

        /// <summary>
        /// SaleChannelApplicationId
        /// </summary>
        public static string SaleChannelApplicationId
        {
            get
            {
                return Container.ConfigService.GetAppSetting<string>("SaleChannelApplicationId", "wx");
            }
        }

        /// <summary>
        /// 微信异步回调
        /// </summary>
        public static string RemotePayWeiXinCallbackUrl
        {
            get
            {
                return Container.ConfigService.GetAppSetting<string>("RemotePayWeiXinCallbackUrl", "http://waimai.buzztimecoffee.com/AppWapCoffee/PayWeiXinNotify/{0}/{1}/");
            }
        }

        /// <summary>
        /// 允许上传文件的最大值
        /// </summary>
        public static int FileMaxSize
        {
            get
            {
                return Container.ConfigService.GetAppSetting<int>("FileMaxSize", 10485760);
            }
        }

        /// <summary>
        /// 允许上传文件的最大值
        /// </summary>
        public static int EffectivePeriodDay
        {
            get
            {
                return Container.ConfigService.GetAppSetting<int>("EffectivePeriodDay", 7);
            }
        }


        /// <summary>
        /// AppId
        /// </summary>
        public static string AppId
        {
            get
            {
                return Container.ConfigService.GetAppSetting<string>("AppId", "wxa691be29e7554dcc");
            }

        }

        /// <summary>
        /// 秘钥
        /// </summary>
        public static string Secret
        {
            get
            {
                return Container.ConfigService.GetAppSetting<string>("Secret", "19f042bcce790a9fd71e9174651e5a06");
            }
        }
    }
}
