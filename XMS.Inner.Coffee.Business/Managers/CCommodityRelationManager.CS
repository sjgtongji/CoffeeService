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
    public class CCommodityRelationManager : DataMapperManagerBase<CCommodityRelationPO, CCommodityRelationDTO>, IManagerBase<CCommodityRelationPO>
    {
        public static readonly CCommodityRelationManager Instance = new CCommodityRelationManager();
        private CCommodityRelationManager() { }
        public int Add(CCommodityRelationPO po, string operatorName)
        {
            using (IEntityContext entityContext = CCommodityRelationPOManager.Instance.CreateEntityContext())
            {
                return this.AddWithContext(entityContext, po, operatorName);
            }
        }
        public int AddWithContext(IEntityContext entityContext, CCommodityRelationPO po, string operatorName)
        {
            if (entityContext == null)
            {
                throw new ArgumentNullException("entityContext");
            }
            if (po == null)
            {
                throw new ArgumentNullException("对象不能为空");
            }
            if (string.IsNullOrWhiteSpace(operatorName))
            {
                throw new ArgumentNullException("操作人不能为空");
            }
            if (po.Id > 0)
            {
                throw new BusinessException("添加对象错误，Id不能大于0");
            }
            if (string.IsNullOrWhiteSpace(po.CommodityUUID))
            {
                throw new BusinessException("商品UUID必须设定");
            }
            if (string.IsNullOrWhiteSpace(po.CommodityWithPropertyUUID))
            {
                throw new BusinessException("组合商品UUID必须设定");
            }
            po.IsDelete = false;
            po.CreateTime = DateTime.Now;
            po.CreateName = operatorName;
            po.UpdateTime = DateTime.Now;
            po.UpdateName = operatorName;
            CCommodityRelationPOManager.Instance.AddWithContext(entityContext, po);
            COperationLogManager.Instance.AddOperationLogWithContext(entityContext, po.Id, EnumOperationLogType.CCommodityRelation, EnumOperationLogAction.Add, null, po, operatorName);
            return po.Id;

        }
        public CCommodityRelationPO GetById(int id)
        {
            return CCommodityRelationPOManager.Instance.GetById(id);
        }
        public CCommodityRelationPO GetByIdWithContext(IEntityContext entityContext, int id)
        {
            return CCommodityRelationPOManager.Instance.GetByIdWithContext(entityContext, id);

        }
        public int Update(CCommodityRelationPO po, string operatorName)
        {
            using (IEntityContext entityContext = CCommodityRelationPOManager.Instance.CreateEntityContext())
            {
                return this.UpdateWithContext(entityContext, po, operatorName);
            }
        }
        public int UpdateWithContext(IEntityContext entityContext, CCommodityRelationPO po, string operatorName)
        {
            if (entityContext == null)
            {
                throw new ArgumentNullException("entityContext");
            }
            if (po == null)
            {
                throw new ArgumentNullException("对象不能为空");
            }
            if (string.IsNullOrWhiteSpace(operatorName))
            {
                throw new ArgumentNullException("操作人不能为空");
            }
            if (po.Id <= 0)
            {
                throw new BusinessException("添加对象错误，Id不能小于0");
            }
            if (string.IsNullOrWhiteSpace(po.CommodityUUID))
            {
                throw new BusinessException("商品UUID必须设定");
            }
            if (string.IsNullOrWhiteSpace(po.CommodityWithPropertyUUID))
            {
                throw new BusinessException("组合商品UUID必须设定");
            }
            po.UpdateTime = DateTime.Now;
            po.UpdateName = operatorName;
            object oldObject = base.DtoToPo(base.PoToDto(this.GetByIdWithContext(entityContext, po.Id)));
            CCommodityRelationPOManager.Instance.UpdateWithContext(entityContext, po);
            COperationLogManager.Instance.AddOperationLogWithContext(entityContext, po.Id, EnumOperationLogType.CCommodityRelation, EnumOperationLogAction.Update, oldObject, po, operatorName);
            return po.Id;

        }
    }
}