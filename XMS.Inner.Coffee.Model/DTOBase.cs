using System;
using System.Runtime.Serialization;
using XMS.Core.Data;

namespace XMS.Inner.Coffee.Model
{
    [Serializable]
    [DataContract]
    public class DTOBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember(Name = "id")]
        public int Id { get; set; }
    }
}
