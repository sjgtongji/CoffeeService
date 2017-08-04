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
    public class CBannerManagerTests
    {
        [TestMethod()]
        public void AddCBannerTest()
        {
            try
            {
                AddCBannerDTO cBanner = new AddCBannerDTO()
                {
                    BannerLink = "BannerLink",
                    ImgURL = "ImgURL",
                    Name = "Name",
                    Sort = 1
                };
                CBannerManager.Instance.AddCBanner(cBanner, "测试");

                UpdateCBannerDTO updateBanner = new UpdateCBannerDTO()
                {
                    BannerLink = "BannerLink1",
                    Id = 1,
                    ImgURL = "ImgURL1",
                    Name = "Name1",
                    Sort = 1
                };

                CBannerManager.Instance.UpdateCBanner(updateBanner, "sdfsdf");
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
          
            Assert.Fail();
        }
    }
}