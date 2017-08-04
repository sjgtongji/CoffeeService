using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XMS.Core;

namespace XMS.Inner.Coffee.Business
{
    // 定义系统中抛出的所有业务异常相关的自定义错误码。
    // 使用示例：	throw ErrorCodeHelper.MemberNotExists.ToException(new {A=1, B="a"});
    internal class ErrorCodeHelper
    {
        #region 自动成功（101000-101000）
        public static readonly ErrorCode OperatorNameNull = new ErrorCode("OperatorNameNull", 100001, "操作人为空，请确认！");
        public static readonly ErrorCode IdGreaterErr = new ErrorCode("IdGreaterErr", 100002, "Id不能大于0，请确认！");
        public static readonly ErrorCode IdLessErr = new ErrorCode("IdLessErr", 100003, "Id不能小于0，请确认！");
        public static readonly ErrorCode BusinessHourWeekNull = new ErrorCode("BusinessHourWeekNull", 100004, "BusinessHourWeek对象为空，请确认！");
        public static readonly ErrorCode CBannerNull = new ErrorCode("CBannerNull", 100005, "CBanner对象为空，请确认！");
        public static readonly ErrorCode CCashCouponNull = new ErrorCode("CCashCouponNull", 100006, "CCashCoupon对象为空，请确认！");
        public static readonly ErrorCode CCashCouponDetailNull = new ErrorCode("CCashCouponDetailNull", 100007, "CCashCouponDetail对象为空，请确认！");
        public static readonly ErrorCode CCommodityNull = new ErrorCode("CCommodityNull", 100008, "CCommodity对象为空，请确认！");
        public static readonly ErrorCode CCommodityCategoryNull = new ErrorCode("CCommodityCategoryNull", 100009, "CCommodityCategory对象为空，请确认！");
        public static readonly ErrorCode CCommodityPropertyNull = new ErrorCode("CCommodityPropertyNull", 100010, "CCommodityProperty对象为空，请确认！");
        public static readonly ErrorCode CCommodityRelationNull = new ErrorCode("CCommodityRelationNull", 100011, "CCommodityRelation对象为空，请确认！");
        public static readonly ErrorCode CCommodityWithPropertyNull = new ErrorCode("CCommodityWithPropertyNull", 100012, "CCommodityWithProperty对象为空，请确认！");
        public static readonly ErrorCode CCouponNull = new ErrorCode("CCouponNull", 100013, "CCoupon对象为空，请确认！");
        public static readonly ErrorCode CDistributionCoordinateNull = new ErrorCode("CDistributionCoordinateNull", 100014, "CDistributionCoordinate对象为空，请确认！");
        public static readonly ErrorCode CDistributionManagerNull = new ErrorCode("CDistributionManagerNull", 100015, "CDistributionManager对象为空，请确认！");
        public static readonly ErrorCode CIntegralRecordNull = new ErrorCode("CIntegralRecordNull", 100016, "CIntegralRecord对象为空，请确认！");
        public static readonly ErrorCode CManagerNull = new ErrorCode("CManagerNull", 100017, "CManager对象为空，请确认！");
        public static readonly ErrorCode CMemberAddressNull = new ErrorCode("CMemberAddressNull", 100018, "CMemberAddress对象为空，请确认！");
        public static readonly ErrorCode CMemberWeiXinNull = new ErrorCode("CMemberWeiXinNull", 100019, "CMemberWeiXin对象为空，请确认！");
        public static readonly ErrorCode COperationLogNull = new ErrorCode("COperationLogNull", 100020, "COperationLog对象为空，请确认！");
        public static readonly ErrorCode COrderNull = new ErrorCode("COrderNull", 100021, "COrder对象为空，请确认！");
        public static readonly ErrorCode COrderCommodityRelationNull = new ErrorCode("COrderCommodityRelationNull", 100022, "COrderCommodityRelation对象为空，请确认！");
        public static readonly ErrorCode CPropertyCategoryNull = new ErrorCode("CPropertyCategoryNull", 100023, "CPropertyCategory对象为空，请确认！");
        public static readonly ErrorCode CResCoordinateNull = new ErrorCode("CResCoordinateNull", 100024, "CResCoordinate对象为空，请确认！");
        public static readonly ErrorCode CRestaurantNull = new ErrorCode("CRestaurantNull", 100025, "CRestaurant对象为空，请确认！");
        public static readonly ErrorCode CVerificationCodeNull = new ErrorCode("CVerificationCodeNull", 100026, "CVerificationCode对象为空，请确认！");
        public static readonly ErrorCode BusinessHourWeekNotExist = new ErrorCode("BusinessHourWeekNotExist", 100027, "BusinessHourWeek对象不存在，请确认！");
        public static readonly ErrorCode CBannerNotExist = new ErrorCode("CBannerNotExist", 100028, "CBanner对象不存在，请确认！");
        public static readonly ErrorCode CCashCouponNotExist = new ErrorCode("CCashCouponNotExist", 100029, "CCashCoupon对象不存在，请确认！");
        public static readonly ErrorCode CCashCouponDetailNotExist = new ErrorCode("CCashCouponDetailNotExist", 100030, "CCashCouponDetail对象不存在，请确认！");
        public static readonly ErrorCode CCommodityNotExist = new ErrorCode("CCommodityNotExist", 100031, "CCommodity对象不存在，请确认！");
        public static readonly ErrorCode CCommodityCategoryNotExist = new ErrorCode("CCommodityCategoryNotExist", 100032, "CCommodityCategory对象不存在，请确认！");
        public static readonly ErrorCode CCommodityPropertyNotExist = new ErrorCode("CCommodityPropertyNotExist", 100033, "CCommodityProperty对象不存在，请确认！");
        public static readonly ErrorCode CCommodityRelationNotExist = new ErrorCode("CCommodityRelationNotExist", 100034, "CCommodityRelation对象不存在，请确认！");
        public static readonly ErrorCode CCommodityWithPropertyNotExist = new ErrorCode("CCommodityWithPropertyNotExist", 100035, "CCommodityWithProperty对象不存在，请确认！");
        public static readonly ErrorCode CCouponNotExist = new ErrorCode("CCouponNotExist", 100036, "CCoupon对象不存在，请确认！");
        public static readonly ErrorCode CDistributionCoordinateNotExist = new ErrorCode("CDistributionCoordinateNotExist", 100037, "CDistributionCoordinate对象不存在，请确认！");
        public static readonly ErrorCode CDistributionManagerNotExist = new ErrorCode("CDistributionManagerNotExist", 100038, "CDistributionManager对象不存在，请确认！");
        public static readonly ErrorCode CIntegralRecordNotExist = new ErrorCode("CIntegralRecordNotExist", 100039, "CIntegralRecord对象不存在，请确认！");
        public static readonly ErrorCode CManagerNotExist = new ErrorCode("CManagerNotExist", 100040, "CManager对象不存在，请确认！");
        public static readonly ErrorCode CMemberAddressNotExist = new ErrorCode("CMemberAddressNotExist", 100041, "CMemberAddress对象不存在，请确认！");
        public static readonly ErrorCode CMemberWeiXinNotExist = new ErrorCode("CMemberWeiXinNotExist", 100042, "CMemberWeiXin对象不存在，请确认！");
        public static readonly ErrorCode COperationLogNotExist = new ErrorCode("COperationLogNotExist", 100043, "COperationLog对象不存在，请确认！");
        public static readonly ErrorCode COrderNotExist = new ErrorCode("COrderNotExist", 100044, "COrder对象不存在，请确认！");
        public static readonly ErrorCode COrderCommodityRelationNotExist = new ErrorCode("COrderCommodityRelationNotExist", 100045, "COrderCommodityRelation对象不存在，请确认！");
        public static readonly ErrorCode CPropertyCategoryNotExist = new ErrorCode("CPropertyCategoryNotExist", 100046, "CPropertyCategory对象不存在，请确认！");
        public static readonly ErrorCode CResCoordinateNotExist = new ErrorCode("CResCoordinateNotExist", 100047, "CResCoordinate对象不存在，请确认！");
        public static readonly ErrorCode CRestaurantNotExist = new ErrorCode("CRestaurantNotExist", 100048, "CRestaurant对象不存在，请确认！");
        public static readonly ErrorCode CVerificationCodeNotExist = new ErrorCode("CVerificationCodeNotExist", 100049, "CVerificationCode对象不存在，请确认！");
        public static readonly ErrorCode CCashCouponAssetPropertyNotSet = new ErrorCode("CCashCouponAssetPropertyNotSet", 100077, "CCashCoupon对象的AssetProperty属性未设定，请确认！");
        public static readonly ErrorCode CCashCouponAssetNameNotSet = new ErrorCode("CCashCouponAssetNameNotSet", 100078, "CCashCoupon对象的AssetName属性未设定，请确认！");
        public static readonly ErrorCode CCashCouponValidStartTimeNotSet = new ErrorCode("CCashCouponValidStartTimeNotSet", 100079, "CCashCoupon对象的ValidStartTime属性未设定，请确认！");
        public static readonly ErrorCode CCashCouponValidEndTimeNotSet = new ErrorCode("CCashCouponValidEndTimeNotSet", 100080, "CCashCoupon对象的ValidEndTime属性未设定，请确认！");
        public static readonly ErrorCode CCashCouponAssetStatusNotSet = new ErrorCode("CCashCouponAssetStatusNotSet", 100081, "CCashCoupon对象的AssetStatus属性未设定，请确认！");
        public static readonly ErrorCode CCashCouponSaleAmountNotSet = new ErrorCode("CCashCouponSaleAmountNotSet", 100082, "CCashCoupon对象的SaleAmount属性未设定，请确认！");
        public static readonly ErrorCode CCashCouponSaleStartTimeNotSet = new ErrorCode("CCashCouponSaleStartTimeNotSet", 100083, "CCashCoupon对象的SaleStartTime属性未设定，请确认！");
        public static readonly ErrorCode CCashCouponSaleEndTimeNotSet = new ErrorCode("CCashCouponSaleEndTimeNotSet", 100084, "CCashCoupon对象的SaleEndTime属性未设定，请确认！");
        public static readonly ErrorCode CCashCouponValAmountNotSet = new ErrorCode("CCashCouponValAmountNotSet", 100086, "CCashCoupon对象的ValAmount属性未设定，请确认！");
        public static readonly ErrorCode CCashCouponDetailAssetPropertyNotSet = new ErrorCode("CCashCouponDetailAssetPropertyNotSet", 100094, "CCashCouponDetail对象的AssetProperty属性未设定，请确认！");
        public static readonly ErrorCode CCashCouponDetailAssetNameNotSet = new ErrorCode("CCashCouponDetailAssetNameNotSet", 100095, "CCashCouponDetail对象的AssetName属性未设定，请确认！");
        public static readonly ErrorCode CCashCouponDetailValidStartTimeNotSet = new ErrorCode("CCashCouponDetailValidStartTimeNotSet", 100096, "CCashCouponDetail对象的ValidStartTime属性未设定，请确认！");
        public static readonly ErrorCode CCashCouponDetailValidEndTimeNotSet = new ErrorCode("CCashCouponDetailValidEndTimeNotSet", 100097, "CCashCouponDetail对象的ValidEndTime属性未设定，请确认！");
        public static readonly ErrorCode CCashCouponDetailSaleAmountNotSet = new ErrorCode("CCashCouponDetailSaleAmountNotSet", 100098, "CCashCouponDetail对象的SaleAmount属性未设定，请确认！");
        public static readonly ErrorCode CCashCouponDetailSaleStartTimeNotSet = new ErrorCode("CCashCouponDetailSaleStartTimeNotSet", 100099, "CCashCouponDetail对象的SaleStartTime属性未设定，请确认！");
        public static readonly ErrorCode CCashCouponDetailSaleEndTimeNotSet = new ErrorCode("CCashCouponDetailSaleEndTimeNotSet", 100100, "CCashCouponDetail对象的SaleEndTime属性未设定，请确认！");
        public static readonly ErrorCode CCashCouponDetailValAmountNotSet = new ErrorCode("CCashCouponDetailValAmountNotSet", 100102, "CCashCouponDetail对象的ValAmount属性未设定，请确认！");
        public static readonly ErrorCode CDistributionCoordinateDistributionIdNotSet = new ErrorCode("CDistributionCoordinateDistributionIdNotSet", 100206, "CDistributionCoordinate对象的DistributionId属性未设定，请确认！");
        public static readonly ErrorCode CDistributionCoordinateLongitudeNotSet = new ErrorCode("CDistributionCoordinateLongitudeNotSet", 100207, "CDistributionCoordinate对象的Longitude属性未设定，请确认！");
        public static readonly ErrorCode CDistributionCoordinateLatitudeNotSet = new ErrorCode("CDistributionCoordinateLatitudeNotSet", 100208, "CDistributionCoordinate对象的Latitude属性未设定，请确认！");
        public static readonly ErrorCode CDistributionCoordinateStateNotSet = new ErrorCode("CDistributionCoordinateStateNotSet", 100209, "CDistributionCoordinate对象的State属性未设定，请确认！");
        public static readonly ErrorCode CDistributionManagerNameNotSet = new ErrorCode("CDistributionManagerNameNotSet", 100217, "CDistributionManager对象的Name属性未设定，请确认！");
        public static readonly ErrorCode CDistributionManagerPassWordNotSet = new ErrorCode("CDistributionManagerPassWordNotSet", 100218, "CDistributionManager对象的PassWord属性未设定，请确认！");
        public static readonly ErrorCode CDistributionManagerUserLevelNotSet = new ErrorCode("CDistributionManagerUserLevelNotSet", 100219, "CDistributionManager对象的UserLevel属性未设定，请确认！");
        public static readonly ErrorCode CDistributionManagerMobileNotSet = new ErrorCode("CDistributionManagerMobileNotSet", 100220, "CDistributionManager对象的Mobile属性未设定，请确认！");
        public static readonly ErrorCode CIntegralRecordOrderIdNotSet = new ErrorCode("CIntegralRecordOrderIdNotSet", 100229, "CIntegralRecord对象的OrderId属性未设定，请确认！");
        public static readonly ErrorCode CIntegralRecordTypeNotSet = new ErrorCode("CIntegralRecordTypeNotSet", 100231, "CIntegralRecord对象的Type属性未设定，请确认！");
        #endregion

        #region
        public static readonly ErrorCode AddResCoordinateNull = new ErrorCode("AddResCoordinateNull", 110036, "AddResCoordinateNull添加对象为空，请确认！");
        public static readonly ErrorCode CoordinateInfoErr = new ErrorCode("CoordinateInfoErr", 110037, "必须设置三个以上坐标点，请确认！");
        #endregion
    }
}
