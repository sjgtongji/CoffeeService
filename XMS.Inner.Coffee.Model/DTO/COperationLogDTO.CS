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
    public class COperationLogDTO : DTOBase
    {
    
        /// <summary>
        /// 非空，对象对应的类别
        /// </summary>
        [DataMember]
        public int Type { get;set; }
        
        /// <summary>
        /// 非空，1：新增，2：修改 3：删除
        /// </summary>
        [DataMember]
        public int Action { get;set; }
        
        /// <summary>
        /// 数据ID
        /// </summary>
        [DataMember]
        public int DataId { get;set; }
        
        /// <summary>
        /// 数据记录
        /// </summary>
        [DataMember]
        public string DataInfo { get;set; }
        
        /// <summary>
        /// 操作人
        /// </summary>
        [DataMember]
        public string OperatorName { get;set; }


        /// <summary>
        /// 非空，对象对应的类别名称
        /// </summary>
        [DataMember]
        public string TypeName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMember]
        public DateTime CreateTime { get; set; }

    }
}