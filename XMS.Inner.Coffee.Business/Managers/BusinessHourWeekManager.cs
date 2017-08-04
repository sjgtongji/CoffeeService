
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMS.Inner.Coffee.Data;
using XMS.Inner.Coffee.Model;
using XMS.Core.Data;
using XMS.Core;

namespace XMS.Inner.Coffee.Business
{
    public class BusinessHourWeekManager: DataMapperManagerBase<BusinessHourWeekPO, BusinessHourWeekDTO>, IManagerBase<BusinessHourWeekPO>
    {
        public static readonly BusinessHourWeekManager Instance = new BusinessHourWeekManager();
        private BusinessHourWeekManager() { }


        public double GetPeriodLong(DateTime startTime1, DateTime endTime1, DateTime startTime2, DateTime endTime2)
        {
            double period = 0;
            if (endTime1 < startTime2)
                return period;
            if (startTime1 > endTime2)
                return period;

            DateTime start = startTime1 > startTime2 ? startTime1 : startTime2;
            DateTime end = endTime1 < endTime2 ? endTime1 : endTime2;
            TimeSpan periodInfo = (end - start);
            period = periodInfo.TotalMilliseconds;
            if (end == start)
                period = 0.1;
            return period;
        }

        /// <summary>
        /// 获取有效的时间段
        /// </summary>
        /// <param name="resId"></param>
        public List<EffectivePeriodDTO> GetEffectivePeriod(int resId)
        {
            CRestaurantPO restaurant = CRestaurantManager.Instance.GetById(resId);
            if (restaurant == null)
                throw ErrorCodeHelper.CRestaurantNotExist.ToException();
            List<EffectivePeriodDTO> listEffectivePeriodDTO = new List<EffectivePeriodDTO>();
            List<BusinessHourWeekPO> listBusinessHourWeek = GetBusinessHourWeekList(null, null, new List<string> { restaurant.ResUUID }, null, new List<int> { 0 });
            if (listBusinessHourWeek == null)
                return listEffectivePeriodDTO;

            List<COrderPO> listOrder = new List<COrderPO>();
            Core.Data.QueryResult<COrderPO> result = COrderManager.Instance.GetOrders(null, null, restaurant.ResUUID, null, null, null, null, null, new List<int> { 1, 2 }, null, null, null, null, null, null, 1, 1000);
            if (result.Items != null)
                listOrder.AddRange(result.Items);
            DateTime now = DateTime.Now;
            listOrder = listOrder.Where(x => x.DeliveryMaxTime.HasValue && x.DeliveryMaxTime.Value > now && x.DeliveryMinTime.HasValue).ToList();

            List<PeriodOrder> listPeriodOrder = new List<PeriodOrder>();
            for (int i = 0; i < AppSettingHelper.EffectivePeriodDay; i++)
            {
                int weekFlag = now.Date.AddDays(i).DayOfWeek == DayOfWeek.Sunday ? 7 : (int)now.Date.AddDays(i).DayOfWeek;
                List<BusinessHourWeekPO> listBusinessHourWeekOrderByWeekFlag = listBusinessHourWeek.Where(x => x.WeekDay == weekFlag).ToList();
                if (listBusinessHourWeekOrderByWeekFlag == null || listBusinessHourWeekOrderByWeekFlag.Count == 0)
                    continue;
                listBusinessHourWeekOrderByWeekFlag = listBusinessHourWeekOrderByWeekFlag.OrderBy(x => x.StartTime).ToList();
                listBusinessHourWeekOrderByWeekFlag.ForEach(item =>
                {
                    PeriodOrder periodOrder = new PeriodOrder()
                    {
                        BusinessHourWeekId = item.Id,
                        StartTime = now.Date.AddDays(i).AddMilliseconds(item.StartTime),
                        EndTime = now.Date.AddDays(i).AddMilliseconds(item.EndTime),
                    };

                    listPeriodOrder.Add(periodOrder);
                });
            }

            //判断订单属于哪个时间段
            listOrder.ForEach(x =>
            {
                PeriodOrder maxItem = null;
                double maxperod = 0;
                listPeriodOrder.ForEach(item =>
                {
                    double perid = GetPeriodLong(x.DeliveryMinTime.Value, x.DeliveryMaxTime.Value, item.StartTime, item.EndTime);
                    if (perid > maxperod)
                    {
                        maxperod = perid;
                        maxItem = item;
                    }
                });

                if (maxItem != null)
                    maxItem.ListOrder.Add(x);
            });

            string[] Day = new string[] { "周日", "周一", "周二", "周三", "周四", "周五", "周六" };
            for (int i = 0; i < AppSettingHelper.EffectivePeriodDay; i++)
            {
                string showName = "{0}";
                switch (i)
                {
                    case 0:
                        showName = "今天({0})";
                        break;
                    case 1:
                        showName = "明天({0})";
                        break;
                    case 2:
                        showName = "后天({0})";
                        break;
                    default:
                        showName = "{0}";
                        break;

                }
                EffectivePeriodDTO effectivePeriod = new EffectivePeriodDTO()
                {
                    Date = now.Date.AddDays(i),
                    ShowName = string.Format(showName, Day[(int)now.Date.AddDays(i).DayOfWeek]),
                    WeekFlag = now.Date.AddDays(i).DayOfWeek == DayOfWeek.Sunday ? 7 : (int)now.Date.AddDays(i).DayOfWeek
                };

                List<BusinessHourWeekPO> listBusinessHourWeekOrderByWeekFlag = listBusinessHourWeek.Where(x => x.WeekDay == effectivePeriod.WeekFlag).ToList();
                if (listBusinessHourWeekOrderByWeekFlag == null || listBusinessHourWeekOrderByWeekFlag.Count == 0)
                    continue;
                listBusinessHourWeekOrderByWeekFlag = listBusinessHourWeekOrderByWeekFlag.OrderBy(x => x.StartTime).ToList();
                listBusinessHourWeekOrderByWeekFlag.ForEach(item => {
                    if (!item.AllowOrderNumber.HasValue || item.AllowOrderNumber.Value <= 0)
                        return;

                    if (item.LatestOrderTime.HasValue && effectivePeriod.Date.AddMilliseconds(item.LatestOrderTime.Value) < now)
                        return;

                    if (item.InAdvance.HasValue && effectivePeriod.Date.AddMilliseconds(item.StartTime).AddHours(-(double)item.InAdvance.Value) < now)
                        return;

                    //if (effectivePeriod.Date.AddMilliseconds(item.StartTime) < now)
                    //    return;

                    //List<COrderPO> listOrderInfo = listOrder.Where(order => (order.DeliveryMaxTime.Value < effectivePeriod.Date.AddMilliseconds(item.StartTime) || 
                    //order.DeliveryMinTime.Value > effectivePeriod.Date.AddMilliseconds(item.EndTime)) == false).OrderBy(order=>order.DeliveryMinTime.Value).ToList();
                    //listOrderInfo = listOrderInfo.Take((int)item.AllowOrderNumber.Value).ToList();

                    //listOrderInfo.ForEach(item1 =>
                    //{
                    //    listOrder.Remove(item1);
                    //});

                    //if (listOrderInfo.Count == (int)item.AllowOrderNumber.Value)
                    //    return;

                    PeriodOrder periodOrder = listPeriodOrder.FirstOrDefault(x => x.BusinessHourWeekId == item.Id && x.StartTime.Date == effectivePeriod.Date);
                    if(periodOrder == null)
                        return;

                    if (periodOrder.ListOrder.Count > 0)
                    {
                        string dd = string.Empty;
                    }

                    EffectivePeriodInfo effectivePeriodInfo = new EffectivePeriodInfo()
                    {
                        EndTime = item.EndTime,
                        StartTime = item.StartTime,
                        Available = true
                    };
                    if (periodOrder.ListOrder.Count >= (int)item.AllowOrderNumber.Value)
                        effectivePeriodInfo.Available = false;
                    effectivePeriod.EffectivePeriod.Add(effectivePeriodInfo);
                });

                if (effectivePeriod.EffectivePeriod.Count > 0)
                    listEffectivePeriodDTO.Add(effectivePeriod);
            }
            return listEffectivePeriodDTO;
        }

