using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMS.Inner.Coffee.Model;
using XMS.Inner.Coffee.Business;
using XMS.Core;

namespace XMS.Inner.Coffee.Service
{
    public class RestCoffeeService : IRestCoffeeService
    {
        public RestReturnValue<CRestaurantDTO> GetManager(string name, string passWord, string deviceId)
        {
            return RestReturnValue<CRestaurantDTO>.Get200OK(CRestaurantManager.Instance.GetRestaurantByLogin(name, passWord, deviceId));
        }

        /// <summary>
        /// orderState 如何为空 全部
        /// </summary>
        /// <param name="resUUID">餐厅UUID</param>
        /// <param name="orderState">为空全部</param>
        /// <param name="startIndex">开始行数</param>
        /// <param name="count">分页</param>
        /// <returns></returns>
        public RestReturnValue<Core.Data.QueryResult<COrderDTO>> GetOrderByResUUID(string resUUID, string orderState ,int startIndex, int count)
        {
            if (string.IsNullOrWhiteSpace(resUUID))
                throw new BusinessException("餐厅UUID不能为空");
            List<int> listOrderState = new List<int>();
            if (!string.IsNullOrWhiteSpace(orderState))
            {
                List<string> listOrderState1 = orderState.Split(new char[] { ',' }).ToList();
                listOrderState.AddRange(listOrderState1.Select(x => int.Parse(x)).ToArray());
            }

            return RestReturnValue<Core.Data.QueryResult<COrderDTO>>.Get200OK(COrderManager.Instance.GetOrderDTOs(null, null, resUUID, null, null, null, null, null, listOrderState, new List<int> { 1, 2 },
                null, null, null, null, null, null, null, null, null, startIndex, count, true, true));
        }

        /// <summary>
        /// 获取门店营业时间段
        /// </summary>
        /// <param name="resUUID"></param>
        /// <returns></returns>
        public RestReturnValue<BusinessHourWeekTypeInfoDTO> GetBusinessHourWeekType(string resUUID)
        {
            return RestReturnValue<BusinessHourWeekTypeInfoDTO>.Get200OK(new BusinessHourWeekTypeInfoDTO { items = BusinessHourWeekManager.Instance.GetBusinessHourWeekType(resUUID) } );
        }

        public RestReturnValue<bool> SetBusinessHourWeekState(int id, byte state)
        {
            return RestReturnValue<bool>.Get200OK(BusinessHourWeekManager.Instance.SetBusinessHourWeekState(id, state, "门店"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resUUID"></param>
        /// <param name="orderId"></param>
        /// <param name="orderState"></param>
        /// <returns></returns>
        public RestReturnValue<bool> SetOrderState(string resUUID, int orderId, int orderState)
        {
            COrderPO orderInf = COrderManager.Instance.GetById(orderId);
            if (orderInf == null)
                throw new BusinessException("订单不存在");
            if (orderInf.ResUUID != resUUID)
                throw new BusinessException("不是此餐厅的订单");

            orderInf.OrderState = orderState;
            COrderManager.Instance.Update(orderInf, "门店");
            return RestReturnValue<bool>.Get200OK(true);
        }

        public RestReturnValue<bool> SetOrderStateByDistributionId(int distributionId, int orderId, int orderState)
        {
            COrderPO orderInf = COrderManager.Instance.GetById(orderId);
            if (orderInf == null)
                throw new BusinessException("订单不存在");
            CDistributionManagerPO distributionManager =  CDistributionManagerManager.Instance.GetById(distributionId);
            if (distributionManager == null)
                throw new BusinessException("骑手不存在");

            if (orderInf.DistributionId.HasValue && orderInf.DistributionId != distributionManager.Id)
                throw new BusinessException("此订单已被抢单");

            orderInf.OrderState = orderState;
            orderInf.DistributionId = distributionId;
            COrderManager.Instance.Update(orderInf, distributionManager.Name);
            return RestReturnValue<bool>.Get200OK(true);
        }

        /// <summary>
        /// 骑手登录接口
        /// </summary>
        /// <param name="passWord"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public RestReturnValue<CDistributionManagerPO> CheckDistributionExsit(string passWord, string mobile)
        {
            return RestReturnValue<CDistributionManagerPO>.Get200OK(CDistributionManagerManager.Instance.CheckDistributionExsit(passWord, mobile));
        }

        /// <summary>
        /// 获取未分配的订单
        /// </summary>
        /// <returns></returns>
        public RestReturnValue<Core.Data.QueryResult<COrderDTO>> GetUnassignedOrder(int distributionId, int startIndex, int count)
        {
            CDistributionManagerPO po = CDistributionManagerManager.Instance.GetById(distributionId);
            if (po == null)
                throw new BusinessException("此骑手不存在");
            return RestReturnValue<Core.Data.QueryResult<COrderDTO>>.Get200OK(COrderManager.Instance.GetOrderDTOs(null, null, po.ResUUID, null, null, null, null, null, new List<int> { 5 }, new List<int> { 1, 2 }, null, null, null, null, null, null, null, null, null, startIndex, count, true, true));
        }

        public RestReturnValue<Core.Data.QueryResult<COrderDTO>> GetAssignedOrderByDistributionId(int distributionId, int startIndex, int count)
        {
            return RestReturnValue<Core.Data.QueryResult<COrderDTO>>.Get200OK(COrderManager.Instance.GetOrderDTOs(null, null, null, null, null, null, null, null, new List<int> { 1, 2, 3, 5, 6, 7, 8 }, new List<int> { 1, 2 }, null, null, null, null, null, null, new List<int> { distributionId }, null, null, startIndex, count, true, true));
        }

        public RestReturnValue<CManagerPO> GetManagerPost(string name, string passWord, string deviceId)
        {
            Core.Data.QueryResult<CManagerPO> result = CManagerManager.Instance.GetManagers(null, name, passWord, null, 1, 1);
            if (result.Items == null || result.Items.Length == 0)
                return RestReturnValue<CManagerPO>.Get200OK(null);

            return RestReturnValue<CManagerPO>.Get200OK(result.Items[0]);
        }
    }
}
