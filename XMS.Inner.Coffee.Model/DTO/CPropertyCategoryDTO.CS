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
    public class CPropertyCategoryDTO : DTOBase
    {

        /// <summary>
        /// UUID
        /// </summary>
        [DataMember(Name = "propertyCategoryUUID")]
        public string PropertyCategoryUUID { get;set; }

        /// <summary>
        /// 排序权重（越大越靠前）
        /// </summary>
        [DataMember(Name = "sort")]
        public int Sort { get;set; }

        /// <summary>
        /// 类别名称（中文）
        /// </summary>
        [DataMember(Name = "chineseName")]
        public string ChineseName { get;set; }

        /// <summary>
        /// 商品名称（英文）
        /// </summary>
        [DataMember(Name = "englishName")]
        public string EnglishName { get;set; }

        /// <summary>
        /// false:不允许多选 true:允许多选
        /// </summary>
        [DataMember(Name = "enableMultiple")]
        public bool EnableMultiple { get;set; }

        /// <summary>
        /// 0：基本属性；1：其他
        /// </summary>
        [DataMember(Name = "type")]
        public int Type { get;set; }

        /// <summary>
        /// 是否删除(0:是,1:否)
        /// </summary>
        [DataMember(Name = "isDelete")]
        public bool IsDelete { get;set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [DataMember(Name = "createName")]
        public string CreateName { get;set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMember(Name = "createTime")]
        public DateTime CreateTime { get;set; }

        /// <summary>
        /// 修改人
        /// </summary>
        [DataMember(Name = "updateName")]
        public string UpdateName { get;set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [DataMember(Name = "updateTime")]
        public DateTime UpdateTime { get;set; }
        
        /// <summary>
        /// 版本
        /// </summary>
        [DataMember]
        public int Version { get;set; }

        /// <summary>
        /// UUID
        /// </summary>
        [DataMember(Name = "resUUID")]
        public string ResUUID { get; set; }

    }
}