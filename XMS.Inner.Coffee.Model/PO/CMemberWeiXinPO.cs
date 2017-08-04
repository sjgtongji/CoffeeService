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
    [Serializable, DataContract]
    public class CMemberWeiXinPO : POBase
    {
        // Properties
        [DataMember, Required, DataType(DbType.String), StringLength(50)]
        public string CreateName { get; set; }
        [DataMember, Required, DataType(DbType.DateTime)]
        public DateTime CreateTime { get; set; }
        [DataMember, Required, DataType(DbType.Boolean)]
        public bool IsDelete { get; set; }
        [DataMember, Required, DataType(DbType.String), StringLength(50)]
        public string MemberUUID { get; set; }
        [DataMember, DataType(DbType.String), StringLength(50)]
        public string Name { get; set; }
        [DataMember, DataType(DbType.String), StringLength(50)]
        public string PhoneNumber { get; set; }
        [DataMember, DataType(DbType.String), StringLength(50)]
        public string PlatformUserId { get; set; }
        [DataMember, DataType(DbType.String), StringLength(50)]
        public string PlatformUserToken { get; set; }
        [DataMember, Required, DataType(DbType.String), StringLength(50)]
        public string UpdateName { get; set; }
        [DataMember, Required, DataType(DbType.DateTime)]
        public DateTime UpdateTime { get; set; }
        [DataMember, Required, DataType(DbType.Int32)]
        public int Version { get; set; }
        [DataMember, DataType(DbType.String), StringLength(50)]
        public string WeixinAccount { get; set; }
        [DataMember, Required, DataType(DbType.String), StringLength(50)]
        public string WeiXinOpenId { get; set; }
        /// <summary>
        /// »ý·Ö
        /// </summary>
        [DataMember]
        [DataType(DbType.Decimal)]
        public decimal? Integral { get; set; }
    }
}