
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
    public class CCommodityCategoryPOManager : POManagerBase<CCommodityCategoryPO> 
    {
        public static readonly CCommodityCategoryPOManager Instance = new CCommodityCategoryPOManager();
        private CCommodityCategoryPOManager() { }

        public Core.Data.QueryResult<CCommodityCategoryPO> GetCommodityCategoryWithTotalCountByPredicate(IPredicate predicate, int startIndex, int count, params ISort[] sorts)
        {
            return EntityContextFactory.Execute<Core.Data.QueryResult<CCommodityCategoryPO>>((entityContext) =>
            {
                return this.GetCommodityCategoryWithTotalCountByPredicateWithContext(entityContext, predicate, startIndex, count, sorts);
            });
        }

        public Core.Data.QueryResult<CCommodityCategoryPO> GetCommodityCategoryWithTotalCountByPredicateWithContext(IEntityContext entityContext, IPredicate predicate, int startIndex, int count, params ISort[] sorts)
        {
            this.EnsureParameterNotNull(entityContext, () => entityContext);

            return entityContext.FindSubsetWithTotalCount<CCommodityCategoryPO>(
                predicate,
                startIndex,
                count,
                sorts
            );
        }
    }
}