        public bool DeleteBusinessHourWeekById(int id, string operatorName)
        {
            BusinessHourWeekPO businessHourWeekPO = GetById(id);
            if (businessHourWeekPO == null)
                throw ErrorCodeHelper.BusinessHourWeekNotExist.ToException();
            businessHourWeekPO.Deleted = true;
            Update(businessHourWeekPO, operatorName);
            return true;
        }

        /// <summary>
        /// 批量删除营业时间段
        /// </summary>
        /// <returns></returns>
        public bool DeleteBusinessHourWeek(string typeUUID, string operatorName)
        {
            if (string.IsNullOrWhiteSpace(typeUUID))
                throw new ArgumentNullException("typeUUID为空");
            List<BusinessHourWeekPO> listBusinessHourWeek = GetBusinessHourWeekList(null, null, null, new List<string> { typeUUID }, null);
            listBusinessHourWeek.ForEach(item => {
                item.Deleted = true;
                Update(item, operatorName);
            });

            return true;
        }

        /// <summary>
        /// 添加时间段(自动生成周一到周日的配置)
        /// </summary>
        /// <param name="businessHourWeekBase"></param>
        public bool AddBusinessHourWeekBase(BusinessHourWeekBaseDTO businessHourWeekBase,string operatorName)
        {
            if (businessHourWeekBase == null)
                throw new ArgumentNullException("businessHourWeekBase为空");
            if (string.IsNullOrWhiteSpace(businessHourWeekBase.Name))
                throw new BusinessException("名称为空");
            string uuid = System.Guid.NewGuid().ToString();
            for (int i = 1; i < 8; i++)
            {
                BusinessHourWeekPO businessHourWeek = new BusinessHourWeekPO()
                {
                    AllowOrderNumber = businessHourWeekBase.AllowOrderNumber,
                    Deleted = false,
                    EndDate = businessHourWeekBase.EndDate,
                    EndTime = businessHourWeekBase.EndTime,
                    InAdvance = businessHourWeekBase.InAdvance,
                    LatestOrderTime = businessHourWeekBase.LatestOrderTime,
                    Name = businessHourWeekBase.Name,
                    ResUUID = businessHourWeekBase.ResUUID,
                    SortIndex = businessHourWeekBase.SortIndex,
                    StartDate = businessHourWeekBase.StartDate,
                    StartTime = businessHourWeekBase.StartTime,
                    State = businessHourWeekBase.State,
                    UUID = System.Guid.NewGuid().ToString(),
                    WeekDay = i,
                    TypeUUID = uuid
                };

                Add(businessHourWeek, operatorName);
            }
            return true;
        }

