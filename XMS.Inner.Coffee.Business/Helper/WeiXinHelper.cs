using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMS.Inner.Coffee.Model;

namespace XMS.Inner.Coffee.Business
{
    public class WeiXinHelper
    {

        public string Url { get; set; }

        public WeiXinHelper(string url)
        {
            this.Url = url;
        }

        /// <summary>
        /// 生成时间
        /// </summary>
        public DateTime? AccessTokenCreateDateTime;
        /// <summary>
        /// 有效时间
        /// </summary>
        public DateTime? AccessTokenEffectiveTime;

        public string AppId { get { return AppSettingHelper.AppId; } }

        public string Secret { get { return AppSettingHelper.Secret; } }

        private string _Access_Token { get; set; }

        public string Access_Token
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_Access_Token) && AccessTokenCreateDateTime.HasValue && AccessTokenEffectiveTime.HasValue && AccessTokenEffectiveTime.Value > DateTime.Now)
                    return _Access_Token;

                string sendMessUrl = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", AppId, Secret);
                try
                {
                    string result = HttpHelper.RequestURL(sendMessUrl, string.Empty);
                    GetAccessTokenResponse getAccessTokenResponse = XMS.Core.Json.JsonSerializer.Deserialize<GetAccessTokenResponse>(result);
                    _Access_Token = getAccessTokenResponse.access_token;
                    AccessTokenCreateDateTime = DateTime.Now;
                    AccessTokenEffectiveTime = AccessTokenCreateDateTime.Value.AddSeconds(getAccessTokenResponse.expires_in - 600);
                }
                catch(Exception ex)
                {
                    XMS.Core.Container.LogService.Error(ex.Message);
                    _Access_Token = string.Empty;
                }

                return _Access_Token;
            }
        }

        /// <summary>
        /// 生成时间
        /// </summary>
        public DateTime? JsapiTicketCreateDateTime;
        /// <summary>
        /// 有效时间
        /// </summary>
        public DateTime? JsapiTicketEffectiveTime;

        private string _JsapiTicket { get; set; }

        public string JsapiTicket
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_JsapiTicket) && JsapiTicketCreateDateTime.HasValue && JsapiTicketEffectiveTime.HasValue && JsapiTicketEffectiveTime.Value > DateTime.Now)
                    return _JsapiTicket;

                string sendMessUrl = string.Format("https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type=jsapi", this.Access_Token);
                try
                {
                    string result = HttpHelper.RequestURL(sendMessUrl, string.Empty);
                    GetJsapiTicketResponse getJsapiTicketResponse = XMS.Core.Json.JsonSerializer.Deserialize<GetJsapiTicketResponse>(result);
                    _JsapiTicket = getJsapiTicketResponse.ticket;
                    JsapiTicketCreateDateTime = DateTime.Now;
                    JsapiTicketEffectiveTime = AccessTokenCreateDateTime.Value.AddSeconds(getJsapiTicketResponse.expires_in - 600);
                }
                catch (Exception ex)
                {
                    XMS.Core.Container.LogService.Error(ex.Message);
                    _JsapiTicket = string.Empty;
                }

                return _JsapiTicket;
            }
        }

        public SignatureObjectDTO signatureObjectDTO
        {
            get
            {
                SignatureObjectDTO signatureObjectDTO = new SignatureObjectDTO()
                {
                    TimeStamp = (int)(DateTime.Now - DateTime.Parse("2017-7-19")).TotalSeconds,
                    NonceStr = "Coffee",
                    AppId = this.AppId
                };
                string url = string.Format("jsapi_ticket={0}&noncestr={1}&timestamp={2}&url={3}", this.JsapiTicket,
                    signatureObjectDTO.NonceStr, signatureObjectDTO.TimeStamp, this.Url);

                signatureObjectDTO.Signature = HelperTool.Sha1(url);
                return signatureObjectDTO;
            }
        }

        class GetAccessTokenResponse
        {
            public string access_token { get; set; }
            public int expires_in { get; set; }
        }

        class GetJsapiTicketResponse
        {
            public string errcode { get; set; }
            public string errmsg { get; set; }
            public string ticket { get; set; }
            public int expires_in { get; set; }

        }
    }
}
