
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMS.Core.Data;
using XMS.Inner.Coffee.Data;
using XMS.Inner.Coffee.Model;

namespace XMS.Inner.Coffee.Data
{
    public class CCouponPOManager : POManagerBase<CCouponPO> 
    {
        public static readonly CCouponPOManager Instance = new CCouponPOManager();
        private CCouponPOManager() { }

        public Core.Data.QueryResult<CCouponPO> GetCouponWithTotalCountByPredicate(IPredicate predicate, int startIndex, int count, params ISort[] sorts)
        {
            return EntityContextFactory.Execute<Core.Data.QueryResult<CCouponPO>>((entityContext) =>
            {
                return this.GetCouponWithTotalCountByPredicateWithContext(entityContext, predicate, startIndex, count, sorts);
            });
        }

        public Core.Data.QueryResult<CCouponPO> GetCouponWithTotalCountByPredicateWithContext(IEntityContext entityContext, IPredicate predicate, int startIndex, int count, params ISort[] sorts)
        {
            this.EnsureParameterNotNull(entityContext, () => entityContext);

            return entityContext.FindSubsetWithTotalCount<CCouponPO>(
                predicate,
                startIndex,
                count,
                sorts
            );
        }
    }
}