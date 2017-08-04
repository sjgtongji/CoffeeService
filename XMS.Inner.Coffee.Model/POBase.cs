using System;
using System.Data;
using System.Runtime.Serialization;
using XMS.Core.Data;

namespace XMS.Inner.Coffee.Model
{
    [Serializable]
    [DataContract]
    public class POBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember]
        [Required]
        [Key]
        [DataGenerated(DataGeneratedOption.Identity)]
        [DataType(DbType.Int32)]
        public int Id { get; set; }
    }
}
