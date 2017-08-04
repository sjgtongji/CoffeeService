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
    public class CCommodityWithPropertyManager: DataMapperManagerBase<CCommodityWithPropertyPO, CCommodityWithPropertyDTO>, IManagerBase<CCommodityWithPropertyPO>
    {
        public static readonly CCommodityWithPropertyManager Instance = new CCommodityWithPropertyManager();
        private CCommodityWithPropertyManager() { }

        public int Add(CCommodityWithPropertyPO po, string operatorName)
        {
            using (IEntityContext entityContext = CCommodityWithPropertyPOManager.Instance.CreateEntityContext())
            {
                return AddWithContext(entityContext, po, operatorName);
            }
        }

        public int AddWithContext(IEntityContext entityContext, CCommodityWithPropertyPO po, string operatorName)
        {
            if (entityContext == null)
                throw new ArgumentNullException("entityContext");
            if (po == null)
                throw new ArgumentNullException("对象不能为空");
            if (string.IsNullOrWhiteSpace(operatorName))
                throw new ArgumentNullException("操作人不能为空");
            if (po.Id > 0)
                throw new BusinessException("添加对象错误，Id不能大于0");
            if (string.IsNullOrWhiteSpace(po.CommodityWithPropertyUUID))
                po.CommodityWithPropertyUUID = System.Guid.NewGuid().ToString();
            if (string.IsNullOrWhiteSpace(po.CommodityUUID))
                throw new BusinessException("商品UUID必须设定");
            //if (string.IsNullOrWhiteSpace(po.PropertyCategoryUUID))
            //    throw new BusinessException("属性类别UUID必须设定");
            //if (string.IsNullOrWhiteSpace(po.CommodityPropertyUUID))
            //    throw new BusinessException("品属性UUID必须设定");

            po.IsDelete = false;
            po.CreateTime = DateTime.Now;
            po.CreateName = operatorName;
            po.UpdateTime = DateTime.Now;
            po.UpdateName = operatorName;

            CCommodityWithPropertyPOManager.Instance.AddWithContext(entityContext, po);
            COperationLogManager.Instance.AddOperationLogWithContext(entityContext,po.Id, EnumOperationLogType.CCommodityWithProperty, EnumOperationLogAction.Add, null, po, operatorName);

            return po.Id;
        }

        public CCommodityWithPropertyPO GetById(int id)
        {
            return CCommodityWithPropertyPOManager.Instance.GetById(id);
        }

        public CCommodityWithPropertyPO GetByIdWithContext(IEntityContext entityContext, int id)
        {
            return CCommodityWithPropertyPOManager.Instance.GetByIdWithContext(entityContext, id);
        }

        public int Update(CCommodityWithPropertyPO po, string operatorName)
        {
            using (IEntityContext entityContext = CCommodityWithPropertyPOManager.Instance.CreateEntityContext())
            {
                return UpdateWithContext(entityContext, po, operatorName);
            }
        }

        public int UpdateWithContext(IEntityContext entityContext, CCommodityWithPropertyPO po, string operatorName)
        {
            if (entityContext == null)
                throw new ArgumentNullException("entityContext");
            if (po == null)
                throw new ArgumentNullException("对象不能为空");
            if (string.IsNullOrWhiteSpace(operatorName))
                throw new ArgumentNullException("操作人不能为空");
            if (po.Id <= 0)
                throw new BusinessException("添加对象错误，Id不能小于0");
            if (string.IsNullOrWhiteSpace(po.CommodityWithPropertyUUID))
                throw new BusinessException("UUID必须设定");
            if (string.IsNullOrWhiteSpace(po.CommodityUUID))
                throw new BusinessException("商品UUID必须设定");
            //if (string.IsNullOrWhiteSpace(po.PropertyCategoryUUID))
            //    throw new BusinessException("属性类别UUID必须设定");
            //if (string.IsNullOrWhiteSpace(po.CommodityPropertyUUID))
            //    throw new BusinessException("品属性UUID必须设定");

            po.UpdateTime = DateTime.Now;
            po.UpdateName = operatorName;

            object oldObject = this.DtoToPo(this.PoToDto(GetByIdWithContext(entityContext, po.Id)));
            CCommodityWithPropertyPOManager.Instance.UpdateWithContext(entityContext, po);
            COperationLogManager.Instance.AddOperationLogWithContext(entityContext,po.Id, EnumOperationLogType.CCommodityWithProperty, EnumOperationLogAction.Update, oldObject, po, operatorName);

            return po.Id;
        }
    }
}
