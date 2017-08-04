using Microsoft.VisualStudio.TestTools.UnitTesting;
using XMS.Inner.Coffee.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMS.Inner.Coffee.Model;
using XMS.Core;

namespace XMS.Inner.Coffee.Business.Tests
{
    [TestClass()]
    public class CCouponManagerTests
    {
        [TestMethod()]
        public void GetNoUseCouponDTOTest()
        {
            try
            {

                decimal sdsds = (decimal)0.16;
                decimal sdsdsss = decimal.Parse(sdsds.ToString("#0.00"));

                sdsds = sdsds * 10;

                Core.Data.QueryResult<CRestaurantPO> restResult = CRestaurantManager.Instance.GetRestaurants(new List<string> { "XXX" }, null, null, null, null, 1, 1, null);
                if (restResult.Items != null && restResult.Items.Length > 0)
                {
                    if (!string.IsNullOrWhiteSpace(restResult.Items[0].ContactNumber))
                    {
                        List<string> phones = restResult.Items[0].ContactNumber.Split(new char[] { ';' }).ToList();
                        phones = phones.Where(x => x.IsMobilePhone()).Distinct().ToList();
                        phones.ForEach(x =>
                        {
                            HttpHelper.SendSms(x, string.Format("门店({0})有新的订单。客户：{1} 地址：{2}", restResult.Items[0].Name, "测试", "地址"));
                        });
                    }
                }

                //HttpHelper.SendSms("15026648702", "您好，您本次的注册验证码是123456");

                //List<CCouponDTO> listCoupon = CCouponManager.Instance.GetNoUseCouponDTO("XXX", "1c45d585-f852-4fe0-b8da-f3e2606d6317");
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }

            Assert.Fail();
        }

        [TestMethod()]
        public void GetCouponDTOsTest()
        {
            Core.Data.QueryResult<CCouponDTO> couponDto = CCouponManager.Instance.GetCouponDTOs(2, "CouponUUID1", new List<int>() { 1 }, new List<int>() { 1 }, new List<int> { 1 }, "XXX", DateTime.Now, 1, 20);
            Assert.Fail();
        }

        [TestMethod()]
        public void AddOrUpdateCCouponDtoTest()
        {
            CCouponDTO couponDTO = new CCouponDTO()
            {
                CouponType = 1,
                CouponUUID = System.Guid.NewGuid().ToString(),
                CreateName = "CreateName",
                CreateTime = DateTime.Now,
                EffectiveEndTime = DateTime.Now.AddDays(-1),
                EffectiveStartTime = DateTime.Now.AddDays(100),
                IsDelete = false,
                OfferAmount = 0,
                Preferential = 1,
                PreferentialType = 1,
                Remark = "Remark",
                ResUUID = System.Guid.NewGuid().ToString(),
                State = 1,
                UpdateName = "UpdateName",
                UpdateTime = DateTime.Now,
                Id = 6
            };
            int CouponId = CCouponManager.Instance.AddOrUpdateCCouponDto(couponDTO, "operatorName");
            Assert.Fail();
        }

        [TestMethod()]
        public void GetNoUseCouponDTOTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetGetAvailableCouponDTOTest()
        {


            //GetAvailableCouponDTO availableCouponDTO = new GetAvailableCouponDTO()
            //{
            //    ResId = 1,
            //    DeliveryType = 1,
            //    commodity = new List<Commodity>().Add(new Commodity { commodityId = 1, quantity = 5, skuList = new List<int> { 1, 2, 3 } })

            //};
            //availableCouponDTO.Commodity.Add(new Commodity { CommodityId = 4, Quantity = 2, SkuList = null });
            //CCouponManager.Instance.GetAvailableCouponDTO(availableCouponDTO, 2);
            Assert.Fail();
        }

        [TestMethod()]
        public void GetAvailableCouponDTOTest()
        {
            ////{ ResId: 13, DeliveryType: 1, Commodity: { { CommodityId: 4, Quantity: 1, SkuList: { } } } }
            //ResId: 1, DeliveryType: 1, Commodity: {{CommodityId: 5, Quantity: 1, SkuList: {}}}}, memberId=2

            List<Commodity> list = new List<Commodity>();
            list.Add(new Commodity { CommodityId = 5, Quantity = 1 });
            GetAvailableCouponDTO getavai = new GetAvailableCouponDTO()
            {
                ResId = 1,
                DeliveryType = 1,
                Commodity = list
            };
            CCouponManager.Instance.GetAvailableCouponDTO(getavai, 2);
            //Assert.Fail();
        }

        [TestMethod()]
        public void GetAvailableCouponDTOTest1()
        {
            //GetAvailableCouponDTO getAvailableCoupon = new GetAvailableCouponDTO() {
            //     Commodity = new List<Commodity>
            //};
            //CCouponManager.Instance.GetAvailableCouponDTO();
            //Assert.Fail();
        }

        [TestMethod()]
        public void AddOrUpdateCouponTest()
        {
            try
            {
                AddOrUpdateCouponDTO addOrUpdateCouponDTO = new AddOrUpdateCouponDTO()
                {
                    CouponName = "CouponName",
                    CouponType = 2,
                    EffectiveEndTime = DateTime.Now.AddDays(1),
                    EffectiveStartTime = DateTime.Now.AddDays(-1),
                    IsDelete = false,
                    OfferAmount = 3,
                    Preferential = 4,
                    PreferentialType = 2,
                    Remark = "Remark",
                    State = 6,
                    ResId = 1,
                    Id = 7
                };
                CCouponManager.Instance.AddOrUpdateCoupon(addOrUpdateCouponDTO, "测试");
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            Assert.Fail();
        }

        [TestMethod()]
        public void GetCouponDTOsTest1()
        {
            Core.Data.QueryResult<CCouponDTO> queryResult = CCouponManager.Instance.GetCouponDTOs(null, null, null, null, null, null, null, 1, 10);


            Assert.Fail();
        }

        [TestMethod()]
        public void GetAvailableCouponDTOTest2()
        {
            Assert.Fail();
        }
    }
}