using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMS.Core.Data;
using XMS.Inner.Coffee.Data;
using XMS.Inner.Coffee.Model;

namespace XMS.Inner.Coffee.Business
{
    public class COperationLogManager: DataMapperManagerBase<COperationLogPO, COperationLogDTO>
    {
        public static readonly COperationLogManager Instance = new COperationLogManager();
        private COperationLogManager() { }

        #region ObjectDifference
        public void AddOperationLog(int key, EnumOperationLogType type, EnumOperationLogAction action, object objOld, object objNew, string sOperator)
        {
            using (IEntityContext entityContext = COperationLogPOManager.Instance.CreateEntityContext())
            {
                AddOperationLogWithContext(entityContext, key, type, action, objOld, objNew, sOperator);
            }
        }

        public void AddOperationLogWithContext(IEntityContext entityContext, int key, EnumOperationLogType type, EnumOperationLogAction action, object objOld, object objNew, string sOperator)
        {
            ObjectComparer objCompare = this.GetResComparer();

            COperationLogPO log = new COperationLogPO();
            log.Type = (byte)type;
            log.Action = (byte)action;
            log.CreateTime = DateTime.Now;
            log.DataInfo = objCompare.GetDifference(objOld, objNew);
            log.DataId = key;
            log.OperatorName = sOperator;
            log.TypeName = type.ToString();

            entityContext.Add(log);
        }

        private ObjectComparer _resComparer = null;

        public ObjectComparer GetResComparer()
        {
            if (_resComparer == null)
            {
                _resComparer = new ObjectComparer(true, IniNotNeedCompareLst());
            }

            return _resComparer;
        }

        private List<string> IniNotNeedCompareLst()
        {
            List<string> lstRes = new List<string>();
            lstRes.Add("LastUpdateTime");
            return lstRes;
        }
        #endregion
    }
}
