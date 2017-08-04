using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMS.Core;
using XMS.Core.Data;
using XMS.Inner.Coffee.Data;
using XMS.Inner.Coffee.Model;
using XMS.Core.PayCenter.Model;

namespace XMS.Inner.Coffee.Business
{
    public class COrderManager : DataMapperManagerBase<COrderPO, COrderDTO>, IManagerBase<COrderPO>
    {
        public static readonly COrderManager Instance = new COrderManager();
        private COrderManager() { }

        public bool DeleteOrder(int orderId, int memberWeiXinId)
        {
            COrderPO order = GetById(orderId);
            if (order == null)
                throw new BusinessException("订单不存在");
            CMemberWeiXinPO memberWeiXinPO = CMemberWeiXinManager.Instance.GetById(memberWeiXinId);
            if(memberWeiXinPO == null)
                throw new BusinessException("微信会员不存在");
            if (memberWeiXinPO.MemberUUID != order.MemberUUID)
                throw new BusinessException("会员和订单不一致");

            order.IsDelete = true;
            COrderManager.Instance.Update(order, memberWeiXinPO.Id.ToString());
            return true;
        }
            
        public bool UpdateOrderInfo(int id, int orderState, string managerRemark, string operatorName)
        {
            if (orderState < 0 || orderState > 4)
                throw new BusinessException("订单状态不正确");
            if (id < 0)
                throw new BusinessException("订单状态不正确");
            COrderPO orderPO =  GetById(id);
            if (orderPO == null)
                throw new BusinessException("此订单不存在");
            orderPO.OrderState = orderState;
            orderPO.ManagerRemark = managerRemark;
            Update(orderPO, operatorName);
            return true;
        }

        //处理支付页返回的支付通知 （如：支付宝支付成功后，会跳转到业务系统页面）
        public Model.PayNotifyResult ProcessPayNotify(Model.ProcessPayNotifyRequestDto processPayNotifyRequestDto)
        {
           
            if (processPayNotifyRequestDto == null)
                throw new ArgumentNullException("ProcessPayNotifyRequestDto");
            if (string.IsNullOrWhiteSpace(processPayNotifyRequestDto.PayOrderId))
                throw new ArgumentNullException("ProcessPayNotifyRequestDto.PayOrderId");
            XMS.Core.ReturnValue<PayOrderDto> payOrderUrl = WCFClient.CoffeePayCenterService.FindPayOrderById(processPayNotifyRequestDto.PayOrderId);
            if (payOrderUrl.Code != 200)
                throw new BusinessException(payOrderUrl.RawMessage);
            if (payOrderUrl.Value == null)
                throw new BusinessException("订单不存在");

            Model.PayNotifyResult payNotifyResult = new Model.PayNotifyResult()
            {
                ErrorMessage = string.Empty,
                HasError = false,
                PayOrderId = processPayNotifyRequestDto.PayOrderId,
                Remark = string.Empty
            };
            if (payOrderUrl.Value.State == PayOrderState.Failed || payOrderUrl.Value.State == PayOrderState.Success)
            {
                return payNotifyResult;
            }

            Core.Data.QueryResult<COrderPO> orderResult = COrderManager.Instance.GetOrders(null, payOrderUrl.Value.OrderId, null, null, null, null, null, null, null, null, null, null, null, null, null, 1, 1);
            if(orderResult.TotalCount == 0)
                throw new BusinessException("订单不存在");

            XMS.Core.Container.LogService.Info("ProcessPayNotify3");

            if (orderResult.Items[0].PayStatus != 0)
                return payNotifyResult;

            XMS.Core.ReturnValue<Core.PayCenter.Model.PayNotifyResult> notifyResult = WCFClient.CoffeePayCenterService.ProcessPayNotify(new Core.PayCenter.Model.ProcessPayNotifyRequestDto()
            {
                Parameters = processPayNotifyRequestDto.Parameters,
                PayMode = processPayNotifyRequestDto.PayMode,
                PayOrderId = processPayNotifyRequestDto.PayOrderId,
                ProductId = processPayNotifyRequestDto.ProductId,
                SaleChannelApplicationId = processPayNotifyRequestDto.SaleChannelApplicationId
            });
            if (notifyResult.Code != 200)
            {
                XMS.Core.Container.LogService.Error(notifyResult.RawMessage);
                payNotifyResult.HasError = true;
                payNotifyResult.ErrorMessage = "提交回调结果时错误";
                return payNotifyResult;
            }
            XMS.Core.Container.LogService.Info("ProcessPayNotify5");
            if (notifyResult.Value == null)
            {
                payNotifyResult.HasError = true;
                payNotifyResult.ErrorMessage = "提交回调结果时错误";
                return payNotifyResult;
            }
            XMS.Core.Container.LogService.Info("ProcessPayNotify6");
            payNotifyResult.HasError = notifyResult.Value.HasError;
            payNotifyResult.ErrorMessage = notifyResult.Value.ErrorMessage;
            payNotifyResult.Remark = notifyResult.Value.Remark;

            if (payNotifyResult.HasError == true)
                return payNotifyResult;

            XMS.Core.Container.LogService.Info("ProcessPayNotify7");
            COrderPO corderPO = orderResult.Items[0];
            corderPO.PayStatus = 2;
            corderPO.PayType = 1;
            corderPO.PayDateTime = DateTime.Now;
            corderPO.PayMomey = payOrderUrl.Value.OrderAmount / 100;
            Update(corderPO, "支付回调");

            #region 给用户送积分
            SendIntegral(corderPO);
            #endregion

            SendOrderSuccessMessage(corderPO);

            return payNotifyResult;
        }

