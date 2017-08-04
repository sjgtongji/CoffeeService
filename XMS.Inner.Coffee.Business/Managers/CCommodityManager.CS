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
    public class CCommodityManager : DataMapperManagerBase<CCommodityPO, CCommodityDTO>, IManagerBase<CCommodityPO>
    {
        // Fields
        public static readonly CCommodityManager Instance = new CCommodityManager();
        private CCommodityManager() { }

        public int Add(CCommodityPO po, string operatorName)
        {
            using (IEntityContext entityContext = CCommodityPOManager.Instance.CreateEntityContext())
            {
                return this.AddWithContext(entityContext, po, operatorName);
            }
        }
        public int AddOrUpdateCommodity(AddOrUpdateCommodityDTO commodity, string operatorName)
        {
            if (commodity == null)
            {
                throw new BusinessException("商品对象未设定");
            }
            if (string.IsNullOrWhiteSpace(operatorName))
            {
                throw new BusinessException("操作人未设定");
            }
            CRestaurantPO restaurant = CRestaurantManager.Instance.GetById(commodity.ResId);
            if (restaurant == null)
            {
                throw new BusinessException("此餐厅不存在");
            }
            List<string> commodityCategoryUUIDs = new List<string> {
        commodity.CommodityCategoryUUID
    };
            Core.Data.QueryResult<CCommodityCategoryPO> commodityCategoryResult = CCommodityCategoryManager.Instance.GetCommodityCategorys(restaurant.ResUUID, commodityCategoryUUIDs, 1, 1);
            if (((commodityCategoryResult == null) || (commodityCategoryResult.Items == null)) || (commodityCategoryResult.Items.Length == 0))
            {
                throw new BusinessException("商品类别不存在");
            }
            CCommodityCategoryPO commodityCategoryPO = commodityCategoryResult.Items[0];
            CCommodityPO commodityPO = null;
            if (commodity.Id > 0)
            {
                commodityPO = Instance.GetById(commodity.Id);
                if (commodityPO == null)
                {
                    throw new BusinessException("商品不存在");
                }
            }
            else
            {
                commodityPO = new CCommodityPO();
            }
            commodityPO.ChineseName = commodity.ChineseName;
            commodityPO.ResUUID = restaurant.ResUUID;
            commodityPO.CommodityCategoryUUID = commodityCategoryPO.CommodityCategoryUUID;
            commodityPO.EnglishName = commodity.EnglishName;
            commodityPO.IsDelete = commodity.IsDelete;
            commodityPO.PicPath = commodity.PicPath;
            commodityPO.PreferentialFlag = commodity.PreferentialFlag;
            commodityPO.PreferentialProportion = commodity.PreferentialProportion;
            commodityPO.PreferentialQuantity = commodity.PreferentialQuantity;
            commodityPO.Price = new decimal?(commodity.Price);
            commodityPO.Remark = commodity.Remark;
            commodityPO.ShowRemark = commodity.ShowRemark;
            commodityPO.Sort = new int?(commodity.Sort);
            commodityPO.State = commodity.State;
            commodityPO.Type = commodity.Type;
            if (commodityPO.Id > 0)
            {
                return this.Update(commodityPO, operatorName);
            }
            return this.Add(commodityPO, operatorName);

        }
        public int AddWithContext(IEntityContext entityContext, CCommodityPO po, string operatorName)
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
                po.CommodityUUID = Guid.NewGuid().ToString();
            }
            if (string.IsNullOrWhiteSpace(po.ChineseName))
            {
                throw new BusinessException("商品名称（中文）不能为空");
            }
            if (!po.Price.HasValue)
            {
                po.Price = new decimal();
            }
            if (!po.Sort.HasValue)
            {
                po.Sort = 0;
            }
            if (string.IsNullOrWhiteSpace(po.CommodityCategoryUUID))
            {
                throw new BusinessException("商品类别UUID必须设定");
            }
            po.IsDelete = false;
            po.CreateTime = DateTime.Now;
            po.CreateName = operatorName;
            po.UpdateTime = DateTime.Now;
            po.UpdateName = operatorName;
            CCommodityPOManager.Instance.AddWithContext(entityContext, po);
            COperationLogManager.Instance.AddOperationLogWithContext(entityContext, po.Id, EnumOperationLogType.CCommodity, EnumOperationLogAction.Add, null, po, operatorName);
            return po.Id;

        }
        public bool DeleteCommodity(int id, string operatorName)
        {
            if (id <= 0)
            {
                throw new BusinessException("商品ID不能为空");
            }
            CCommodityPO commodityPO = this.GetById(id);
            if (commodityPO == null)
            {
                throw new BusinessException("此商品不存在");
            }
            commodityPO.IsDelete = true;
            this.Update(commodityPO, operatorName);
            return true;

        }
        public CCommodityPO GetById(int id)
        {
            return CCommodityPOManager.Instance.GetById(id);
        }
        public CCommodityPO GetByIdWithContext(IEntityContext entityContext, int id)
        {
            return CCommodityPOManager.Instance.GetByIdWithContext(entityContext, id);
        }
        public Core.Data.QueryResult<CCommodityDTO> GetCCommodityDTOs(int? id, string resUUID, string commodityUUID, List<string> commodityCategoryUUIDs, List<int> states, int startIndex, int count, string name)
        {
            Core.Data.QueryResult<CCommodityDTO> commodityResult = new Core.Data.QueryResult<CCommodityDTO>();
            Core.Data.QueryResult<CCommodityPO> queryResult = this.GetCCommoditys(id, resUUID, commodityUUID, commodityCategoryUUIDs, states, startIndex, count, name);
            if (queryResult.TotalCount != 0)
            {
                commodityResult.TotalCount = queryResult.TotalCount;
                if ((queryResult.Items != null) && (queryResult.Items.Length > 0))
                {
                    commodityResult.Items = base.PoToDtoList(queryResult.Items.ToList<CCommodityPO>()).ToArray();
                }
            }
            return commodityResult;

        }
        public Core.Data.QueryResult<CCommodityPO> GetCCommoditys(int? id, string resUUID, string commodityUUID, List<string> commodityCategoryUUIDs, List<int> states, int startIndex, int count, string name)
        {
            Sort sort = PredicateFactory.Sort("Sort", "desc");
            IPredicate predicate = this.GetCCommoditysCondition(id, resUUID, commodityUUID, commodityCategoryUUIDs, states, name);
            ISort[] sorts = new ISort[] { sort };
            return CCommodityPOManager.Instance.GetCCommodityWithTotalCountByPredicate(predicate, startIndex, count, sorts);
        }
        private IPredicate GetCCommoditysCondition(int? id, string resUUID, string commodityUUID, List<string> commodityCategoryUUIDs, List<int> states, string name)
        {
            List<IPredicate> predicates = new List<IPredicate>();
            predicates.Add(PredicateFactory.Equal<CCommodityPropertyPO>(t => t.IsDelete, false));

            if (id.HasValue)
            {
                predicates.Add(PredicateFactory.Equal<CCommodityPO>(t => t.Id, id.Value));
            }
            if (!string.IsNullOrWhiteSpace(resUUID))
            {
                predicates.Add(PredicateFactory.Equal<CCommodityPO>(t => t.ResUUID, resUUID));
            }
            if (!string.IsNullOrWhiteSpace(commodityUUID))
            {
                predicates.Add(PredicateFactory.Equal<CCommodityPO>(t => t.CommodityUUID, commodityUUID));
            }
            if ((commodityCategoryUUIDs != null) && (commodityCategoryUUIDs.Count > 0))
            {
                predicates.Add(PredicateFactory.In<CCommodityPO>(t => t.CommodityCategoryUUID, commodityCategoryUUIDs.ToArray()));
            }
            if ((states != null) && (states.Count > 0))
            {
                predicates.Add(PredicateFactory.In<CCommodityPO>(t => t.State, states.ToArray()));
            }
            if (!string.IsNullOrWhiteSpace(name))
            {
                IPredicate[] predicateArray1 = new IPredicate[2];
                predicateArray1[0] = PredicateFactory.Like<CCommodityPO>(t => t.ChineseName, "%" + name.ToSafeSQLLike() + "%");
                predicateArray1[1] = PredicateFactory.Like<CCommodityPO>(t => t.EnglishName, "%" + name.ToSafeSQLLike() + "%");
                predicates.Add(PredicateFactory.Or(predicateArray1));
            }
            return PredicateFactory.And(predicates.ToArray());

        }
        public CCommodityDTO GetCommodityById(int id)
        {
            return base.PoToDto(this.GetById(id));
        }
        public bool SetCommodityState(int id, int state, string operatorName)
        {
            if (id <= 0)
            {
                throw new BusinessException("商品ID不能为空");
            }
            if (((state != 0) && (state != 1)) && (state != 2))
            {
                throw new BusinessException("商品状态不正确");
            }
            CCommodityPO commodityPO = this.GetById(id);
            if (commodityPO == null)
            {
                throw new BusinessException("此商品不存在");
            }
            commodityPO.State = state;
            this.Update(commodityPO, operatorName);
            return true;
        }
        public int Update(CCommodityPO po, string operatorName)
        {
            using (IEntityContext entityContext = CCommodityPOManager.Instance.CreateEntityContext())
            {
                return this.UpdateWithContext(entityContext, po, operatorName);
            }
        }
        public int UpdateWithContext(IEntityContext entityContext, CCommodityPO po, string operatorName)
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
                throw new BusinessException("商品UUID不能为空");
            }
            if (string.IsNullOrWhiteSpace(po.ChineseName))
            {
                throw new BusinessException("商品名称（中文）不能为空");
            }
            if (!po.Price.HasValue)
            {
                po.Price = new decimal();
            }
            if (!po.Sort.HasValue)
            {
                po.Sort = 0;
            }
            if (string.IsNullOrWhiteSpace(po.CommodityCategoryUUID))
            {
                throw new BusinessException("商品类别UUID必须设定");
            }
            po.CreateTime = DateTime.Now;
            po.CreateName = operatorName;
            po.UpdateTime = DateTime.Now;
            po.UpdateName = operatorName;
            object oldObject = base.DtoToPo(base.PoToDto(this.GetByIdWithContext(entityContext, po.Id)));
            CCommodityPOManager.Instance.UpdateWithContext(entityContext, po);
            COperationLogManager.Instance.AddOperationLogWithContext(entityContext, po.Id, EnumOperationLogType.CCommodity, EnumOperationLogAction.Update, oldObject, po, operatorName);
            return po.Id;
        }
    }
}