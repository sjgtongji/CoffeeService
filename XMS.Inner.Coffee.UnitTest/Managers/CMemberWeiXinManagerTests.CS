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
        public void AddOrUpdateDTOTest()
        {
            try
            {
                CMemberWeiXinDTO dto = new CMemberWeiXinDTO()
                {
                    Id = 0,
                    MemberUUID = "MemberUUID",
                    Name = "Name",
                    PhoneNumber = "PhoneNumber",
                    PlatformUserId = "PlatformUserId",
                    PlatformUserToken = "PlatformUserToken",
                    WeixinAccount = "WeixinAccount",
                    WeiXinOpenId = "WeiXinOpenId"
                };
                int id = CMemberWeiXinManager.Instance.AddOrUpdateDTO(dto, "operatorName");
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }

            Assert.Fail();
        }

        [TestMethod()]
        public void GetMemberWeiXinsTest()
        {
            //try
            //{
            //    Core.Data.QueryResult<CMemberWeiXinPO> result = CMemberWeiXinManager.Instance.GetMemberWeiXins("MemberUUID", "WeiXinOpenId", "PhoneNumber", "Name", 1, 1);
            //}
            //catch (Exception ex)
            //{
            //    string message = ex.Message;
            //}

            Assert.Fail();
        }

        [TestMethod()]
        public void AddOrUpdateDTOTest1()
        {
            CMemberWeiXinDTO dto = new CMemberWeiXinDTO()
            {
                CreateName = "CreateName",
                CreateTime = DateTime.Now,
                IsDelete = false,
                MemberUUID = System.Guid.NewGuid().ToString(),
                Name = "Name",
                PhoneNumber = "15026434702",
                PlatformUserId = "1",
                PlatformUserToken = "PlatformUserToken",
                UpdateName = "UpdateName",
                UpdateTime = DateTime.Now,
                WeixinAccount = "WeixinAccount",
                WeiXinOpenId = "WeiXinOpenId",
            };
            CMemberWeiXinManager.Instance.AddOrUpdateDTO(dto, "曹组");

            Core.Data.QueryResult<CMemberWeiXinDTO> resultCode = CMemberWeiXinManager.Instance.GetMemberWeiXinDTOs(null, "1c45d585-f852-4fe0-b8da-f3e2606d6317", "WeiXinOpenId", "15026434702", "Name", 1, 20);
            string message = string.Empty;
            Assert.Fail();
        }
    }
}