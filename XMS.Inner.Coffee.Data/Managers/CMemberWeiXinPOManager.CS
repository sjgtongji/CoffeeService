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
    public class CMemberWeiXinPOManager : POManagerBase<CMemberWeiXinPO>
    {
        public static readonly CMemberWeiXinPOManager Instance = new CMemberWeiXinPOManager();
        private CMemberWeiXinPOManager() { }

        public QueryResult<CMemberWeiXinPO> GetMemberWeiXinWithTotalCountByPredicate(IPredicate predicate, int startIndex, int count, params ISort[] sorts)
        {
            return EntityContextFactory.Execute<QueryResult<CMemberWeiXinPO>>(entityContext => this.GetMemberWeiXinWithTotalCountByPredicateWithContext(entityContext, predicate, startIndex, count, sorts), 1);
        }
        public QueryResult<CMemberWeiXinPO> GetMemberWeiXinWithTotalCountByPredicateWithContext(IEntityContext entityContext, IPredicate predicate, int startIndex, int count, params ISort[] sorts)
        {
            this.EnsureParameterNotNull(entityContext, () => entityContext);

            return entityContext.FindSubsetWithTotalCount<CMemberWeiXinPO>(
                predicate,
                startIndex,
                count,
                sorts
            );
        }
    }



}