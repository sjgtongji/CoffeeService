using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XMS.Foundation.SMS.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            XMS.Foundation.SMS.Business.AccountManager.Instance.AddAccount("TestAccount", "admin");
        }
    }
}
