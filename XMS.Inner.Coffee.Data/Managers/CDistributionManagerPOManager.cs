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
    public class CDistributionManagerPOManager : POManagerBase<CDistributionManagerPO> 
    {
        public static readonly CDistributionManagerPOManager Instance = new CDistributionManagerPOManager();
        private CDistributionManagerPOManager() {}
        
        public CDistributionManagerPO GetById(int id, bool throwExceptionWhenNotExists)
        {
            return EntityContextFactory.Execute<CDistributionManagerPO>((entityContext) =>
            {
                return this.GetByIdWithContext(entityContext, id, throwExceptionWhenNotExists);
            });
        }
        
        public CDistributionManagerPO GetByIdWithContext(IEntityContext entityContext, int id, bool throwExceptionWhenNotExists)
        {
            this.EnsureParameterNotNull(entityContext, () => entityContext);
            if (id < 1)
            {
                throw new Core.ArgumentInvalidException("Id");
            }
            CDistributionManagerPO po = entityContext.FindByPrimaryKey<CDistributionManagerPO>(id);

            if (throwExceptionWhenNotExists)
            {
                if (po == null)
                {
                    throw new EntityNotExistException("CDistributionManagerPO " + id);
                }
            }

            return po;
        }        
        
        public CDistributionManagerPO[] GetAll()
        {
            using (IEntityContext entityContext = CDistributionManagerPOManager.Instance.CreateEntityContext())
            {
                List<IPredicate> predicates = new List<Core.Data.IPredicate>();
                predicates.Add(PredicateFactory.Equal<CDistributionManagerPO>(o => o.IsDelete, false));
                IPredicate predicate = PredicateFactory.And(predicates.ToArray());
                return entityContext.Find<CDistributionManagerPO>(predicate);
            }
        }
        
        public QueryResult<CDistributionManagerPO> GetCDistributionManagerWithTotalCountByPredicate(IPredicate predicate, int startIndex, int count, params ISort[] sorts)
        {
            return EntityContextFactory.Execute<QueryResult<CDistributionManagerPO>>((entityContext) =>
            {
                return this.GetCDistributionManagerWithTotalCountByPredicateWithContext(entityContext, predicate, startIndex, count, sorts);
            });
        }
        
        public QueryResult<CDistributionManagerPO> GetCDistributionManagerWithTotalCountByPredicateWithContext(IEntityContext entityContext, IPredicate predicate, int startIndex, int count, params ISort[] sorts)
        {
            this.EnsureParameterNotNull(entityContext, () => entityContext);

            return entityContext.FindSubsetWithTotalCount<CDistributionManagerPO>(
                predicate,
                startIndex,
                count,
                sorts
            );
        }
    }
}