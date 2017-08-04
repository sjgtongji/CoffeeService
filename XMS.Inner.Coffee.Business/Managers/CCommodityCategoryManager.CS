using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using XMS.Core;
using XMS.Core.Data;
using XMS.Inner.Coffee.Data;
using XMS.Inner.Coffee.Model;

namespace XMS.Inner.Coffee.Business
{
    public class CCommodityCategoryManager : DataMapperManagerBase<CCommodityCategoryPO, CCommodityCategoryDTO>, IManagerBase<CCommodityCategoryPO>
    {
        public static readonly CCommodityCategoryManager Instance = new CCommodityCategoryManager();
        private CCommodityCategoryManager() { }

        public int Add(CCommodityCategoryPO po, string operatorName)
        {
            using (IEntityContext entityContext = CCommodityCategoryPOManager.Instance.CreateEntityContext())
            {
                return this.AddWithContext(entityContext, po, operatorName);
            }
        }
        public int AddOrUpdateCommodityCategory(AddOrUpdateCommodityCategoryDTO commodityCategory, string operatorName)
        {
            if (commodityCategory == null)
            {
                throw new ArgumentNullException("商品类别必须设定");
            }
            if (string.IsNullOrWhiteSpace(operatorName))
            {
                throw new ArgumentNullException("操作人必须设定");
            }
            CRestaurantPO restaurant = CRestaurantManager.Instance.GetById(commodityCategory.ResId);
            if (restaurant == null)
            {
                throw new BusinessException("此餐厅不存在");
            }
            CCommodityCategoryPO commodityCategoryPO = null;
            if (commodityCategory.Id > 0)
            {
                commodityCategoryPO = Instance.GetById(commodityCategory.Id);
                if (commodityCategoryPO == null)
                {
                    throw new BusinessException("商品类别不存在");
                }
            }
            else
            {
                commodityCategoryPO = new CCommodityCategoryPO();
            }
            commodityCategoryPO.ChineseName = commodityCategory.ChineseName;
            commodityCategoryPO.EnglishName = commodityCategory.EnglishName;
            commodityCategoryPO.PicPath = commodityCategory.PicPath;
            commodityCategoryPO.Remark = commodityCategory.Remark;
            commodityCategoryPO.ResUUID = restaurant.ResUUID;
            commodityCategoryPO.Sort = commodityCategory.Sort;
            commodityCategoryPO.IsDelete = commodityCategory.IsDelete;
            if (commodityCategoryPO.Id > 0)
            {
                return this.Update(commodityCategoryPO, operatorName);
            }
            return this.Add(commodityCategoryPO, operatorName);

        }
        public int AddWithContext(IEntityContext entityContext, CCommodityCategoryPO po, string operatorName)
        {
            if (entityContext == null)
            {
                throw new ArgumentNullException("entityContext");
            }
            if (po == null)
            {
                throw new ArgumentNullException("对象不可以为空");
            }
            if (string.IsNullOrWhiteSpace(operatorName))
            {
                throw new BusinessException("操作人不能为空");
            }
            if (po.Id > 0)
            {
                throw new BusinessException("添加对象的ID不能大于0");
            }
            if (string.IsNullOrWhiteSpace(po.CommodityCategoryUUID))
            {
                po.CommodityCategoryUUID = Guid.NewGuid().ToString();
            }
            if (string.IsNullOrWhiteSpace(po.ChineseName))
            {
                throw new BusinessException("商品属性不能为空");
            }
            if (string.IsNullOrWhiteSpace(po.ResUUID))
            {
                throw new BusinessException("餐厅ID必须设定");
            }
            po.IsDelete = false;
            po.CreateTime = DateTime.Now;
            po.UpdateTime = DateTime.Now;
            po.CreateName = operatorName;
            po.UpdateName = operatorName;
            CCommodityCategoryPOManager.Instance.AddWithContext(entityContext, po);
            COperationLogManager.Instance.AddOperationLogWithContext(entityContext, po.Id, EnumOperationLogType.CCommodityCategory, EnumOperationLogAction.Add, null, po, operatorName);
            return po.Id;

        }
        public bool DeleteCommodityCategory(int id, string operatorName)
        {
            if (id <= 0)
            {
                throw new BusinessException("商品类别ID有误");
            }
            CCommodityCategoryPO commodityCategoryPO = this.GetById(id);
            if (commodityCategoryPO == null)
            {
                throw new BusinessException("商品类别不存在");
            }
            commodityCategoryPO.IsDelete = true;
            this.Update(commodityCategoryPO, operatorName);
            return true;

        }
        public CCommodityCategoryPO GetById(int id)
        {
            return CCommodityCategoryPOManager.Instance.GetById(id);
        }
        public CCommodityCategoryPO GetByIdWithContext(IEntityContext entityContext, int id)
        {
            return CCommodityCategoryPOManager.Instance.GetByIdWithContext(entityContext, id);
        }
        public List<CommodityCategoryInfoDTO> GetCommodityCategory(int resId)
        {
            Core.Data.QueryResult<CCommodityPropertyPO> resultCCommodityProperty;
            Core.Data.QueryResult<CPropertyCategoryPO> resultPropertyCategory;
            if (resId <= 0)
            {
                throw new BusinessException("餐厅ID不正确");
            }
            CRestaurantPO restaurant = CRestaurantManager.Instance.GetById(resId);
            if (restaurant == null)
            {
                throw new BusinessException("餐厅不存在");
            }
            List<CommodityCategoryInfoDTO> listCommodityCategoryInfo = new List<CommodityCategoryInfoDTO>();
            Core.Data.QueryResult<CCommodityCategoryPO> queryCommodityCategory = this.GetCommodityCategorys(restaurant.ResUUID, null, 1, 1);

            if (queryCommodityCategory.TotalCount != 0)
            {
                queryCommodityCategory = this.GetCommodityCategorys(restaurant.ResUUID, null, 1, (int)queryCommodityCategory.TotalCount);
                List<string> listCommodityCategory = new List<string>();
                if ((queryCommodityCategory.Items != null) && (queryCommodityCategory.Items.Length > 0))
                {
                    listCommodityCategory = queryCommodityCategory.Items.Select(x=>x.CommodityCategoryUUID).Distinct<string>().ToList<string>();
                }
                int? id = null;
                List<int> states = new List<int> { 1 };
                Core.Data.QueryResult<CCommodityPO> resultCommodity = CCommodityManager.Instance.GetCCommoditys(id, restaurant.ResUUID, null, listCommodityCategory, states, 1, 1, null);
                if (((resultCommodity == null) || (resultCommodity.Items == null)) || (resultCommodity.Items.Length == 0))
                {
                    return listCommodityCategoryInfo;
                }
                if (resultCommodity.TotalCount > 1)
                {
                    List<int> list2 = new List<int> { 1 };
                    resultCommodity = CCommodityManager.Instance.GetCCommoditys(null, restaurant.ResUUID, null, listCommodityCategory, list2, 1, (int)resultCommodity.TotalCount, null);
                }
                List<string> commodityUUIDs = new List<string>();
                if ((resultCommodity.Items != null) && (resultCommodity.Items.Length > 0))
                {
                    commodityUUIDs = resultCommodity.Items.Select(x=>x.CommodityUUID).Distinct<string>().ToList<string>();
                }
                resultCCommodityProperty = CCommodityPropertyManager.Instance.GetCommodityPropertys(null, restaurant.ResUUID, null, commodityUUIDs, null, 1, 1);
                if (((resultCCommodityProperty != null) && (resultCCommodityProperty.Items != null)) && (resultCCommodityProperty.TotalCount > 1))
                {
                    resultCCommodityProperty = CCommodityPropertyManager.Instance.GetCommodityPropertys(null, restaurant.ResUUID, null, commodityUUIDs, null, 1, (int)resultCCommodityProperty.TotalCount);
                }
                List<string> listPropertyCategory = new List<string>();
                if (((resultCCommodityProperty != null) && (resultCCommodityProperty.Items != null)) && (resultCCommodityProperty.Items.Length > 0))
                {
                    listPropertyCategory = resultCCommodityProperty.Items.Select(x=>x.PropertyCategoryUUID).Distinct<string>().ToList<string>();
                }
                resultPropertyCategory = new Core.Data.QueryResult<CPropertyCategoryPO>();
                if ((listPropertyCategory != null) && (listPropertyCategory.Count > 0))
                {
                    resultPropertyCategory = CPropertyCategoryManager.Instance.GetPropertyCategorys(restaurant.ResUUID, listPropertyCategory, 1, 1);
                }
                if (((resultPropertyCategory != null) && (resultPropertyCategory.Items != null)) && (resultPropertyCategory.TotalCount > 1))
                {
                    resultPropertyCategory = CPropertyCategoryManager.Instance.GetPropertyCategorys(restaurant.ResUUID, listPropertyCategory, 1, (int)resultPropertyCategory.TotalCount);
                }
                CCommodityCategoryPO[] items = queryCommodityCategory.Items;

                for (int i = 0; i < items.Length; i++)
                {
                    CCommodityCategoryPO itemCommodityCategory = items[i];
                    List<CommodityInfoDTO> listCommodityInfoD;
                    if (itemCommodityCategory != null)
                    {
                        CommodityCategoryInfoDTO odto1 = new CommodityCategoryInfoDTO
                        {
                            Id = itemCommodityCategory.Id,
                            ChineseName = itemCommodityCategory.ChineseName,
                            EnglishName = itemCommodityCategory.EnglishName,
                            PicPath = itemCommodityCategory.PicPath
                        };
                        CommodityCategoryInfoDTO commodityCategoryInfo = odto1;
                        listCommodityCategoryInfo.Add(commodityCategoryInfo);
                        List<CCommodityPO> listCCommodity = resultCommodity.Items.Where(x=>x.CommodityCategoryUUID == itemCommodityCategory.CommodityCategoryUUID).ToList();

                        if (listCCommodity != null)
                        {
                            listCommodityInfoD = new List<CommodityInfoDTO>();
                            commodityCategoryInfo.GoodsList = listCommodityInfoD;
                            listCCommodity.ForEach(delegate (CCommodityPO itemCommodity)
                            {
                                if (itemCommodity != null)
                                {
                                    CommodityInfoDTO odto2 = new CommodityInfoDTO
                                    {
                                        ChineseName = itemCommodity.ChineseName,
                                        EnglishName = itemCommodity.EnglishName,
                                        Id = itemCommodity.Id,
                                        Price = itemCommodity.Price.HasValue ? itemCommodity.Price.Value : decimal.Zero,
                                        ShowRemark = itemCommodity.ShowRemark,
                                        Type = itemCommodity.Type,
                                        PicPath = itemCommodity.PicPath
                                    };
                                    CommodityInfoDTO commodityInfoDTO = odto2;
                                    listCommodityInfoD.Add(commodityInfoDTO);
                                    List<CCommodityPropertyPO> listCommodityProperty = (from x in resultCCommodityProperty.Items
                                                                                        where x.CommodityUUID == itemCommodity.CommodityUUID
                                                                                        select x).ToList<CCommodityPropertyPO>();
                                    if ((listCommodityProperty != null) && (listCommodityProperty.Count != 0))
                                    {
                                        List<string> listPropertyCategoryUUID = listCommodityProperty.Select(x => x.PropertyCategoryUUID).Distinct<string>().ToList<string>();
                                        List<PropertyCategoryInfoDTO> listPropertyCategoryInfo = new List<PropertyCategoryInfoDTO>();
                                        commodityInfoDTO.SkuInfo = listPropertyCategoryInfo;
                                        listPropertyCategoryUUID.ForEach(delegate (string itemPropertyCategoryUUID)
                                        {
                                            if (!string.IsNullOrWhiteSpace(itemPropertyCategoryUUID))
                                            {
                                                CPropertyCategoryPO propertyCategoryPO = resultPropertyCategory.Items.FirstOrDefault<CPropertyCategoryPO>(x => x.PropertyCategoryUUID == itemPropertyCategoryUUID);
                                                if (propertyCategoryPO != null)
                                                {
                                                    PropertyCategoryInfoDTO odto3 = new PropertyCategoryInfoDTO
                                                    {
                                                        ChineseName = propertyCategoryPO.ChineseName,
                                                        EnableMultiple = propertyCategoryPO.EnableMultiple,
                                                        EnglishName = propertyCategoryPO.EnglishName,
                                                        Id = propertyCategoryPO.Id,
                                                        Type = propertyCategoryPO.Type
                                                    };
                                                    PropertyCategoryInfoDTO propertyCategoryInfoDTO = odto3;
                                                    listPropertyCategoryInfo.Add(propertyCategoryInfoDTO);
                                                    List<CCommodityPropertyPO> listCommodityPropertyOther = (from x in listCommodityProperty
                                                                                                             where x.PropertyCategoryUUID == itemPropertyCategoryUUID
                                                                                                             select x).ToList<CCommodityPropertyPO>();
                                                    if (listCommodityPropertyOther != null)
                                                    {
                                                        List<CCommodityPropertyInfoDTO> commodityPropertyInfos = new List<CCommodityPropertyInfoDTO>();
                                                        propertyCategoryInfoDTO.List = commodityPropertyInfos;
                                                        listCommodityPropertyOther.ForEach(delegate (CCommodityPropertyPO x) {
                                                            if (x != null)
                                                            {
                                                                CCommodityPropertyInfoDTO odto4 = new CCommodityPropertyInfoDTO
                                                                {
                                                                    AddPrice = x.AddPrice.HasValue ? x.AddPrice.Value : decimal.Zero,
                                                                    ChineseName = x.ChineseName,
                                                                    EnglishName = x.EnglishName,
                                                                    Id = x.Id
                                                                };
                                                                CCommodityPropertyInfoDTO commodityPropertyInfo = odto4;
                                                                commodityPropertyInfos.Add(commodityPropertyInfo);
                                                            }
                                                        });
                                                    }
                                                }
                                            }
                                        });
                                    }
                                }
                            });
                        }
                    }
                }
            }

            return listCommodityCategoryInfo;
        }

