using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XMS.Core;
using XMS.Core.WCF;
using XMS.Core.Data;
using XMS.Inner.Coffee.Business;
using XMS.Inner.Coffee.Model;
using System.ServiceModel;
using System.ComponentModel;
using XMS.Core.PayCenter.Model;

namespace XMS.Inner.Coffee.Service
{
    public partial class CoffeeService : WCFServiceBase, ICoffeeService
    {
        #region 餐厅相关
        public ReturnValue<bool> SetRestaurantState(int resId, int state, string operatorName)
        {
            return ReturnValue<bool>.Get200OK(CRestaurantManager.Instance.SetRestaurantState(resId, state, operatorName));
        }

        public ReturnValue<bool> DeleteRestaurant(int resId, string operatorName)
        {
            return ReturnValue<bool>.Get200OK(CRestaurantManager.Instance.DeleteRestaurant(resId, operatorName));
        }

        public ReturnValue<int> AddOrUpdateRestaurantDTO(AddOrUpdateRestaurantDTO restaurant, string operatorName)
        {
            return ReturnValue<int>.Get200OK(CRestaurantManager.Instance.AddOrUpdateRestaurantDTO(restaurant, operatorName));
        }

        public ReturnValue<CRestaurantDTO> GetRestaurantById(int resId)
        {
            return ReturnValue<CRestaurantDTO>.Get200OK(CRestaurantManager.Instance.GetRestaurantById(resId));
        }
        /// <summary>
        /// 获取餐厅列表集合
        /// </summary>
        /// <returns></returns>
        public ReturnValue<Core.Data.QueryResult<CRestaurantDTO>> GetRestaurantDTOByCondition(List<string> resUUID, string name, string address, string cityId, decimal? longitude, decimal? latitude, string alphabet, int startIndex, int count, bool containOutRange, List<int> state)
        {
            return ReturnValue<Core.Data.QueryResult<CRestaurantDTO>>.Get200OK(CRestaurantManager.Instance.GetRestaurantDTOByCondition(resUUID, name, address, cityId, longitude, latitude, alphabet, startIndex, count, containOutRange, state, null, null));
        }
        #endregion

        #region 商品类别
        /// <summary>
        /// 获取商品类别
        /// </summary>
        /// <param name="resUUID"></param>
        /// <returns></returns>
        public ReturnValue<List<CCommodityCategoryDTO>> GetCommodityCategoryDtos(string resUUID)
        {
            return ReturnValue<List<CCommodityCategoryDTO>>.Get200OK(CCommodityCategoryManager.Instance.GetCommodityCategoryDtos(resUUID));
        }
        /// <summary>
        /// 新增或者修改商品类别
        /// </summary>
        /// <param name="commodityCategory"></param>
        /// <param name="operatorName"></param>
        /// <returns></returns>
        public ReturnValue<int> AddOrUpdateCommodityCategory(AddOrUpdateCommodityCategoryDTO commodityCategory, string operatorName)
        {
            return ReturnValue<int>.Get200OK(CCommodityCategoryManager.Instance.AddOrUpdateCommodityCategory(commodityCategory, operatorName));
        }

        /// <summary>
        /// 删除商品类别
        /// </summary>
        /// <param name="id"></param>
        /// <param name="operatorName"></param>
        /// <returns></returns>
        public ReturnValue<bool> DeleteCommodityCategory(int id, string operatorName)
        {
            return ReturnValue<bool>.Get200OK(CCommodityCategoryManager.Instance.DeleteCommodityCategory(id, operatorName));
        }

        public ReturnValue<Core.Data.QueryResult<CCommodityCategoryDTO>> GetCommodityCategoryQuery(string resUUID, int startIndex, int count)
        {
            return ReturnValue<Core.Data.QueryResult<CCommodityCategoryDTO>>.Get200OK(CCommodityCategoryManager.Instance.GetCommodityCategoryQuery(resUUID, startIndex, count));
        }