        /// <summary>
        /// 给用户送积分（根据订单订单金额）
        /// </summary>
        /// <param name="corderPO"></param>
        public void SendIntegral(COrderPO corderPO)
        {
            if (corderPO == null)
                return;
            CIntegralRecordPO integralRecord = new CIntegralRecordPO()
            {
                CreateName = corderPO.UpdateName,
                CreateTime = DateTime.Now,
                Integral = corderPO.OrderMomey,
                IsDelete = false,
                OrderId = corderPO.OrderUUID.ToString(),
                Type = 1,
                UpdateName = corderPO.UpdateName,
                UpdateTime = DateTime.Now,
            };

            Core.Data.QueryResult<CMemberWeiXinPO> memberWeiXinResult = CMemberWeiXinManager.Instance.GetMemberWeiXins(null, corderPO.MemberUUID, null, null, null, 1, 1);
            if (memberWeiXinResult.Items == null || memberWeiXinResult.Items.Length == 0)
                return;

            CMemberWeiXinPO memberWeiXinPO = memberWeiXinResult.Items[0];
            memberWeiXinPO.Integral = (memberWeiXinPO.Integral.HasValue ? memberWeiXinPO.Integral.Value : 0) + integralRecord.Integral;

            CIntegralRecordManager.Instance.Add(integralRecord, "系统");


            #region 更新优惠卷状态
            if (string.IsNullOrWhiteSpace(corderPO.CouponUUID) || !corderPO.CouponUUID.StartsWith("CouponDetail-"))
            {
                CMemberWeiXinManager.Instance.Update(memberWeiXinPO, "系统");
                return;
            }

            List<string> listValue = corderPO.CouponUUID.Split(new char[] { '-' }).ToList();
            if (listValue.Count != 3)
            {
                CMemberWeiXinManager.Instance.Update(memberWeiXinPO, "系统");
                return;
            }

            CCashCouponDetailPO cashCouponDetailPO = CCashCouponDetailManager.Instance.GetById(int.Parse(listValue[1]));
            if (cashCouponDetailPO == null)
            {
                CMemberWeiXinManager.Instance.Update(memberWeiXinPO, "系统");
                return;
            }


            cashCouponDetailPO.UseState = 1;
            cashCouponDetailPO.UseTime = DateTime.Now;
            CCashCouponDetailManager.Instance.Update(cashCouponDetailPO, "支付回调");

            memberWeiXinPO.Integral = (memberWeiXinPO.Integral.HasValue ? memberWeiXinPO.Integral.Value : 0) - cashCouponDetailPO.SaleAmount;
            CMemberWeiXinManager.Instance.Update(memberWeiXinPO, "系统");
            #endregion
        }
        public bool SendOrderSuccessMessage(COrderPO corderPO)
        {
            try
            {
                Core.Data.QueryResult<COrderCommodityRelationPO> relationResult = COrderCommodityRelationManager.Instance.GetCOrderCommodityRelations(null, new List<string> { corderPO.OrderUUID }, null, null, 1, 1000);
                if (relationResult == null || relationResult.Items == null || relationResult.Items.Length == 0)
                    throw new BusinessException("未获取到预订的商品信息");

                Core.Data.QueryResult<CMemberAddressPO> memberAddressResult = CMemberAddressManager.Instance.GetMemberAddress(null, corderPO.MemberAddressUUID, null, null, null, null, null, null, 1, 1);
                if (memberAddressResult == null || memberAddressResult.Items == null || memberAddressResult.Items.Length == 0)
                    throw new BusinessException("未获取到订单地址信息");
                List<string> listInfo = relationResult.Items.Select(x => x.ChineseName + (string.IsNullOrWhiteSpace(x.ChinesePropertyName) ? string.Empty : "(" + x.ChinesePropertyName + ")") + "X" + x.Quantity).ToList();
                CMemberAddressPO memberAddress = memberAddressResult.Items[0];

                Core.Data.QueryResult<CRestaurantPO> restResult = CRestaurantManager.Instance.GetRestaurants(new List<string> { corderPO.ResUUID }, null, null, null, null, 1, 1, null, null, null);
                if (restResult.Items != null && restResult.Items.Length > 0)
                {
                    if (!string.IsNullOrWhiteSpace(restResult.Items[0].ContactNumber))
                    {
                        List<string> phones = restResult.Items[0].ContactNumber.Split(new char[] { ';' }).ToList();
                        phones = phones.Where(x => x.IsMobilePhone()).Distinct().ToList();
                        phones.ForEach(x =>
                        {
                            HttpHelper.SendSms(x, string.Format("门店({0})有新的订单（金额：￥{8}）。{1}{2}预订了{3},请在{4}之间送达{5}。联系方式：{6} 地址：{7}",
                                restResult.Items[0].Name, memberAddress.Name, memberAddress.Sex, string.Join(",", listInfo),
                                corderPO.DeliveryMinTime.Value.ToString("yyyy-MM-dd HH:mm") + "-" + corderPO.DeliveryMaxTime.Value.ToString("HH:mm"),
                                 string.IsNullOrWhiteSpace(corderPO.Remark) ? string.Empty : ("(备注：" + corderPO.Remark + ")"),
                                 memberAddress.Telephone, corderPO.DeliveryAddress, corderPO.PayMomey.HasValue ? corderPO.PayMomey.Value.ToString("#0.00") : string.Empty));
                        });
                    }
                }
                else
                {
                    XMS.Core.Container.LogService.Info(string.Format("订单ID:{0} 未获取商店无法发送短信", corderPO.Id));
                }
            }
            catch (Exception ex)
            {
                XMS.Core.Container.LogService.Info(string.Format("订单ID:{0} 发送短信异常：{1}", corderPO.Id, ex.Message));
                return false;
            }

            return true;
        }

        public AddOrderResultDTO GetPayInfo(int orderId, int memberId)
        {
            AddOrderResultDTO addOrderResult = new AddOrderResultDTO();
            if (orderId <= 0)
                throw new BusinessException("订单ID不正确");
            if (memberId <= 0)
                throw new BusinessException("会员ID不正确");
            CMemberWeiXinPO memberWeiXinPO = CMemberWeiXinManager.Instance.GetById(memberId);
            if (memberWeiXinPO == null)
                throw new BusinessException("此用户不存在");
            COrderDTO orderDto = PoToDto(COrderManager.Instance.GetById(orderId));
            if (orderDto == null)
                throw new BusinessException("此订单不存在");
            if (orderDto.MemberUUID != memberWeiXinPO.MemberUUID)
                throw new BusinessException("此订单不属于此用户");
            if (!orderDto.PayMomey.HasValue)
                throw new BusinessException("支付金额未设定");

            addOrderResult.Order = orderDto;
            Dictionary<string, string> dic = GetPayURL(orderDto.OrderUUID, orderDto.PayMomey.Value, memberId, memberWeiXinPO.WeiXinOpenId);
            if (dic == null)
                addOrderResult.Succ = false;
            else
            {
                addOrderResult.PayInfo = new PayInfoDTO()
                {
                    PayUrl = dic
                };
                addOrderResult.Succ = true;
            }
            return addOrderResult;
        }


        /// <summary>
        /// 获取订单详情
        /// </summary>
        /// <param name="id"></param>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public COrderDTO GetOrderInfo(int id, int memberId)
        {
            if (id <= 0)
                throw new BusinessException("订单ID不正确");
            if (memberId <= 0)
                throw new BusinessException("会员ID不正确");
            CMemberWeiXinPO memberWeiXinPO = CMemberWeiXinManager.Instance.GetById(memberId);
            if (memberWeiXinPO == null)
                throw new BusinessException("此用户不存在");
            COrderDTO orderDto = PoToDto(COrderManager.Instance.GetById(id));
            if (orderDto == null)
                throw new BusinessException("此订单不存在");
            if (orderDto.MemberUUID != memberWeiXinPO.MemberUUID)
                throw new BusinessException("此订单不属于此用户");
            Core.Data.QueryResult<COrderCommodityRelationDTO> orderCommodityRelationResult = COrderCommodityRelationManager.Instance.GetCOrderCommodityRelationDtos(null, new List<string> { orderDto.OrderUUID }, null, null, 1, 1000);
            if (orderCommodityRelationResult == null || orderCommodityRelationResult.Items == null || orderCommodityRelationResult.Items.Length == 0)
                return orderDto;
            orderDto.ListCOrderCommodityRelation = orderCommodityRelationResult.Items.ToList();
            Core.Data.QueryResult<CMemberAddressPO> addressQuery = CMemberAddressManager.Instance.GetMemberAddress(null, orderDto.MemberAddressUUID, null, null, null, null, null, null, 1, 1);
            if (addressQuery == null || addressQuery.Items == null || addressQuery.Items.Length == 0)
                throw new BusinessException("未获取到此订单地址");
            orderDto.Address = CMemberAddressManager.Instance.PoToDto(addressQuery.Items[0]);

            if (orderDto.DistributionId.HasValue && orderDto.DistributionId.Value > 0)
            {
                CDistributionManagerPO distributionManager = CDistributionManagerManager.Instance.GetById(orderDto.DistributionId.Value);
                if (distributionManager == null)
                    throw new BusinessException("骑手不存在");
                orderDto.DistributionMobile = distributionManager.Mobile;
                orderDto.DistributionName = distributionManager.Name;
            }
            return orderDto;
        }

