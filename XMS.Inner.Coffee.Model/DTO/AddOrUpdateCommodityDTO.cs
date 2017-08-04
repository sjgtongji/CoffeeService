using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XMS.Inner.Coffee.Model
{
    [DataContract]
    [Serializable]
    public class AddOrUpdateCommodityDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember()]
        public int Id { get; set; }

        /// <summary>
        /// 商品名称（中文）
        /// </summary>
        [DataMember()]
        public string ChineseName { get; set; }

        /// <summary>
        /// 商品名称（英文）
        /// </summary>
        [DataMember()]
        public string EnglishName { get; set; }

        /// <summary>
        /// 商品基础价格
        /// </summary>
        [DataMember()]
        public decimal Price { get; set; }

        /// <summary>
        /// 排序权重（越大越靠前）
        /// </summary>
        [DataMember()]
        public int Sort { get; set; }

        /// <summary>
        /// 商品类别UUID
        /// </summary>
        [DataMember()]
        public string CommodityCategoryUUID { get; set; }

        /// <summary>
        /// 0:普通 1：组合
        /// </summary>
        [DataMember()]
        public int Type { get; set; }

        /// <summary>
        /// 0：初始；1：上线；2：下线
        /// </summary>
        [DataMember()]
        public int State { get; set; }

        /// <summary>
        /// 显示备注
        /// </summary>
        [DataMember()]
        public string ShowRemark { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataMember()]
        public string Remark { get; set; }

        /// <summary>
        /// 是否删除(0:是,1:否)
        /// </summary>
        [DataMember()]
        public bool IsDelete { get; set; }

        /// <summary>
        /// 餐厅ID
        /// </summary>
        [DataMember()]
        public int ResId{ get; set; }


        /// <summary>
        /// 类别图片路径
        /// </summary>
        [DataMember()]
        public string PicPath { get; set; }

        /// <summary>
        /// 是否参加优惠(true:参加，false:不参加)
        /// </summary>
        [DataMember()]
        public bool PreferentialFlag { get; set; }

        /// <summary>
        /// 优惠数量
        /// </summary>
        [DataMember()]
        public int? PreferentialQuantity { get; set; }

        /// <summary>
        /// 优惠百分比（百分比）
        /// </summary>
        [DataMember()]
        public decimal? PreferentialProportion { get; set; }
    }
}
