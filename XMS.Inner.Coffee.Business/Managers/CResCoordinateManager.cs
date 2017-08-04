
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
    public class CResCoordinateManager: DataMapperManagerBase<CResCoordinatePO, CResCoordinateDTO>, IManagerBase<CResCoordinatePO>
    {
        public static readonly CResCoordinateManager Instance = new CResCoordinateManager();
        private CResCoordinateManager() { }

        /// <summary>
        /// 添加餐厅坐标
        /// </summary>
        /// <returns></returns>
        public bool AddResCoordinate(AddResCoordinateDTO addResCoordinate,string operatorName)
        {
            if (addResCoordinate == null)
                ErrorCodeHelper.AddResCoordinateNull.ToException();
            if (addResCoordinate.ResId <= 0)
                ErrorCodeHelper.CRestaurantNotExist.ToException();
            CRestaurantPO restaurant = CRestaurantManager.Instance.GetById(addResCoordinate.ResId);
            if (restaurant == null)
                ErrorCodeHelper.CRestaurantNotExist.ToException();
            if (addResCoordinate.ListCoordinateInfo != null || addResCoordinate.ListCoordinateInfo.Count < 3)
                ErrorCodeHelper.CoordinateInfoErr.ToException();

            List<CResCoordinatePO> listResCoordinate = GetCResCoordinateList(null, restaurant.ResUUID, null, null, null);
            if (listResCoordinate != null && listResCoordinate.Count > 0)
            {
                listResCoordinate.ForEach(item =>
                {
                    item.IsDelete = true;
                    Update(item, operatorName);
                });
            }

            string uuid = System.Guid.NewGuid().ToString();
            addResCoordinate.ListCoordinateInfo.ForEach(item =>
            {
                CResCoordinatePO po = new CResCoordinatePO
                {
                    CreateName = operatorName,
                    CreateTime = DateTime.Now,
                    IsDelete = false,
                    Latitude = item.Latitude,
                    Longitude = item.Longitude,
                    MarkUUID = uuid,
                    ResUUID = restaurant.ResUUID,
                };

                Add(po, operatorName);
            });

            return true;
        }


        #region 自动生成
        public CResCoordinatePO GetById(int id)
        {
            return CResCoordinatePOManager.Instance.GetById(id);
        }

        public CResCoordinatePO GetByIdWithContext(IEntityContext entityContext, int id)
        {
            return CResCoordinatePOManager.Instance.GetByIdWithContext(entityContext, id);
        }

        public List<CResCoordinatePO> GetCResCoordinateList(int? id, string resUUID, List<decimal> longitude, List<decimal> latitude, List<string> markUUID)
        {
            List<CResCoordinatePO> listResult = new List<CResCoordinatePO>();
            int count = 1;
            while(true)
            {
                Core.Data.QueryResult<CResCoordinatePO> result = GetCResCoordinate(id, resUUID, longitude, latitude, markUUID, count, 1000);
                if (result.Items != null && result.Items.Length > 0)
                    listResult.AddRange(result.Items);
                count = count + 1000;
                if (result.TotalCount <= (count - 1))
                    break;
            }

            return listResult;
        }
        
        public Core.Data.QueryResult<CResCoordinatePO> GetCResCoordinate(int? id, string resUUID, List<decimal> longitude, List<decimal> latitude, List<string> markUUID,  int startIndex, int count)
        {
            IPredicate predicate = this.GetCResCoordinateCondition(id, resUUID, longitude, latitude, markUUID);
            Sort sort = PredicateFactory.Sort("Id", "asc");
            Core.Data.QueryResult<CResCoordinatePO> resultRest = CResCoordinatePOManager.Instance.GetCResCoordinateWithTotalCountByPredicate(predicate, startIndex, count, sort);
            return resultRest;
        }

        private IPredicate GetCResCoordinateCondition(int? id, string resUUID, List<decimal> longitude, List<decimal> latitude, List<string> markUUID)
        {
            List<IPredicate> predicates = new List<IPredicate>();

            #region 基本条件
            predicates.Add(PredicateFactory.Equal<CResCoordinatePO>(t => t.IsDelete, false));
            if (id.HasValue)
            {
                predicates.Add(PredicateFactory.Equal<CResCoordinatePO>(t => t.Id, id));
            }
            if (!string.IsNullOrWhiteSpace(resUUID))
            {
                predicates.Add(PredicateFactory.Equal<CResCoordinatePO>(t => t.ResUUID, resUUID));
            }   
            if (longitude != null && longitude.Count > 0)
            {
                predicates.Add(PredicateFactory.In<CResCoordinatePO>(t => t.Longitude, longitude.ToArray()));
            }
            if (latitude != null && latitude.Count > 0)
            {
                predicates.Add(PredicateFactory.In<CResCoordinatePO>(t => t.Latitude, latitude.ToArray()));
            }
            if (markUUID != null && markUUID.Count > 0)
            {
                predicates.Add(PredicateFactory.In<CResCoordinatePO>(t => t.MarkUUID, markUUID.ToArray()));
            }
            #endregion

            #region 数量条件
            #endregion
            return PredicateFactory.And(predicates.ToArray());
        }

        public int AddWithContext(IEntityContext entityContext, CResCoordinatePO po, string operatorName)
        {
            if (po == null)
                throw ErrorCodeHelper.CResCoordinateNull.ToException();

            EnumOperationLogAction enumOperationLogAction = EnumOperationLogAction.Add;
            CResCoordinatePO oldObject = null;
            CResCoordinatePO newObject = po;
            
            newObject.CreateName =  operatorName;
            newObject.CreateTime =  DateTime.Now;
            
            CResCoordinatePOManager.Instance.AddWithContext(entityContext, newObject);
            po.Id = newObject.Id;

            COperationLogManager.Instance.AddOperationLog(po.Id, EnumOperationLogType.CResCoordinate, enumOperationLogAction, oldObject, newObject, operatorName);
            return po.Id;
        }
        
        public int UpdateWithContext(IEntityContext entityContext, CResCoordinatePO po, string operatorName)
        {
            if (po == null)
                throw ErrorCodeHelper.CResCoordinateNull.ToException();

            EnumOperationLogAction enumOperationLogAction = EnumOperationLogAction.Update;
            CResCoordinatePO oldObject = null;
            CResCoordinatePO newObject = po;
                        
            
            oldObject = GetByIdWithContext(entityContext, po.Id);  
            if (oldObject == null)
                throw ErrorCodeHelper.CResCoordinateNotExist.ToException();  
            CResCoordinatePOManager.Instance.UpdateWithContext(entityContext, newObject);

            COperationLogManager.Instance.AddOperationLog(po.Id, EnumOperationLogType.CResCoordinate, enumOperationLogAction, oldObject, newObject, operatorName);
            return po.Id;
        }
        
        public int Add(CResCoordinatePO po, string operatorName)
        {
            using (IEntityContext entityContext = CResCoordinatePOManager.Instance.CreateEntityContext())
            {
                po.Id = AddWithContext(entityContext, po, operatorName);
                return po.Id;
            }
        }
        
        public int Update(CResCoordinatePO po, string operatorName)
        {
            using (IEntityContext entityContext = CResCoordinatePOManager.Instance.CreateEntityContext())
            {
                po.Id = UpdateWithContext(entityContext, po, operatorName);
                return po.Id;
            }
        }
        
        #endregion
    }
}
