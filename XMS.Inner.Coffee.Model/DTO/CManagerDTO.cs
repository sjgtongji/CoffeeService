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
    public class CManagerDTO : DTOBase
    {
        // Properties
        [DataMember(Name = "createName")]
        public string CreateName { get; set; }
        [DataMember(Name = "createTime")]
        public DateTime CreateTime { get; set; }
        [DataMember(Name = "isDelete")]
        public bool IsDelete { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "passWord")]
        public string PassWord { get; set; }
        [DataMember(Name = "updateName")]
        public string UpdateName { get; set; }
        [DataMember(Name = "updateTime")]
        public DateTime UpdateTime { get; set; }
        [DataMember(Name = "userLevel")]
        public int UserLevel { get; set; }
        public int Version { get; set; }
    }
}