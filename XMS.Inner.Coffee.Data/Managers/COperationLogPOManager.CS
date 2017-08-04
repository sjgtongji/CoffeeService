
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMS.Inner.Coffee.Data;
using XMS.Inner.Coffee.Model;

namespace XMS.Inner.Coffee.Data
{
    public class COperationLogPOManager : POManagerBase<COperationLogPO> 
    {
        public static readonly COperationLogPOManager Instance = new COperationLogPOManager();
        private COperationLogPOManager() { }
    }
}