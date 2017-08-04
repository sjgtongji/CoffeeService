using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMS.Inner.Coffee.Data;
using XMS.Inner.Coffee.Model;
using XMS.Core.Data;

namespace XMS.Inner.Coffee.Data
{
    public class CBannerPOManager : POManagerBase<CBannerPO> 
    {
        public static readonly CBannerPOManager Instance = new CBannerPOManager();
        private CBannerPOManager() {}
        
        public CBannerPO GetById(int id, bool throwExceptionWhenNotExists)
        {
            return EntityContextFactory.Execute<CBannerPO>((entityContext) =>
            {
                return this.GetByIdWithContext(entityContext, id, throwExceptionWhenNotExists);
            });
        }
        
        public CBannerPO GetByIdWithContext(IEntityContext entityContext, int id, bool throwExceptionWhenNotExists)
        {
            this.EnsureParameterNotNull(entityContext, () => entityContext);
            if (id < 1)
            {
                throw new Core.ArgumentInvalidException("Id");
            }
            CBannerPO po = entityContext.FindByPrimaryKey<CBannerPO>(id);

            if (throwExceptionWhenNotExists)
            {
                if (po == null)
                {
                    throw new EntityNotExistException("CBannerPO " + id);
                }
            }

            return po;
        }        
        
        public CBannerPO[] GetAll()
        {
            using (IEntityContext entityContext = CBannerPOManager.Instance.CreateEntityContext())
            {
                List<IPredicate> predicates = new List<Core.Data.IPredicate>();
                predicates.Add(PredicateFactory.Equal<CBannerPO>(o => o.IsDelete, false));
                IPredicate predicate = PredicateFactory.And(predicates.ToArray());
                return entityContext.Find<CBannerPO>(predicate);
            }
        }
        
        public QueryResult<CBannerPO> GetCBannerWithTotalCountByPredicate(IPredicate predicate, int startIndex, int count, params ISort[] sorts)
        {
            return EntityContextFactory.Execute<QueryResult<CBannerPO>>((entityContext) =>
            {
                return this.GetCBannerWithTotalCountByPredicateWithContext(entityContext, predicate, startIndex, count, sorts);
            });
        }
        
        public QueryResult<CBannerPO> GetCBannerWithTotalCountByPredicateWithContext(IEntityContext entityContext, IPredicate predicate, int startIndex, int count, params ISort[] sorts)
        {
            this.EnsureParameterNotNull(entityContext, () => entityContext);

            return entityContext.FindSubsetWithTotalCount<CBannerPO>(
                predicate,
                startIndex,
                count,
                sorts
            );
        }
    }
}