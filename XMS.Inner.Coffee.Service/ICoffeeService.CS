using XMS.Core;
using XMS.Core.WCF;
using XMS.Core.Data;
using XMS.Inner.Coffee.Business;
using XMS.Inner.Coffee.Model;
using System.ServiceModel;
using System.ComponentModel;
using XMS.Core.PayCenter.Model;
using System.Collections.Generic;
using System;

namespace XMS.Inner.Coffee.Service
{
    [ServiceContract(Namespace = "http://www.xiaomishu.com/coffee")]
    public interface ICoffeeService
    {
        // Methods
        [OperationContract]
        ReturnValue<AddOrderResultDTO> AddOrder(List<AddOrderDTO> addOrders);
        [OperationContract]
        ReturnValue<int> AddOrUpdateCCouponDto(CCouponDTO dto, string operatorName);
        [OperationContract]
        ReturnValue<int> AddOrUpdateCommodity(AddOrUpdateCommodityDTO commodity, string operatorName);
        [OperationContract]
        ReturnValue<int> AddOrUpdateCommodityCategory(AddOrUpdateCommodityCategoryDTO commodityCategory, string operatorName);
        [OperationContract]
        ReturnValue<int> AddOrUpdateCommodityProperty(AddOrUpdateCommodityPropertyDTO commodityProperty, string operatorName);
        [OperationContract]
        ReturnValue<int> AddOrUpdateCoupon(AddOrUpdateCouponDTO coupon, string operatorName);
        [OperationContract]
        ReturnValue<int> AddOrUpdateMemberAddress(AddMemberAddressDTO dto, string operatorName);
        [OperationContract]
        ReturnValue<int> AddOrUpdateMemberWeiXin(CMemberWeiXinDTO dto, string operatorName);
        [OperationContract]
        ReturnValue<int> AddOrUpdateRestaurantDTO(AddOrUpdateRestaurantDTO restaurant, string operatorName);
        [OperationContract]
        ReturnValue<int> AddOrUpdatetPropertyCategory(AddOrUpdatetPropertyCategoryDTO propertyCategory, string operatorName);
        [OperationContract]
        ReturnValue<bool> CheckCode(int memberId, string code);
        [OperationContract]
        ReturnValue<int> CreateVerificationCode(int memberId, string phoneNumber);
        [OperationContract]
        ReturnValue<bool> DeleteCommodity(int id, string operatorName);
        [OperationContract]
        ReturnValue<bool> DeleteCommodityCategory(int id, string operatorName);
        [OperationContract]
        ReturnValue<bool> DeleteCommodityProperty(int id, string operatorName);
        [OperationContract]
        ReturnValue<bool> DeleteCoupon(int id, string operatorName);
        [OperationContract]
        ReturnValue<bool> DeletePropertyCategory(int id, string operatorName);
        [OperationContract]
        ReturnValue<bool> DeleteRestaurant(int resId, string operatorName);
        [OperationContract]
        ReturnValue<List<COrderPO>> GetAllOrders(int? id, string orderUUID, string resUUID, string memberUUID, string memberName, string telephone, DateTime? orderTime, List<int> orderState, List<int> payStatus, List<int> payType, List<int> deliveryType, string cityId, List<string> memberAddressUUID, List<string> couponUUID, List<int> preferentialType);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="getAvailableCoupon"></param>
        /// <param name="memberId"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnValue<List<CCouponDTO>> GetAvailableCouponDTO(GetAvailableCouponDTO getAvailableCoupon, int memberId);
        [OperationContract]
        ReturnValue<Core.Data.QueryResult<CCommodityDTO>> GetCCommodityDTOs(int? id, string resUUID, string commodityUUID, List<string> commodityCategoryUUIDs, List<int> states, int startIndex, int count, string name);
        [OperationContract]
        ReturnValue<CCommodityDTO> GetCommodityById(int id);
        [OperationContract]
        ReturnValue<List<CommodityCategoryInfoDTO>> GetCommodityCategory(int resId);
        [OperationContract]
        ReturnValue<CCommodityCategoryDTO> GetCommodityCategoryById(int id);
        [OperationContract]
        ReturnValue<List<CommodityCategoryInfoDTO>> GetCommodityCategoryByResUUID(string resUUID);
        [OperationContract]
        ReturnValue<List<CCommodityCategoryDTO>> GetCommodityCategoryDtos(string resUUID);
        [OperationContract]
        ReturnValue<Core.Data.QueryResult<CCommodityCategoryDTO>> GetCommodityCategoryQuery(string resUUID, int startIndex, int count);
        [OperationContract]
        ReturnValue<CCommodityPropertyDTO> GetCommodityPropertyById(int id);
        [OperationContract]
        ReturnValue<Core.Data.QueryResult<CCommodityPropertyDTO>> GetCommodityPropertyDTOs(List<int> id, string resUUID, string commodityPropertyUUID, List<string> commodityUUIDs, List<string> propertyCategoryUUIDs, int startIndex, int count);
        [OperationContract]
        ReturnValue<CCouponDTO> GetCouponById(int id);
        [OperationContract]
        ReturnValue<Core.Data.QueryResult<CCouponDTO>> GetCoupons(int? id, string couponUUID, List<int> couponType, List<int> preferentialType, List<int> state, string resUUID, DateTime? ordrTime, int startIndex, int count);
        [OperationContract]
        ReturnValue<List<CommodityInfoDTO>> GetGoodsByClassify(int commodityCategoryId);
        [OperationContract]
        ReturnValue<List<GoodsClassifyDTO>> GetGoodsClassifyDTO(int resId);
        [OperationContract]
        ReturnValue<CMemberAddressDTO> GetLastMemberAddress(int weixinMemberId);
        [OperationContract]
        ReturnValue<Core.Data.QueryResult<CManagerPO>> GetManagers(int? id, string name, string passWord, int? userLevel, int startIndex, int count);
        [OperationContract]
        ReturnValue<Core.Data.QueryResult<CMemberAddressDTO>> GetMemberAddress(string memberAddressUUID, string memberUUID, string name, string telephone, string cityId, string address, string houseNumber, int startIndex, int count);

