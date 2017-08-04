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
    public class CCommodityPropertyPOManager : POManagerBase<CCommodityPropertyPO>
    {
        public static readonly CCommodityPropertyPOManager Instance = new CCommodityPropertyPOManager();
        private CCommodityPropertyPOManager() { }

        public QueryResult<CCommodityPropertyPO> GetCommodityPropertyWithTotalCountByPredicate(IPredicate predicate, int startIndex, int count, params ISort[] sorts)
        {
            return EntityContextFactory.Execute<QueryResult<CCommodityPropertyPO>>(entityContext => this.GetCommodityPropertyWithTotalCountByPredicateWithContext(entityContext, predicate, startIndex, count, sorts), 1);

        }
        public QueryResult<CCommodityPropertyPO> GetCommodityPropertyWithTotalCountByPredicateWithContext(IEntityContext entityContext, IPredicate predicate, int startIndex, int count, params ISort[] sorts)
        {
            this.EnsureParameterNotNull(entityContext, () => entityContext);

            return entityContext.FindSubsetWithTotalCount<CCommodityPropertyPO>(
                predicate,
                startIndex,
                count,
                sorts
            );
        }
    }
}