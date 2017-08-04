using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMS.Inner.Coffee.Business
{
    /// <summary>
    /// 引用其他服务
    /// </summary>
    public class WCFClient
    {
        /// <summary>W
        /// 内网服务
        /// </summary>
        public static XMS.Core.PayCenter.ICoffeePayCenterService CoffeePayCenterService
        {
            get
            {
                return XMS.Core.Container.Instance.Resolve<XMS.Core.PayCenter.ICoffeePayCenterService>();
            }
        }
    }
}
