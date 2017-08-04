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
    public class CVerificationCodePO : POBase
    {
        // Properties
        [DataMember, DataType(DbType.String), StringLength(20)]
        public string Code { get; set; }
        [DataMember, DataType(DbType.DateTime)]
        public DateTime? EffectiveEndTime { get; set; }
        [DataMember, DataType(DbType.String), StringLength(50)]
        public string MemberUUID { get; set; }
        [DataMember, DataType(DbType.String), StringLength(50)]
        public string PhoneNumber { get; set; }
        [DataMember, DataType(DbType.Int32)]
        public int? VerificationState { get; set; }
        [DataMember, DataType(DbType.String), StringLength(50)]
        public string WeiXinOpenId { get; set; }
    }
}