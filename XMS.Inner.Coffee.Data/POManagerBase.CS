using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using XMS.Core;
using XMS.Core.Data;
using XMS.Inner.Coffee.Model;

namespace XMS.Inner.Coffee.Data
{
    public abstract class POManagerBase<PO> where PO : POBase, new()
    {
       
        protected POManagerBase()
        {
        }

        /// <summary>
        /// 创建可同时执行读写操作的实体访问上下文对象。
        /// </summary>
        /// <returns></returns>
        public IEntityContext CreateEntityContext()
        {
            return EntityContextFactory.CreateEntityContext();
        }

        /// <summary>
        /// 创建可同时执行读写或者只读操作的实体访问上下文对象。
        /// </summary>
        /// <returns></returns>
        public IEntityContext CreateEntityContext(bool readOnly)
        {
            return EntityContextFactory.CreateEntityContext(readOnly);
        }


        #region WithContext
        public int AddWithContext(IEntityContext entityContext, PO po)
        {
            this.EnsureParameterNotNull(entityContext, () => entityContext);
            this.EnsureParameterNotNull(po, () => po);

            entityContext.Add<PO>(po);

            return po.Id;
        }

        public int UpdateWithContext(IEntityContext entityContext, PO po)
        {
            this.EnsureParameterNotNull(entityContext, () => entityContext);
            this.EnsureParameterNotNull(po, () => po);

            entityContext.Update<PO>(po);
            return po.Id;
        }

        public PO GetByIdWithContext(IEntityContext entityContext, int id)
        {
            if (id <= 0)
            {
                return null;
            }

            if (entityContext == null)
            {
                throw new ArgumentNullException("entityContext");
            }

            IPredicate predicate = PredicateFactory.Equal<PO>(o => o.Id, id);
            return entityContext.Find<PO>(predicate).FirstOrDefault();
        }
        #endregion

        public virtual PO GetById(int id)
        {
            using (IEntityContext entityContext = CreateEntityContext(true))
            {
                return GetByIdWithContext(entityContext, id);
            }
        }

        public int Add(PO po)
        {
            this.EnsureParameterNotNull(po, () => po);
            using (IEntityContext entityContext = CreateEntityContext(false))
            {
                entityContext.Add<PO>(po);
            }
            return po.Id;
        }

        public int Update(PO po)
        {
            this.EnsureParameterNotNull(po, () => po);
            using (IEntityContext entityContext = CreateEntityContext(false))
            {
                entityContext.Update<PO>(po);
            }
            return po.Id;
        }

        internal void EnsureParameterNotNull<T>(T value, Expression<Func<T>> parameters)
        {
            if (value == null)
            {
                throw new ArgumentNullException(this.GetMemberName(parameters.Body));
            }
        }

        private string GetMemberName(Expression expression)
        {
            MemberExpression memberExpression = expression as MemberExpression;
            if (memberExpression == null)
            {
                return null;
            }
            else
            {
                string name = this.GetMemberName(memberExpression.Expression);
                return string.IsNullOrEmpty(name) ? memberExpression.Member.Name : name + "." + memberExpression.Member.Name;
            }
        }
    }
}
