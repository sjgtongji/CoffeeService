using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMS.Core.Data;
using XMS.Inner.Coffee.Model;

namespace XMS.Inner.Coffee.Business
{
    public interface IManagerBase<PO> where PO : POBase, new()
    {
        int Add(PO po, string operatorName);

        int AddWithContext(IEntityContext entityContext, PO po, string operatorName);

        int Update(PO po, string operatorName);

        int UpdateWithContext(IEntityContext entityContext, PO po, string operatorName);

        PO GetById(int id);

        PO GetByIdWithContext(IEntityContext entityContext, int id);
    }
}