        public AddOrderResultDTO AddOrder(List<AddOrderDTO> addOrders)
        {
            AddOrderResultDTO addOrderResult = new AddOrderResultDTO();
            if (addOrders == null || addOrders.Count == 0)
                throw new ArgumentNullException("订单信息为空，请确认");
            if (addOrders[0].ResId <= 0)
                throw new ArgumentNullException("餐厅ID必须设定");
            if (addOrders[0].MemberId <= 0)
                throw new ArgumentNullException("会员ID为空");
            if (addOrders[0].MemberAddressId <= 0)
                throw new ArgumentNullException("地址UUID必须设定");
            //if (addOrders[0].DeliveryType == 0)
            //{
            //    if (!addOrders[0].DeliveryMaxTime.HasValue)
            //        throw new ArgumentNullException("最大配送时间必须设定");
            //    if (!addOrders[0].DeliveryMinTime.HasValue)
            //        throw new ArgumentNullException("最小配送时间必须设定");

            //    if (addOrders[0].DeliveryMaxTime < addOrders[0].DeliveryMinTime)
            //}

            if (!addOrders[0].DeliveryMaxTime.HasValue)
                throw new ArgumentNullException("最大配送时间必须设定");
            if (!addOrders[0].DeliveryMinTime.HasValue)
                throw new ArgumentNullException("最小配送时间必须设定");

            if (addOrders[0].DeliveryMaxTime.Value < addOrders[0].DeliveryMinTime.Value)
                throw new BusinessException("最大送达时间一定大于最小送达时间");
            if (addOrders[0].DeliveryMinTime.Value < DateTime.Now)
                throw new BusinessException("送达时间有误");

            CRestaurantPO restaurantPO = CRestaurantManager.Instance.GetById(addOrders[0].ResId);
            if (restaurantPO == null)
                throw new BusinessException("此餐厅不存在");

            Core.Data.QueryResult<CMemberWeiXinDTO> memberInfo = CMemberWeiXinManager.Instance.GetMemberWeiXinDTOs(addOrders[0].MemberId, null, null, null, null, 1, 1);
            if (memberInfo == null || memberInfo.Items == null || memberInfo.Items.Length <= 0)
                throw new ArgumentNullException("会员对象不存在");
            CMemberWeiXinDTO cMemberWeiXinDTO = memberInfo.Items[0];


            Core.Data.QueryResult<CMemberAddressDTO> memberAddress = CMemberAddressManager.Instance.GetMemberAddressDTO(addOrders[0].MemberAddressId, null, null, null, null, null, null, null, 1, 1);
            if (memberAddress == null || memberAddress.Items == null || memberAddress.Items.Length <= 0)
                throw new ArgumentNullException("会员地址对象不存在");
            CMemberAddressDTO cMemberAddressDTO = memberAddress.Items[0];

            CCouponPO cCouponPO = null;
            if (addOrders[0].CouponId > 0)
            {
                Core.Data.QueryResult<CCouponPO> cCoupon = CCouponManager.Instance.GetCoupons(addOrders[0].CouponId, null, null, null, null, null, null, 1, 1);
                if (cCoupon == null || cCoupon.Items == null || cCoupon.Items.Length == 0)
                    throw new BusinessException("优惠卷不可用");
                cCouponPO = cCoupon.Items[0];
            }
            else if (addOrders[0].CouponId < 0 && addOrders[0].CouponId >= -100)
            {
                GetAvailableCouponDTO getAvailableCoupon = new GetAvailableCouponDTO()
                {
                    ResId = restaurantPO.Id,
                    DeliveryType = addOrders[0].DeliveryType,
                    Commodity = new List<Commodity>(),
                    DeliveryMaxTime = addOrders[0].DeliveryMaxTime,
                    DeliveryMinTime= addOrders[0].DeliveryMinTime
                };
                addOrders.ForEach(x =>
                {
                    Commodity commodity = new Commodity()
                    {
                        CommodityId = x.CommodityId,
                        Quantity = x.Quantity,
                        SkuList = x.SkuList
                    };
                    getAvailableCoupon.Commodity.Add(commodity);
                });
                List<CCouponDTO> listCoupon = CCouponManager.Instance.GetAvailableCouponDTO(getAvailableCoupon, cMemberWeiXinDTO.Id);
                if (listCoupon != null && listCoupon.Count > 0)
                    cCouponPO = CCouponManager.Instance.DtoToPo(listCoupon.FirstOrDefault(item => item.Id == addOrders[0].CouponId));
            }
            else if (addOrders[0].CouponId < -100)
            {
                List<CCashCouponDetailPO> listDetail = CCashCouponDetailManager.Instance.GetCCashCouponDetailList(null, null, new List<int> { cMemberWeiXinDTO.Id }, new List<int> { 0 }, new List<int> { 1 }, null, null, null, null, null, null);
                CCashCouponDetailPO cashCouponDetailPO = listDetail.FirstOrDefault(x => (-100 - x.Id) == addOrders[0].CouponId);
                if (cashCouponDetailPO != null)
                {
                    cCouponPO = new CCouponPO
                    {
                        Id = addOrders[0].CouponId,
                        CouponType = 4
                    };
                    TimeSpan span = (TimeSpan)(DateTime.Now - DateTime.Parse("2016-1-1"));
                    cCouponPO.CouponUUID = "CouponDetail-" + cashCouponDetailPO.Id.ToString() + "-" + span.Minutes;
                    cCouponPO.EffectiveEndTime = cashCouponDetailPO.ValidStartTime;
                    cCouponPO.CreateName = "自动";
                    cCouponPO.CreateTime = DateTime.Now;
                    cCouponPO.EffectiveStartTime = cashCouponDetailPO.ValidEndTime;
                    cCouponPO.IsDelete = false;
                    cCouponPO.OfferAmount = decimal.Zero;
                    cCouponPO.Preferential = cashCouponDetailPO.ValAmount;
                    cCouponPO.PreferentialType = 1;
                    cCouponPO.Remark = cashCouponDetailPO.Description;
                    cCouponPO.ResUUID = restaurantPO.ResUUID;
                    cCouponPO.State = 1;
                    cCouponPO.UpdateName = "自动";
                    cCouponPO.UpdateTime = DateTime.Now;
                    cCouponPO.CouponName = cashCouponDetailPO.AssetName;
                }
            }


            //订单对象
            COrderPO orderPo = new COrderPO()
            {
                CityId = addOrders[0].CityId,
                CouponUUID = cCouponPO == null ? null : cCouponPO.CouponUUID,
                CreateName = cMemberWeiXinDTO.Name,
                CreateTime = DateTime.Now,
                DeliveryAddress = cMemberAddressDTO.Address + " " + cMemberAddressDTO.HouseNumber,
                DeliveryMaxTime = addOrders[0].DeliveryMaxTime,
                DeliveryMinTime = addOrders[0].DeliveryMinTime,
                DeliveryType = addOrders[0].DeliveryType,
                IsDelete = false,
                MemberAddressUUID = cMemberAddressDTO.MemberAddressUUID,
                MemberName = cMemberAddressDTO.Name,
                MemberUUID = cMemberWeiXinDTO.MemberUUID,
                OrderState = 0,
                OrderTime = DateTime.Now,
                OrderUUID = System.Guid.NewGuid().ToString(),
                PayStatus = 0,
                //OrderMomey = orderMomey,
                Remark = addOrders[0].Remark,
                ResUUID = restaurantPO.ResUUID,
                ServerFee = restaurantPO.ServerFee,
                Telephone = cMemberAddressDTO.Telephone,
                UpdateName = cMemberWeiXinDTO.Name,
                UpdateTime = DateTime.Now,
                //PayMomey = payMomey,
                Quantity = addOrders.Sum(x => x.Quantity),
                IsOutOfTime = addOrders[0].IsOutOfTime,
                CouponName = cCouponPO == null ? string.Empty : cCouponPO.CouponName,
                OrderId = restaurantPO.Alphabet + DateTime.Now.ToString("yyyyMMdd") + 8.GetRandom()
            };

            //decimal orderMomey = restaurantPO.ServerFee;
            decimal orderMomey = 0;
            List<List<CCommodityWithPropertyPO>> listListCommodityWithProperty = new List<List<CCommodityWithPropertyPO>>();
            List<COrderCommodityRelationPO> listOrderCommodityRelation = new List<COrderCommodityRelationPO>();
            addOrders.ForEach(addOrder =>
            {
                decimal commodityMomey = 0;
                Core.Data.QueryResult<CCommodityPO> commodityInfo = CCommodityManager.Instance.GetCCommoditys(addOrder.CommodityId, null, null, null, null, 1, 1, null);
                if (commodityInfo == null || commodityInfo.Items == null || commodityInfo.Items.Length == 0)
                    throw new BusinessException("不存在此商品");
                CCommodityPO commodity = commodityInfo.Items[0];
                if (commodity.State != 1)
                    throw new BusinessException("此商品已下架");
                if (commodity.ResUUID != restaurantPO.ResUUID)
                    throw new BusinessException("此商品不属于此餐厅");

                List<CCommodityPropertyPO> listCommodityProperty = new List<CCommodityPropertyPO>();
                if (addOrder.SkuList != null && addOrder.SkuList.Count > 0)
                {
                    Core.Data.QueryResult<CCommodityPropertyPO> commodityList = CCommodityPropertyManager.Instance.GetCommodityPropertys(addOrder.SkuList, restaurantPO.ResUUID, null, new List<string> { commodity.CommodityUUID }, null, 1, addOrder.SkuList.Count);
                    if (commodityList == null || commodityList.Items == null)
                        throw new BusinessException("商品属性获取失败");
                    if (commodityList.Items.Length != addOrder.SkuList.Count)
                        throw new BusinessException("部分商品属性获取失败");
                    listCommodityProperty.AddRange(commodityList.Items);
                }

                //获取商品属性类别
                List<CPropertyCategoryPO> listCPropertyCategoryPO = new List<CPropertyCategoryPO>();
                if (listCommodityProperty != null && listCommodityProperty.Count > 0)
                {
                    List<string> listPropertyCategory = listCommodityProperty.Select(x => x.PropertyCategoryUUID).Distinct().ToList();
                    Core.Data.QueryResult<CPropertyCategoryPO> propertyCategoryResult = CPropertyCategoryManager.Instance.GetPropertyCategorys(null, listPropertyCategory, 1, listPropertyCategory.Count);
                    if (propertyCategoryResult == null || propertyCategoryResult.Items == null || propertyCategoryResult.Items.Length != listPropertyCategory.Count)
                        throw new BusinessException("选择的商品属性可能已下架");
                    listCPropertyCategoryPO.AddRange(propertyCategoryResult.Items);
                    listCPropertyCategoryPO = listCPropertyCategoryPO.OrderByDescending(x => x.Sort).ToList();
                }

                //组合商品（CCommodityWithProperty）
                List<string> listChinesePropertyName = new List<string>();
                List<string> listEnglishPropertyName = new List<string>();
                List<CCommodityWithPropertyPO> listCommodityWithProperty = new List<CCommodityWithPropertyPO>();
                if (listCommodityProperty == null || listCommodityProperty.Count == 0)
                {
                    CCommodityWithPropertyPO commodityWithPropertyPO = new CCommodityWithPropertyPO()
                    {
                        CommodityPropertyUUID = string.Empty,
                        CommodityUUID = commodity.CommodityUUID,
                        CommodityWithPropertyUUID = System.Guid.NewGuid().ToString(),
                        CreateName = cMemberWeiXinDTO.Name,
                        CreateTime = DateTime.Now,
                        IsDelete = false,
                        PropertyCategoryUUID = string.Empty,
                        Remark = string.Empty,
                        ResUUID = restaurantPO.ResUUID,
                        UpdateName = cMemberWeiXinDTO.Name,
                        UpdateTime = DateTime.Now,
                    };
                    listCommodityWithProperty.Add(commodityWithPropertyPO);
                }
                else
                {
                    string commodityWithPropertyUUID = System.Guid.NewGuid().ToString();
                    listCPropertyCategoryPO.ForEach(propertyCategory => {

                        List<CCommodityPropertyPO> listcommodityPropertyPO = listCommodityProperty.Where(po => po.PropertyCategoryUUID == propertyCategory.PropertyCategoryUUID).OrderByDescending(x => x.Sort).ToList();

                        listcommodityPropertyPO.ForEach(item =>
                        {
                            if (item == null)
                                return;
                            CCommodityWithPropertyPO commodityWithPropertyPO = new CCommodityWithPropertyPO()
                            {
                                CommodityPropertyUUID = item.CommodityPropertyUUID,
                                CommodityUUID = commodity.CommodityUUID,
                                CommodityWithPropertyUUID = commodityWithPropertyUUID,
                                CreateName = cMemberWeiXinDTO.Name,
                                CreateTime = DateTime.Now,
                                IsDelete = false,
                                PropertyCategoryUUID = item.PropertyCategoryUUID,
                                Remark = string.Empty,
                                ResUUID = restaurantPO.ResUUID,
                                UpdateName = cMemberWeiXinDTO.Name,
                                UpdateTime = DateTime.Now
                            };
                            listCommodityWithProperty.Add(commodityWithPropertyPO);

                            if (!string.IsNullOrWhiteSpace(item.ChineseName))
                                listChinesePropertyName.Add(item.ChineseName);
                            if (!string.IsNullOrWhiteSpace(item.EnglishName))
                                listEnglishPropertyName.Add(item.EnglishName);
                        });

                    });
                }

                commodityMomey = (commodity.Price.HasValue ? commodity.Price.Value : 0);
                commodityMomey = commodityMomey + listCommodityProperty.Sum(x => (x.AddPrice.HasValue ? x.AddPrice.Value : 0));
                orderMomey = orderMomey + commodityMomey * addOrder.Quantity;

                //订单商品关系(COrderCommodityRelation)
                COrderCommodityRelationPO orderCommodityRelationPO = new COrderCommodityRelationPO()
                {
                    CommodityWithPropertyUUID = listCommodityWithProperty[0].CommodityWithPropertyUUID,
                    CreateName = cMemberWeiXinDTO.CreateName,
                    CreateTime = DateTime.Now,
                    IsDelete = false,
                    OrderUUID = orderPo.OrderUUID,
                    Price = commodityMomey,
                    Quantity = addOrder.Quantity,
                    UpdateName = cMemberWeiXinDTO.CreateName,
                    UpdateTime = DateTime.Now,
                    ChineseName = commodity.ChineseName,
                    EnglishName = commodity.EnglishName,
                    CommodityUUID = commodity.CommodityUUID,
                    ChinesePropertyName = string.Join(";", listChinesePropertyName),
                    EnglishPropertyName = string.Join(";", listEnglishPropertyName)
                };
                listOrderCommodityRelation.Add(orderCommodityRelationPO);
                listListCommodityWithProperty.Add(listCommodityWithProperty);
            });

            decimal payMomey = orderMomey;
            if (cCouponPO != null)
            {
                if (cCouponPO.CouponType == 3 && cCouponPO.OfferAmount <= 0)
                    throw new BusinessException("满足优惠最低消费额设定有误（不能小于等于0）");
                if (cCouponPO.CouponType == 3 && cCouponPO.OfferAmount > payMomey)
                    throw new BusinessException("此订单未满足优惠");
                if (cCouponPO.CouponType == 1)
                {
                    List<COrderPO> allList = COrderManager.Instance.GetAllOrders(null, null, null, cMemberWeiXinDTO.MemberUUID, null, null, null, null, null, null, null, null, null, null, null);
                    if (allList != null && allList.Count > 0)
                        throw new BusinessException("此用户非新用户,不可以使用此优惠卷");
                }

                if (!cCouponPO.Preferential.HasValue)
                    throw new BusinessException("优惠额度未设定");

                if (cCouponPO.PreferentialType == 1)
                    payMomey = payMomey - cCouponPO.Preferential.Value;
                else if (cCouponPO.PreferentialType == 2)
                    payMomey = (payMomey * cCouponPO.Preferential.Value) / 10;

                payMomey = payMomey + restaurantPO.ServerFee;

                if (payMomey < 0)
                    payMomey = 0;
            }
            else
                payMomey = payMomey + restaurantPO.ServerFee;

            orderPo.OrderMomey = orderMomey;
            orderPo.PayMomey = decimal.Parse(payMomey.ToString("#0.00"));

            if (orderPo.PayMomey <= 0)
                throw new BusinessException("支付金额不能小于等于0");

            if (cCouponPO != null)
            {
                orderPo.Preferential = cCouponPO.Preferential;
                orderPo.PreferentialType = cCouponPO.PreferentialType;
            }

            //订单商品关系(COrderCommodityRelation)
            using (IEntityContext entityContext = COrderPOManager.Instance.CreateEntityContext())
            {
                try
                {
                    entityContext.BeginTransaction();
                    int orderId = COrderManager.Instance.AddWithContext(entityContext, orderPo, cMemberWeiXinDTO.Name);
                    if (orderId <= 0)
                        throw new BusinessException("添加订单错误");

                    listListCommodityWithProperty.ForEach(item =>
                    {
                        if (item == null || item.Count == 0)
                            return;
                        item.ForEach(x =>
                        {
                            if (x == null)
                                return;
                            int commodityWithPropertyId = CCommodityWithPropertyManager.Instance.AddWithContext(entityContext, x, cMemberWeiXinDTO.Name);
                            if (commodityWithPropertyId <= 0)
                                throw new BusinessException("添加组合商品错误");
                        });
                    });

                    listOrderCommodityRelation.ForEach(item =>
                    {
                        if (item == null)
                            return;
                        int orderCommodityRelationId = COrderCommodityRelationManager.Instance.AddWithContext(entityContext, item, cMemberWeiXinDTO.Name);
                        if (orderCommodityRelationId <= 0)
                            throw new BusinessException("添加订单商品关系错误");
                    });
                    entityContext.Commit();
                }
                catch (Exception ex)
                {
                    entityContext.Rollback();
                    throw ex;
                }
            }

            addOrderResult.Order = PoToDto(orderPo);

            Dictionary<string, string> dic = GetPayURL(orderPo.OrderUUID, payMomey, cMemberWeiXinDTO.Id, cMemberWeiXinDTO.WeiXinOpenId);
            if (dic == null)
                addOrderResult.Succ = false;
            else
            {
                addOrderResult.PayInfo = new PayInfoDTO()
                {
                    PayUrl = dic
                };
                addOrderResult.Succ = true;
            }
            return addOrderResult;
        }

