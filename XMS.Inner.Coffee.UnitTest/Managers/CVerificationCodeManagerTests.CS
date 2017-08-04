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
    public class CVerificationCodeManagerTests
    {
        [TestMethod()]
        public void CreateVerificationCodeTest()
        {
            //CVerificationCodeManager.Instance.CreateVerificationCode(2, "15026648702");
            CVerificationCodeManager.Instance.CheckCode(2, "402904");
            Assert.Fail();
        }
    }
}