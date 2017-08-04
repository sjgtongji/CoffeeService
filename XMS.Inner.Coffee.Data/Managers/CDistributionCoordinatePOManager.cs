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
    public class CDistributionCoordinatePOManager : POManagerBase<CDistributionCoordinatePO> 
    {
        public static readonly CDistributionCoordinatePOManager Instance = new CDistributionCoordinatePOManager();
        private CDistributionCoordinatePOManager() {}
        
        public CDistributionCoordinatePO GetById(int id, bool throwExceptionWhenNotExists)
        {
            return EntityContextFactory.Execute<CDistributionCoordinatePO>((entityContext) =>
            {
                return this.GetByIdWithContext(entityContext, id, throwExceptionWhenNotExists);
            });
        }
        
        public CDistributionCoordinatePO GetByIdWithContext(IEntityContext entityContext, int id, bool throwExceptionWhenNotExists)
        {
            this.EnsureParameterNotNull(entityContext, () => entityContext);
            if (id < 1)
            {
                throw new Core.ArgumentInvalidException("Id");
            }
            CDistributionCoordinatePO po = entityContext.FindByPrimaryKey<CDistributionCoordinatePO>(id);

            if (throwExceptionWhenNotExists)
            {
                if (po == null)
                {
                    throw new EntityNotExistException("CDistributionCoordinatePO " + id);
                }
            }

            return po;
        }        
        
        public CDistributionCoordinatePO[] GetAll()
        {
            using (IEntityContext entityContext = CDistributionCoordinatePOManager.Instance.CreateEntityContext())
            {
                List<IPredicate> predicates = new List<Core.Data.IPredicate>();
                predicates.Add(PredicateFactory.Equal<CDistributionCoordinatePO>(o => o.IsDelete, false));
                IPredicate predicate = PredicateFactory.And(predicates.ToArray());
                return entityContext.Find<CDistributionCoordinatePO>(predicate);
            }
        }
        
        public QueryResult<CDistributionCoordinatePO> GetCDistributionCoordinateWithTotalCountByPredicate(IPredicate predicate, int startIndex, int count, params ISort[] sorts)
        {
            return EntityContextFactory.Execute<QueryResult<CDistributionCoordinatePO>>((entityContext) =>
            {
                return this.GetCDistributionCoordinateWithTotalCountByPredicateWithContext(entityContext, predicate, startIndex, count, sorts);
            });
        }
        
        public QueryResult<CDistributionCoordinatePO> GetCDistributionCoordinateWithTotalCountByPredicateWithContext(IEntityContext entityContext, IPredicate predicate, int startIndex, int count, params ISort[] sorts)
        {
            this.EnsureParameterNotNull(entityContext, () => entityContext);

            return entityContext.FindSubsetWithTotalCount<CDistributionCoordinatePO>(
                predicate,
                startIndex,
                count,
                sorts
            );
        }
    }
}