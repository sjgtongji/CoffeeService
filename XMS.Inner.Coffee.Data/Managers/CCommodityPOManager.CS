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
    public class CCommodityPOManager : POManagerBase<CCommodityPO> 
    {
        public static readonly CCommodityPOManager Instance = new CCommodityPOManager();
        private CCommodityPOManager() { }


        public Core.Data.QueryResult<CCommodityPO> GetCCommodityWithTotalCountByPredicate(IPredicate predicate, int startIndex, int count, params ISort[] sorts)
        {
            return EntityContextFactory.Execute<Core.Data.QueryResult<CCommodityPO>>((entityContext) =>
            {
                return this.GetCCommodityWithTotalCountByPredicateWithContext(entityContext, predicate, startIndex, count, sorts);
            });
        }

        public Core.Data.QueryResult<CCommodityPO> GetCCommodityWithTotalCountByPredicateWithContext(IEntityContext entityContext, IPredicate predicate, int startIndex, int count, params ISort[] sorts)
        {
            this.EnsureParameterNotNull(entityContext, () => entityContext);

            return entityContext.FindSubsetWithTotalCount<CCommodityPO>(
                predicate,
                startIndex,
                count,
                sorts
            );
        }
    }
}