        public ReturnValue<CCommodityCategoryDTO> GetCommodityCategoryById(int id)
        {
            return ReturnValue<CCommodityCategoryDTO>.Get200OK(CCommodityCategoryManager.Instance.GetCommodityCategoryById(id));
        }
        #endregion

        #region 商品
        /// <summary>
        /// 新增或者修改商品
        /// </summary>
        /// <param name="commodity"></param>
        /// <param name="operatorName"></param>
        /// <returns></returns>
        public ReturnValue<int> AddOrUpdateCommodity(AddOrUpdateCommodityDTO commodity, string operatorName)
        {
            return ReturnValue<int>.Get200OK(CCommodityManager.Instance.AddOrUpdateCommodity(commodity, operatorName));
        }

        public ReturnValue<bool> DeleteCommodity(int id, string operatorName)
        {
            return ReturnValue<bool>.Get200OK(CCommodityManager.Instance.DeleteCommodity(id, operatorName));
        }

        public ReturnValue<bool> SetCommodityState(int id, int state, string operatorName)
        {
            return ReturnValue<bool>.Get200OK(CCommodityManager.Instance.SetCommodityState(id, state, operatorName));
        }

        public ReturnValue<Core.Data.QueryResult<CCommodityDTO>> GetCCommodityDTOs(int? id, string resUUID, string commodityUUID, List<string> commodityCategoryUUIDs, List<int> states, int startIndex, int count, string name)
        {
            return ReturnValue<Core.Data.QueryResult<CCommodityDTO>>.Get200OK(CCommodityManager.Instance.GetCCommodityDTOs(id, resUUID, commodityUUID, commodityCategoryUUIDs, states, startIndex, count, name));
        }

        public ReturnValue<CCommodityDTO> GetCommodityById(int id)
        {
            return ReturnValue<CCommodityDTO>.Get200OK(CCommodityManager.Instance.GetCommodityById(id));
        }
        #endregion

        #region 商品属性类别
        public ReturnValue<List<CPropertyCategoryDTO>> GetPropertyCategoryDTO(int resId)
        {
            return ReturnValue<List<CPropertyCategoryDTO>>.Get200OK(CPropertyCategoryManager.Instance.GetPropertyCategoryDTO(resId));
        }

        public ReturnValue<int> AddOrUpdatetPropertyCategory(AddOrUpdatetPropertyCategoryDTO propertyCategory, string operatorName)
        {
            return ReturnValue<int>.Get200OK(CPropertyCategoryManager.Instance.AddOrUpdatetPropertyCategory(propertyCategory, operatorName));
        }

        public ReturnValue<bool> DeletePropertyCategory(int id, string operatorName)
        {
            return ReturnValue<bool>.Get200OK(CPropertyCategoryManager.Instance.DeletePropertyCategory(id, operatorName));
        }

        public ReturnValue<Core.Data.QueryResult<CPropertyCategoryDTO>> GetPropertyCategoryQuery(string resUUID, List<string> propertyCategoryUUIDs, int startIndex, int count)
        {
            return ReturnValue<Core.Data.QueryResult<CPropertyCategoryDTO>>.Get200OK(CPropertyCategoryManager.Instance.GetPropertyCategoryQuery(resUUID, propertyCategoryUUIDs, startIndex, count));
        }

        public ReturnValue<CPropertyCategoryDTO> GetPropertyCategoryById(int id)
        {
            return ReturnValue<CPropertyCategoryDTO>.Get200OK(CPropertyCategoryManager.Instance.GetPropertyCategoryById(id));
        }
        #endregion

        #region 商品属性
        public ReturnValue<bool> DeleteCommodityProperty(int id, string operatorName)
        {
            return ReturnValue<bool>.Get200OK(CCommodityPropertyManager.Instance.DeleteCommodityProperty(id, operatorName));
        }

