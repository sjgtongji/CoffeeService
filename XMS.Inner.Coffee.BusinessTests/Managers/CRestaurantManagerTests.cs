using Microsoft.VisualStudio.TestTools.UnitTesting;
using XMS.Inner.Coffee.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMS.Inner.Coffee.Business.Tests
{
    [TestClass()]
    public class CRestaurantManagerTests
    {
        [TestMethod()]
        public void GetRestaurantByLoginTest()
        {
            CRestaurantManager.Instance.GetRestaurantByLogin("loginName", "password", "");
            Assert.Fail();
        }
    }
}