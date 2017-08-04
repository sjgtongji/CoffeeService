
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
    public class CManagerPOManager : POManagerBase<CManagerPO> 
    {
        public static readonly CManagerPOManager Instance = new CManagerPOManager();
        private CManagerPOManager() { }

        public Core.Data.QueryResult<CManagerPO> GetManagerWithTotalCountByPredicate(IPredicate predicate, int startIndex, int count, params ISort[] sorts)
        {
            return EntityContextFactory.Execute<Core.Data.QueryResult<CManagerPO>>((entityContext) =>
            {
                return this.GetManagerWithTotalCountByPredicateWithContext(entityContext, predicate, startIndex, count, sorts);
            });
        }

        public Core.Data.QueryResult<CManagerPO> GetManagerWithTotalCountByPredicateWithContext(IEntityContext entityContext, IPredicate predicate, int startIndex, int count, params ISort[] sorts)
        {
            this.EnsureParameterNotNull(entityContext, () => entityContext);

            return entityContext.FindSubsetWithTotalCount<CManagerPO>(
                predicate,
                startIndex,
                count,
                sorts
            );
        }
    }
}