        /// <summary>
        /// ����������ȡȫ����ַ
        /// </summary>
        /// <param name="memberAddressUUID"></param>
        /// <param name="memberUUID"></param>
        /// <param name="name"></param>
        /// <param name="telephone"></param>
        /// <param name="cityId"></param>
        /// <param name="address"></param>
        /// <param name="houseNumber"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnValue<List<CMemberAddressDTO>> GetAllMemberAddress(int? id, string memberAddressUUID, string memberUUID, string name, string telephone, string cityId, string address, string houseNumber, int? shopId);

        [OperationContract]
        ReturnValue<Core.Data.QueryResult<CMemberWeiXinDTO>> GetMemberWeiXins(int? id, string memberUUID, string weiXinOpenId, string phoneNumber, string name, int startIndex, int count);
        [OperationContract]
        ReturnValue<List<CCouponDTO>> GetNoUseCouponDTO(string resUUID, string memberUUID);
        [OperationContract]
        ReturnValue<List<CCouponDTO>> GetNoUseCouponDTOById(int resId, int memberId);

        /// <summary>
        /// �������Ϣ������������Ϣ��
        /// </summary>
        /// <param name="id"></param>
        /// <param name="orderUUID"></param>
        /// <param name="resUUID"></param>
        /// <param name="memberUUID"></param>
        /// <param name="memberName"></param>
        /// <param name="telephone"></param>
        /// <param name="minCreateTime"></param>
        /// <param name="MaxCreateTime"></param>
        /// <param name="orderState"></param>
        /// <param name="payStatus"></param>
        /// <param name="payType"></param>
        /// <param name="deliveryType"></param>
        /// <param name="cityId"></param>
        /// <param name="memberAddressUUID"></param>
        /// <param name="couponUUID"></param>
        /// <param name="preferentialType"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <param name="orderCommodityRelationFlag"></param>
        /// <param name="memberAddressFlag"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnValue<Core.Data.QueryResult<COrderDTO>> GetOrderDTOs(int? id, string orderUUID, string resUUID, string memberUUID, string memberName, string telephone, DateTime? minCreateTime, DateTime? MaxCreateTime,
            List<int> orderState, List<int> payStatus, List<int> payType, List<int> deliveryType, string cityId,
            List<string> memberAddressUUID, List<string> couponUUID, List<int> preferentialType, int startIndex, int count, bool orderCommodityRelationFlag = false, bool memberAddressFlag = false);
        [OperationContract]
        ReturnValue<COrderDTO> GetOrderInfo(int id, int memberId);
        [OperationContract]
        ReturnValue<Core.Data.QueryResult<COrderPO>> GetOrders(int? id, string orderUUID, string resUUID, string memberUUID, string memberName, string telephone, DateTime? orderTime, List<int> orderState, List<int> payStatus, List<int> payType, List<int> deliveryType, string cityId, List<string> memberAddressUUID, List<string> couponUUID, List<int> preferentialType, int startIndex, int count);
        [OperationContract]
        ReturnValue<AddOrderResultDTO> GetPayInfo(int orderId, int memberId);
        [OperationContract]
        ReturnValue<CPropertyCategoryDTO> GetPropertyCategoryById(int id);
        [OperationContract]
        ReturnValue<List<CPropertyCategoryDTO>> GetPropertyCategoryDTO(int resId);
        [OperationContract]
        ReturnValue<Core.Data.QueryResult<CPropertyCategoryDTO>> GetPropertyCategoryQuery(string resUUID, List<string> propertyCategoryUUIDs, int startIndex, int count);
        [OperationContract]
        ReturnValue<CRestaurantDTO> GetRestaurantById(int resId);
        [OperationContract]
        ReturnValue<Core.Data.QueryResult<CRestaurantDTO>> GetRestaurantDTOByCondition(List<string> resUUID, string name, string address, string cityId, decimal? longitude, decimal? latitude, string alphabet, int startIndex, int count, bool containOutRange, List<int> state);
        [OperationContract]
        ReturnValue<Model.PayNotifyResult> ProcessPayNotify(Model.ProcessPayNotifyRequestDto processPayNotifyRequestDto);
        [OperationContract]
        ReturnValue<bool> SetCommodityState(int id, int state, string operatorName);
        [OperationContract]
        ReturnValue<bool> SetRestaurantState(int resId, int state, string operatorName);
        [OperationContract]
        ReturnValue<bool> UpdateOrderInfo(int id, int orderState, string managerRemark, string operatorName);
        [OperationContract]
        ReturnValue<PhotoResult> UploadPic(byte[] picData, string filePath, bool autoControl);

