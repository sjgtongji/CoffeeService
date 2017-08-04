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
    public class FileManagerTests
    {
        [TestMethod()]
        public void UploadPicTest()
        {
            try
            {
                PhotoResult photoResult = FileManager.Instance.UploadPic(new Byte[] { 1, 1, 23, 1, 21, 2, 1 }, "ceshi.png", false);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }

        }
    }
}