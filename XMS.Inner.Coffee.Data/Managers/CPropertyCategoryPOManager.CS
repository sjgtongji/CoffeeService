
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
    public class CPropertyCategoryPOManager : POManagerBase<CPropertyCategoryPO> 
    {
        public static readonly CPropertyCategoryPOManager Instance = new CPropertyCategoryPOManager();
        private CPropertyCategoryPOManager() { }

        public Core.Data.QueryResult<CPropertyCategoryPO> GetPropertyCategoryWithTotalCountByPredicate(IPredicate predicate, int startIndex, int count, params ISort[] sorts)
        {
            return EntityContextFactory.Execute<Core.Data.QueryResult<CPropertyCategoryPO>>((entityContext) =>
            {
                return this.GetPropertyCategoryWithTotalCountByPredicateWithContext(entityContext, predicate, startIndex, count, sorts);
            });
        }

        public Core.Data.QueryResult<CPropertyCategoryPO> GetPropertyCategoryWithTotalCountByPredicateWithContext(IEntityContext entityContext, IPredicate predicate, int startIndex, int count, params ISort[] sorts)
        {
            this.EnsureParameterNotNull(entityContext, () => entityContext);

            return entityContext.FindSubsetWithTotalCount<CPropertyCategoryPO>(
                predicate,
                startIndex,
                count,
                sorts
            );
        }
    }
}