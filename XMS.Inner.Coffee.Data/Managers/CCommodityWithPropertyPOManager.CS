
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMS.Inner.Coffee.Data;
using XMS.Inner.Coffee.Model;

namespace XMS.Inner.Coffee.Data
{
    public class CCommodityWithPropertyPOManager : POManagerBase<CCommodityWithPropertyPO> 
    {
        public static readonly CCommodityWithPropertyPOManager Instance = new CCommodityWithPropertyPOManager();
        private CCommodityWithPropertyPOManager() { }
    }
}