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
    public class COrderCommodityRelationManager: DataMapperManagerBase<COrderCommodityRelationPO, COrderCommodityRelationDTO>, IManagerBase<COrderCommodityRelationPO>
    {
        public static readonly COrderCommodityRelationManager Instance = new COrderCommodityRelationManager();
        private COrderCommodityRelationManager() { }

        public Core.Data.QueryResult<COrderCommodityRelationDTO> GetCOrderCommodityRelationDtos(int? id, List<string> orderUUID, List<string> commodityWithPropertyUUID, List<string> commodityUUID, int startIndex, int count)
        {
            Core.Data.QueryResult<COrderCommodityRelationDTO> restulDto = new Core.Data.QueryResult<COrderCommodityRelationDTO>();
            Core.Data.QueryResult<COrderCommodityRelationPO> orderCommodityRelationResult = GetCOrderCommodityRelations(id, orderUUID, commodityWithPropertyUUID, commodityUUID, startIndex, count);
            if (orderCommodityRelationResult == null || orderCommodityRelationResult.Items == null || orderCommodityRelationResult.Items.Length == 0)
                return restulDto;
            restulDto.TotalCount = orderCommodityRelationResult.TotalCount;
            restulDto.Items = PoToDtoList(orderCommodityRelationResult.Items.ToList()).ToArray();
            return restulDto;
        }

        public Core.Data.QueryResult<COrderCommodityRelationPO> GetCOrderCommodityRelations(int? id, List<string> orderUUID, List<string> commodityWithPropertyUUID, List<string> commodityUUID,  int startIndex, int count)
        {
            IPredicate predicate = this.GetOrderCommodityRelationsCondition(id, orderUUID, commodityWithPropertyUUID, commodityUUID);
            Core.Data.QueryResult<COrderCommodityRelationPO> resultRest = COrderCommodityRelationPOManager.Instance.GetOrderCommodityRelationWithTotalCountByPredicate(predicate, startIndex, count, null);
            return resultRest;
        }

        private IPredicate GetOrderCommodityRelationsCondition(int? id, List<string> orderUUID, List<string> commodityWithPropertyUUID, List<string> commodityUUID)
        {
            List<IPredicate> predicates = new List<IPredicate>();

            #region 基本条件
            predicates.Add(PredicateFactory.Equal<COrderCommodityRelationPO>(t => t.IsDelete, false));
            if (id.HasValue)
            {
                predicates.Add(PredicateFactory.Equal<COrderCommodityRelationPO>(t => t.Id, id.Value));
            }
            if (orderUUID != null && orderUUID.Count > 0)
            {
                predicates.Add(PredicateFactory.In<COrderCommodityRelationPO>(t => t.OrderUUID, orderUUID.ToArray()));
            }
            if (commodityWithPropertyUUID != null && commodityWithPropertyUUID.Count > 0)
            {
                predicates.Add(PredicateFactory.In<COrderCommodityRelationPO>(t => t.CommodityWithPropertyUUID, commodityWithPropertyUUID.ToArray()));
            }
            if (commodityUUID != null && commodityUUID.Count > 0)
            {
                predicates.Add(PredicateFactory.In<COrderCommodityRelationPO>(t => t.CommodityUUID, commodityUUID.ToArray()));
            }
            #endregion

            #region 数量条件
            #endregion
            return PredicateFactory.And(predicates.ToArray());
        }

        public int Add(COrderCommodityRelationPO po, string operatorName)
        {
            using (IEntityContext entityContext = COrderCommodityRelationPOManager.Instance.CreateEntityContext())
            {
                return AddWithContext(entityContext, po, operatorName);
            }
        }

        public int AddWithContext(IEntityContext entityContext, COrderCommodityRelationPO po, string operatorName)
        {
            if (entityContext == null)
                throw new ArgumentNullException("entityContext");
            if (po == null)
                throw new ArgumentNullException("对象不能为空");
            if (string.IsNullOrWhiteSpace(operatorName))
                throw new ArgumentNullException("操作人不能为空");
            if (po.Id > 0)
                throw new BusinessException("添加对象错误，Id不能大于0");
            if (string.IsNullOrWhiteSpace(po.OrderUUID))
                throw new BusinessException("订单UUID必须设定");
            if (string.IsNullOrWhiteSpace(po.CommodityWithPropertyUUID))
                throw new BusinessException("组合商品UUID必须设定");
            if (!po.Price.HasValue)
                throw new BusinessException("组合商品单价必须设定");

            po.IsDelete = false;
            po.CreateTime = DateTime.Now;
            po.CreateName = operatorName;
            po.UpdateTime = DateTime.Now;
            po.UpdateName = operatorName;

            COrderCommodityRelationPOManager.Instance.AddWithContext(entityContext, po);
            COperationLogManager.Instance.AddOperationLogWithContext(entityContext,po.Id, EnumOperationLogType.COrderCommodityRelation, EnumOperationLogAction.Add, null, po, operatorName);

            return po.Id;
        }

        public COrderCommodityRelationPO GetById(int id)
        {
            return COrderCommodityRelationPOManager.Instance.GetById(id);
        }

        public COrderCommodityRelationPO GetByIdWithContext(IEntityContext entityContext, int id)
        {
            return COrderCommodityRelationPOManager.Instance.GetByIdWithContext(entityContext, id);
        }

        public int Update(COrderCommodityRelationPO po, string operatorName)
        {
            using (IEntityContext entityContext = COrderCommodityRelationPOManager.Instance.CreateEntityContext())
            {
                return UpdateWithContext(entityContext, po, operatorName);
            }
        }

        public int UpdateWithContext(IEntityContext entityContext, COrderCommodityRelationPO po, string operatorName)
        {
            if (entityContext == null)
                throw new ArgumentNullException("entityContext");
            if (po == null)
                throw new ArgumentNullException("对象不能为空");
            if (string.IsNullOrWhiteSpace(operatorName))
                throw new ArgumentNullException("操作人不能为空");
            if (po.Id <= 0)
                throw new BusinessException("添加对象错误，Id不能小于0");
            if (string.IsNullOrWhiteSpace(po.OrderUUID))
                throw new BusinessException("订单UUID必须设定");
            if (string.IsNullOrWhiteSpace(po.CommodityWithPropertyUUID))
                throw new BusinessException("组合商品UUID必须设定");
            if (!po.Price.HasValue)
                throw new BusinessException("组合商品单价必须设定");

            po.UpdateTime = DateTime.Now;
            po.UpdateName = operatorName;

            object oldObject = this.DtoToPo(this.PoToDto(GetByIdWithContext(entityContext, po.Id)));
            COrderCommodityRelationPOManager.Instance.UpdateWithContext(entityContext, po);
            COperationLogManager.Instance.AddOperationLogWithContext(entityContext,po.Id, EnumOperationLogType.COrderCommodityRelation, EnumOperationLogAction.Update, oldObject, po, operatorName);

            return po.Id;
        }
    }
}
