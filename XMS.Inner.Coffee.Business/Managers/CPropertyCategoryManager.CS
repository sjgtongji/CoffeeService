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
    public class CPropertyCategoryManager : DataMapperManagerBase<CPropertyCategoryPO, CPropertyCategoryDTO>, IManagerBase<CPropertyCategoryPO>
    {
        public static readonly CPropertyCategoryManager Instance = new CPropertyCategoryManager();
        private CPropertyCategoryManager() { }
        public int Add(CPropertyCategoryPO po, string operatorName)
        {
            using (IEntityContext entityContext = CPropertyCategoryPOManager.Instance.CreateEntityContext())
            {
                return this.AddWithContext(entityContext, po, operatorName);
            }
        }
        public int AddOrUpdatetPropertyCategory(AddOrUpdatetPropertyCategoryDTO propertyCategory, string operatorName)
        {
            if (propertyCategory == null)
            {
                throw new ArgumentNullException("商品类别必须设定");
            }
            if (string.IsNullOrWhiteSpace(operatorName))
            {
                throw new ArgumentNullException("操作人必须设定");
            }
            CRestaurantPO restaurant = CRestaurantManager.Instance.GetById(propertyCategory.ResId);
            if (restaurant == null)
            {
                throw new BusinessException("此餐厅不存在");
            }
            CPropertyCategoryPO propertyCategoryPO = null;
            if (propertyCategory.Id > 0)
            {
                propertyCategoryPO = Instance.GetById(propertyCategory.Id);
                if (propertyCategoryPO == null)
                {
                    throw new BusinessException("商品类别不存在");
                }
            }
            else
            {
                propertyCategoryPO = new CPropertyCategoryPO();
            }
            propertyCategoryPO.ChineseName = propertyCategory.ChineseName;
            propertyCategoryPO.EnableMultiple = propertyCategory.EnableMultiple;
            propertyCategoryPO.EnglishName = propertyCategory.EnglishName;
            propertyCategoryPO.IsDelete = propertyCategory.IsDelete;
            propertyCategoryPO.ResUUID = restaurant.ResUUID;
            propertyCategoryPO.Sort = propertyCategory.Sort;
            propertyCategoryPO.Type = propertyCategory.Type;
            if (propertyCategoryPO.Id > 0)
            {
                return this.Update(propertyCategoryPO, operatorName);
            }
            return this.Add(propertyCategoryPO, operatorName);

        }
        public int AddWithContext(IEntityContext entityContext, CPropertyCategoryPO po, string operatorName)
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
            if (string.IsNullOrWhiteSpace(po.PropertyCategoryUUID))
            {
                po.PropertyCategoryUUID = Guid.NewGuid().ToString();
            }
            if (string.IsNullOrWhiteSpace(po.ChineseName))
            {
                throw new BusinessException("类别名称（中文）不能为空");
            }
            po.IsDelete = false;
            po.CreateTime = DateTime.Now;
            po.CreateName = operatorName;
            po.UpdateTime = DateTime.Now;
            po.UpdateName = operatorName;
            CPropertyCategoryPOManager.Instance.AddWithContext(entityContext, po);
            COperationLogManager.Instance.AddOperationLogWithContext(entityContext, po.Id, EnumOperationLogType.CPropertyCategory, EnumOperationLogAction.Add, null, po, operatorName);
            return po.Id;

        }
        public bool DeletePropertyCategory(int id, string operatorName)
        {
            if (id <= 0)
            {
                throw new BusinessException("商品类别ID有误");
            }
            CPropertyCategoryPO propertyCategoryPO = this.GetById(id);
            if (propertyCategoryPO == null)
            {
                throw new BusinessException("商品类别不存在");
            }
            propertyCategoryPO.IsDelete = true;
            this.Update(propertyCategoryPO, operatorName);
            return true;

        }
        public CPropertyCategoryPO GetById(int id)
        {
            return CPropertyCategoryPOManager.Instance.GetById(id);

        }
        public CPropertyCategoryPO GetByIdWithContext(IEntityContext entityContext, int id)
        {
            return CPropertyCategoryPOManager.Instance.GetByIdWithContext(entityContext, id);
        }
        public CPropertyCategoryDTO GetPropertyCategoryById(int id)
        {
            return base.PoToDto(this.GetById(id));
        }
        public List<CPropertyCategoryDTO> GetPropertyCategoryDTO(int resId)
        {
            if (resId <= 0)
            {
                throw new BusinessException("餐厅ID不正确");
            }
            CRestaurantPO restaurant = CRestaurantManager.Instance.GetById(resId);
            if (restaurant == null)
            {
                throw new BusinessException("餐厅不存在");
            }
            List<CPropertyCategoryDTO> list = new List<CPropertyCategoryDTO>();
            Core.Data.QueryResult<CPropertyCategoryPO> queryPropertyCategory = this.GetPropertyCategorys(restaurant.ResUUID, null, 1, 0x3e8);
            if ((queryPropertyCategory.TotalCount != 0) && ((queryPropertyCategory.Items != null) && (queryPropertyCategory.Items.Length > 0)))
            {
                list.AddRange(base.PoToDtoList(queryPropertyCategory.Items.ToList<CPropertyCategoryPO>()));
            }
            return list;

        }
        public Core.Data.QueryResult<CPropertyCategoryDTO> GetPropertyCategoryQuery(string resUUID, List<string> propertyCategoryUUIDs, int startIndex, int count)
        {
            if (string.IsNullOrWhiteSpace(resUUID))
            {
                throw new BusinessException("餐厅ID存在");
            }
            Core.Data.QueryResult<CPropertyCategoryDTO> listPropertyCategory = new Core.Data.QueryResult<CPropertyCategoryDTO>();
            Core.Data.QueryResult<CPropertyCategoryPO> queryResult = this.GetPropertyCategorys(resUUID, propertyCategoryUUIDs, startIndex, count);
            if (queryResult.TotalCount != 0)
            {
                listPropertyCategory.TotalCount = queryResult.TotalCount;
                if ((queryResult.Items != null) && (queryResult.Items.Length > 0))
                {
                    listPropertyCategory.Items = base.PoToDtoList(queryResult.Items.ToList<CPropertyCategoryPO>()).ToArray();
                }
            }
            return listPropertyCategory;
        }
        public Core.Data.QueryResult<CPropertyCategoryPO> GetPropertyCategorys(string resUUID, List<string> propertyCategoryUUIDs, int startIndex, int count)
        {
            Sort sort = PredicateFactory.Sort("Sort", "desc");
            IPredicate predicate = this.GetPropertyCategorysCondition(resUUID, propertyCategoryUUIDs);
            ISort[] sorts = new ISort[] { sort };
            return CPropertyCategoryPOManager.Instance.GetPropertyCategoryWithTotalCountByPredicate(predicate, startIndex, count, sorts);

        }
        private IPredicate GetPropertyCategorysCondition(string resUUID, List<string> propertyCategoryUUIDs)
        {
            List<IPredicate> predicates = new List<IPredicate>();

            #region 基本条件
            predicates.Add(PredicateFactory.Equal<CManagerPO>(t => t.IsDelete, false));
            if (!string.IsNullOrWhiteSpace(resUUID))
            {
                predicates.Add(PredicateFactory.Equal<CPropertyCategoryPO>(t => t.ResUUID, resUUID));
            }
            if ((propertyCategoryUUIDs != null) && (propertyCategoryUUIDs.Count > 0))
            {
                predicates.Add(PredicateFactory.In<CPropertyCategoryPO>(t => t.PropertyCategoryUUID, propertyCategoryUUIDs.ToArray()));
            }

            #endregion

            #region 数量条件
            #endregion
            return PredicateFactory.And(predicates.ToArray());
        }
        public int Update(CPropertyCategoryPO po, string operatorName)
        {
            using (IEntityContext entityContext = CPropertyCategoryPOManager.Instance.CreateEntityContext())
            {
                return this.UpdateWithContext(entityContext, po, operatorName);
            }

        }
        public int UpdateWithContext(IEntityContext entityContext, CPropertyCategoryPO po, string operatorName)
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
            if (string.IsNullOrWhiteSpace(po.PropertyCategoryUUID))
            {
                throw new BusinessException("属性类别UUID不能为空");
            }
            if (string.IsNullOrWhiteSpace(po.ChineseName))
            {
                throw new BusinessException("类别名称（中文）不能为空");
            }
            po.UpdateTime = DateTime.Now;
            po.UpdateName = operatorName;
            object oldObject = base.DtoToPo(base.PoToDto(this.GetByIdWithContext(entityContext, po.Id)));
            CPropertyCategoryPOManager.Instance.UpdateWithContext(entityContext, po);
            COperationLogManager.Instance.AddOperationLogWithContext(entityContext, po.Id, EnumOperationLogType.CPropertyCategory, EnumOperationLogAction.Update, oldObject, po, operatorName);
            return po.Id;

        }
    }



}