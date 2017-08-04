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
    public class CCommodityPropertyManager:  DataMapperManagerBase<CCommodityPropertyPO, CCommodityPropertyDTO>, IManagerBase<CCommodityPropertyPO>
    {
        public static readonly CCommodityPropertyManager Instance = new CCommodityPropertyManager();
        private CCommodityPropertyManager() { }

        public bool DeleteCommodityProperty(int id, string operatorName)
        {
            if (id <= 0)
                throw new BusinessException("商品属性ID不能为空");
            CCommodityPropertyPO commodityPropertyPO = GetById(id);
            if (commodityPropertyPO == null)
                throw new BusinessException("此商品属性不存在");
            commodityPropertyPO.IsDelete = true;
            Update(commodityPropertyPO, operatorName);
            return true;
        }

        public int AddOrUpdateCommodityProperty(AddOrUpdateCommodityPropertyDTO commodityProperty, string operatorName)
        {
            if (commodityProperty == null)
                throw new BusinessException("商品对象未设定");
            if (string.IsNullOrWhiteSpace(operatorName))
                throw new BusinessException("操作人未设定");
            if (string.IsNullOrWhiteSpace(commodityProperty.PropertyCategoryUUID))
                throw new BusinessException("请设定属性类别");
            if (string.IsNullOrWhiteSpace(commodityProperty.CommodityUUID))
                throw new BusinessException("请设定商品");

            CRestaurantPO restaurant = CRestaurantManager.Instance.GetById(commodityProperty.ResId);
            if (restaurant == null)
                throw new BusinessException("此餐厅不存在");

            Core.Data.QueryResult<CCommodityPO> commodityResult = CCommodityManager.Instance.GetCCommoditys(null, null, commodityProperty.CommodityUUID, null, null, 1, 1, null);
            if (commodityResult == null || commodityResult.Items == null || commodityResult.Items.Length == 0)
                throw new BusinessException("此商品不存在");

            Core.Data.QueryResult<CPropertyCategoryPO> propertyCategoryResult = CPropertyCategoryManager.Instance.GetPropertyCategorys(null, new List<string> { commodityProperty.PropertyCategoryUUID }, 1, 1);
            if (propertyCategoryResult == null || propertyCategoryResult.Items == null || propertyCategoryResult.Items.Length == 0)
                throw new BusinessException("此属性类别不存在");

            CCommodityPropertyPO commodityPropertyPO = null;
            if (commodityProperty.Id > 0)
            {
                commodityPropertyPO = CCommodityPropertyManager.Instance.GetById(commodityProperty.Id);
                if (commodityPropertyPO == null)
                    throw new BusinessException("商品不存在");
            }
            else
            {
                commodityPropertyPO = new CCommodityPropertyPO();
            }

            commodityPropertyPO.AddPrice = commodityProperty.AddPrice;
            commodityPropertyPO.ChineseName = commodityProperty.ChineseName;
            commodityPropertyPO.CommodityUUID = commodityProperty.CommodityUUID;
            commodityPropertyPO.EnglishName = commodityProperty.EnglishName;
            commodityPropertyPO.IsDelete = commodityProperty.IsDelete;
            commodityPropertyPO.PropertyCategoryUUID = commodityProperty.PropertyCategoryUUID;
            commodityPropertyPO.ResUUID = restaurant.ResUUID;
            commodityPropertyPO.Sort = commodityProperty.Sort;

            if (commodityPropertyPO.Id > 0)
                return Update(commodityPropertyPO, operatorName);
            else
                return Add(commodityPropertyPO, operatorName);
        }

        public Core.Data.QueryResult<CCommodityPropertyDTO> GetCommodityPropertyDTOs(List<int> id, string resUUID, string commodityPropertyUUID, List<string> commodityUUIDs, List<string> propertyCategoryUUIDs, int startIndex, int count)
        {
            Core.Data.QueryResult<CCommodityPropertyDTO> commodityPropertyResult = new Core.Data.QueryResult<CCommodityPropertyDTO>();
            Core.Data.QueryResult<CCommodityPropertyPO> queryResult = GetCommodityPropertys(id, resUUID, commodityPropertyUUID, commodityUUIDs, propertyCategoryUUIDs, startIndex, count);
            if (queryResult.TotalCount == 0)
                return commodityPropertyResult;
            commodityPropertyResult.TotalCount = queryResult.TotalCount;
            if (queryResult.Items != null && queryResult.Items.Length > 0)
                commodityPropertyResult.Items = PoToDtoList(queryResult.Items.ToList()).ToArray();

            return commodityPropertyResult;
        }

        public Core.Data.QueryResult<CCommodityPropertyPO> GetCommodityPropertys(List<int> id, string resUUID, string commodityPropertyUUID, List<string> commodityUUIDs, List<string> propertyCategoryUUIDs, int startIndex, int count)
        {
            Sort sort = PredicateFactory.Sort("Sort", "desc"); ;

            IPredicate predicate = this.GetCommodityPropertysCondition(id, resUUID, commodityPropertyUUID, commodityUUIDs, propertyCategoryUUIDs);
            Core.Data.QueryResult<CCommodityPropertyPO> resultRest = CCommodityPropertyPOManager.Instance.GetCommodityPropertyWithTotalCountByPredicate(predicate, startIndex, count, sort);
            return resultRest;
        }

        private IPredicate GetCommodityPropertysCondition(List<int> id, string resUUID, string commodityPropertyUUID, List<string> commodityUUIDs, List<string> propertyCategoryUUIDs)
        {
            List<IPredicate> predicates = new List<IPredicate>();

            #region 基本条件
            predicates.Add(PredicateFactory.Equal<CCommodityPropertyPO>(t => t.IsDelete, false));
            if (id != null && id.Count > 0)
                predicates.Add(PredicateFactory.In<CCommodityPropertyPO>(t => t.Id, id.ToArray()));
            if (!string.IsNullOrWhiteSpace(resUUID))
            {
                predicates.Add(PredicateFactory.Equal<CCommodityPropertyPO>(t => t.ResUUID, resUUID));
            }
            if (!string.IsNullOrWhiteSpace(commodityPropertyUUID))
            {
                predicates.Add(PredicateFactory.Equal<CCommodityPropertyPO>(t => t.CommodityPropertyUUID, commodityPropertyUUID));
            }

            if(commodityUUIDs != null && commodityUUIDs.Count > 0)
                predicates.Add(PredicateFactory.In<CCommodityPropertyPO>(t => t.CommodityUUID, commodityUUIDs.ToArray()));

            if (propertyCategoryUUIDs != null && propertyCategoryUUIDs.Count > 0)
                predicates.Add(PredicateFactory.In<CCommodityPropertyPO>(t => t.PropertyCategoryUUID, propertyCategoryUUIDs.ToArray()));
            #endregion

            #region 数量条件
            #endregion
            return PredicateFactory.And(predicates.ToArray());
        }

        public int Add(CCommodityPropertyPO po, string operatorName)
        {
            using (IEntityContext entityContext = CCommodityPropertyPOManager.Instance.CreateEntityContext())
            {
                return AddWithContext(entityContext, po, operatorName);
            }
        }

        public int AddWithContext(IEntityContext entityContext, CCommodityPropertyPO po, string operatorName)
        {
            if (entityContext == null)
                throw new ArgumentNullException("entityContext");
            if (po == null)
                throw new ArgumentNullException("对象不能为空");
            if (string.IsNullOrWhiteSpace(operatorName))
                throw new ArgumentNullException("操作人不能为空");
            if (po.Id > 0)
                throw new BusinessException("添加对象错误，Id不能大于0");
            if (string.IsNullOrWhiteSpace(po.CommodityPropertyUUID))
                po.CommodityPropertyUUID = System.Guid.NewGuid().ToString();
            if (string.IsNullOrWhiteSpace(po.ChineseName))
                throw new BusinessException("属性名称（中文）不能为空");
            if (string.IsNullOrWhiteSpace(operatorName))
                throw new BusinessException("操作人不能为空");
            if (!po.AddPrice.HasValue)
                po.AddPrice = 0;
            if (string.IsNullOrWhiteSpace(po.CommodityUUID))
                throw new BusinessException("商品UUID必须设定");
            if (string.IsNullOrWhiteSpace(po.PropertyCategoryUUID))
                throw new BusinessException("属性类别UUID必须设定");

            po.IsDelete = false;
            po.CreateTime = DateTime.Now;
            po.CreateName = operatorName;
            po.UpdateTime = DateTime.Now;
            po.UpdateName = operatorName;

            CCommodityPropertyPOManager.Instance.AddWithContext(entityContext, po);
            COperationLogManager.Instance.AddOperationLogWithContext(entityContext,po.Id, EnumOperationLogType.CCommodityProperty, EnumOperationLogAction.Add, null, po, operatorName);

            return po.Id;
        }

        public CCommodityPropertyPO GetById(int id)
        {
            return CCommodityPropertyPOManager.Instance.GetById(id);
        }

        public CCommodityPropertyDTO GetCommodityPropertyById(int id)
        {
            return PoToDto(GetById(id));
        }

        public CCommodityPropertyPO GetByIdWithContext(IEntityContext entityContext, int id)
        {
            return CCommodityPropertyPOManager.Instance.GetByIdWithContext(entityContext, id);
        }

        public int Update(CCommodityPropertyPO po, string operatorName)
        {
            using (IEntityContext entityContext = CCommodityPropertyPOManager.Instance.CreateEntityContext())
            {
                return UpdateWithContext(entityContext, po, operatorName);
            }
        }

        public int UpdateWithContext(IEntityContext entityContext, CCommodityPropertyPO po, string operatorName)
        {
            if (entityContext == null)
                throw new ArgumentNullException("entityContext");
            if (po == null)
                throw new ArgumentNullException("对象不能为空");
            if (string.IsNullOrWhiteSpace(operatorName))
                throw new ArgumentNullException("操作人不能为空");
            if (po.Id <= 0)
                throw new BusinessException("添加对象错误，Id不能小于0");
            if (string.IsNullOrWhiteSpace(po.CommodityPropertyUUID))
                throw new BusinessException("UUID不能为空");
            if (string.IsNullOrWhiteSpace(po.ChineseName))
                throw new BusinessException("属性名称（中文）不能为空");
            if (string.IsNullOrWhiteSpace(operatorName))
                throw new BusinessException("操作人不能为空");
            if (!po.AddPrice.HasValue)
                po.AddPrice = 0;
            if (string.IsNullOrWhiteSpace(po.CommodityUUID))
                throw new BusinessException("商品UUID必须设定");
            if (string.IsNullOrWhiteSpace(po.PropertyCategoryUUID))
                throw new BusinessException("属性类别UUID必须设定");

            po.UpdateTime = DateTime.Now;
            po.UpdateName = operatorName;

            object oldObject = this.DtoToPo(this.PoToDto(GetByIdWithContext(entityContext, po.Id)));
            CCommodityPropertyPOManager.Instance.UpdateWithContext(entityContext, po);
            COperationLogManager.Instance.AddOperationLogWithContext(entityContext,po.Id, EnumOperationLogType.CCommodityProperty, EnumOperationLogAction.Update, oldObject, po, operatorName);

            return po.Id;
        }
    }
}
