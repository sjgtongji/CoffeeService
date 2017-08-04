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
    public class CBannerDTO : DTOBase
    {
    
        /// <summary>
        /// Banner图片地址
        /// </summary>
        [DataMember]
        public string ImgURL { get;set; }
        
        /// <summary>
        /// Banner名称
        /// </summary>
        [DataMember]
        public string Name { get;set; }
        
        /// <summary>
        /// Banner跳转地址
        /// </summary>
        [DataMember]
        public string BannerLink { get;set; }
        
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

        /// <summary>
        /// 排序
        /// </summary>
        [DataMember]
        public int Sort { get; set; }
    }
}