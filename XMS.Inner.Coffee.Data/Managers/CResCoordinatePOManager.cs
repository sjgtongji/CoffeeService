﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMS.Inner.Coffee.Model;
using XMS.Core.Data;

namespace XMS.Inner.Coffee.Data
{
    public class CResCoordinatePOManager : POManagerBase<CResCoordinatePO> 
    {
        public static readonly CResCoordinatePOManager Instance = new CResCoordinatePOManager();
        private CResCoordinatePOManager() {}
        
        public CResCoordinatePO GetById(int id, bool throwExceptionWhenNotExists)
        {
            return EntityContextFactory.Execute<CResCoordinatePO>((entityContext) =>
            {
                return this.GetByIdWithContext(entityContext, id, throwExceptionWhenNotExists);
            });
        }
        
        public CResCoordinatePO GetByIdWithContext(IEntityContext entityContext, int id, bool throwExceptionWhenNotExists)
        {
            this.EnsureParameterNotNull(entityContext, () => entityContext);
            if (id < 1)
            {
                throw new Core.ArgumentInvalidException("Id");
            }
            CResCoordinatePO po = entityContext.FindByPrimaryKey<CResCoordinatePO>(id);

            if (throwExceptionWhenNotExists)
            {
                if (po == null)
                {
                    throw new EntityNotExistException("CResCoordinatePO " + id);
                }
            }

            return po;
        }        
        
        public CResCoordinatePO[] GetAll()
        {
            using (IEntityContext entityContext = CResCoordinatePOManager.Instance.CreateEntityContext())
            {
                List<IPredicate> predicates = new List<Core.Data.IPredicate>();
                predicates.Add(PredicateFactory.Equal<CResCoordinatePO>(o => o.IsDelete, false));
                IPredicate predicate = PredicateFactory.And(predicates.ToArray());
                return entityContext.Find<CResCoordinatePO>(predicate);
            }
        }
        
        public QueryResult<CResCoordinatePO> GetCResCoordinateWithTotalCountByPredicate(IPredicate predicate, int startIndex, int count, params ISort[] sorts)
        {
            return EntityContextFactory.Execute<QueryResult<CResCoordinatePO>>((entityContext) =>
            {
                return this.GetCResCoordinateWithTotalCountByPredicateWithContext(entityContext, predicate, startIndex, count, sorts);
            });
        }
        
        public QueryResult<CResCoordinatePO> GetCResCoordinateWithTotalCountByPredicateWithContext(IEntityContext entityContext, IPredicate predicate, int startIndex, int count, params ISort[] sorts)
        {
            this.EnsureParameterNotNull(entityContext, () => entityContext);

            return entityContext.FindSubsetWithTotalCount<CResCoordinatePO>(
                predicate,
                startIndex,
                count,
                sorts
            );
        }
    }
}