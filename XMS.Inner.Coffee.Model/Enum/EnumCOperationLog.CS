using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMS.Inner.Coffee.Model
{
    /// <summary>
    /// 操作日志->对象类别
    /// </summary>
    public enum EnumOperationLogType
    {
        CRestaurant = 1,
        CCommodityCategory = 2,
        CCommodity = 3,
        CPropertyCategory = 4,
        CCommodityProperty = 5,
        CCommodityWithProperty = 6,
        CCommodityRelation = 7,
        CMemberWeiXin = 8,
        CMemberAddress = 9,
        CCoupon = 10,
        COrder = 11,
        COrderCommodityRelation = 12,
        CManager = 13,
        CVerificationCode = 14,
        CResCoordinate = 15,
        BusinessHourWeek = 16,
        CCashCouponDetail = 17,
        CCashCoupon = 18,
        CDistributionCoordinate= 19,
        CDistributionManager =20,
        CIntegralRecord = 21,
        CBanner = 22

    }

    /// <summary>
    /// 操作日志->操作类别
    /// </summary>
    public enum EnumOperationLogAction
    {
        Add = 1,
        Update = 2,
        Delete = 3,
    }
}
