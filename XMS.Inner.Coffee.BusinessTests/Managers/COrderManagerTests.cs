using Microsoft.VisualStudio.TestTools.UnitTesting;
using XMS.Inner.Coffee.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMS.Core.PayCenter.Model;

namespace XMS.Inner.Coffee.Business.Tests
{
    [TestClass()]
    public class COrderManagerTests
    {
        [TestMethod()]
        public void ProcessPayNotifyTest()
        {
            try
            {

            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }

            Assert.Fail();
        }

        [TestMethod()]
        public void SendOrderSuccessMessageTest()
        {
            HttpHelper.SendSms("15026648702", string.Format("门店(222)有新的订单（金额：￥22）。22预订了2,请在2之间送达2。联系方式：22 地址：2222"));
            Assert.Fail();
        }
    }
}