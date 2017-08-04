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
    public class CCommodityDTO : DTOBase
    {

        /// <summary>
        /// UUID
        /// </summary>
        [DataMember(Name = "commodityUUID")]
        public string CommodityUUID { get;set; }

        /// <summary>
        /// 商品名称（中文）
        /// </summary>
        [DataMember(Name = "chineseName")]
        public string ChineseName { get;set; }

        /// <summary>
        /// 商品名称（英文）
        /// </summary>
        [DataMember(Name = "englishName")]
        public string EnglishName { get;set; }

        /// <summary>
        /// 商品基础价格
        /// </summary>
        [DataMember(Name = "price")]
        public decimal? Price { get;set; }

        /// <summary>
        /// 排序权重（越大越靠前）
        /// </summary>
        [DataMember(Name = "sort")]
        public int? Sort { get;set; }

        /// <summary>
        /// 商品类别UUID
        /// </summary>
        [DataMember(Name = "commodityCategoryUUID")]
        public string CommodityCategoryUUID { get;set; }

        /// <summary>
        /// 0:普通 1：组合
        /// </summary>
        [DataMember(Name = "type")]
        public int Type { get;set; }

        /// <summary>
        /// 0：初始；1：上线；2：下线
        /// </summary>
        [DataMember(Name = "state")]
        public int State { get;set; }

        /// <summary>
        /// 显示备注
        /// </summary>
        [DataMember(Name = "showRemark")]
        public string ShowRemark { get;set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataMember(Name = "remark")]
        public string Remark { get;set; }

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


        /// <summary>
        /// 类别图片路径
        /// </summary>
        [DataMember(Name = "picPath")]
        public string PicPath { get; set; }

        /// <summary>
        /// 是否参加优惠(true:参加，false:不参加)
        /// </summary>
        [DataMember(Name = "preferentialFlag")]
        public bool PreferentialFlag { get; set; }

        /// <summary>
        /// 优惠数量
        /// </summary>
        [DataMember(Name = "preferentialQuantity")]
        public int? PreferentialQuantity { get; set; }

        /// <summary>
        /// 优惠百分比（百分比）
        /// </summary>
        [DataMember(Name = "preferentialProportion")]
        public decimal? PreferentialProportion { get; set; }
    }
}