        private Dictionary<string, string> GetPayURL(string orderUUID,decimal payMomey, int memberId, string weiXinOpenId)
        {
            Core.Data.QueryResult<COrderDTO> resultQuery = COrderManager.Instance.GetOrderDTOs(null, orderUUID, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, 1, 1);
            CRestaurantPO restaurant = null;
            COrderDTO order = null;
            if (resultQuery != null && resultQuery.Items != null && resultQuery.Items.Length > 0)
            {
                order = resultQuery.Items[0];
                Core.Data.QueryResult<CRestaurantPO> restResult = CRestaurantManager.Instance.GetRestaurants(new List<string> { resultQuery.Items[0].ResUUID }, null, null, null, null, 1, 1, null, null, null);
                if (restResult != null && restResult.Items != null && restResult.Items.Length > 0)
                    restaurant = restResult.Items[0];
            }

            GetPayUrlRequestDto objGetPayUrlRequestDto = new GetPayUrlRequestDto();
            objGetPayUrlRequestDto.OurOrderId = orderUUID;
            objGetPayUrlRequestDto.OrderAmount = payMomey * 100;
            objGetPayUrlRequestDto.Parameters = new Dictionary<string, string>();
            objGetPayUrlRequestDto.PayMode = 8;
            objGetPayUrlRequestDto.ProductId = AppSettingHelper.ProductId;
            objGetPayUrlRequestDto.SaleChannelApplicationId = AppSettingHelper.SaleChannelApplicationId;
            objGetPayUrlRequestDto.PayOrderId = orderUUID;
            objGetPayUrlRequestDto.OurOrderId = orderUUID;
            objGetPayUrlRequestDto.Parameters["notify_url"] = string.Format(AppSettingHelper.RemotePayWeiXinCallbackUrl, orderUUID, 8);
            objGetPayUrlRequestDto.Parameters["subject"] = string.Format("订单:{0}", order != null ? order.OrderId : string.Empty);
            objGetPayUrlRequestDto.Parameters["Body"] = string.Format("订单:{0}", order != null ? order.OrderId : string.Empty);
            objGetPayUrlRequestDto.Parameters["detail"] = string.Format("订单:{0}", order != null ? order.OrderId : string.Empty);
            objGetPayUrlRequestDto.Parameters["MemberId"] = memberId.ToString();
            objGetPayUrlRequestDto.Parameters["CashierCode"] = "";
            objGetPayUrlRequestDto.Parameters["openid"] = weiXinOpenId;
            objGetPayUrlRequestDto.Parameters["DeviceInfo"] = restaurant == null ? string.Empty : restaurant.Alphabet;

            ReturnValue<GetPayUrlResponseDto> resultWeiXin = WCFClient.CoffeePayCenterService.GetPayUrl(objGetPayUrlRequestDto);
            if (resultWeiXin.Code == 200 && resultWeiXin.Value != null)
            {
                return resultWeiXin.Value.dic;
            }
            else
            {
                XMS.Core.Container.LogService.Info(string.Format("获取支付信息错误 详细:{0}", resultWeiXin.RawMessage));
            }
            return null;
        }

