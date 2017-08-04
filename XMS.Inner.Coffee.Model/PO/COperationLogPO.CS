

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
    public class COperationLogPO : POBase
    {
    
        /// <summary>
        /// 非空，对象对应的类别
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.Int32)]
        public int Type { get;set; }
        
        /// <summary>
        /// 非空，1：新增，2：修改 3：删除
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.Int32)]
        public int Action { get;set; }
        
        /// <summary>
        /// 数据ID
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.Int32)]
        public int DataId { get;set; }
        
        /// <summary>
        /// 数据记录
        /// </summary>
        [DataMember]
        [DataType(DbType.String)]
        [StringLength(2147483647)]
        public string DataInfo { get;set; }
        
        /// <summary>
        /// 操作人
        /// </summary>
        [DataMember]
        [DataType(DbType.String)]
        [StringLength(100)]
        public string OperatorName { get;set; }

        /// <summary>
        /// 非空，对象对应的类别名称
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.String)]
        [StringLength(100)]
        public string TypeName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMember]
        [Required]
        [DataType(DbType.DateTime)]
        public DateTime CreateTime { get; set; }
    }
}