
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
    public class CVerificationCodePOManager : POManagerBase<CVerificationCodePO> 
    {
        public static readonly CVerificationCodePOManager Instance = new CVerificationCodePOManager();
        private CVerificationCodePOManager() { }

        public Core.Data.QueryResult<CVerificationCodePO> GetVerificationCodeWithTotalCountByPredicate(IPredicate predicate, int startIndex, int count, params ISort[] sorts)
        {
            return EntityContextFactory.Execute<Core.Data.QueryResult<CVerificationCodePO>>((entityContext) =>
            {
                return this.GetVerificationCodeWithTotalCountByPredicateWithContext(entityContext, predicate, startIndex, count, sorts);
            });
        }

        public Core.Data.QueryResult<CVerificationCodePO> GetVerificationCodeWithTotalCountByPredicateWithContext(IEntityContext entityContext, IPredicate predicate, int startIndex, int count, params ISort[] sorts)
        {
            this.EnsureParameterNotNull(entityContext, () => entityContext);

            return entityContext.FindSubsetWithTotalCount<CVerificationCodePO>(
                predicate,
                startIndex,
                count,
                sorts
            );
        }
    }
}