        public List<COrderPO> GetAllOrders(int? id, string orderUUID, string resUUID, string memberUUID, string memberName, string telephone, DateTime? orderTime,
           List<int> orderState, List<int> payStatus, List<int> payType, List<int> deliveryType, string cityId,
           List<string> memberAddressUUID, List<string> couponUUID, List<int> preferentialType)
        {
            List<COrderPO> list = new List<COrderPO>();
            Core.Data.QueryResult<COrderPO> queryResult =  COrderManager.Instance.GetOrders(id, orderUUID, resUUID, memberUUID, memberName, telephone, orderTime,
                orderState, payStatus, payType, deliveryType, cityId, memberAddressUUID, couponUUID, preferentialType, 1, 1000);

            if (queryResult == null || queryResult.Items == null || queryResult.Items.Length == 0)
                return list;

            list.AddRange(queryResult.Items);

            if (queryResult.TotalCount <= 1000)
                return list;

            int count = 1000;
            while (true)
            {
                queryResult = COrderManager.Instance.GetOrders(id, orderUUID, resUUID, memberUUID, memberName, telephone, orderTime,
                orderState, payStatus, payType, deliveryType, cityId, memberAddressUUID, couponUUID, preferentialType, count + 1, 1000);
                if (queryResult == null || queryResult.Items == null || queryResult.Items.Length == 0)
                    break;
                list.AddRange(queryResult.Items);
                if (count + 1000 >= queryResult.TotalCount)
                    break;
                count = 1000 + count;
            }
            return list;
        }

