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
    public class CIntegralRecordDTO : DTOBase
    {     
        /// <summary>
        /// 关联订单ID
        /// </summary>
        [DataMember]
        public string OrderId { get;set; }
        
        /// <summary>
        /// 积分（积分>0加积分，积分<减积分）
        /// </summary>
        [DataMember]
        public decimal Integral { get;set; }
        
        /// <summary>
        /// 类别（0：订单 1：优惠卷）
        /// </summary>
        [DataMember]
        public int Type { get;set; }
        
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public bool IsDelete { get;set; }
        
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string CreateName { get;set; }
        
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public DateTime CreateTime { get;set; }
        
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string UpdateName { get;set; }
        
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public DateTime UpdateTime { get;set; }
        
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int Version { get;set; }
        
    }
}