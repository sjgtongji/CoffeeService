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
    public class CMemberWeiXinDTO : DTOBase
    {
    
        /// <summary>
        /// UUID
        /// </summary>
        [DataMember(Name= "memberUUID")]
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
        /// 姓名
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get;set; }

        /// <summary>
        /// 平台会员Id
        /// </summary>
        [DataMember(Name = "platformUserId")]
        public string PlatformUserId { get;set; }

        /// <summary>
        /// 平台会员Token
        /// </summary>
        [DataMember(Name = "platformUserToken")]
        public string PlatformUserToken { get;set; }

        /// <summary>
        /// 平台微信账号
        /// </summary>
        [DataMember(Name = "weixinAccount")]
        public string WeixinAccount { get;set; }

        /// <summary>
        /// 是否删除(0:是,1:否)
        /// </summary>
        [DataMember(Name = "isDelete")]
        public bool IsDelete { get;set; }
        
        /// <summary>
        /// 创建人
        /// </summary>
        [DataMember]
        public string CreateName { get;set; }
        
        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMember]
        public DateTime CreateTime { get;set; }
        
        /// <summary>
        /// 修改人
        /// </summary>
        [DataMember]
        public string UpdateName { get;set; }
        
        /// <summary>
        /// 修改时间
        /// </summary>
        [DataMember]
        public DateTime UpdateTime { get;set; }
        
        /// <summary>
        /// 版本
        /// </summary>
        [DataMember]
        public int Version { get;set; }

        /// <summary>
        /// 积分
        /// </summary>
        [DataMember]
        public decimal? Integral { get; set; }
    }
}