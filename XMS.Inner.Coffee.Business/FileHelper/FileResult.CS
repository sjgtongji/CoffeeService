using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace XMS.Inner.Coffee.Business
{
    /// <summary>
    /// 文件上传结果类
    /// </summary>
    [DataContract]
    public class FileResult
    {
        /// <summary>
        /// 文件Md5值
        /// </summary>
        [DataMember]
        public string Md5 { get; set; }

        /// <summary>
        /// 文件路径，相对路径
        /// </summary>
        [DataMember]
        public string Path { get; set; }


        /// <summary>
        /// 文件Url
        /// </summary>
        [DataMember]
        public string Url { get; set; }

    }
}
