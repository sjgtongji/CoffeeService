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
    public class BusinessHourWeekManagerTests
    {
        [TestMethod()]
        public void GetEffectivePeriodTest()
        {
            DateTime dd = DateTime.Now;
            DateTime dsdfs = DateTime.Now;
            TimeSpan ddd = (dd - dsdfs);

            List<EffectivePeriodDTO> listErr = BusinessHourWeekManager.Instance.GetEffectivePeriod(25);

            Assert.Fail();
        }

        [TestMethod()]
        public void GetBusinessHourWeekTypeTest()
        {
            try
            {
                WeiXinHelper WeiXinHelper = new WeiXinHelper("http://waimaitest.buzztimecoffee.com/AppWapCoffee/OrderResult");
                string sss = WeiXinHelper.Access_Token;

                string sfsdf = WeiXinHelper.JsapiTicket;

                SignatureObjectDTO signatureObjectDTO  = WeiXinHelper.signatureObjectDTO;

                //string mmmm = HelperTool.Sha1( "jsapi_ticket=kgt8ON7yVITDhtdwci0qeV_mddtgNPEwi9Br3FWgptmjU010H0MiGMygmSU1hJhfEzInTfHzw3Kd92l-wQYM_A&noncestr=asdasdasd&timestamp=1500447034&url=http://waimaitest.buzztimecoffee.com/AppWapCoffee/OrderResult");
            }
            catch (Exception ex)
            {
                string messs = ex.Message;
            }
           


            BusinessHourWeekManager.Instance.GetBusinessHourWeekType("66de6c00-407c-4997-8bbe-ec09a3c8a7e5");
            Assert.Fail();
        }
    }
}