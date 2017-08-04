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
    public class CPropertyCategoryManagerTests
    {
        [TestMethod()]
        public void AddOrUpdatetPropertyCategoryTest()
        {
            //AddOrUpdatetPropertyCategoryDTO addOrUpdatetPropertyCategoryDTO = new AddOrUpdatetPropertyCategoryDTO()
            //{
            //    ChineseName = "ChineseName1",
            //    EnableMultiple = true,
            //    EnglishName = "EnglishName1",
            //    Id = 8,
            //    IsDelete = false,
            //    ResId = 1,
            //    Sort = 12,
            //    Type = 0 
            //};

            //int id = CPropertyCategoryManager.Instance.AddOrUpdatetPropertyCategory(addOrUpdatetPropertyCategoryDTO, "操作人");
            //CPropertyCategoryManager.Instance.DeletePropertyCategory(8, "操作人");

            List<CPropertyCategoryDTO> list = CPropertyCategoryManager.Instance.GetPropertyCategoryDTO(1);
            Assert.Fail();
        }
    }
}