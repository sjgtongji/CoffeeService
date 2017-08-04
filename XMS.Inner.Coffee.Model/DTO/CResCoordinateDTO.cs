﻿using System;
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
    public class CResCoordinateDTO : DTOBase
    {
    
        /// <summary>
        /// 餐厅UUID
        /// </summary>
        [DataMember]
        public string ResUUID { get;set; }
        
        /// <summary>
        /// 经度
        /// </summary>
        [DataMember]
        public decimal Longitude { get;set; }
        
        /// <summary>
        /// 纬度
        /// </summary>
        [DataMember]
        public decimal Latitude { get;set; }
        
        /// <summary>
        /// 标记UUID，根据此属性确定一组坐标
        /// </summary>
        [DataMember]
        public string MarkUUID { get;set; }
        
        /// <summary>
        /// 是否删除(0:是,1:否)
        /// </summary>
        [DataMember]
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
        
    }
}