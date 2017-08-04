using Microsoft.VisualStudio.TestTools.UnitTesting;
using XMS.Inner.Coffee.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMS.Inner.Coffee.Model;

namespace XMS.Inner.Coffee.Business.Tests
{
    [TestClass()]
    public class CMemberWeiXinManagerTests
    {
        [TestMethod()]
        public void GetMemberWeiXinsTest()
        {
            try
            {
                CMemberWeiXinPO CMemberWeiXinPO = new CMemberWeiXinPO() { };
                CMemberWeiXinManager.Instance.GetMemberWeiXins(null, null, "o9j1twZEAPDUCCRIwKa7Wgp6bDXE", null, null, 1, 1);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
           
            Assert.Fail();
        }
    }
}