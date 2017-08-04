using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace XMS.Inner.Coffee.Business
{
    /// <summary>
    /// 图片上传结果类
    /// </summary>
    [DataContract]
    public class PhotoResult : FileResult
    {
        /// <summary>
        /// 宽度
        /// </summary>
        [DataMember]
        public int Width { get; set; }

        /// <summary> 
        /// 高度
        /// </summary>
        [DataMember]
        public int Height { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        [DataMember]
        public float Latitude { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        [DataMember]
        public float Longitude { get; set; }

        /// <summary>
        /// 拍照时间
        /// </summary>
        [DataMember]
        public DateTime? PhotoTime { get; set; }
    }
}
