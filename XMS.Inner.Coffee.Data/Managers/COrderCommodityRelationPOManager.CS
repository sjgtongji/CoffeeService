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
    public class COrderCommodityRelationPOManager : POManagerBase<COrderCommodityRelationPO>
    {
        public static readonly COrderCommodityRelationPOManager Instance = new COrderCommodityRelationPOManager();
        private COrderCommodityRelationPOManager() { }

        public QueryResult<COrderCommodityRelationPO> GetOrderCommodityRelationWithTotalCountByPredicate(IPredicate predicate, int startIndex, int count, params ISort[] sorts)
        {
            return EntityContextFactory.Execute<QueryResult<COrderCommodityRelationPO>>(entityContext => this.GetOrderCommodityRelationWithTotalCountByPredicateWithContext(entityContext, predicate, startIndex, count, sorts), 1);
        }
        public QueryResult<COrderCommodityRelationPO> GetOrderCommodityRelationWithTotalCountByPredicateWithContext(IEntityContext entityContext, IPredicate predicate, int startIndex, int count, params ISort[] sorts)
        {
            this.EnsureParameterNotNull(entityContext, () => entityContext);

            return entityContext.FindSubsetWithTotalCount<COrderCommodityRelationPO>(
                predicate,
                startIndex,
                count,
                sorts
            );
        }
    }
}