        public Core.Data.QueryResult<COrderDTO> GetOrderDTOs(int? id, string orderUUID, string resUUID, string memberUUID, string memberName, string telephone, DateTime? minCreateTime, DateTime? MaxCreateTime,
            List<int> orderState, List<int> payStatus, List<int> payType, List<int> deliveryType, string cityId,
            List<string> memberAddressUUID, List<string> couponUUID, List<int> preferentialType, List<int> distributionId, List<int> assignCategory, List<int> messageStatus, int startIndex, int count, bool orderCommodityRelationFlag = false, bool memberAddressFlag = false)
        {
            Core.Data.QueryResult<COrderDTO> ctoQuery = new Core.Data.QueryResult<COrderDTO>();
            Core.Data.QueryResult<COrderPO> orderQuery = GetOrdersNew(id, orderUUID, resUUID, memberUUID, memberName, telephone, minCreateTime, MaxCreateTime, orderState, payStatus, payType, deliveryType, cityId, memberAddressUUID, couponUUID, preferentialType,
                distributionId, assignCategory, messageStatus, startIndex, count);
            ctoQuery.TotalCount = orderQuery.TotalCount;

            if (orderQuery.Items != null && orderQuery.Items.Length > 0)
            {
                ctoQuery.Items = PoToDtoList(orderQuery.Items.ToList()).ToArray();

                List<string> listOrderUUID = ctoQuery.Items.Select(x => x.OrderUUID).ToList();

                Core.Data.QueryResult<COrderCommodityRelationDTO> commodityQuery = COrderCommodityRelationManager.Instance.GetCOrderCommodityRelationDtos(null, listOrderUUID, null, null, 1, 1000);

               

                foreach (var item in ctoQuery.Items)
                {
                    if (orderCommodityRelationFlag == true)
                    {
                        if (commodityQuery.Items != null && commodityQuery.Items.Length > 0)
                        {
                            item.ListCOrderCommodityRelation = commodityQuery.Items.Where(item1 => item1.OrderUUID == item.OrderUUID).ToList();
                        }
                    }

                    if (memberAddressFlag == true)
                    {
                        Core.Data.QueryResult<CMemberAddressDTO> memberAddress = CMemberAddressManager.Instance.GetMemberAddressDTO(null, item.MemberAddressUUID, null, null, null, null, null, null, 1, 1);
                        if (memberAddress.Items != null && memberAddress.Items.Length > 0)
                            item.Address = memberAddress.Items[0];
                    }
                    if (item.DistributionId.HasValue)
                    {
                        CDistributionManagerPO distributionManager = CDistributionManagerManager.Instance.GetById(item.DistributionId.Value);
                        if (distributionManager != null)
                        {
                            item.DistributionName = distributionManager.Name;
                            item.DistributionMobile = distributionManager.Mobile;
                        }
                    }
                    
                }
            }

            if (ctoQuery.Items == null)
                ctoQuery.Items = new COrderDTO[] { };

            return ctoQuery;
        }


        public Core.Data.QueryResult<COrderPO> GetOrders(int? id, string orderUUID, string resUUID, string memberUUID, string memberName, string telephone, DateTime? orderTime,
            List<int> orderState, List<int> payStatus, List<int> payType, List<int> deliveryType, string cityId,
            List<string> memberAddressUUID, List<string> couponUUID, List<int> preferentialType, int startIndex, int count)
        {
            Sort sort = PredicateFactory.Sort("CreateTime", "desc");
            IPredicate predicate = this.GetOrdersCondition(id, orderUUID, resUUID, memberUUID, memberName, telephone, orderTime,
                orderState, payStatus, payType, deliveryType, cityId, memberAddressUUID, couponUUID, preferentialType);
            Core.Data.QueryResult<COrderPO> resultRest = COrderPOManager.Instance.GetOrderWithTotalCountByPredicate(predicate, startIndex, count, sort);
            return resultRest;
        }

