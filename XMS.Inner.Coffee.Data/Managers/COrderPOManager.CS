
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
    public class COrderPOManager : POManagerBase<COrderPO> 
    {
        public static readonly COrderPOManager Instance = new COrderPOManager();
        private COrderPOManager() { }

        public Core.Data.QueryResult<COrderPO> GetOrderWithTotalCountByPredicate(IPredicate predicate, int startIndex, int count, params ISort[] sorts)
        {
            return EntityContextFactory.Execute<Core.Data.QueryResult<COrderPO>>((entityContext) =>
            {
                return this.GetOrderWithTotalCountByPredicateWithContext(entityContext, predicate, startIndex, count, sorts);
            });
        }

        public Core.Data.QueryResult<COrderPO> GetOrderWithTotalCountByPredicateWithContext(IEntityContext entityContext, IPredicate predicate, int startIndex, int count, params ISort[] sorts)
        {
            this.EnsureParameterNotNull(entityContext, () => entityContext);

            return entityContext.FindSubsetWithTotalCount<COrderPO>(
                predicate,
                startIndex,
                count,
                sorts
            );
        }
    }
}