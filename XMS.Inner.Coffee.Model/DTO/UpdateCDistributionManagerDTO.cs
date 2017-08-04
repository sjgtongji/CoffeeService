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
    public class UpdateCDistributionManagerDTO : DTOBase
    {
    
        ///// <summary>
        ///// 自增ID
        ///// </summary>
        //[DataMember]
        //public int Id { get;set; }
        
        /// <summary>
        /// 用户名
        /// </summary>
        [DataMember]
        public string Name { get;set; }
        
        /// <summary>
        /// 密码
        /// </summary>
        [DataMember]
        public string PassWord { get;set; }
        
        /// <summary>
        /// 级别
        /// </summary>
        [DataMember]
        public int UserLevel { get;set; }
        
        /// <summary>
        /// 手机号码
        /// </summary>
        [DataMember]
        public string Mobile { get;set; }

        /// <summary>
        /// 餐厅配送人员
        /// </summary>
        [DataMember]
        public string ResUUID { get; set; }

    }
}