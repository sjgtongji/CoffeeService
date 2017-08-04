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
    public class CCashCouponDetailPOManager : POManagerBase<CCashCouponDetailPO> 
    {
        public static readonly CCashCouponDetailPOManager Instance = new CCashCouponDetailPOManager();
        private CCashCouponDetailPOManager() {}
        
        public CCashCouponDetailPO GetById(int id, bool throwExceptionWhenNotExists)
        {
            return EntityContextFactory.Execute<CCashCouponDetailPO>((entityContext) =>
            {
                return this.GetByIdWithContext(entityContext, id, throwExceptionWhenNotExists);
            });
        }
        
        public CCashCouponDetailPO GetByIdWithContext(IEntityContext entityContext, int id, bool throwExceptionWhenNotExists)
        {
            this.EnsureParameterNotNull(entityContext, () => entityContext);
            if (id < 1)
            {
                throw new Core.ArgumentInvalidException("Id");
            }
            CCashCouponDetailPO po = entityContext.FindByPrimaryKey<CCashCouponDetailPO>(id);

            if (throwExceptionWhenNotExists)
            {
                if (po == null)
                {
                    throw new EntityNotExistException("CCashCouponDetailPO " + id);
                }
            }

            return po;
        }        
        
        public CCashCouponDetailPO[] GetAll()
        {
            using (IEntityContext entityContext = CCashCouponDetailPOManager.Instance.CreateEntityContext())
            {
                List<IPredicate> predicates = new List<Core.Data.IPredicate>();
                predicates.Add(PredicateFactory.Equal<CCashCouponDetailPO>(o => o.IsDelete, false));
                IPredicate predicate = PredicateFactory.And(predicates.ToArray());
                return entityContext.Find<CCashCouponDetailPO>(predicate);
            }
        }
        
        public QueryResult<CCashCouponDetailPO> GetCCashCouponDetailWithTotalCountByPredicate(IPredicate predicate, int startIndex, int count, params ISort[] sorts)
        {
            return EntityContextFactory.Execute<QueryResult<CCashCouponDetailPO>>((entityContext) =>
            {
                return this.GetCCashCouponDetailWithTotalCountByPredicateWithContext(entityContext, predicate, startIndex, count, sorts);
            });
        }
        
        public QueryResult<CCashCouponDetailPO> GetCCashCouponDetailWithTotalCountByPredicateWithContext(IEntityContext entityContext, IPredicate predicate, int startIndex, int count, params ISort[] sorts)
        {
            this.EnsureParameterNotNull(entityContext, () => entityContext);

            return entityContext.FindSubsetWithTotalCount<CCashCouponDetailPO>(
                predicate,
                startIndex,
                count,
                sorts
            );
        }
    }
}