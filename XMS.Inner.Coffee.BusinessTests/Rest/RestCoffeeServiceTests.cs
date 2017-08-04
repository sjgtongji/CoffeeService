using Microsoft.VisualStudio.TestTools.UnitTesting;
using XMS.Inner.Coffee.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMS.Inner.Coffee.Service.Tests
{
    [TestClass()]
    public class RestCoffeeServiceTests
    {
        [TestMethod()]
        public void GetOrderByResUUIDTest()
        {
            RestCoffeeService getOrderByResUUID = new RestCoffeeService() { };
            getOrderByResUUID.GetOrderByResUUID("NJXL", "0", 1, 20);
            Assert.Fail();
        }
    }
}