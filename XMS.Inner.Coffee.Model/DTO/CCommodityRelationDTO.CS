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
    public class CCommodityRelationDTO : DTOBase
    {

        /// <summary>
        /// 商品UUID
        /// </summary>
        [DataMember(Name = "commodityUUID")]
        public string CommodityUUID { get;set; }

        /// <summary>
        /// UUID(一个商品，通过此属性分组)
        /// </summary>
        [DataMember(Name = "commodityWithPropertyUUID")]
        public string CommodityWithPropertyUUID { get;set; }

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
        [DataMember(Name = "version")]
        public int Version { get;set; }

        /// <summary>
        /// UUID
        /// </summary>
        [DataMember(Name = "resUUID")]
        public string ResUUID { get; set; }

    }
}