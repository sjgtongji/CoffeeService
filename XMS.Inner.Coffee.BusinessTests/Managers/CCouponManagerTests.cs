using Microsoft.VisualStudio.TestTools.UnitTesting;
using XMS.Inner.Coffee.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMS.Inner.Coffee.Model;
using XMS.Core.Data;

namespace XMS.Inner.Coffee.Business.Tests
{
    [TestClass()]
    public class CCouponManagerTests
    {
        [TestMethod()]
        public void GetAvailableCouponDTOTest()
        {
            try
            {
                GetAvailableCouponDTO getAvailableCoupon = new GetAvailableCouponDTO()
                {
                    Commodity = new List<Commodity>
                 {
                     //new Commodity { CommodityId=4, Quantity=1, SkuList = new List<int> { 376 } },
                     //new Commodity { CommodityId=4, Quantity=1, SkuList = new List<int> { 375 } },
                     //new Commodity { CommodityId=4, Quantity=1, SkuList = new List<int> { 376 } },
                     new Commodity { CommodityId=5, Quantity=1, SkuList = new List<int> { 374 } },
                 },
                    ResId = 1,
                    DeliveryType = 0,
                };
                CCouponManager.Instance.GetAvailableCouponDTO(getAvailableCoupon, 2);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }

            //CommodityId: 5, Quantity: 1, SkuList: { 374}

            Assert.Fail();
        }

        [TestMethod()]
        public void GetAvailableCouponDTOTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetRestaurantDTOByConditionTest()
        {
            QueryResult<CRestaurantDTO> restul = CRestaurantManager.Instance.GetRestaurantDTOByCondition(null, null, null, null, (decimal)31.238222, (decimal)121.468501, null, 1, 1000, false, new List<int> { 1 }, null, null);
            Assert.Fail();
        }
    }
}