        /// <summary>
        /// 添加时间段(自动生成周一到周日的配置)
        /// </summary>
        /// <param name="businessHourWeekBase"></param>
        public bool UpdateBusinessHourWeekBase(UpdateBusinessHourWeekBaseDTO updateBusinessHourWeekBase, string operatorName)
        {
            if (updateBusinessHourWeekBase == null)
                throw new ArgumentNullException("businessHourWeekBase为空");

            List<BusinessHourWeekPO> listBusinessHour = GetBusinessHourWeekList(null, null, null, new List<string> { updateBusinessHourWeekBase.TypeUUID }, null);
            if (string.IsNullOrWhiteSpace(updateBusinessHourWeekBase.Name))
            {
                if (listBusinessHour != null && listBusinessHour.Count > 0)
                    updateBusinessHourWeekBase.Name = listBusinessHour[0].Name;
            }

            listBusinessHour.ForEach(item =>
            {
                item.AllowOrderNumber = updateBusinessHourWeekBase.AllowOrderNumber;
                item.EndDate = updateBusinessHourWeekBase.EndDate;
                item.EndTime = updateBusinessHourWeekBase.EndTime;
                item.InAdvance = updateBusinessHourWeekBase.InAdvance;
                item.LatestOrderTime = updateBusinessHourWeekBase.LatestOrderTime;
                item.Name = updateBusinessHourWeekBase.Name;
                item.ResUUID = updateBusinessHourWeekBase.ResUUID;
                item.SortIndex = updateBusinessHourWeekBase.SortIndex;
                item.StartDate = updateBusinessHourWeekBase.StartDate;
                item.StartTime = updateBusinessHourWeekBase.StartTime;
                item.State = updateBusinessHourWeekBase.State;

                Update(item, operatorName);
            });
            return true;
        }