        public CCommodityCategoryDTO GetCommodityCategoryById(int id)
        {
            return base.PoToDto(this.GetById(id));
        }
        public List<CommodityCategoryInfoDTO> GetCommodityCategoryByResUUID(string resUUID)
        {
            if (string.IsNullOrWhiteSpace(resUUID))
            {
                throw new ArgumentNullException("resUUID不能为空");
            }
            List<string> list1 = new List<string> {
                resUUID
            };
            Core.Data.QueryResult<CRestaurantPO> result = CRestaurantManager.Instance.GetRestaurants(list1, null, null, null, null, 1, 1, null, null, null);
            if (((result == null) || (result.Items == null)) || (result.Items.Length == 0))
            {
                throw new BusinessException("获取餐厅失败,此餐厅不存在");
            }
            if (result.Items[0].State != 1)
            {
                throw new BusinessException("此餐厅未上线");
            }
            return this.GetCommodityCategory(result.Items[0].Id);

        }
        public List<CCommodityCategoryDTO> GetCommodityCategoryDtos(string resUUID)
        {
            if (string.IsNullOrWhiteSpace(resUUID))
            {
                throw new BusinessException("餐厅ID存在");
            }
            List<CCommodityCategoryDTO> listCommodityCategory = new List<CCommodityCategoryDTO>();
            Core.Data.QueryResult<CCommodityCategoryPO> queryResult = this.GetCommodityCategorys(resUUID, null, 1, 1);
            if (queryResult.TotalCount != 0)
            {
                if (queryResult.TotalCount > 1)
                {
                    queryResult = this.GetCommodityCategorys(resUUID, null, 1, (int)queryResult.TotalCount);
                }
                if ((queryResult.Items != null) && (queryResult.Items.Length > 0))
                {
                    listCommodityCategory.AddRange(base.PoToDtoList(queryResult.Items.ToList<CCommodityCategoryPO>()));
                }
            }
            return listCommodityCategory;

        }
        public Core.Data.QueryResult<CCommodityCategoryDTO> GetCommodityCategoryQuery(string resUUID, int startIndex, int count)
        {
            if (string.IsNullOrWhiteSpace(resUUID))
            {
                throw new BusinessException("餐厅ID存在");
            }
            Core.Data.QueryResult<CCommodityCategoryDTO> listCommodityCategory = new Core.Data.QueryResult<CCommodityCategoryDTO>();
            Core.Data.QueryResult<CCommodityCategoryPO> queryResult = this.GetCommodityCategorys(resUUID, null, startIndex, count);
            if (queryResult.TotalCount != 0)
            {
                listCommodityCategory.TotalCount = queryResult.TotalCount;
                listCommodityCategory.Items = base.PoToDtoList(queryResult.Items.ToList<CCommodityCategoryPO>()).ToArray();
            }
            return listCommodityCategory;
        }
        public Core.Data.QueryResult<CCommodityCategoryPO> GetCommodityCategorys(string resUUID, List<string> commodityCategoryUUIDs, int startIndex, int count)
        {
            Sort sort = PredicateFactory.Sort("Sort", "desc");
            IPredicate predicate = this.GetCommodityCategorysCondition(resUUID, commodityCategoryUUIDs);
            ISort[] sorts = new ISort[] { sort };
            return CCommodityCategoryPOManager.Instance.GetCommodityCategoryWithTotalCountByPredicate(predicate, startIndex, count, sorts);
        }
        private IPredicate GetCommodityCategorysCondition(string resUUID, List<string> commodityCategoryUUIDs)
        {
            List<IPredicate> predicates = new List<IPredicate>();
            predicates.Add(PredicateFactory.Equal<CCommodityPropertyPO>(t => t.IsDelete, false));
            if (!string.IsNullOrWhiteSpace(resUUID))
            {
                predicates.Add(PredicateFactory.Equal<CCommodityCategoryPO>(t => t.ResUUID, resUUID));
            }
            if ((commodityCategoryUUIDs != null) && (commodityCategoryUUIDs.Count > 0))
            {
                predicates.Add(PredicateFactory.In<CCommodityCategoryPO>(t => t.CommodityCategoryUUID, commodityCategoryUUIDs.ToArray()));
            }
            return PredicateFactory.And(predicates.ToArray());

        }
        public List<CommodityInfoDTO> GetGoodsByClassify(int commodityCategoryId)
        {
            Core.Data.QueryResult<CCommodityPropertyPO> resultCCommodityProperty;
            Core.Data.QueryResult<CPropertyCategoryPO> resultPropertyCategory;
            if (commodityCategoryId <= 0)
            {
                throw new BusinessException("商品类别错误");
            }
            List<CommodityInfoDTO> listCommodity = new List<CommodityInfoDTO>();
            CCommodityCategoryPO po = this.GetById(commodityCategoryId);
            if (po != null)
            {
                int? id = null;
                List<string> commodityCategoryUUIDs = new List<string> {po.CommodityCategoryUUID};
                List<int> states = new List<int> { 1 };
                Core.Data.QueryResult<CCommodityPO> resultCommodity = CCommodityManager.Instance.GetCCommoditys(id, null, null, commodityCategoryUUIDs, states, 1, 1, null);
                if (((resultCommodity == null) || (resultCommodity.Items == null)) || (resultCommodity.Items.Length == 0))
                {
                    return listCommodity;
                }
                if (resultCommodity.TotalCount > 1)
                {
                    List<string> list3 = new List<string> {po.CommodityCategoryUUID};
                    List<int> list4 = new List<int> { 1 };
                    resultCommodity = CCommodityManager.Instance.GetCCommoditys(null, null, null, list3, list4, 1, (int)resultCommodity.TotalCount, null);
                }
                List<string> commodityUUIDs = new List<string>();
                if ((resultCommodity.Items != null) && (resultCommodity.Items.Length > 0))
                {
                    commodityUUIDs = resultCommodity.Items.Select(x=>x.CommodityUUID).Distinct<string>().ToList<string>();
                }
                resultCCommodityProperty = CCommodityPropertyManager.Instance.GetCommodityPropertys(null, po.ResUUID, null, commodityUUIDs, null, 1, 1);
                if (((resultCCommodityProperty != null) && (resultCCommodityProperty.Items != null)) && (resultCCommodityProperty.TotalCount > 1))
                {
                    resultCCommodityProperty = CCommodityPropertyManager.Instance.GetCommodityPropertys(null, po.ResUUID, null, commodityUUIDs, null, 1, (int)resultCCommodityProperty.TotalCount);
                }
                List<string> listPropertyCategory = new List<string>();
                if (((resultCCommodityProperty != null) && (resultCCommodityProperty.Items != null)) && (resultCCommodityProperty.Items.Length > 0))
                {
                    listPropertyCategory = resultCCommodityProperty.Items.Select(x=>x.PropertyCategoryUUID).Distinct<string>().ToList<string>();
                }
                resultPropertyCategory = new Core.Data.QueryResult<CPropertyCategoryPO>();
                if ((listPropertyCategory != null) && (listPropertyCategory.Count > 0))
                {
                    resultPropertyCategory = CPropertyCategoryManager.Instance.GetPropertyCategorys(po.ResUUID, listPropertyCategory, 1, 1);
                }
                if (((resultPropertyCategory != null) && (resultPropertyCategory.Items != null)) && (resultPropertyCategory.TotalCount > 1))
                {
                    resultPropertyCategory = CPropertyCategoryManager.Instance.GetPropertyCategorys(po.ResUUID, listPropertyCategory, 1, (int)resultPropertyCategory.TotalCount);
                }
                CommodityCategoryInfoDTO odto1 = new CommodityCategoryInfoDTO
                {
                    Id = po.Id,
                    ChineseName = po.ChineseName,
                    EnglishName = po.EnglishName,
                    PicPath = po.PicPath
                };
                CommodityCategoryInfoDTO commodityCategoryInfo = odto1;
                List<CCommodityPO> listCCommodity = (from x in resultCommodity.Items
                                                     where x.CommodityCategoryUUID == po.CommodityCategoryUUID
                                                     select x).ToList<CCommodityPO>();
                if (listCCommodity == null)
                {
                    return listCommodity;
                }
                commodityCategoryInfo.GoodsList = listCommodity;
                listCCommodity.ForEach(delegate (CCommodityPO itemCommodity)
                {
                    if (itemCommodity != null)
                    {
                        CommodityInfoDTO odto11 = new CommodityInfoDTO
                        {
                            ChineseName = itemCommodity.ChineseName,
                            EnglishName = itemCommodity.EnglishName,
                            Id = itemCommodity.Id,
                            Price = itemCommodity.Price.HasValue ? itemCommodity.Price.Value : decimal.Zero,
                            ShowRemark = itemCommodity.ShowRemark,
                            Type = itemCommodity.Type,
                            PicPath = itemCommodity.PicPath
                        };
                        CommodityInfoDTO commodityInfoDTO = odto11;
                        listCommodity.Add(commodityInfoDTO);
                        List<CCommodityPropertyPO> listCommodityProperty = (from x in resultCCommodityProperty.Items
                                                                            where x.CommodityUUID == itemCommodity.CommodityUUID
                                                                            select x).ToList<CCommodityPropertyPO>();
                        if ((listCommodityProperty != null) && (listCommodityProperty.Count != 0))
                        {
                            List<string> listPropertyCategoryUUID = listCommodityProperty.Select<CCommodityPropertyPO, string>(x => x.PropertyCategoryUUID).Distinct<string>().ToList<string>();
                            List<PropertyCategoryInfoDTO> listPropertyCategoryInfo = new List<PropertyCategoryInfoDTO>();
                            listPropertyCategoryUUID.ForEach(delegate (string itemPropertyCategoryUUID)
                            {
                                if (!string.IsNullOrWhiteSpace(itemPropertyCategoryUUID))
                                {
                                    CPropertyCategoryPO propertyCategoryPO = resultPropertyCategory.Items.FirstOrDefault<CPropertyCategoryPO>(x => x.PropertyCategoryUUID == itemPropertyCategoryUUID);
                                    if (propertyCategoryPO != null)
                                    {
                                        PropertyCategoryInfoDTO odto2 = new PropertyCategoryInfoDTO
                                        {
                                            ChineseName = propertyCategoryPO.ChineseName,
                                            EnableMultiple = propertyCategoryPO.EnableMultiple,
                                            EnglishName = propertyCategoryPO.EnglishName,
                                            Id = propertyCategoryPO.Id,
                                            Type = propertyCategoryPO.Type,
                                            Sort = propertyCategoryPO.Sort
                                        };
                                        PropertyCategoryInfoDTO propertyCategoryInfoDTO = odto2;
                                        listPropertyCategoryInfo.Add(propertyCategoryInfoDTO);
                                        List<CCommodityPropertyPO> listCommodityPropertyOther = (from x in listCommodityProperty
                                                                                                 where x.PropertyCategoryUUID == itemPropertyCategoryUUID
                                                                                                 select x).ToList<CCommodityPropertyPO>();
                                        if (listCommodityPropertyOther != null)
                                        {
                                            List<CCommodityPropertyInfoDTO> commodityPropertyInfos = new List<CCommodityPropertyInfoDTO>();
                                            propertyCategoryInfoDTO.List = commodityPropertyInfos;
                                            listCommodityPropertyOther.ForEach(delegate (CCommodityPropertyPO x)
                                            {
                                                if (x != null)
                                                {
                                                    CCommodityPropertyInfoDTO odto3 = new CCommodityPropertyInfoDTO
                                                    {
                                                        AddPrice = x.AddPrice.HasValue ? x.AddPrice.Value : decimal.Zero,
                                                        ChineseName = x.ChineseName,
                                                        EnglishName = x.EnglishName,
                                                        Id = x.Id
                                                    };
                                                    CCommodityPropertyInfoDTO commodityPropertyInfo = odto3;
                                                    commodityPropertyInfos.Add(commodityPropertyInfo);
                                                }
                                            });
                                        }

                                    }
                                }
                            });
                            commodityInfoDTO.SkuInfo = listPropertyCategoryInfo.OrderByDescending(x => x.Sort).ToList<PropertyCategoryInfoDTO>();
                        }
                    }
                });
            }
            return listCommodity;
        }
        public List<GoodsClassifyDTO> GetGoodsClassifyDTO(int resId)
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
            List<GoodsClassifyDTO> list = new List<GoodsClassifyDTO>();
            Core.Data.QueryResult<CCommodityCategoryPO> queryCommodityCategory = this.GetCommodityCategorys(restaurant.ResUUID, null, 1, 1);
            if (queryCommodityCategory.TotalCount != 0)
            {
                if (queryCommodityCategory.TotalCount > 1)
                {
                    queryCommodityCategory = this.GetCommodityCategorys(restaurant.ResUUID, null, 1, (int)queryCommodityCategory.TotalCount);
                }
                foreach (CCommodityCategoryPO item in queryCommodityCategory.Items)
                {
                    GoodsClassifyDTO ydto1 = new GoodsClassifyDTO
                    {
                        ChineseName = item.ChineseName,
                        EnglishName = item.EnglishName,
                        Id = item.Id,
                        PicPath = item.PicPath
                    };
                    GoodsClassifyDTO goodsClassifyDTO = ydto1;
                    list.Add(goodsClassifyDTO);
                }
            }
            return list;

        }
        public int Update(CCommodityCategoryPO po, string operatorName)
        {
            using (IEntityContext entityContext = CCommodityCategoryPOManager.Instance.CreateEntityContext())
            {
                return this.UpdateWithContext(entityContext, po, operatorName);
            }
        }
        public int UpdateWithContext(IEntityContext entityContext, CCommodityCategoryPO po, string operatorName)
        {
            if (entityContext == null)
            {
                throw new ArgumentNullException("entityContext");
            }
            if (po == null)
            {
                throw new ArgumentNullException("对象不可以为空");
            }
            if (string.IsNullOrWhiteSpace(operatorName))
            {
                throw new BusinessException("操作人不能为空");
            }
            if (po.Id <= 0)
            {
                throw new BusinessException("添加对象的ID不能小于0");
            }
            if (string.IsNullOrWhiteSpace(po.CommodityCategoryUUID))
            {
                throw new BusinessException("UUID不能为空");
            }
            if (string.IsNullOrWhiteSpace(po.ChineseName))
            {
                throw new BusinessException("商品属性不能为空");
            }
            if (string.IsNullOrWhiteSpace(po.ResUUID))
            {
                throw new BusinessException("餐厅ID必须设定");
            }
            po.UpdateTime = DateTime.Now;
            po.UpdateName = operatorName;
            object oldObject = base.DtoToPo(base.PoToDto(this.GetByIdWithContext(entityContext, po.Id)));
            CCommodityCategoryPOManager.Instance.UpdateWithContext(entityContext, po);
            COperationLogManager.Instance.AddOperationLogWithContext(entityContext, po.Id, EnumOperationLogType.CCommodityCategory, EnumOperationLogAction.Update, oldObject, po, operatorName);
            return po.Id;

        }
    }
}