        /// <summary>
        /// ���Ӳ�����������
        /// </summary>
        /// <param name="addResCoordinate"></param>
        /// <param name="operatorName"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnValue<bool> AddResCoordinate(AddResCoordinateDTO addResCoordinate, string operatorName);

        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <param name="id"></param>
        /// <param name="resUUID"></param>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <param name="markUUID"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnValue<List<CResCoordinatePO>> GetCResCoordinateList(int? id, string resUUID, List<decimal> longitude, List<decimal> latitude, List<string> markUUID);
        #region Ԥ��ʱ��ι���
        /// <summary>
        /// ����UUID
        /// </summary>
        /// <param name="resUUID"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnValue<List<BusinessHourWeekDTO>> GetBusinessHourWeeksByResUUID(string resUUID);

        /// <summary>
        /// ����UUID
        /// </summary>
        /// <param name="resUUID"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnValue<KeyValue<string, string>[]> GetAllBusinessHourTypesByResUUID(string resUUID);

        /// <summary>
        /// ��ȡʱ��ΰ���
        /// </summary>
        /// <param name="id"></param>
        /// <param name="uUID"></param>
        /// <param name="resUUID"></param>
        /// <param name="typeUUID"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnValue<List<BusinessHourWeekPO>> GetBusinessHourWeekList(int? id, List<string> uUID, List<string> resUUID, List<string> typeUUID);

        /// <summary>
        /// ���Ӳ���Ӫҵʱ���(����)
        /// </summary>
        /// <param name="businessHourWeekBase"></param>
        /// <param name="operatorName"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnValue<bool> AddBusinessHourWeekBase(BusinessHourWeekBaseDTO businessHourWeekBase, string operatorName);

        /// <summary>
        /// �޸Ĳ���Ӫҵʱ���(����)
        /// </summary>
        /// <param name="updateBusinessHourWeekBase"></param>
        /// <param name="operatorName"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnValue<bool> UpdateBusinessHourWeekBase(UpdateBusinessHourWeekBaseDTO updateBusinessHourWeekBase, string operatorName);

        /// <summary>
        /// ���ӵ���Ӫҵʱ���
        /// </summary>
        /// <param name="addBusinessHourWeek"></param>
        /// <param name="operatorName"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnValue<int> AddBusinessHourWeek(AddBusinessHourWeekDTO addBusinessHourWeek, string operatorName);

        /// <summary>
        /// ���õ���Ӫҵʱ��ε�״̬
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="state"></param>
        /// <param name="operatorName"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnValue<bool> SetBusinessHourWeekState(int Id, byte state, string operatorName);

        /// <summary>
        /// �޸ĵ���Ӫҵʱ���
        /// </summary>
        /// <param name="businessHourWeek"></param>
        /// <param name="operatorName"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnValue<int> UpdateBusinessHourWeek(UpdateBusinessHourWeekDTO businessHourWeek, string operatorName);

        /// <summary>
        /// ����ɾ��ʱ���
        /// </summary>
        /// <param name="typeUUID"></param>
        /// <param name="operatorName"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnValue<bool> DeleteBusinessHourWeek(string typeUUID, string operatorName);

        /// <summary>
        /// ɾ��ʱ���
        /// </summary>
        /// <param name="id"></param>
        /// <param name="operatorName"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnValue<bool> DeleteBusinessHourWeekById(int id, string operatorName);

        /// <summary>
        /// ��ȡ��ЧӪҵʱ���
        /// </summary>
        /// <param name="resId"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnValue<List<EffectivePeriodDTO>> GetEffectivePeriod(int resId);
        #endregion


        #region �������
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="memberWeiXinId"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnValue<bool> DeleteOrder(int orderId, int memberWeiXinId);

        /// <summary>
        /// ɾ����Ա��ַ
        /// </summary>
        /// <param name="memberAddressId"></param>
        /// <param name="memberWeiXinId"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnValue<bool> DeleteMemberAddress(int memberAddressId, int memberWeiXinId);
        #endregion

        /// <summary>
        /// ��ȡǩ��
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnValue<SignatureObjectDTO> GetSignatureObjectDTO(string url);


        #region Banner���
        /// <summary>
        /// ��ȡBanner�б�
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnValue<List<CBannerDTO>> GetCBannerList(int? id, string name);

        /// <summary>
        /// ����Banner
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnValue<int> AddCBanner(AddCBannerDTO cBanner, string operatorName);

        /// <summary>
        /// ����Banner
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnValue<int> UpdateCBanner(UpdateCBannerDTO cBanner, string operatorName);
        #endregion
    }


}