        private IPredicate GetOrdersCondition(int? id, string orderUUID, string resUUID, string memberUUID, string memberName, string telephone, DateTime? orderTime,
            List<int> orderState, List<int> payStatus, List<int> payType, List<int> deliveryType, string cityId,
            List<string> memberAddressUUID, List<string> couponUUID, List<int> preferentialType)
        {
            List<IPredicate> predicates = new List<IPredicate>();

            #region 基本条件
            predicates.Add(PredicateFactory.Equal<COrderPO>(t => t.IsDelete, false));
            if (id.HasValue)
            {
                predicates.Add(PredicateFactory.Equal<COrderPO>(t => t.Id, id));
            }
            if (!string.IsNullOrWhiteSpace(orderUUID))
            {
                predicates.Add(PredicateFactory.Equal<COrderPO>(t => t.OrderUUID, orderUUID));
            }
            if (!string.IsNullOrWhiteSpace(resUUID))
            {
                predicates.Add(PredicateFactory.Equal<COrderPO>(t => t.ResUUID, resUUID));
            }
            if (!string.IsNullOrWhiteSpace(memberUUID))
            {
                predicates.Add(PredicateFactory.Equal<COrderPO>(t => t.MemberUUID, memberUUID));
            }
            if (!string.IsNullOrWhiteSpace(memberName))
            {
                predicates.Add(PredicateFactory.Like<COrderPO>(t => t.MemberName, "%" + memberName.ToSafeSQLLike() + "%"));
            }
            if (!string.IsNullOrWhiteSpace(telephone))
            {
                predicates.Add(PredicateFactory.Like<COrderPO>(t => t.Telephone, "%" + telephone.ToSafeSQLLike() + "%"));
            }
            if (orderTime.HasValue)
            {
                predicates.Add(PredicateFactory.GreaterEqual<COrderPO>(t => t.OrderTime, orderTime.Value.Date));
                predicates.Add(PredicateFactory.LessEqual<COrderPO>(t => t.OrderTime, orderTime.Value.Date.AddDays(1).AddMilliseconds(-1)));
            }
            if (orderState != null && orderState.Count > 0)
            {
                predicates.Add(PredicateFactory.In<COrderPO>(t => t.OrderState, orderState));
            }
            if (payStatus != null && payStatus.Count > 0)
            {
                predicates.Add(PredicateFactory.In<COrderPO>(t => t.PayStatus, payStatus));
            }
            if (payType != null && payType.Count > 0)
            {
                predicates.Add(PredicateFactory.In<COrderPO>(t => t.PayType, payType));
            }
            if (deliveryType != null && deliveryType.Count > 0)
            {
                predicates.Add(PredicateFactory.In<COrderPO>(t => t.DeliveryType, deliveryType));
            }
            if (!string.IsNullOrWhiteSpace(cityId))
            {
                predicates.Add(PredicateFactory.Equal<COrderPO>(t => t.CityId, cityId));
            }


            if (memberAddressUUID != null && memberAddressUUID.Count > 0)
            {
                predicates.Add(PredicateFactory.In<COrderPO>(t => t.MemberAddressUUID, memberAddressUUID));
            }
            if (couponUUID != null && couponUUID.Count > 0)
            {
                predicates.Add(PredicateFactory.In<COrderPO>(t => t.CouponUUID, couponUUID));
            }
            if (preferentialType != null && preferentialType.Count > 0)
            {
                predicates.Add(PredicateFactory.In<COrderPO>(t => t.PreferentialType, preferentialType));
            }
            #endregion

            #region 数量条件
            #endregion
            return PredicateFactory.And(predicates.ToArray());
        }

        public Core.Data.QueryResult<COrderPO> GetOrdersNew(int? id, string orderUUID, string resUUID, string memberUUID, string memberName, string telephone, DateTime? minCreateTime, DateTime? maxCreateTime,
    List<int> orderState, List<int> payStatus, List<int> payType, List<int> deliveryType, string cityId,
    List<string> memberAddressUUID, List<string> couponUUID, List<int> preferentialType, List<int> distributionId, List<int> assignCategory, List<int> messageStatus, int startIndex, int count)
        {
            Sort sort = PredicateFactory.Sort("CreateTime", "desc");
            IPredicate predicate = this.GetOrdersConditionNew(id, orderUUID, resUUID, memberUUID, memberName, telephone, minCreateTime, maxCreateTime,
                orderState, payStatus, payType, deliveryType, cityId, memberAddressUUID, couponUUID, preferentialType, distributionId, assignCategory, messageStatus);
            Core.Data.QueryResult<COrderPO> resultRest = COrderPOManager.Instance.GetOrderWithTotalCountByPredicate(predicate, startIndex, count, sort);
            return resultRest;
        }

