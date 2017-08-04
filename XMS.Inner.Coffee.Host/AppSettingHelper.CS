using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using XMS.Core;

namespace XMS.Inner.Coffee.Host
{
    // 定义当前工程配置在 conf/App.config 中的所有简单配置项
    // 系统中用到的所有配置在 conf/App.config 的内容必须都通过本类以配置的方式进行读取
    // 除了 XMS.Core 中内置的 AppName、AppVersion 和 Windows服务宿主安装所需要的 ServiceName、DisplayName 等配置项外，
    // 不允许在 .net 自带的 Web.config 或 app.config 的 AppSettings 配置节中定义配置项。
    internal class AppSettingHelper
    {
        //public static string[] Domains
        //{
        //    get
        //    {
        //        return Container.ConfigService.GetAppSetting<string>("Domains", Empty<string>.Array);
        //    }
        //}

        //public static int[] Values
        //{
        //    get
        //    {
        //        return Container.ConfigService.GetAppSetting<int>("Domains", Empty<int>.Array);
        //    }
        //}

        public static int AllowMaxUnConfirmDay
        {
            get
            {
                return Container.ConfigService.GetAppSetting<int>("AllowMaxUnConfirmDay", 10);
            }
        }
    }
}
