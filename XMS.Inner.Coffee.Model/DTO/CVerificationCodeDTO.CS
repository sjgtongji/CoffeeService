using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using XMS.Core;
using XMS.Core.Data;
using System.Data;

namespace XMS.Inner.Coffee.Model
{
    [DataContract]
    [Serializable]
    public class CVerificationCodeDTO : DTOBase
    {

        /// <summary>
        /// UUID
        /// </summary>
        [DataMember(Name = "memberUUID")]
        public string MemberUUID { get;set; }

        /// <summary>
        /// 微信OpenId
        /// </summary>
        [DataMember(Name = "weiXinOpenId")]
        public string WeiXinOpenId { get;set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [DataMember(Name = "phoneNumber")]
        public string PhoneNumber { get;set; }

        /// <summary>
        /// 验证码
        /// </summary>
        [DataMember(Name = "code")]
        public string Code { get;set; }

        /// <summary>
        /// 验证状态（0：未验证 1：验证）
        /// </summary>
        [DataMember(Name = "verificationState")]
        public int? VerificationState { get;set; }

        /// <summary>
        /// 有效结束时间（未设定一致可以用）
        /// </summary>
        [DataMember(Name = "effectiveEndTime")]
        public DateTime? EffectiveEndTime { get;set; }
        
    }
}