        public ReturnValue<int> AddOrUpdateCommodityProperty(AddOrUpdateCommodityPropertyDTO commodityProperty, string operatorName)
        {
            return ReturnValue<int>.Get200OK(CCommodityPropertyManager.Instance.AddOrUpdateCommodityProperty(commodityProperty, operatorName));
        }

        public ReturnValue<Core.Data.QueryResult<CCommodityPropertyDTO>> GetCommodityPropertyDTOs(List<int> id, string resUUID, string commodityPropertyUUID, List<string> commodityUUIDs, List<string> propertyCategoryUUIDs, int startIndex, int count)
        {
            return ReturnValue<Core.Data.QueryResult<CCommodityPropertyDTO>>.Get200OK(CCommodityPropertyManager.Instance.GetCommodityPropertyDTOs(id, resUUID, commodityPropertyUUID, commodityUUIDs, propertyCategoryUUIDs, startIndex, count));
        }

        public ReturnValue<CCommodityPropertyDTO> GetCommodityPropertyById(int id)
        {
            return ReturnValue<CCommodityPropertyDTO>.Get200OK(CCommodityPropertyManager.Instance.GetCommodityPropertyById(id));
        }
        #endregion

        #region 优惠卷
        public ReturnValue<bool> DeleteCoupon(int id, string operatorName)
        {
            return ReturnValue<bool>.Get200OK(CCouponManager.Instance.DeleteCoupon(id, operatorName));
        }

        public ReturnValue<int> AddOrUpdateCoupon(AddOrUpdateCouponDTO coupon, string operatorName)
        {
            return ReturnValue<int>.Get200OK(CCouponManager.Instance.AddOrUpdateCoupon(coupon, operatorName));
        }

        public ReturnValue<CCouponDTO> GetCouponById(int id)
        {
            return ReturnValue<CCouponDTO>.Get200OK(CCouponManager.Instance.GetCouponById(id));
        }

        /// <summary>
        /// 获取优惠卷
        /// </summary>
        /// <returns></returns>
        public ReturnValue<Core.Data.QueryResult<CCouponDTO>> GetCoupons(int? id, string couponUUID, List<int> couponType, List<int> preferentialType, List<int> state, string resUUID, DateTime? ordrTime, int startIndex, int count)
        {
            return ReturnValue<Core.Data.QueryResult<CCouponDTO>>.Get200OK(CCouponManager.Instance.GetCouponDTOs(id, couponUUID, couponType, preferentialType, state, resUUID, ordrTime, startIndex, count));
        }
        #endregion

        #region 上传图片
        public ReturnValue<PhotoResult> UploadPic(byte[] picData, string filePath, bool autoControl)
        {
            return ReturnValue<PhotoResult>.Get200OK(FileManager.Instance.UploadPic(picData, filePath, autoControl));
        }
        #endregion

        #region 订单相关
        public ReturnValue<bool> UpdateOrderInfo(int id, int orderState, string managerRemark, string operatorName)
        {
            return ReturnValue<bool>.Get200OK(COrderManager.Instance.UpdateOrderInfo(id, orderState, managerRemark, operatorName));
        }
        #endregion

        public ReturnValue<Core.Data.QueryResult<COrderDTO>> GetOrderDTOs(int? id, string orderUUID, string resUUID, string memberUUID, string memberName, string telephone, DateTime? minCreateTime, DateTime? MaxCreateTime,
            List<int> orderState, List<int> payStatus, List<int> payType, List<int> deliveryType, string cityId,
            List<string> memberAddressUUID, List<string> couponUUID, List<int> preferentialType, int startIndex, int count, bool orderCommodityRelationFlag = false, bool memberAddressFlag = false)
        {
            return ReturnValue<Core.Data.QueryResult<COrderDTO>>.Get200OK(COrderManager.Instance.GetOrderDTOs(id, orderUUID, resUUID, memberUUID, memberName, telephone, minCreateTime, MaxCreateTime, orderState,
                payStatus, payType, deliveryType, cityId, memberAddressUUID, couponUUID, preferentialType, null, null, null, startIndex, count, orderCommodityRelationFlag, memberAddressFlag));
        }

