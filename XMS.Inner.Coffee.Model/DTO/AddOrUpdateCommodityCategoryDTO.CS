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
    public class AddOrUpdateCommodityCategoryDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember(Name = "id")]
        public int Id { get; set; }

        /// <summary>
        /// 排序权重（越大越靠前）
        /// </summary>
        [DataMember(Name = "sort")]
        public int? Sort { get; set; }

        /// <summary>
        /// 类别名称（中文）
        /// </summary>
        [DataMember(Name = "chineseName")]
        public string ChineseName { get; set; }

        /// <summary>
        /// 类别名称（英文）
        /// </summary>
        [DataMember(Name = "englishName")]
        public string EnglishName { get; set; }

        /// <summary>
        /// 类别图片路径
        /// </summary>
        [DataMember(Name = "picPath")]
        public string PicPath { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataMember(Name = "remark")]
        public string Remark { get; set; }

        /// <summary>
        /// 是否删除(0:是,1:否)
        /// </summary>
        [DataMember(Name = "isDelete")]
        public bool IsDelete { get; set; }

        /// <summary>
        /// 餐厅ID
        /// </summary>
        [DataMember(Name = "resId")]
        public int ResId { get; set; }
    }
}
