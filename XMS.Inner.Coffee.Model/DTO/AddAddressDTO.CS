using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XMS.Inner.Coffee.Model.DTO
{
    [DataContract]
    [Serializable]
    public class AddAddressDTO
    {
        /// <summary>
        /// UUID
        /// </summary>
        [DataMember]
        public string MemberUUID { get; set; }

        /// <summary>
        /// 微信OpenId
        /// </summary>
        [DataMember]
        public string WeiXinOpenId { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [DataMember]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// 平台会员Id
        /// </summary>
        [DataMember]
        public string PlatformUserId { get; set; }

        /// <summary>
        /// 平台会员Token
        /// </summary>
        [DataMember]
        public string PlatformUserToken { get; set; }

        /// <summary>
        /// 平台微信账号
        /// </summary>
        [DataMember]
        public string WeixinAccount { get; set; }

        /// <summary>
        /// 是否删除(0:是,1:否)
        /// </summary>
        [DataMember]
        public bool IsDelete { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [DataMember]
        public string CreateName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMember]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        [DataMember]
        public string UpdateName { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [DataMember]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        [DataMember]
        public int Version { get; set; }
    }
}