        private IPredicate GetOrdersConditionNew(int? id, string orderUUID, string resUUID, string memberUUID, string memberName, string telephone, DateTime? minCreateTime, DateTime? maxCreateTime,
            List<int> orderState, List<int> payStatus, List<int> payType, List<int> deliveryType, string cityId,
            List<string> memberAddressUUID, List<string> couponUUID, List<int> preferentialType, List<int> distributionId,List<int> assignCategory, List<int> messageStatus)
        {
            List<IPredicate> predicates = new List<IPredicate>();

            #region 基本条件
            predicates.Add(PredicateFactory.Equal<COrderPO>(t => t.IsDelete, false));

            if (distributionId != null && distributionId.Count > 0)
            {
                predicates.Add(PredicateFactory.In<COrderPO>(t => t.DistributionId, distributionId));
            }
            if (assignCategory != null && assignCategory.Count > 0)
            {
                predicates.Add(PredicateFactory.In<COrderPO>(t => t.AssignCategory, assignCategory));
            }
            if (messageStatus != null && messageStatus.Count > 0)
            {
                predicates.Add(PredicateFactory.In<COrderPO>(t => t.MessageStatus, messageStatus));
            }

            if (id.HasValue)
            {
                predicates.Add(PredicateFactory.Equal<COrderPO>(t => t.Id, id));
            }
            if (!string.IsNullOrWhiteSpace(orderUUID))
            {
                predicates.Add(PredicateFactory.Equal<COrderPO>(t => t.OrderUUID, orderUUID));
            }
            if (!string.IsNullOrWhiteSpace(resUUID))
            {
                predicates.Add(PredicateFactory.Equal<COrderPO>(t => t.ResUUID, resUUID));
            }
            if (!string.IsNullOrWhiteSpace(memberUUID))
            {
                predicates.Add(PredicateFactory.Equal<COrderPO>(t => t.MemberUUID, memberUUID));
            }
            if (!string.IsNullOrWhiteSpace(memberName))
            {
                predicates.Add(PredicateFactory.Like<COrderPO>(t => t.MemberName, "%" + memberName.ToSafeSQLLike() + "%"));
            }
            if (!string.IsNullOrWhiteSpace(telephone))
            {
                predicates.Add(PredicateFactory.Like<COrderPO>(t => t.Telephone, "%" + telephone.ToSafeSQLLike() + "%"));
            }
            if (minCreateTime.HasValue)
            {
                predicates.Add(PredicateFactory.GreaterEqual<COrderPO>(t => t.CreateTime, minCreateTime.Value));
            }
            if (maxCreateTime.HasValue)
            {
                predicates.Add(PredicateFactory.LessEqual<COrderPO>(t => t.CreateTime, maxCreateTime.Value));
            }
            if (orderState != null && orderState.Count > 0)
            {
                predicates.Add(PredicateFactory.In<COrderPO>(t => t.OrderState, orderState));
            }
            if (payStatus != null && payStatus.Count > 0)
            {
                predicates.Add(PredicateFactory.In<COrderPO>(t => t.PayStatus, payStatus));
            }
            if (payType != null && payType.Count > 0)
            {
                predicates.Add(PredicateFactory.In<COrderPO>(t => t.PayType, payType));
            }
            if (deliveryType != null && deliveryType.Count > 0)
            {
                predicates.Add(PredicateFactory.In<COrderPO>(t => t.DeliveryType, deliveryType));
            }
            if (!string.IsNullOrWhiteSpace(cityId))
            {
                predicates.Add(PredicateFactory.Equal<COrderPO>(t => t.CityId, cityId));
            }


            if (memberAddressUUID != null && memberAddressUUID.Count > 0)
            {
                predicates.Add(PredicateFactory.In<COrderPO>(t => t.MemberAddressUUID, memberAddressUUID));
            }
            if (couponUUID != null && couponUUID.Count > 0)
            {
                predicates.Add(PredicateFactory.In<COrderPO>(t => t.CouponUUID, couponUUID));
            }
            if (preferentialType != null && preferentialType.Count > 0)
            {
                predicates.Add(PredicateFactory.In<COrderPO>(t => t.PreferentialType, preferentialType));
            }
            #endregion

            #region 数量条件
            #endregion
            return PredicateFactory.And(predicates.ToArray());
        }


        public int Add(COrderPO po, string operatorName)
        {
            using (IEntityContext entityContext = COrderPOManager.Instance.CreateEntityContext())
            {
                return AddWithContext(entityContext, po, operatorName);
            }
        }

        public int AddWithContext(IEntityContext entityContext, COrderPO po, string operatorName)
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
                po.OrderUUID = System.Guid.NewGuid().ToString();
            if (string.IsNullOrWhiteSpace(po.ResUUID))
                throw new BusinessException("餐厅UUID不能为空");
            if (string.IsNullOrWhiteSpace(po.MemberUUID))
                throw new BusinessException("会员UUID不能为空");
            if (string.IsNullOrWhiteSpace(po.MemberName))
                throw new BusinessException("订餐人不能为空");
            if (string.IsNullOrWhiteSpace(po.Telephone))
                throw new BusinessException("用户号码不能为空");
            if (po.OrderState != 0)
                throw new BusinessException("新增订单状态必须是未确认");
            if (po.DeliveryType != 0 && po.DeliveryType != 1)
                throw new BusinessException("配送方式不确认");
            if (string.IsNullOrWhiteSpace(po.DeliveryAddress))
                throw new BusinessException("送货地址不能为空");
            if (string.IsNullOrWhiteSpace(po.MemberAddressUUID))
                throw new BusinessException("会员地址UUID不能为空");
            if (!string.IsNullOrWhiteSpace(po.CouponUUID))
            {
                if (po.PreferentialType != 1 && po.PreferentialType != 2)
                    throw new BusinessException("优惠类型不正确");
                if (!po.Preferential.HasValue)
                    throw new BusinessException("优惠金额未设定");
            }

            po.IsDelete = false;
            po.CreateTime = DateTime.Now;
            po.CreateName = operatorName;
            po.UpdateTime = DateTime.Now;
            po.UpdateName = operatorName;

            COrderPOManager.Instance.AddWithContext(entityContext, po);
            COperationLogManager.Instance.AddOperationLogWithContext(entityContext,po.Id, EnumOperationLogType.COrder, EnumOperationLogAction.Add, null, po, operatorName);

            return po.Id;
        }

        public COrderPO GetById(int id)
        {
            return COrderPOManager.Instance.GetById(id);
        }

        public COrderPO GetByIdWithContext(IEntityContext entityContext, int id)
        {
            return COrderPOManager.Instance.GetByIdWithContext(entityContext, id);
        }

        public int Update(COrderPO po, string operatorName)
        {
            using (IEntityContext entityContext = COrderPOManager.Instance.CreateEntityContext())
            {
                return UpdateWithContext(entityContext, po, operatorName);
            }
        }

        public int UpdateWithContext(IEntityContext entityContext, COrderPO po, string operatorName)
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
                po.OrderUUID = System.Guid.NewGuid().ToString();
            if (string.IsNullOrWhiteSpace(po.ResUUID))
                throw new BusinessException("餐厅UUID不能为空");
            if (string.IsNullOrWhiteSpace(po.MemberUUID))
                throw new BusinessException("会员UUID不能为空");
            if (string.IsNullOrWhiteSpace(po.MemberName))
                throw new BusinessException("订餐人不能为空");
            if (string.IsNullOrWhiteSpace(po.Telephone))
                throw new BusinessException("用户号码不能为空");
            //if (po.OrderState != 0)
            //    throw new BusinessException("新增订单状态必须是未确认");
            if (po.DeliveryType != 0 && po.DeliveryType != 1)
                throw new BusinessException("配送方式不确认");
            if (string.IsNullOrWhiteSpace(po.DeliveryAddress))
                throw new BusinessException("送货地址不能为空");
            if (string.IsNullOrWhiteSpace(po.MemberAddressUUID))
                throw new BusinessException("会员地址UUID不能为空");
            if (!string.IsNullOrWhiteSpace(po.CouponUUID))
            {
                if (po.PreferentialType != 1 && po.PreferentialType != 2)
                    throw new BusinessException("优惠类型不正确");
                if (!po.Preferential.HasValue)
                    throw new BusinessException("优惠金额未设定");
            }

            po.UpdateTime = DateTime.Now;
            po.UpdateName = operatorName;

            object oldObject = this.DtoToPo(this.PoToDto(GetByIdWithContext(entityContext, po.Id)));
            COrderPOManager.Instance.UpdateWithContext(entityContext, po);
            COperationLogManager.Instance.AddOperationLogWithContext(entityContext,po.Id, EnumOperationLogType.COrder, EnumOperationLogAction.Update, oldObject, po, operatorName);

            return po.Id;
        }
    }
}
