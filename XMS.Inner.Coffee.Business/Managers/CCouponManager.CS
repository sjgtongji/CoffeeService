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
    public class CCouponManager : DataMapperManagerBase<CCouponPO, CCouponDTO>, IManagerBase<CCouponPO>
    {
        public static readonly CCouponManager Instance = new CCouponManager();
        private CCouponManager() { }

        public int Add(CCouponPO po, string operatorName)
        {
            using (IEntityContext entityContext = CCouponPOManager.Instance.CreateEntityContext())
            {
                return this.AddWithContext(entityContext, po, operatorName);
            }

        }
        public int AddOrUpdateCCouponDto(CCouponDTO dto, string operatorName)
        {
            if (dto == null)
            {
                throw new ArgumentNullException("CCouponDTO对象为空");
            }
            CCouponPO po = base.DtoToPo(dto);
            if (po.Id > 0)
            {
                return this.Update(po, operatorName);
            }
            return this.Add(po, operatorName);

        }
        public int AddOrUpdateCoupon(AddOrUpdateCouponDTO coupon, string operatorName)
        {
            if (coupon == null)
            {
                throw new BusinessException("优惠卷未设定");
            }
            if (string.IsNullOrWhiteSpace(operatorName))
            {
                throw new BusinessException("操作人未设定");
            }
            CRestaurantPO restaurant = CRestaurantManager.Instance.GetById(coupon.ResId);
            if (restaurant == null)
            {
                throw new BusinessException("此餐厅不存在");
            }
            CCouponPO couponPO = null;
            if (coupon.Id > 0)
            {
                couponPO = Instance.GetById(coupon.Id);
                if (couponPO == null)
                {
                    throw new BusinessException("优惠卷不存在");
                }
            }
            else
            {
                CCouponPO npo1 = new CCouponPO
                {
                    CouponName = coupon.CouponName,
                    ResUUID = restaurant.ResUUID,
                    CouponType = coupon.CouponType,
                    EffectiveEndTime = coupon.EffectiveEndTime,
                    EffectiveStartTime = coupon.EffectiveStartTime,
                    IsDelete = coupon.IsDelete,
                    OfferAmount = coupon.OfferAmount,
                    Preferential = coupon.Preferential,
                    PreferentialType = coupon.PreferentialType,
                    Remark = coupon.Remark,
                    State = coupon.State
                };
                couponPO = npo1;
            }
            couponPO.CouponName = coupon.CouponName;
            couponPO.ResUUID = restaurant.ResUUID;
            couponPO.CouponType = coupon.CouponType;
            couponPO.EffectiveEndTime = coupon.EffectiveEndTime;
            couponPO.EffectiveStartTime = coupon.EffectiveStartTime;
            couponPO.IsDelete = coupon.IsDelete;
            couponPO.OfferAmount = coupon.OfferAmount;
            couponPO.Preferential = coupon.Preferential;
            couponPO.PreferentialType = coupon.PreferentialType;
            couponPO.Remark = coupon.Remark;
            couponPO.State = coupon.State;
            if (couponPO.Id > 0)
            {
                return this.Update(couponPO, operatorName);
            }
            return this.Add(couponPO, operatorName);

        }
        public int AddWithContext(IEntityContext entityContext, CCouponPO po, string operatorName)
        {
            decimal? preferential;
            decimal num;
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
            if (string.IsNullOrWhiteSpace(po.CouponUUID))
            {
                po.CouponUUID = Guid.NewGuid().ToString();
            }
            if ((po.PreferentialType != 1) && (po.PreferentialType != 2))
            {
                throw new BusinessException("优惠类型不正确");
            }
            if (!po.Preferential.HasValue)
            {
                throw new BusinessException("优惠金额未设定");
            }
            if (po.PreferentialType == 2)
            {
                preferential = po.Preferential;
                num = new decimal();
                if (((preferential.GetValueOrDefault() < num) ? preferential.HasValue : false) || (((preferential = po.Preferential).GetValueOrDefault() > (num = 10)) ? preferential.HasValue : false))
                {
                    throw new BusinessException("优惠比例不合理");
                }
            }
            else if (po.PreferentialType == 1)
            {
                preferential = po.Preferential;
                num = new decimal();
                if ((preferential.GetValueOrDefault() < num) ? preferential.HasValue : false)
                {
                    throw new BusinessException("优惠额度不合理");
                }
            }
            po.IsDelete = false;
            po.CreateTime = DateTime.Now;
            po.CreateName = operatorName;
            po.UpdateTime = DateTime.Now;
            po.UpdateName = operatorName;
            CCouponPOManager.Instance.AddWithContext(entityContext, po);
            COperationLogManager.Instance.AddOperationLogWithContext(entityContext, po.Id, EnumOperationLogType.CCoupon, EnumOperationLogAction.Add, null, po, operatorName);
            return po.Id;

        }
        public bool DeleteCoupon(int id, string operatorName)
        {
            if (id <= 0)
            {
                throw new BusinessException("优惠卷ID不能为空");
            }
            CCouponPO couponPO = this.GetById(id);
            if (couponPO == null)
            {
                throw new BusinessException("此优惠卷不存在");
            }
            couponPO.IsDelete = true;
            this.Update(couponPO, operatorName);
            return true;

        }
        public List<CCouponDTO> GetAvailableCouponDTO(GetAvailableCouponDTO getAvailableCoupon, int memberId)
        {
            if (((getAvailableCoupon == null) || (getAvailableCoupon.Commodity == null)) || (getAvailableCoupon.Commodity.Count == 0))
            {
                throw new BusinessException("商品信息为空");
            }
            if (CMemberWeiXinManager.Instance.GetById(memberId) == null)
            {
                throw new BusinessException("此微信用户不存在");
            }
            if (getAvailableCoupon.ResId <= 0)
            {
                throw new BusinessException("餐厅ID不正确");
            }
            CRestaurantPO restaurant = CRestaurantManager.Instance.GetById(getAvailableCoupon.ResId);
            if (restaurant == null)
            {
                throw new BusinessException("此餐厅不存在");
            }
            if (restaurant.IsDelete)
            {
                throw new BusinessException("此餐厅已被删除");
            }
            decimal orderMomey = new decimal();
            //decimal preferentialMomey = new decimal();
            List<string> remark = new List<string>();
            List<Commodity> listCommodity = new List<Model.Commodity>();
            getAvailableCoupon.Commodity.ForEach(delegate (Commodity item) {
                Commodity commodity = listCommodity.FirstOrDefault(x => x.CommodityId == item.CommodityId);
                if (commodity == null)
                {
                    commodity = new Commodity { CommodityId = item.CommodityId, Quantity = item.Quantity };
                    listCommodity.Add(commodity);
                }
                else
                {
                    commodity.Quantity = commodity.Quantity + item.Quantity;
                }

                //bool preferentialFlag = false;
                if (item != null)
                {
                    decimal commityMomey = new decimal();
                    List<int> states = new List<int> { 1 };
                    Core.Data.QueryResult<CCommodityPO> commodityQuery = CCommodityManager.Instance.GetCCommoditys(new int?(item.CommodityId), restaurant.ResUUID, null, null, states, 1, 1, null);
                    if ((commodityQuery.Items == null) || (commodityQuery.Items.Length == 0))
                    {
                        throw new BusinessException("商品信息错误，请重新选择");
                    }
                    commityMomey += commodityQuery.Items[0].Price.HasValue ? commodityQuery.Items[0].Price.Value : decimal.Zero;
                    //if (commodityQuery.Items[0].PreferentialFlag && (item.Quantity >= (commodityQuery.Items[0].PreferentialQuantity.HasValue ? commodityQuery.Items[0].PreferentialQuantity.Value : 1)))
                    //{
                    //    preferentialFlag = true;
                    //}
                    if ((item.SkuList == null) || (item.SkuList.Count == 0))
                    {
                        //if (preferentialFlag)
                        //{
                        //    preferentialMomey += this.GetPreferentialMomey(item.Quantity, commodityQuery.Items[0].PreferentialQuantity, commodityQuery.Items[0].PreferentialProportion, commityMomey);
                        //    remark.Add(string.Format("商品({0})满{1}享{2}优惠", commodityQuery.Items[0].ChineseName, commodityQuery.Items[0].PreferentialQuantity.HasValue ? commodityQuery.Items[0].PreferentialQuantity.Value : 1, commodityQuery.Items[0].PreferentialProportion.HasValue ? (commodityQuery.Items[0].PreferentialProportion.Value.ToString("#0.00") + "%") : "0%"));
                        //}
                        //orderMomey += commityMomey * item.Quantity;
                    }
                    else
                    {
                        Core.Data.QueryResult<CCommodityPropertyPO> commodityPropertyResult = CCommodityPropertyManager.Instance.GetCommodityPropertys(item.SkuList, restaurant.ResUUID, null, null, null, 1, item.SkuList.Count);
                        if (((commodityPropertyResult.Items == null) || (commodityPropertyResult.Items.Length == 0)) || (commodityPropertyResult.Items.Length != item.SkuList.Count))
                        {
                            throw new BusinessException("商品信息错误，请重新选择");
                        }
                        foreach (CCommodityPropertyPO propertyItem in commodityPropertyResult.Items)
                        {
                            if (propertyItem != null)
                            {
                                commityMomey += propertyItem.AddPrice.HasValue ? propertyItem.AddPrice.Value : decimal.Zero;
                            }
                        }
                        //if (preferentialFlag)
                        //{
                        //    preferentialMomey += this.GetPreferentialMomey(item.Quantity, commodityQuery.Items[0].PreferentialQuantity, commodityQuery.Items[0].PreferentialProportion, commityMomey);
                        //    remark.Add(string.Format("商品({0})满{1}享{2}折", commodityQuery.Items[0].ChineseName, commodityQuery.Items[0].PreferentialQuantity.HasValue ? commodityQuery.Items[0].PreferentialQuantity.Value : 1, commodityQuery.Items[0].PreferentialProportion.HasValue ? commodityQuery.Items[0].PreferentialProportion.Value : 10M));
                        //}
                        //orderMomey += commityMomey * item.Quantity;
                    }
                    //if (preferentialFlag)
                    //{
                    //    preferentialMomey += this.GetPreferentialMomey(item.Quantity, commodityQuery.Items[0].PreferentialQuantity, commodityQuery.Items[0].PreferentialProportion, commityMomey);
                    //    remark.Add(string.Format("商品({0})满{1}享{2}优惠", commodityQuery.Items[0].ChineseName, commodityQuery.Items[0].PreferentialQuantity.HasValue ? commodityQuery.Items[0].PreferentialQuantity.Value : 1, commodityQuery.Items[0].PreferentialProportion.HasValue ? (commodityQuery.Items[0].PreferentialProportion.Value.ToString("#0.00") + "%") : "0%"));
                    //}
                    orderMomey += commityMomey * item.Quantity;
                }
            });

            decimal preferentialMomey = new decimal();
            listCommodity.ForEach(item => {
                bool preferentialFlag = false;
                if (item != null)
                {
                    decimal commityMomey = new decimal();
                    List<int> states = new List<int> { 1 };
                    Core.Data.QueryResult<CCommodityPO> commodityQuery = CCommodityManager.Instance.GetCCommoditys(new int?(item.CommodityId), restaurant.ResUUID, null, null, states, 1, 1, null);
                    if ((commodityQuery.Items == null) || (commodityQuery.Items.Length == 0))
                    {
                        throw new BusinessException("商品信息错误，请重新选择");
                    }
                    commityMomey += commodityQuery.Items[0].Price.HasValue ? commodityQuery.Items[0].Price.Value : decimal.Zero;
                    if (commodityQuery.Items[0].PreferentialFlag && (item.Quantity >= (commodityQuery.Items[0].PreferentialQuantity.HasValue ? commodityQuery.Items[0].PreferentialQuantity.Value : 1)))
                    {
                        preferentialFlag = true;
                    }

                    if (preferentialFlag)
                    {
                        preferentialMomey += this.GetPreferentialMomey(item.Quantity, commodityQuery.Items[0].PreferentialQuantity, commodityQuery.Items[0].PreferentialProportion, commityMomey);
                        remark.Add(string.Format("商品({0})满{1}享{2}优惠", commodityQuery.Items[0].ChineseName, commodityQuery.Items[0].PreferentialQuantity.HasValue ? commodityQuery.Items[0].PreferentialQuantity.Value : 1, commodityQuery.Items[0].PreferentialProportion.HasValue ? ((100 - commodityQuery.Items[0].PreferentialProportion.Value).ToString("#0.00") + "%") : "0%"));
                    }
                }
            });

            List<CCouponDTO> listCoupon = this.GetNoUseCouponDTOById(restaurant.Id, memberId);
            if (listCoupon == null)
            {
                listCoupon = new List<CCouponDTO>();
            }
            //2017.03.13 修改 开始
            listCoupon = (from x in listCoupon
                          where (x.CouponType != 3) || (x.OfferAmount <= orderMomey)
                          select x).ToList<CCouponDTO>();
            //listCoupon = (from x in listCoupon
            //              where (x.CouponType != 3) || (x.Preferential <= orderMomey)
            //              select x).ToList<CCouponDTO>();
            //2017.03.13 修改 结束

            #region 配送时间也要满足有效时间
            if (getAvailableCoupon.DeliveryMinTime.HasValue)
            {
                listCoupon = listCoupon.Where(couponItem => !couponItem.EffectiveStartTime.HasValue || couponItem.EffectiveStartTime <= getAvailableCoupon.DeliveryMinTime).ToList();
            }
            if(getAvailableCoupon.DeliveryMaxTime.HasValue)
            {
                listCoupon = listCoupon.Where(couponItem => !couponItem.EffectiveEndTime.HasValue || couponItem.EffectiveEndTime >= getAvailableCoupon.DeliveryMaxTime).ToList();
            }
            #endregion

                if (preferentialMomey > decimal.Zero)
            {
                CCouponDTO ndto1 = new CCouponDTO
                {
                    Id = -1,
                    CouponType = 4
                };
                TimeSpan span = (TimeSpan)(DateTime.Now - DateTime.Parse("2016-1-1"));
                ndto1.CouponUUID = "GoodsOffer" + span.Minutes + HelperTool.GenerateRandomNumber(10);
                ndto1.EffectiveEndTime = new DateTime?(DateTime.Now.AddDays(-100.0));
                ndto1.CreateName = "自动";
                ndto1.CreateTime = DateTime.Now;
                ndto1.EffectiveStartTime = new DateTime?(DateTime.Now.AddDays(100.0));
                ndto1.IsDelete = false;
                ndto1.OfferAmount = decimal.Zero;
                ndto1.Preferential = new decimal?(decimal.Parse(preferentialMomey.ToString("#0.00")));
                ndto1.PreferentialType = 1;
                ndto1.Remark = string.Join(";", remark);
                ndto1.ResUUID = restaurant.ResUUID;
                ndto1.State = 1;
                ndto1.UpdateName = "自动";
                ndto1.UpdateTime = DateTime.Now;
                ndto1.CouponName = "商品满数量有折扣";
                listCoupon.Add(ndto1);
            }

            #region 积分抵用券
            CMemberWeiXinPO memberWeiXinPO = CMemberWeiXinManager.Instance.GetById(memberId);
            if (memberWeiXinPO == null || !memberWeiXinPO.Integral.HasValue || memberWeiXinPO.Integral.Value <= 0)
                return listCoupon;
            List<CCashCouponPO> listCashCoupon = CCashCouponManager.Instance.GetCCashCouponList(null, null, null, DateTime.Now, DateTime.Now, new List<int> { 1 }, memberWeiXinPO.Integral, DateTime.Now, DateTime.Now);
            if (listCashCoupon == null || listCashCoupon.Count == 0)
                return listCoupon;
            listCashCoupon.ForEach(item => {
                if (item.ValAmount > orderMomey)
                    return;
                CCashCouponDetailPO cashCouponDetail = new CCashCouponDetailPO()
                {
                    AssetName = item.AssetName,
                    AssetProperty = item.AssetProperty,
                    AssetStatus = 1,
                    CreateName = memberWeiXinPO.Id.ToString(),
                    CreateTime = DateTime.Now,
                    Description = item.Description,
                    IsDelete = false,
                    MemberWeiXinId = memberWeiXinPO.Id,
                    SaleAmount = item.SaleAmount,
                    SaleEndTime = item.SaleEndTime,
                    SaleStartTime = item.SaleStartTime,
                    UpdateName = item.UpdateName,
                    UpdateTime = DateTime.Now,
                    UseState = 0,
                    ValAmount = item.ValAmount,
                    ValidEndTime = item.ValidEndTime,
                    ValidStartTime = item.ValidStartTime,
                };
                int detailId = CCashCouponDetailManager.Instance.Add(cashCouponDetail, "系统");
                if (detailId <= 0)
                    return;

                CCouponDTO ndto1 = new CCouponDTO
                {
                    Id = -100 - detailId,
                    CouponType = 4
                };
                TimeSpan span = (TimeSpan)(DateTime.Now - DateTime.Parse("2016-1-1"));
                ndto1.CouponUUID = "CouponDetail-" + detailId.ToString() + "-" + span.Minutes;
                ndto1.EffectiveEndTime = cashCouponDetail.ValidStartTime;
                ndto1.CreateName = "自动";
                ndto1.CreateTime = DateTime.Now;
                ndto1.EffectiveStartTime = cashCouponDetail.ValidEndTime;
                ndto1.IsDelete = false;
                ndto1.OfferAmount = decimal.Zero;
                ndto1.Preferential = cashCouponDetail.ValAmount;
                ndto1.PreferentialType = 1;
                ndto1.Remark = cashCouponDetail.Description;
                ndto1.ResUUID = restaurant.ResUUID;
                ndto1.State = 1;
                ndto1.UpdateName = "自动";
                ndto1.UpdateTime = DateTime.Now;
                ndto1.CouponName = cashCouponDetail.AssetName;
                listCoupon.Add(ndto1);

            });
            #endregion
            return listCoupon;

        }
        public CCouponPO GetById(int id)
        {
            return CCouponPOManager.Instance.GetById(id);
        }
        public CCouponPO GetByIdWithContext(IEntityContext entityContext, int id)
        {
            return CCouponPOManager.Instance.GetByIdWithContext(entityContext, id);
        }
        public CCouponDTO GetCouponById(int id)
        {
            return base.PoToDto(this.GetById(id));

        }
        public Core.Data.QueryResult<CCouponDTO> GetCouponDTOs(int? id, string couponUUID, List<int> couponType, List<int> preferentialType, List<int> state, string resUUID, DateTime? ordorTime, int startIndex, int count)
        {
            Core.Data.QueryResult<CCouponDTO> resultDTO = new Core.Data.QueryResult<CCouponDTO>();
            Core.Data.QueryResult<CCouponPO> resultPO = this.GetCoupons(id, couponUUID, couponType, preferentialType, state, resUUID, ordorTime, startIndex, count);
            if (resultPO != null)
            {
                resultDTO.TotalCount = resultPO.TotalCount;
                if ((resultPO.Items != null) && (resultPO.Items.Length > 0))
                {
                    resultDTO.Items = base.PoToDtoList(resultPO.Items.ToList<CCouponPO>()).ToArray();
                }
            }
            return resultDTO;

        }
        public Core.Data.QueryResult<CCouponPO> GetCoupons(int? id, string couponUUID, List<int> couponType, List<int> preferentialType, List<int> state, string resUUID, DateTime? ordrTime, int startIndex, int count)
        {
            Sort sort = PredicateFactory.Sort("CreateTime", "desc");
            IPredicate predicate = this.GetCouponsCondition(id, couponUUID, couponType, preferentialType, state, resUUID, ordrTime);
            ISort[] sorts = new ISort[] { sort };
            return CCouponPOManager.Instance.GetCouponWithTotalCountByPredicate(predicate, startIndex, count, sorts);
        }
        private IPredicate GetCouponsCondition(int? id, string couponUUID, List<int> couponType, List<int> preferentialType, List<int> state, string resUUID, DateTime? ordrTime)
        {
            List<IPredicate> predicates = new List<IPredicate>();
            predicates.Add(PredicateFactory.Equal<CCommodityPropertyPO>(t => t.IsDelete, false));
            if (id.HasValue)
            {
                predicates.Add(PredicateFactory.Equal<CCouponPO>(t => t.Id, id));
            }
            if (!string.IsNullOrWhiteSpace(couponUUID))
            {
                predicates.Add(PredicateFactory.Equal<CCouponPO>(t => t.CouponUUID, couponUUID));
            }
            if ((couponType != null) && (couponType.Count > 0))
            {
                predicates.Add(PredicateFactory.In<CCouponPO>(t => t.CouponType, couponType.ToArray()));
            }
            if ((preferentialType != null) && (preferentialType.Count > 0))
            {
                predicates.Add(PredicateFactory.In<CCouponPO>(t => t.PreferentialType, preferentialType.ToArray()));
            }
            if ((state != null) && (state.Count > 0))
            {
                predicates.Add(PredicateFactory.In<CCouponPO>(t => t.State, state.ToArray()));
            }
            if (!string.IsNullOrWhiteSpace(resUUID))
            {
                predicates.Add(PredicateFactory.Equal<CCouponPO>(t => t.ResUUID, resUUID));
            }
            if (ordrTime.HasValue)
            {
                IPredicate[] predicates1 = new IPredicate[2];
                predicates1[0] = PredicateFactory.LessEqual<CCouponPO>(t => t.EffectiveStartTime, ordrTime);
                predicates1[1] = PredicateFactory.Equal<CCouponPO>(t => t.EffectiveStartTime, null);
                predicates.Add(PredicateFactory.Or(predicates1));
                IPredicate[] predicates2 = new IPredicate[2];
                predicates2[0] = PredicateFactory.GreaterEqual<CCouponPO>(t => t.EffectiveEndTime, ordrTime);
                
                predicates2[1] = PredicateFactory.Equal<CCouponPO>(t => t.EffectiveEndTime, null);
                predicates.Add(PredicateFactory.Or(predicates2));
            }

            return PredicateFactory.And(predicates.ToArray());
        }
        public List<CCouponDTO> GetNoUseCouponDTO(string resUUID, string memberUUID)
        {
            if (string.IsNullOrWhiteSpace(memberUUID))
            {
                throw new BusinessException("会员UUID不能为空");
            }
            List<CCouponDTO> list = new List<CCouponDTO>();
            int? id = null;
            List<int> state = new List<int> { 1 };
            Core.Data.QueryResult<CCouponDTO> resultFree = this.GetCouponDTOs(id, null, null, null, state, null, new DateTime?(DateTime.Now), 1, 1000);
            if (((resultFree != null) && (resultFree.Items != null)) && (resultFree.Items.Length != 0))
            {
                if (resultFree.Items.Count<CCouponDTO>(x=>string.IsNullOrWhiteSpace(x.ResUUID)) > 0)
                {
                    list.AddRange(resultFree.Items.Where(x=>string.IsNullOrWhiteSpace(x.ResUUID)));
                }
            }
            
            if (!string.IsNullOrWhiteSpace(resUUID))
            {
                id = null;
                List<int> list2 = new List<int> { 1 };
                Core.Data.QueryResult<CCouponDTO> resultRes = this.GetCouponDTOs(id, null, null, null, list2, resUUID, new DateTime?(DateTime.Now), 1, 1000);
                if (((resultRes != null) && (resultRes.Items != null)) && (resultRes.Items.Length > 0))
                {
                    CCouponDTO[] items = resultRes.Items;
                    for (int i = 0; i < items.Length; i++)
                    {
                        CCouponDTO item = items[i];
                        if ((item != null) && (list.Count<CCouponDTO>(x => (x.Id == item.Id)) <= 0))
                        {
                            list.Add(item);
                        }
                    }
                }
            }
            List<COrderPO> listOrder = COrderManager.Instance.GetAllOrders(null, null, null, memberUUID, null, null, null, null, null, null, null, null, null, null, null);
            if ((listOrder != null) && (listOrder.Count > 0))
            {
                List<CCouponDTO> listRe = list.Where<CCouponDTO>(x=>x.CouponType == 1).ToList<CCouponDTO>();
                if ((listRe != null) && (listRe.Count > 0))
                {
                    listRe.ForEach(delegate (CCouponDTO x) {
                        list.Remove(x);
                    });
                }
            }
            //List<string> listCouponUUID = new List<string>();
            //if (listOrder != null)
            //{
            //    listCouponUUID = listOrder.Select<COrderPO, string>(x=> x.CouponUUID).Distinct<string>().ToList<string>();
            //}
            //(from x in list
            // where listCouponUUID.Contains(x.CouponUUID)
            // select x).Distinct<CCouponDTO>().ToList<CCouponDTO>().ForEach(delegate (CCouponDTO item) {
            //     list.Remove(item);
            // });
            return list;
        }
        public List<CCouponDTO> GetNoUseCouponDTOById(int resId, int memberId)
        {
            CRestaurantPO restaurantPO = CRestaurantManager.Instance.GetById(resId);
            if (restaurantPO == null)
            {
                throw new BusinessException("此餐厅不存在");
            }
            CMemberWeiXinPO memberWeiXinPO = CMemberWeiXinManager.Instance.GetById(memberId);
            if (memberWeiXinPO == null)
            {
                throw new BusinessException("此微信用户不存在");
            }
            return this.GetNoUseCouponDTO(restaurantPO.ResUUID, memberWeiXinPO.MemberUUID);

        }
        public decimal GetPreferentialMomey(int quantity, int? preferentialQuantity, decimal? preferentialProportion, decimal momey)
        {
            decimal preferentialMomey = new decimal();
            if (quantity > 0)
            {
                int? nullable;
                int num2;
                decimal? nullable2;
                if (!preferentialQuantity.HasValue || (((nullable = preferentialQuantity).GetValueOrDefault() <= (num2 = 0)) ? nullable.HasValue : false))
                {
                    return preferentialMomey;
                }
                if (!preferentialProportion.HasValue || (((nullable2 = preferentialProportion).GetValueOrDefault() >= 100) ? nullable2.HasValue : false))
                {
                    return preferentialMomey;
                }
                for (int loop = 1; loop <= quantity; loop++)
                {
                    int num4 = loop;
                    nullable = num4 % preferentialQuantity;
                    num2 = 0;
                    if ((nullable.GetValueOrDefault() == num2) ? nullable.HasValue : false)
                    {
                        preferentialMomey += momey * ((100-preferentialProportion.Value) / 100M);
                    }
                }
            }
            return preferentialMomey;

        }
        public int Update(CCouponPO po, string operatorName)
        {
            using (IEntityContext entityContext = CCouponPOManager.Instance.CreateEntityContext())
            {
                return this.UpdateWithContext(entityContext, po, operatorName);
            }
        }
        public int UpdateWithContext(IEntityContext entityContext, CCouponPO po, string operatorName)
        {
            decimal? preferential;
            decimal num;
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
            if (string.IsNullOrWhiteSpace(po.CouponUUID))
            {
                po.CouponUUID = Guid.NewGuid().ToString();
            }
            if ((po.PreferentialType != 1) && (po.PreferentialType != 2))
            {
                throw new BusinessException("优惠类型不正确");
            }
            if (!po.Preferential.HasValue)
            {
                throw new BusinessException("优惠金额未设定");
            }
            if (po.PreferentialType == 2)
            {
                preferential = po.Preferential;
                num = new decimal();
                if (((preferential.GetValueOrDefault() < num) ? preferential.HasValue : false) || (((preferential = po.Preferential).GetValueOrDefault() > (num = 10)) ? preferential.HasValue : false))
                {
                    throw new BusinessException("优惠比例不合理");
                }
            }
            else if (po.PreferentialType == 1)
            {
                preferential = po.Preferential;
                num = new decimal();
                if ((preferential.GetValueOrDefault() < num) ? preferential.HasValue : false)
                {
                    throw new BusinessException("优惠额度不合理");
                }
            }
            po.UpdateTime = DateTime.Now;
            po.UpdateName = operatorName;
            object oldObject = base.DtoToPo(base.PoToDto(this.GetByIdWithContext(entityContext, po.Id)));
            CCouponPOManager.Instance.UpdateWithContext(entityContext, po);
            COperationLogManager.Instance.AddOperationLogWithContext(entityContext, po.Id, EnumOperationLogType.CCoupon, EnumOperationLogAction.Update, oldObject, po, operatorName);
            return po.Id;

        }
    }
}