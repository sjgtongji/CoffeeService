
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
    public class CMemberAddressPOManager : POManagerBase<CMemberAddressPO> 
    {
        public static readonly CMemberAddressPOManager Instance = new CMemberAddressPOManager();
        private CMemberAddressPOManager() { }

        public Core.Data.QueryResult<CMemberAddressPO> GetMemberAddressWithTotalCountByPredicate(IPredicate predicate, int startIndex, int count, params ISort[] sorts)
        {
            return EntityContextFactory.Execute<Core.Data.QueryResult<CMemberAddressPO>>((entityContext) =>
            {
                return this.GetMemberAddressWithTotalCountByPredicateWithContext(entityContext, predicate, startIndex, count, sorts);
            });
        }

        public Core.Data.QueryResult<CMemberAddressPO> GetMemberAddressWithTotalCountByPredicateWithContext(IEntityContext entityContext, IPredicate predicate, int startIndex, int count, params ISort[] sorts)
        {
            this.EnsureParameterNotNull(entityContext, () => entityContext);

            return entityContext.FindSubsetWithTotalCount<CMemberAddressPO>(
                predicate,
                startIndex,
                count,
                sorts
            );
        }
    }
}