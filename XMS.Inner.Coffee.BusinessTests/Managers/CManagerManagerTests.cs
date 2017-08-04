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
    public class CManagerManagerTests
    {
        [TestMethod()]
        public void GetManagersTest()
        {
            try
            {
                Core.Data.QueryResult<CManagerPO> result = CManagerManager.Instance.GetManagers(null, "", "", null, 1, 1);
            }
            catch
            {
                string ee = string.Empty;
            }

           
            Assert.Fail();
        }
    }
}