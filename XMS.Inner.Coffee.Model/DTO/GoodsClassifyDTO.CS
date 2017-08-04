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
    public class GoodsClassifyDTO
    {
        /// <summary>
        /// UUID
        /// </summary>
        [DataMember(Name = "id")]
        public int Id { get; set; }

        /// <summary>
        /// 商品名称（中文）
        /// </summary>
        [DataMember(Name = "chineseName")]
        public string ChineseName { get; set; }

        /// <summary>
        /// 商品名称（英文）
        /// </summary>
        [DataMember(Name = "englishName")]
        public string EnglishName { get; set; }

        /// <summary>
        /// 类别图片路径
        /// </summary>
        [DataMember(Name = "picPath")]
        public string PicPath { get; set; }
    }
}