        /// <summary>
        /// 生成单个时间段
        /// </summary>
        /// <param name="addBusinessHourWeek"></param>
        /// <param name="operatorName"></param>
        /// <returns></returns>
        public int AddBusinessHourWeek(AddBusinessHourWeekDTO addBusinessHourWeek, string operatorName)
        {
            if (addBusinessHourWeek == null)
                throw ErrorCodeHelper.BusinessHourWeekNull.ToException();
            if (string.IsNullOrWhiteSpace(operatorName))
                throw ErrorCodeHelper.OperatorNameNull.ToException();

            string uuid = System.Guid.NewGuid().ToString();
            List<BusinessHourWeekPO> listBusinessHour = GetBusinessHourWeekList(null, null, null, new List<string> { addBusinessHourWeek.TypeUUID }, null);
            if (listBusinessHour.Count(x => x.WeekDay == addBusinessHourWeek.WeekDay) > 0)
                throw new BusinessException("已存在此时间段");

            if (string.IsNullOrWhiteSpace(addBusinessHourWeek.Name))
            {
                if (listBusinessHour != null && listBusinessHour.Count > 0)
                    addBusinessHourWeek.Name = listBusinessHour[0].Name;
            }

            BusinessHourWeekPO businessHourWeek = new BusinessHourWeekPO()
            {
                AllowOrderNumber = addBusinessHourWeek.AllowOrderNumber,
                Deleted = false,
                EndDate = addBusinessHourWeek.EndDate,
                EndTime = addBusinessHourWeek.EndTime,
                InAdvance = addBusinessHourWeek.InAdvance,
                LatestOrderTime = addBusinessHourWeek.LatestOrderTime,
                Name = addBusinessHourWeek.Name,
                ResUUID = addBusinessHourWeek.ResUUID,
                SortIndex = addBusinessHourWeek.SortIndex,
                StartDate = addBusinessHourWeek.StartDate,
                StartTime = addBusinessHourWeek.StartTime,
                State = addBusinessHourWeek.State,
                UUID = System.Guid.NewGuid().ToString(),
                WeekDay = addBusinessHourWeek.WeekDay,
                TypeUUID = addBusinessHourWeek.TypeUUID,
            };

            return Add(businessHourWeek, operatorName);
        }

        /// <summary>
        /// 设置时间
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="state"></param>
        /// <param name="operatorName"></param>
        /// <returns></returns>
        public bool SetBusinessHourWeekState(int Id, byte state, string operatorName)
        {
            BusinessHourWeekPO businessHourWeek = GetById(Id);
            if (businessHourWeek == null)
                throw ErrorCodeHelper.BusinessHourWeekNotExist.ToException();

            businessHourWeek.State = state;
            Update(businessHourWeek, operatorName);
            return true;
        }

        /// <summary>
        /// 修改时间段
        /// </summary>
        /// <param name="businessHourWeek"></param>
        /// <param name="operatorName"></param>
        /// <returns></returns>
        public int UpdateBusinessHourWeek(UpdateBusinessHourWeekDTO businessHourWeek, string operatorName)
        {

            if (businessHourWeek == null)
                throw ErrorCodeHelper.BusinessHourWeekNull.ToException();
            if (string.IsNullOrWhiteSpace(operatorName))
                throw ErrorCodeHelper.OperatorNameNull.ToException();

            if (businessHourWeek.Id <= 0)
            {
                throw ErrorCodeHelper.IdLessErr.ToException();
            }

            BusinessHourWeekPO businessHourWeekPO = BusinessHourWeekManager.Instance.GetById(businessHourWeek.Id);
            if (businessHourWeekPO == null)
                throw ErrorCodeHelper.BusinessHourWeekNotExist.ToException();

            businessHourWeekPO.AllowOrderNumber = businessHourWeek.AllowOrderNumber;
            businessHourWeekPO.EndDate = businessHourWeek.EndDate;
            businessHourWeekPO.EndTime = businessHourWeek.EndTime;
            businessHourWeekPO.InAdvance = businessHourWeek.InAdvance;
            businessHourWeekPO.LatestOrderTime = businessHourWeek.LatestOrderTime;
            businessHourWeekPO.Name = businessHourWeek.Name;
            businessHourWeekPO.ResUUID = businessHourWeek.ResUUID;
            businessHourWeekPO.SortIndex = businessHourWeek.SortIndex;
            businessHourWeekPO.StartDate = businessHourWeek.StartDate;
            businessHourWeekPO.StartTime = businessHourWeek.StartTime;
            businessHourWeekPO.State = businessHourWeek.State;
            businessHourWeekPO.UUID = System.Guid.NewGuid().ToString();

            return Update(businessHourWeekPO, operatorName);
        }


