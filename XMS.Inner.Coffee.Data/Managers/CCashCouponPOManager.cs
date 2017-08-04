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
    public class CCashCouponPOManager : POManagerBase<CCashCouponPO> 
    {
        public static readonly CCashCouponPOManager Instance = new CCashCouponPOManager();
        private CCashCouponPOManager() {}
        
        public CCashCouponPO GetById(int id, bool throwExceptionWhenNotExists)
        {
            return EntityContextFactory.Execute<CCashCouponPO>((entityContext) =>
            {
                return this.GetByIdWithContext(entityContext, id, throwExceptionWhenNotExists);
            });
        }
        
        public CCashCouponPO GetByIdWithContext(IEntityContext entityContext, int id, bool throwExceptionWhenNotExists)
        {
            this.EnsureParameterNotNull(entityContext, () => entityContext);
            if (id < 1)
            {
                throw new Core.ArgumentInvalidException("Id");
            }
            CCashCouponPO po = entityContext.FindByPrimaryKey<CCashCouponPO>(id);

            if (throwExceptionWhenNotExists)
            {
                if (po == null)
                {
                    throw new EntityNotExistException("CCashCouponPO " + id);
                }
            }

            return po;
        }        
        
        public CCashCouponPO[] GetAll()
        {
            using (IEntityContext entityContext = CCashCouponPOManager.Instance.CreateEntityContext())
            {
                List<IPredicate> predicates = new List<Core.Data.IPredicate>();
                predicates.Add(PredicateFactory.Equal<CCashCouponPO>(o => o.IsDelete, false));
                IPredicate predicate = PredicateFactory.And(predicates.ToArray());
                return entityContext.Find<CCashCouponPO>(predicate);
            }
        }
        
        public QueryResult<CCashCouponPO> GetCCashCouponWithTotalCountByPredicate(IPredicate predicate, int startIndex, int count, params ISort[] sorts)
        {
            return EntityContextFactory.Execute<QueryResult<CCashCouponPO>>((entityContext) =>
            {
                return this.GetCCashCouponWithTotalCountByPredicateWithContext(entityContext, predicate, startIndex, count, sorts);
            });
        }
        
        public QueryResult<CCashCouponPO> GetCCashCouponWithTotalCountByPredicateWithContext(IEntityContext entityContext, IPredicate predicate, int startIndex, int count, params ISort[] sorts)
        {
            this.EnsureParameterNotNull(entityContext, () => entityContext);

            return entityContext.FindSubsetWithTotalCount<CCashCouponPO>(
                predicate,
                startIndex,
                count,
                sorts
            );
        }
    }
}