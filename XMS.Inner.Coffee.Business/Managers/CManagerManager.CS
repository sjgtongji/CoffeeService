using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMS.Core;
using XMS.Core.Data;
using XMS.Inner.Coffee.Data;
using XMS.Inner.Coffee.Model;

namespace XMS.Inner.Coffee.Business
{
    public class CManagerManager: DataMapperManagerBase<CManagerPO, CManagerDTO>, IManagerBase<CManagerPO>
    {
        public static readonly CManagerManager Instance = new CManagerManager();
        private CManagerManager() { }

        public Core.Data.QueryResult<CManagerPO> GetManagers(int? id, string name, string passWord, int? userLevel, int startIndex, int count)
        {
            IPredicate predicate = this.GetManagersCondition(id, name, passWord, userLevel);
            Core.Data.QueryResult<CManagerPO> resultRest = CManagerPOManager.Instance.GetManagerWithTotalCountByPredicate(predicate, startIndex, count, null);
            return resultRest;
        }

        private IPredicate GetManagersCondition(int? id, string name, string passWord, int? userLevel)
        {
            List<IPredicate> predicates = new List<IPredicate>();

            #region 基本条件
            predicates.Add(PredicateFactory.Equal<CManagerPO>(t => t.IsDelete, false));
            if (id.HasValue)
                predicates.Add(PredicateFactory.Equal<CManagerPO>(t => t.Id, id.Value));
            if (!string.IsNullOrWhiteSpace(name))
            {
                predicates.Add(PredicateFactory.Equal<CManagerPO>(t => t.Name, name));
            }
            if (!string.IsNullOrWhiteSpace(passWord))
            {
                predicates.Add(PredicateFactory.Equal<CManagerPO>(t => t.PassWord, passWord));
            }
            if (userLevel.HasValue)
            {
                predicates.Add(PredicateFactory.Equal<CManagerPO>(t => t.UserLevel, userLevel.Value));
            }
            #endregion

            #region 数量条件
            #endregion
            return PredicateFactory.And(predicates.ToArray());
        }


        public int Add(CManagerPO po, string operatorName)
        {
            using (IEntityContext entityContext = CManagerPOManager.Instance.CreateEntityContext())
            {
                return AddWithContext(entityContext, po, operatorName);
            }
        }

        public int AddWithContext(IEntityContext entityContext, CManagerPO po, string operatorName)
        {
            if (entityContext == null)
                throw new ArgumentNullException("entityContext");
            if (po == null)
                throw new ArgumentNullException("对象不能为空");
            if (string.IsNullOrWhiteSpace(operatorName))
                throw new ArgumentNullException("操作人不能为空");
            if (po.Id > 0)
                throw new BusinessException("添加对象错误，Id不能大于0");
           
            po.IsDelete = false;
            po.CreateTime = DateTime.Now;
            po.CreateName = operatorName;
            po.UpdateTime = DateTime.Now;
            po.UpdateName = operatorName;

            CManagerPOManager.Instance.AddWithContext(entityContext, po);
            COperationLogManager.Instance.AddOperationLogWithContext(entityContext,po.Id, EnumOperationLogType.CManager, EnumOperationLogAction.Add, null, po, operatorName);

            return po.Id;
        }

        public CManagerPO GetById(int id)
        {
            return CManagerPOManager.Instance.GetById(id);
        }

        public CManagerPO GetByIdWithContext(IEntityContext entityContext, int id)
        {
            return CManagerPOManager.Instance.GetByIdWithContext(entityContext, id);
        }

        public int Update(CManagerPO po, string operatorName)
        {
            using (IEntityContext entityContext = CManagerPOManager.Instance.CreateEntityContext())
            {
                return UpdateWithContext(entityContext, po, operatorName);
            }
        }

        public int UpdateWithContext(IEntityContext entityContext, CManagerPO po, string operatorName)
        {
            if (entityContext == null)
                throw new ArgumentNullException("entityContext");
            if (po == null)
                throw new ArgumentNullException("对象不能为空");
            if (string.IsNullOrWhiteSpace(operatorName))
                throw new ArgumentNullException("操作人不能为空");
            if (po.Id <= 0)
                throw new BusinessException("添加对象错误，Id不能小于0");

            po.UpdateTime = DateTime.Now;
            po.UpdateName = operatorName;

            object oldObject = this.DtoToPo(this.PoToDto(GetByIdWithContext(entityContext, po.Id)));
            CManagerPOManager.Instance.UpdateWithContext(entityContext, po);
            COperationLogManager.Instance.AddOperationLogWithContext(entityContext,po.Id, EnumOperationLogType.CManager, EnumOperationLogAction.Update, oldObject, po, operatorName);

            return po.Id;
        }
    }
}