        /// <summary>
        /// 获取餐厅下的有效周规则
        /// </summary>
        /// <param name="resUUID">餐厅UUID</param>
        /// <returns></returns>
        public List<BusinessHourWeekDTO> GetBusinessHourWeeksByResUUID(string resUUID)
        {
            if (String.IsNullOrWhiteSpace(resUUID))
            {
                throw new ArgumentNullOrWhiteSpaceException("resID");
            }

            return PoToDtoList(GetBusinessHourWeekList(null, null, new List<string> { resUUID }, null, null)); 
        }

        /// <summary>
        /// 获取指定餐厅所有营业时间的类型组成的数组。
        /// </summary>
        /// <param name="resUUID"></param>
        /// <returns></returns>
        public KeyValue<string, string>[] GetAllBusinessHourTypesByResUUID(string resUUID)
        {
            Dictionary<string, KeyValue<string, string>> diKeyValue = new Dictionary<string, KeyValue<string, string>>();


            List<BusinessHourWeekDTO> businessHourWeeks = BusinessHourWeekManager.Instance.GetBusinessHourWeeksByResUUID(resUUID);
            if (businessHourWeeks != null && businessHourWeeks.Count > 0)
            {
                businessHourWeeks = businessHourWeeks.OrderByDescending(x => (x.SortIndex.HasValue ? x.SortIndex.Value : 0)).ToList();
                for (int i = 0; i < businessHourWeeks.Count; i++)
                {
                    if (businessHourWeeks[i] != null)
                    {
                        string key = businessHourWeeks[i].TypeUUID;
                        if (!diKeyValue.ContainsKey(key))
                        {
                            diKeyValue[key] = new KeyValue<string, string> { Key = businessHourWeeks[i].TypeUUID, Value = businessHourWeeks[i].Name };
                        }
                    }
                }
            }

            if (diKeyValue.Count > 0)
            {
                return diKeyValue.Values.ToArray();
            }

            return Empty<KeyValue<string, string>>.Array;
        }

        public List<BusinessHourWeekTypeDTO> GetBusinessHourWeekType(string resUUID)
        {
            List<BusinessHourWeekTypeDTO> listBusinessHourWeekType = new List<BusinessHourWeekTypeDTO>();

            KeyValue<string, string>[] lstBusinessName = GetAllBusinessHourTypesByResUUID(resUUID);

            if (lstBusinessName == null || lstBusinessName.Length == 0)
                return listBusinessHourWeekType;

            List<BusinessHourWeekDTO> listBusinessHourWeek = GetBusinessHourWeeksByResUUID(resUUID);

            foreach (var item in lstBusinessName)
            {
                if (item == null)
                    continue;

                BusinessHourWeekTypeDTO businessHourWeekType = new BusinessHourWeekTypeDTO()
                {
                    Key = item.Key,
                    Value = item.Value
                };

                businessHourWeekType.listBusinessHourWeek.AddRange(listBusinessHourWeek.Where(x => x.TypeUUID == item.Key));
                listBusinessHourWeekType.Add(businessHourWeekType);
            }

            return listBusinessHourWeekType;
        }

        #region 自动生成
        public BusinessHourWeekPO GetById(int id)
        {
            return BusinessHourWeekPOManager.Instance.GetById(id);
        }

        public BusinessHourWeekPO GetByIdWithContext(IEntityContext entityContext, int id)
        {
            return BusinessHourWeekPOManager.Instance.GetByIdWithContext(entityContext, id);
        }

        public List<BusinessHourWeekPO> GetBusinessHourWeekList(int? id, List<string> uUID, List<string> resUUID, List<string> typeUUID, List<int> states)
        {
            List<BusinessHourWeekPO> listResult = new List<BusinessHourWeekPO>();
            int count = 1;
            while(true)
            {
                Core.Data.QueryResult<BusinessHourWeekPO> result = GetBusinessHourWeek(id, uUID, resUUID, typeUUID, states, count, 1000);
                if (result.Items != null && result.Items.Length > 0)
                    listResult.AddRange(result.Items);
                count = count + 1000;
                if (result.TotalCount <= (count - 1))
                    break;
            }

            return listResult;
        }
        
