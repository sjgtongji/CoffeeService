
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
    public class CDistributionCoordinateManager: DataMapperManagerBase<CDistributionCoordinatePO, CDistributionCoordinateDTO>, IManagerBase<CDistributionCoordinatePO>
    {
        public static readonly CDistributionCoordinateManager Instance = new CDistributionCoordinateManager();
        private CDistributionCoordinateManager() { }

        #region 自动生成
        public CDistributionCoordinatePO GetById(int id)
        {
            return CDistributionCoordinatePOManager.Instance.GetById(id);
        }

        public CDistributionCoordinatePO GetByIdWithContext(IEntityContext entityContext, int id)
        {
            return CDistributionCoordinatePOManager.Instance.GetByIdWithContext(entityContext, id);
        }

        public List<CDistributionCoordinatePO> GetCDistributionCoordinateList(int? id, List<int> distributionId, List<int> state)
        {
            List<CDistributionCoordinatePO> listResult = new List<CDistributionCoordinatePO>();
            int count = 1;
            while(true)
            {
                Core.Data.QueryResult<CDistributionCoordinatePO> result = GetCDistributionCoordinate(id, distributionId, state, count, 1000);
                if (result.Items != null && result.Items.Length > 0)
                    listResult.AddRange(result.Items);
                count = count + 1000;
                if (result.TotalCount <= (count - 1))
                    break;
            }

            return listResult;
        }
        
        public Core.Data.QueryResult<CDistributionCoordinatePO> GetCDistributionCoordinate(int? id, List<int> distributionId, List<int> state,  int startIndex, int count)
        {
            IPredicate predicate = this.GetCDistributionCoordinateCondition(id, distributionId, state);
            Core.Data.QueryResult<CDistributionCoordinatePO> resultRest = CDistributionCoordinatePOManager.Instance.GetCDistributionCoordinateWithTotalCountByPredicate(predicate, startIndex, count, null);
            return resultRest;
        }

        private IPredicate GetCDistributionCoordinateCondition(int? id, List<int> distributionId, List<int> state)
        {
            List<IPredicate> predicates = new List<IPredicate>();

            #region 基本条件
            if (id.HasValue)
            {
                predicates.Add(PredicateFactory.Equal<CDistributionCoordinatePO>(t => t.Id, id));
            }
            if (distributionId != null && distributionId.Count > 0)
            {
                predicates.Add(PredicateFactory.In<CDistributionCoordinatePO>(t => t.DistributionId, distributionId.ToArray()));
            }
            if (state != null && state.Count > 0)
            {
                predicates.Add(PredicateFactory.In<CDistributionCoordinatePO>(t => t.State, state.ToArray()));
            }
            #endregion

            #region 数量条件
            #endregion
            return PredicateFactory.And(predicates.ToArray());
        }

        public int AddWithContext(IEntityContext entityContext, CDistributionCoordinatePO po, string operatorName)
        {
            if (po == null)
                throw ErrorCodeHelper.CDistributionCoordinateNull.ToException();

            EnumOperationLogAction enumOperationLogAction = EnumOperationLogAction.Add;
            CDistributionCoordinatePO oldObject = null;
            CDistributionCoordinatePO newObject = po;
            
            newObject.CreateName =  operatorName;
            newObject.CreateTime =  DateTime.Now;
            newObject.UpdateName =  operatorName;
            newObject.UpdateTime =  DateTime.Now;
            
            CDistributionCoordinatePOManager.Instance.AddWithContext(entityContext, newObject);
            po.Id = newObject.Id;

            COperationLogManager.Instance.AddOperationLog(po.Id, EnumOperationLogType.CDistributionCoordinate, enumOperationLogAction, oldObject, newObject, operatorName);
            return po.Id;
        }
        
        public int UpdateWithContext(IEntityContext entityContext, CDistributionCoordinatePO po, string operatorName)
        {
            if (po == null)
                throw ErrorCodeHelper.CDistributionCoordinateNull.ToException();

            EnumOperationLogAction enumOperationLogAction = EnumOperationLogAction.Update;
            CDistributionCoordinatePO oldObject = null;
            CDistributionCoordinatePO newObject = po;
                        
            newObject.UpdateName =  operatorName;
            newObject.UpdateTime =  DateTime.Now;
            
            oldObject = GetByIdWithContext(entityContext, po.Id);  
            if (oldObject == null)
                throw ErrorCodeHelper.CDistributionCoordinateNotExist.ToException();  
            CDistributionCoordinatePOManager.Instance.UpdateWithContext(entityContext, newObject);

            COperationLogManager.Instance.AddOperationLog(po.Id, EnumOperationLogType.CDistributionCoordinate, enumOperationLogAction, oldObject, newObject, operatorName);
            return po.Id;
        }
        
        public int Add(CDistributionCoordinatePO po, string operatorName)
        {
            using (IEntityContext entityContext = CDistributionCoordinatePOManager.Instance.CreateEntityContext())
            {
                po.Id = AddWithContext(entityContext, po, operatorName);
                return po.Id;
            }
        }
        
        public int Update(CDistributionCoordinatePO po, string operatorName)
        {
            using (IEntityContext entityContext = CDistributionCoordinatePOManager.Instance.CreateEntityContext())
            {
                po.Id = UpdateWithContext(entityContext, po, operatorName);
                return po.Id;
            }
        }

       
        public int AddCDistributionCoordinate(AddCDistributionCoordinateDTO cDistributionCoordinate, string operatorName)
        {
            if (cDistributionCoordinate == null)
               throw ErrorCodeHelper.CDistributionCoordinateNull.ToException();
            if (string.IsNullOrWhiteSpace(operatorName))
                throw ErrorCodeHelper.OperatorNameNull.ToException();

            CDistributionCoordinatePO cDistributionCoordinatePO = new CDistributionCoordinatePO();
            
            
            return Add(cDistributionCoordinatePO, operatorName);
        }
        
        public int UpdateCDistributionCoordinate(UpdateCDistributionCoordinateDTO cDistributionCoordinate, string operatorName)
        {            
        
            if (cDistributionCoordinate == null)
               throw ErrorCodeHelper.CDistributionCoordinateNull.ToException();
            if (string.IsNullOrWhiteSpace(operatorName))
                throw ErrorCodeHelper.OperatorNameNull.ToException();

            CDistributionCoordinatePO cDistributionCoordinatePO = CDistributionCoordinateManager.Instance.GetById(cDistributionCoordinate.Id);
            if (cDistributionCoordinatePO == null)
                    throw ErrorCodeHelper.CDistributionCoordinateNotExist.ToException();
            
            if (cDistributionCoordinate.Id <= 0)
            {
                throw ErrorCodeHelper.IdLessErr.ToException();
            }
            

            return Update(cDistributionCoordinatePO, operatorName);
        }
        
        #endregion
    }
}
