using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMS.Inner.Coffee.Data;
using XMS.Inner.Coffee.Model;
using XMS.Core.Data;

namespace XMS.Inner.Coffee.Data
{
    public class CIntegralRecordPOManager : POManagerBase<CIntegralRecordPO> 
    {
        public static readonly CIntegralRecordPOManager Instance = new CIntegralRecordPOManager();
        private CIntegralRecordPOManager() {}
        
        public CIntegralRecordPO GetById(int id, bool throwExceptionWhenNotExists)
        {
            return EntityContextFactory.Execute<CIntegralRecordPO>((entityContext) =>
            {
                return this.GetByIdWithContext(entityContext, id, throwExceptionWhenNotExists);
            });
        }
        
        public CIntegralRecordPO GetByIdWithContext(IEntityContext entityContext, int id, bool throwExceptionWhenNotExists)
        {
            this.EnsureParameterNotNull(entityContext, () => entityContext);
            if (id < 1)
            {
                throw new Core.ArgumentInvalidException("Id");
            }
            CIntegralRecordPO po = entityContext.FindByPrimaryKey<CIntegralRecordPO>(id);

            if (throwExceptionWhenNotExists)
            {
                if (po == null)
                {
                    throw new EntityNotExistException("CIntegralRecordPO " + id);
                }
            }

            return po;
        }        
        
        public CIntegralRecordPO[] GetAll()
        {
            using (IEntityContext entityContext = CIntegralRecordPOManager.Instance.CreateEntityContext())
            {
                List<IPredicate> predicates = new List<Core.Data.IPredicate>();
                predicates.Add(PredicateFactory.Equal<CIntegralRecordPO>(o => o.IsDelete, false));
                IPredicate predicate = PredicateFactory.And(predicates.ToArray());
                return entityContext.Find<CIntegralRecordPO>(predicate);
            }
        }
        
        public QueryResult<CIntegralRecordPO> GetCIntegralRecordWithTotalCountByPredicate(IPredicate predicate, int startIndex, int count, params ISort[] sorts)
        {
            return EntityContextFactory.Execute<QueryResult<CIntegralRecordPO>>((entityContext) =>
            {
                return this.GetCIntegralRecordWithTotalCountByPredicateWithContext(entityContext, predicate, startIndex, count, sorts);
            });
        }
        
        public QueryResult<CIntegralRecordPO> GetCIntegralRecordWithTotalCountByPredicateWithContext(IEntityContext entityContext, IPredicate predicate, int startIndex, int count, params ISort[] sorts)
        {
            this.EnsureParameterNotNull(entityContext, () => entityContext);

            return entityContext.FindSubsetWithTotalCount<CIntegralRecordPO>(
                predicate,
                startIndex,
                count,
                sorts
            );
        }
    }
}