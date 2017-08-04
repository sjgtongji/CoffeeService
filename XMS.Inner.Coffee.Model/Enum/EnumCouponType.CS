using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMS.Inner.Coffee.Model
{
    public enum EnumCouponType
    {
        /// <summary>
        /// 全部优惠卷
        /// </summary>
        All = 0,
        /// <summary>
        /// 未使用的优惠卷
        /// </summary>
        NoUse = 1,
        /// <summary>
        /// 已使用的优惠卷
        /// </summary>
        AlreadyUsed = 2
    }
}
