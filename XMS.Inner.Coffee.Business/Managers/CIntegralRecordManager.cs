
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
    public class CIntegralRecordManager: DataMapperManagerBase<CIntegralRecordPO, CIntegralRecordDTO>, IManagerBase<CIntegralRecordPO>
    {
        public static readonly CIntegralRecordManager Instance = new CIntegralRecordManager();
        private CIntegralRecordManager() { }

        #region 自动生成
        public CIntegralRecordPO GetById(int id)
        {
            return CIntegralRecordPOManager.Instance.GetById(id);
        }

        public CIntegralRecordPO GetByIdWithContext(IEntityContext entityContext, int id)
        {
            return CIntegralRecordPOManager.Instance.GetByIdWithContext(entityContext, id);
        }

        public List<CIntegralRecordPO> GetCIntegralRecordList(int? id, List<string> orderId, List<int> type)
        {
            List<CIntegralRecordPO> listResult = new List<CIntegralRecordPO>();
            int count = 1;
            while(true)
            {
                Core.Data.QueryResult<CIntegralRecordPO> result = GetCIntegralRecord(id, orderId, type, count, 1000);
                if (result.Items != null && result.Items.Length > 0)
                    listResult.AddRange(result.Items);
                count = count + 1000;
                if (result.TotalCount <= (count - 1))
                    break;
            }

            return listResult;
        }
        
        public Core.Data.QueryResult<CIntegralRecordPO> GetCIntegralRecord(int? id, List<string> orderId, List<int> type,  int startIndex, int count)
        {
            IPredicate predicate = this.GetCIntegralRecordCondition(id, orderId, type);
            Core.Data.QueryResult<CIntegralRecordPO> resultRest = CIntegralRecordPOManager.Instance.GetCIntegralRecordWithTotalCountByPredicate(predicate, startIndex, count, null);
            return resultRest;
        }

        private IPredicate GetCIntegralRecordCondition(int? id, List<string> orderId, List<int> type)
        {
            List<IPredicate> predicates = new List<IPredicate>();

            #region 基本条件
            if (id.HasValue)
            {
                predicates.Add(PredicateFactory.Equal<CIntegralRecordPO>(t => t.Id, id));
            }
            if (orderId != null && orderId.Count > 0)
            {
                predicates.Add(PredicateFactory.In<CIntegralRecordPO>(t => t.OrderId, orderId.ToArray()));
            }
            if (type != null && type.Count > 0)
            {
                predicates.Add(PredicateFactory.In<CIntegralRecordPO>(t => t.Type, type.ToArray()));
            }
            #endregion

            #region 数量条件
            #endregion
            return PredicateFactory.And(predicates.ToArray());
        }

        public int AddWithContext(IEntityContext entityContext, CIntegralRecordPO po, string operatorName)
        {
            if (po == null)
                throw ErrorCodeHelper.CIntegralRecordNull.ToException();
             if (string.IsNullOrWhiteSpace(po.OrderId))
                throw ErrorCodeHelper.CIntegralRecordOrderIdNotSet.ToException();           

            EnumOperationLogAction enumOperationLogAction = EnumOperationLogAction.Add;
            CIntegralRecordPO oldObject = null;
            CIntegralRecordPO newObject = po;
            
            newObject.CreateName =  operatorName;
            newObject.CreateTime =  DateTime.Now;
            newObject.UpdateName =  operatorName;
            newObject.UpdateTime =  DateTime.Now;
            
            CIntegralRecordPOManager.Instance.AddWithContext(entityContext, newObject);
            po.Id = newObject.Id;

            COperationLogManager.Instance.AddOperationLog(po.Id, EnumOperationLogType.CIntegralRecord, enumOperationLogAction, oldObject, newObject, operatorName);
            return po.Id;
        }
        
        public int UpdateWithContext(IEntityContext entityContext, CIntegralRecordPO po, string operatorName)
        {
            if (po == null)
                throw ErrorCodeHelper.CIntegralRecordNull.ToException();
             if (string.IsNullOrWhiteSpace(po.OrderId))
                throw ErrorCodeHelper.CIntegralRecordOrderIdNotSet.ToException();          

            EnumOperationLogAction enumOperationLogAction = EnumOperationLogAction.Update;
            CIntegralRecordPO oldObject = null;
            CIntegralRecordPO newObject = po;
                        
            newObject.UpdateName =  operatorName;
            newObject.UpdateTime =  DateTime.Now;
            
            oldObject = GetByIdWithContext(entityContext, po.Id);  
            if (oldObject == null)
                throw ErrorCodeHelper.CIntegralRecordNotExist.ToException();  
            CIntegralRecordPOManager.Instance.UpdateWithContext(entityContext, newObject);

            COperationLogManager.Instance.AddOperationLog(po.Id, EnumOperationLogType.CIntegralRecord, enumOperationLogAction, oldObject, newObject, operatorName);
            return po.Id;
        }
        
        public int Add(CIntegralRecordPO po, string operatorName)
        {
            using (IEntityContext entityContext = CIntegralRecordPOManager.Instance.CreateEntityContext())
            {
                po.Id = AddWithContext(entityContext, po, operatorName);
                return po.Id;
            }
        }
        
        public int Update(CIntegralRecordPO po, string operatorName)
        {
            using (IEntityContext entityContext = CIntegralRecordPOManager.Instance.CreateEntityContext())
            {
                po.Id = UpdateWithContext(entityContext, po, operatorName);
                return po.Id;
            }
        }
        
        #endregion
    }
}
