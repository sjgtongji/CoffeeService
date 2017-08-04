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
using System.ServiceModel.Web;

namespace XMS.Inner.Coffee.Service
{
    [ServiceContract(Namespace = "http://www.xiaomishu.com/Rest")]
    public interface IRestCoffeeService
    {
        [Description("门店登录,/getManager?name={name}&passWord={passWord}&deviceId={deviceId}")]
        [WebGet(UriTemplate = "/getManager?name={name}&passWord={passWord}&deviceId={deviceId}", RequestFormat = WebMessageFormat.Json)]
        [OperationContract]
        [SecurityBehavior(NeedVerifyHeader = false, NeedTokenHeader = false)]
        RestReturnValue<CRestaurantDTO> GetManager(string name, string passWord, string deviceId);

        [Description("获取门店订单,/getOrderByResUUID?resUUID={resUUID}&orderState={orderState}&startIndex={startIndex}&count={count}")]
        [WebGet(UriTemplate = "/getOrderByResUUID?resUUID={resUUID}&orderState={orderState}&startIndex={startIndex}&count={count}", RequestFormat = WebMessageFormat.Json)]
        [OperationContract]
        [SecurityBehavior(NeedVerifyHeader = false, NeedTokenHeader = false)]
        RestReturnValue<Core.Data.QueryResult<COrderDTO>> GetOrderByResUUID(string resUUID, string orderState, int startIndex, int count);


        [Description("获取门店订单,/setOrderState?resUUID={resUUID}&orderId={orderId}&orderState={orderState}")]
        [WebGet(UriTemplate = "/setOrderState?resUUID={resUUID}&orderId={orderId}&orderState={orderState}", RequestFormat = WebMessageFormat.Json)]
        [OperationContract]
        [SecurityBehavior(NeedVerifyHeader = false, NeedTokenHeader = false)]
        RestReturnValue<bool> SetOrderState(string resUUID, int orderId,int orderState);

        [Description("获取门店时间段,/getBusinessHourWeekType?resUUID={resUUID}")]
        [WebGet(UriTemplate = "/GetBusinessHourWeekType?resUUID={resUUID}", RequestFormat = WebMessageFormat.Json)]
        [OperationContract]
        [SecurityBehavior(NeedVerifyHeader = false, NeedTokenHeader = false)]
        RestReturnValue<BusinessHourWeekTypeInfoDTO> GetBusinessHourWeekType(string resUUID);

        [Description("设置餐厅时间段状态,/setBusinessHourWeekState?id={id}&state={state}")]
        [WebGet(UriTemplate = "/setBusinessHourWeekState?id={id}&state={state}", RequestFormat = WebMessageFormat.Json)]
        [OperationContract]
        [SecurityBehavior(NeedVerifyHeader = false, NeedTokenHeader = false)]
        RestReturnValue<bool> SetBusinessHourWeekState(int id, byte state);

        [Description("骑手登录接口,/checkDistributionExsit?passWord={passWord}&mobile={mobile}")]
        [WebGet(UriTemplate = "/checkDistributionExsit?passWord={passWord}&mobile={mobile}", RequestFormat = WebMessageFormat.Json)]
        [OperationContract]
        [SecurityBehavior(NeedVerifyHeader = false, NeedTokenHeader = false)]
        RestReturnValue<CDistributionManagerPO> CheckDistributionExsit(string passWord, string mobile);


        [Description("获取未被抢单的订单,/getUnassignedOrder?distributionId={distributionId}&startIndex={startIndex}&count={count}")]
        [WebGet(UriTemplate = "/getUnassignedOrder?distributionId={distributionId}&startIndex={startIndex}&count={count}", RequestFormat = WebMessageFormat.Json)]
        [OperationContract]
        [SecurityBehavior(NeedVerifyHeader = false, NeedTokenHeader = false)]
        RestReturnValue<Core.Data.QueryResult<COrderDTO>> GetUnassignedOrder(int distributionId, int startIndex, int count);

        [Description("骑手获取自己的订单,/getAssignedOrderByDistributionId?distributionId={distributionId}&startIndex={startIndex}&count={count}")]
        [WebGet(UriTemplate = "/getAssignedOrderByDistributionId?distributionId={distributionId}&startIndex={startIndex}&count={count}", RequestFormat = WebMessageFormat.Json)]
        [OperationContract]
        [SecurityBehavior(NeedVerifyHeader = false, NeedTokenHeader = false)]
        RestReturnValue<Core.Data.QueryResult<COrderDTO>> GetAssignedOrderByDistributionId(int distributionId, int startIndex, int count);


        [Description("骑手修改订单状态,/setOrderStateByDistributionId?distributionId={distributionId}&orderId={orderId}&orderState={orderState}")]
        [WebGet(UriTemplate = "/setOrderStateByDistributionId?distributionId={distributionId}&orderId={orderId}&orderState={orderState}", RequestFormat = WebMessageFormat.Json)]
        [OperationContract]
        [SecurityBehavior(NeedVerifyHeader = false, NeedTokenHeader = false)]
        RestReturnValue<bool> SetOrderStateByDistributionId(int distributionId, int orderId, int orderState);

        [Description("获得一个webApp,/getManagerPost?name={name}&passWord={passWord}&deviceId={deviceId}")]
        [WebGet(UriTemplate = "/getManagerPost?name={name}&passWord={passWord}&deviceId={deviceId}", RequestFormat = WebMessageFormat.Json)]
        [OperationContract]
        [SecurityBehavior(NeedVerifyHeader = false, NeedTokenHeader = false)]
        RestReturnValue<CManagerPO> GetManagerPost(string name, string passWord, string deviceId);
    }
}
