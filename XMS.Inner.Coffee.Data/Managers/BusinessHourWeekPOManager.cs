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
    public class BusinessHourWeekPOManager : POManagerBase<BusinessHourWeekPO> 
    {
        public static readonly BusinessHourWeekPOManager Instance = new BusinessHourWeekPOManager();
        private BusinessHourWeekPOManager() {}
        
        public BusinessHourWeekPO GetById(int id, bool throwExceptionWhenNotExists)
        {
            return EntityContextFactory.Execute<BusinessHourWeekPO>((entityContext) =>
            {
                return this.GetByIdWithContext(entityContext, id, throwExceptionWhenNotExists);
            });
        }
        
        public BusinessHourWeekPO GetByIdWithContext(IEntityContext entityContext, int id, bool throwExceptionWhenNotExists)
        {
            this.EnsureParameterNotNull(entityContext, () => entityContext);
            if (id < 1)
            {
                throw new Core.ArgumentInvalidException("Id");
            }
            BusinessHourWeekPO po = entityContext.FindByPrimaryKey<BusinessHourWeekPO>(id);

            if (throwExceptionWhenNotExists)
            {
                if (po == null)
                {
                    throw new EntityNotExistException("BusinessHourWeekPO " + id);
                }
            }

            return po;
        }        
        
        public BusinessHourWeekPO[] GetAll()
        {
            using (IEntityContext entityContext = BusinessHourWeekPOManager.Instance.CreateEntityContext())
            {
                List<IPredicate> predicates = new List<Core.Data.IPredicate>();
                predicates.Add(PredicateFactory.Equal<BusinessHourWeekPO>(o => o.Deleted, false));
                IPredicate predicate = PredicateFactory.And(predicates.ToArray());
                return entityContext.Find<BusinessHourWeekPO>(predicate);
            }
        }
        
        public QueryResult<BusinessHourWeekPO> GetBusinessHourWeekWithTotalCountByPredicate(IPredicate predicate, int startIndex, int count, params ISort[] sorts)
        {
            return EntityContextFactory.Execute<QueryResult<BusinessHourWeekPO>>((entityContext) =>
            {
                return this.GetBusinessHourWeekWithTotalCountByPredicateWithContext(entityContext, predicate, startIndex, count, sorts);
            });
        }
        
        public QueryResult<BusinessHourWeekPO> GetBusinessHourWeekWithTotalCountByPredicateWithContext(IEntityContext entityContext, IPredicate predicate, int startIndex, int count, params ISort[] sorts)
        {
            this.EnsureParameterNotNull(entityContext, () => entityContext);

            return entityContext.FindSubsetWithTotalCount<BusinessHourWeekPO>(
                predicate,
                startIndex,
                count,
                sorts
            );
        }
    }
}