        /// <summary>
        /// 添加订单
        /// </summary>
        /// <returns></returns>
        public ReturnValue<AddOrderResultDTO> AddOrder(List<AddOrderDTO> addOrders)
        {
            return ReturnValue<AddOrderResultDTO>.Get200OK(COrderManager.Instance.AddOrder(addOrders));
        }

        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <param name="resId"></param>
        /// <returns></returns>
        public ReturnValue<List<CommodityCategoryInfoDTO>> GetCommodityCategory(int resId)
        {
            return ReturnValue<List<CommodityCategoryInfoDTO>>.Get200OK(CCommodityCategoryManager.Instance.GetCommodityCategory(resId));
        }

        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <param name="resUUID"></param>
        /// <returns></returns>
        public ReturnValue<List<CommodityCategoryInfoDTO>> GetCommodityCategoryByResUUID(string resUUID)
        {
            return ReturnValue<List<CommodityCategoryInfoDTO>>.Get200OK(CCommodityCategoryManager.Instance.GetCommodityCategoryByResUUID(resUUID));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="operatorName"></param>
        /// <returns></returns>
        public ReturnValue<int> AddOrUpdateMemberWeiXin(CMemberWeiXinDTO dto, string operatorName)
        {
            return ReturnValue<int>.Get200OK(CMemberWeiXinManager.Instance.AddOrUpdateDTO(dto, operatorName));
        }

        /// <summary>
        /// 添加用户地址
        /// </summary>
        /// <returns></returns>
        public ReturnValue<int> AddOrUpdateMemberAddress(AddMemberAddressDTO dto, string operatorName)
        {
            return ReturnValue<int>.Get200OK(CMemberAddressManager.Instance.AddOrUpdateMemberAddress(dto, operatorName));
        }

        /// <summary>
        /// 获取微信用户集合
        /// </summary>
        /// <returns></returns>
        public ReturnValue<Core.Data.QueryResult<CMemberWeiXinDTO>> GetMemberWeiXins(int? id, string memberUUID, string weiXinOpenId, string phoneNumber, string name, int startIndex, int count)
        {
            return ReturnValue<Core.Data.QueryResult<CMemberWeiXinDTO>>.Get200OK(CMemberWeiXinManager.Instance.GetMemberWeiXinDTOs(id, memberUUID, weiXinOpenId, phoneNumber, name, startIndex, count));
        }

        /// <summary>
        /// 通过条件获取地址集合
        /// </summary>
        /// <returns></returns>
        public ReturnValue<Core.Data.QueryResult<CMemberAddressDTO>> GetMemberAddress(string memberAddressUUID, string memberUUID, string name, string telephone, string cityId, string address, string houseNumber, int startIndex, int count)
        {
            return ReturnValue<Core.Data.QueryResult<CMemberAddressDTO>>.Get200OK(CMemberAddressManager.Instance.GetMemberAddressDTO(null, memberAddressUUID, memberUUID, name, telephone, cityId, address, houseNumber, startIndex, count));
        }

        /// <summary>
        /// 获取全部地址（根据）
        /// </summary>
        /// <param name="memberAddressUUID"></param>
        /// <param name="memberUUID"></param>
        /// <param name="name"></param>
        /// <param name="telephone"></param>
        /// <param name="cityId"></param>
        /// <param name="address"></param>
        /// <param name="houseNumber"></param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        public ReturnValue<List<CMemberAddressDTO>> GetAllMemberAddress(int? id, string memberAddressUUID, string memberUUID, string name, string telephone, string cityId, string address, string houseNumber, int? shopId)
        {
            return ReturnValue<List<CMemberAddressDTO>>.Get200OK(CMemberAddressManager.Instance.GetAllMemberAddress(id, memberAddressUUID, memberUUID, name, telephone, cityId, address, houseNumber, shopId));
        }

        /// <summary>
        /// 获取未使用的优惠卷
        /// </summary>
        /// <returns></returns>
        public ReturnValue<List<CCouponDTO>> GetNoUseCouponDTO(string resUUID, string memberUUID)
        {
            return ReturnValue<List<CCouponDTO>>.Get200OK(CCouponManager.Instance.GetNoUseCouponDTO(resUUID, memberUUID));
        }


        /// <summary>
        /// 获取未使用的优惠卷
        /// </summary>
        /// <returns></returns>
        public ReturnValue<List<CCouponDTO>> GetNoUseCouponDTOById(int resId, int memberId)
        {
            return ReturnValue<List<CCouponDTO>>.Get200OK(CCouponManager.Instance.GetNoUseCouponDTOById(resId, memberId));
        }

        /// <summary>
        /// 获取订单集合
        /// </summary>
        /// <returns></returns>
        public ReturnValue<Core.Data.QueryResult<COrderPO>> GetOrders(int? id, string orderUUID, string resUUID, string memberUUID, string memberName, string telephone, DateTime? orderTime,
           List<int> orderState, List<int> payStatus, List<int> payType, List<int> deliveryType, string cityId,
           List<string> memberAddressUUID, List<string> couponUUID, List<int> preferentialType, int startIndex, int count)
        {
            return ReturnValue<Core.Data.QueryResult<COrderPO>>.Get200OK(COrderManager.Instance.GetOrders(id, orderUUID, resUUID, memberUUID, memberName, telephone, orderTime,
                orderState, payStatus, payType, deliveryType, cityId, memberAddressUUID, couponUUID, preferentialType, startIndex, count));
        }

        public ReturnValue<List<COrderPO>> GetAllOrders(int? id, string orderUUID, string resUUID, string memberUUID, string memberName, string telephone, DateTime? orderTime,
           List<int> orderState, List<int> payStatus, List<int> payType, List<int> deliveryType, string cityId,
           List<string> memberAddressUUID, List<string> couponUUID, List<int> preferentialType)
        {
            return ReturnValue<List<COrderPO>>.Get200OK(COrderManager.Instance.GetAllOrders(id, orderUUID, resUUID, memberUUID, memberName, telephone, orderTime,
                orderState, payStatus, payType, deliveryType, cityId, memberAddressUUID, couponUUID, preferentialType));
        }

        public ReturnValue<int> AddOrUpdateCCouponDto(CCouponDTO dto, string operatorName)
        {
            return ReturnValue<int>.Get200OK(CCouponManager.Instance.AddOrUpdateCCouponDto(dto, operatorName));
        }

        public ReturnValue<CMemberAddressDTO> GetLastMemberAddress(int weixinMemberId)
        {
            return ReturnValue<CMemberAddressDTO>.Get200OK(CMemberAddressManager.Instance.GetLastMemberAddress(weixinMemberId));
        }

        public ReturnValue<Model.PayNotifyResult> ProcessPayNotify(Model.ProcessPayNotifyRequestDto processPayNotifyRequestDto)
        {
            return ReturnValue<Model.PayNotifyResult>.Get200OK(COrderManager.Instance.ProcessPayNotify(processPayNotifyRequestDto));
        }

        public ReturnValue<List<CommodityInfoDTO>> GetGoodsByClassify(int commodityCategoryId)
        {
            return ReturnValue<List<CommodityInfoDTO>>.Get200OK(CCommodityCategoryManager.Instance.GetGoodsByClassify(commodityCategoryId));
        }

        public ReturnValue<List<GoodsClassifyDTO>> GetGoodsClassifyDTO(int resId)
        {
            return ReturnValue<List<GoodsClassifyDTO>>.Get200OK(CCommodityCategoryManager.Instance.GetGoodsClassifyDTO(resId));
        }

        public ReturnValue<List<CCouponDTO>> GetAvailableCouponDTO(GetAvailableCouponDTO getAvailableCoupon, int memberId)
        {
            return ReturnValue<List<CCouponDTO>>.Get200OK(CCouponManager.Instance.GetAvailableCouponDTO(getAvailableCoupon, memberId));
        }

        public ReturnValue<bool> CheckCode(int memberId, string code)
        {
            return ReturnValue<bool>.Get200OK(CVerificationCodeManager.Instance.CheckCode(memberId, code));
        }

        public ReturnValue<int> CreateVerificationCode(int memberId, string phoneNumber)
        {
            return ReturnValue<int>.Get200OK(CVerificationCodeManager.Instance.CreateVerificationCode(memberId, phoneNumber));
        }

        public ReturnValue<COrderDTO> GetOrderInfo(int id, int memberId)
        {
            return ReturnValue<COrderDTO>.Get200OK(COrderManager.Instance.GetOrderInfo(id, memberId));
        }

        public ReturnValue<AddOrderResultDTO> GetPayInfo(int orderId, int memberId)
        {
            return ReturnValue<AddOrderResultDTO>.Get200OK(COrderManager.Instance.GetPayInfo(orderId, memberId));
        }

        public ReturnValue<Core.Data.QueryResult<CManagerPO>> GetManagers(int? id, string name, string passWord, int? userLevel, int startIndex, int count)
        {
            return ReturnValue<Core.Data.QueryResult<CManagerPO>>.Get200OK(CManagerManager.Instance.GetManagers(id, name, passWord, userLevel, startIndex, count));
        }

        /// <summary>
        /// 添加餐厅坐标序列
        /// </summary>
        /// <param name="addResCoordinate"></param>
        /// <param name="operatorName"></param>
        /// <returns></returns>
        public ReturnValue<bool> AddResCoordinate(AddResCoordinateDTO addResCoordinate, string operatorName)
        {
            return ReturnValue<bool>.Get200OK(CResCoordinateManager.Instance.AddResCoordinate(addResCoordinate, operatorName));
        }


        public ReturnValue<List<CResCoordinatePO>> GetCResCoordinateList(int? id, string resUUID, List<decimal> longitude, List<decimal> latitude, List<string> markUUID)
        {
            return ReturnValue<List<CResCoordinatePO>>.Get200OK(CResCoordinateManager.Instance.GetCResCoordinateList(id, resUUID, longitude, latitude, markUUID));
        }
        #region 预订时间段规则
        /// <summary>
        /// 餐厅UUID
        /// </summary>
        /// <param name="resUUID"></param>
        /// <returns></returns>
        public ReturnValue<List<BusinessHourWeekDTO>> GetBusinessHourWeeksByResUUID(string resUUID) {
            return ReturnValue<List<BusinessHourWeekDTO>>.Get200OK(BusinessHourWeekManager.Instance.GetBusinessHourWeeksByResUUID(resUUID));
        }

        /// <summary>
        /// 餐厅UUID
        /// </summary>
        /// <param name="resUUID"></param>
        /// <returns></returns>
        public ReturnValue<KeyValue<string, string>[]> GetAllBusinessHourTypesByResUUID(string resUUID)
        {
            return ReturnValue<KeyValue<string, string>[]>.Get200OK(BusinessHourWeekManager.Instance.GetAllBusinessHourTypesByResUUID(resUUID));
        }

        public ReturnValue<List<BusinessHourWeekPO>> GetBusinessHourWeekList(int? id, List<string> uUID, List<string> resUUID, List<string> typeUUID)
        {
            return ReturnValue<List<BusinessHourWeekPO>>.Get200OK(BusinessHourWeekManager.Instance.GetBusinessHourWeekList(id, uUID, resUUID, typeUUID, null));
        }

        public ReturnValue<bool> AddBusinessHourWeekBase(BusinessHourWeekBaseDTO businessHourWeekBase, string operatorName)
        {
            return ReturnValue<bool>.Get200OK(BusinessHourWeekManager.Instance.AddBusinessHourWeekBase(businessHourWeekBase, operatorName));
        }

        public ReturnValue<bool> UpdateBusinessHourWeekBase(UpdateBusinessHourWeekBaseDTO updateBusinessHourWeekBase, string operatorName)
        {
            return ReturnValue<bool>.Get200OK(BusinessHourWeekManager.Instance.UpdateBusinessHourWeekBase(updateBusinessHourWeekBase, operatorName));
        }

        public ReturnValue<int> AddBusinessHourWeek(AddBusinessHourWeekDTO addBusinessHourWeek, string operatorName)
        {
            return ReturnValue<int>.Get200OK(BusinessHourWeekManager.Instance.AddBusinessHourWeek(addBusinessHourWeek, operatorName));
        }

        public ReturnValue<bool> SetBusinessHourWeekState(int Id, byte state, string operatorName)
        {
            return ReturnValue<bool>.Get200OK(BusinessHourWeekManager.Instance.SetBusinessHourWeekState(Id, state, operatorName));
        }

        public ReturnValue<int> UpdateBusinessHourWeek(UpdateBusinessHourWeekDTO businessHourWeek, string operatorName)
        {
            return ReturnValue<int>.Get200OK(BusinessHourWeekManager.Instance.UpdateBusinessHourWeek(businessHourWeek, operatorName));
        }

        public ReturnValue<bool> DeleteBusinessHourWeek(string typeUUID, string operatorName)
        {
            return ReturnValue<bool>.Get200OK(BusinessHourWeekManager.Instance.DeleteBusinessHourWeek(typeUUID, operatorName));
        }

        public ReturnValue<bool> DeleteBusinessHourWeekById(int id, string operatorName)
        {
            return ReturnValue<bool>.Get200OK(BusinessHourWeekManager.Instance.DeleteBusinessHourWeekById(id, operatorName));
        }

        public ReturnValue<List<EffectivePeriodDTO>> GetEffectivePeriod(int resId)
        {
            return ReturnValue<List<EffectivePeriodDTO>>.Get200OK(BusinessHourWeekManager.Instance.GetEffectivePeriod(resId));
        }
        #endregion

        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="memberWeiXinId"></param>
        /// <returns></returns>
        public ReturnValue<bool> DeleteOrder(int orderId, int memberWeiXinId)
        {
            return ReturnValue<bool>.Get200OK(COrderManager.Instance.DeleteOrder(orderId, memberWeiXinId));
        }

        /// <summary>
        /// 删除会员地址
        /// </summary>
        /// <param name="memberAddressId"></param>
        /// <param name="memberWeiXinId"></param>
        /// <returns></returns>
        public ReturnValue<bool> DeleteMemberAddress(int memberAddressId, int memberWeiXinId)
        {
            return ReturnValue<bool>.Get200OK(CMemberAddressManager.Instance.DeleteMemberAddress(memberAddressId, memberWeiXinId));
        }

        public ReturnValue<SignatureObjectDTO> GetSignatureObjectDTO(string url)
        {
            return ReturnValue<SignatureObjectDTO>.Get200OK(new WeiXinHelper(url).signatureObjectDTO);
        }

        #region Banner相关
        /// <summary>
        /// 获取Banner列表
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public ReturnValue<List<CBannerDTO>> GetCBannerList(int? id, string name)
        {
            return ReturnValue<List<CBannerDTO>>.Get200OK(CBannerManager.Instance.GetCBannerList(id, name));
        }
        /// <summary>
        /// 添加Banner
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public ReturnValue<int> AddCBanner(AddCBannerDTO cBanner, string operatorName)
        {
            return ReturnValue<int>.Get200OK(CBannerManager.Instance.AddCBanner(cBanner, operatorName));
        }
        /// <summary>
        /// 添加Banner
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public ReturnValue<int> UpdateCBanner(UpdateCBannerDTO cBanner, string operatorName)
        {
            return ReturnValue<int>.Get200OK(CBannerManager.Instance.UpdateCBanner(cBanner, operatorName));
        }
        #endregion
    }
}