        public Core.Data.QueryResult<BusinessHourWeekPO> GetBusinessHourWeek(int? id, List<string> uUID, List<string> resUUID, List<string> typeUUID, List<int> states,  int startIndex, int count)
        {
            IPredicate predicate = this.GetBusinessHourWeekCondition(id, uUID, resUUID, typeUUID, states);
            Core.Data.QueryResult<BusinessHourWeekPO> resultRest = BusinessHourWeekPOManager.Instance.GetBusinessHourWeekWithTotalCountByPredicate(predicate, startIndex, count, null);
            return resultRest;
        }

        private IPredicate GetBusinessHourWeekCondition(int? id, List<string> uUID, List<string> resUUID, List<string> typeUUID, List<int> states)
        {
            List<IPredicate> predicates = new List<IPredicate>();

            #region 基本条件
            predicates.Add(PredicateFactory.Equal<BusinessHourWeekPO>(t => t.Deleted, false));
            if (id.HasValue)
            {
                predicates.Add(PredicateFactory.Equal<BusinessHourWeekPO>(t => t.Id, id));
            }
            if (uUID != null && uUID.Count > 0)
            {
                predicates.Add(PredicateFactory.In<BusinessHourWeekPO>(t => t.UUID, uUID.ToArray()));
            }
            if (resUUID != null && resUUID.Count > 0)
            {
                predicates.Add(PredicateFactory.In<BusinessHourWeekPO>(t => t.ResUUID, resUUID.ToArray()));
            }
            if (typeUUID != null && typeUUID.Count > 0)
            {
                predicates.Add(PredicateFactory.In<BusinessHourWeekPO>(t => t.TypeUUID, typeUUID.ToArray()));
            }
            if (states != null && states.Count > 0)
            {
                predicates.Add(PredicateFactory.In<BusinessHourWeekPO>(t => t.State, states.ToArray()));
            }
            #endregion

            #region 数量条件
            #endregion
            return PredicateFactory.And(predicates.ToArray());
        }

        public int AddWithContext(IEntityContext entityContext, BusinessHourWeekPO po, string operatorName)
        {
            if (po == null)
                throw ErrorCodeHelper.BusinessHourWeekNull.ToException();

            EnumOperationLogAction enumOperationLogAction = EnumOperationLogAction.Add;
            BusinessHourWeekPO oldObject = null;
            BusinessHourWeekPO newObject = po;
            
            
            BusinessHourWeekPOManager.Instance.AddWithContext(entityContext, newObject);
            po.Id = newObject.Id;

            COperationLogManager.Instance.AddOperationLog(po.Id, EnumOperationLogType.BusinessHourWeek, enumOperationLogAction, oldObject, newObject, operatorName);
            return po.Id;
        }
        
        public int UpdateWithContext(IEntityContext entityContext, BusinessHourWeekPO po, string operatorName)
        {
            if (po == null)
                throw ErrorCodeHelper.BusinessHourWeekNull.ToException();

            EnumOperationLogAction enumOperationLogAction = EnumOperationLogAction.Update;
            BusinessHourWeekPO oldObject = null;
            BusinessHourWeekPO newObject = po;
                        
            
            oldObject = GetByIdWithContext(entityContext, po.Id);  
            if (oldObject == null)
                throw ErrorCodeHelper.BusinessHourWeekNotExist.ToException();  
            BusinessHourWeekPOManager.Instance.UpdateWithContext(entityContext, newObject);

            COperationLogManager.Instance.AddOperationLog(po.Id, EnumOperationLogType.BusinessHourWeek, enumOperationLogAction, oldObject, newObject, operatorName);
            return po.Id;
        }
        
        public int Add(BusinessHourWeekPO po, string operatorName)
        {
            using (IEntityContext entityContext = BusinessHourWeekPOManager.Instance.CreateEntityContext())
            {
                po.Id = AddWithContext(entityContext, po, operatorName);
                return po.Id;
            }
        }
        
        public int Update(BusinessHourWeekPO po, string operatorName)
        {
            using (IEntityContext entityContext = BusinessHourWeekPOManager.Instance.CreateEntityContext())
            {
                po.Id = UpdateWithContext(entityContext, po, operatorName);
                return po.Id;
            }
        }
        
        #endregion
    }
}
