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
    public class CMemberAddressManagerTests
    {
        [TestMethod()]
        public void GetLastMemberAddressTest()
        {
            try
            {
                CMemberAddressManager.Instance.GetLastMemberAddress(1);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
          
        }
    }
}