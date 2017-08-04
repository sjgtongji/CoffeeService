
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
    public class CRestaurantPOManager : POManagerBase<CRestaurantPO> 
    {
        public static readonly CRestaurantPOManager Instance = new CRestaurantPOManager();
        private CRestaurantPOManager() { }

        public Core.Data.QueryResult<CRestaurantPO> GetRestaurantWithTotalCountByPredicate(IPredicate predicate, int startIndex, int count, params ISort[] sorts)
        {
            return EntityContextFactory.Execute<Core.Data.QueryResult<CRestaurantPO>>((entityContext) =>
            {
                return this.GetRestaurantWithTotalCountByPredicateWithContext(entityContext, predicate, startIndex, count, sorts);
            });
        }

        public Core.Data.QueryResult<CRestaurantPO> GetRestaurantWithTotalCountByPredicateWithContext(IEntityContext entityContext, IPredicate predicate, int startIndex, int count, params ISort[] sorts)
        {
            this.EnsureParameterNotNull(entityContext, () => entityContext);

            return entityContext.FindSubsetWithTotalCount<CRestaurantPO>(
                predicate,
                startIndex,
                count,
                sorts
            );
        }
    }
}