
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMS.Inner.Coffee.Data;
using XMS.Inner.Coffee.Model;
using XMS.Core.Data;
using XMS.Core;

namespace XMS.Inner.Coffee.Business
{
    public class CBannerManager : DataMapperManagerBase<CBannerPO, CBannerDTO>, IManagerBase<CBannerPO>
    {
        public static readonly CBannerManager Instance = new CBannerManager();
        private CBannerManager() { }


        public List<CBannerDTO> GetCBannerList(int? id, string name)
        {
            List<CBannerPO> listResult = new List<CBannerPO>();
            int count = 1;
            while (true)
            {
                Core.Data.QueryResult<CBannerPO> result = GetCBanner(id, name, count, 1000);
                if (result.Items != null && result.Items.Length > 0)
                    listResult.AddRange(result.Items);
                count = count + 1000;
                if (result.TotalCount <= (count - 1))
                    break;
            }

            return PoToDtoList(listResult);
        }

        public Core.Data.QueryResult<CBannerPO> GetCBanner(int? id, string name, int startIndex, int count)
        {
            IPredicate predicate = this.GetCBannerCondition(id, name);
            Core.Data.QueryResult<CBannerPO> resultBanner = CBannerPOManager.Instance.GetCBannerWithTotalCountByPredicate(predicate, startIndex, count, null);
            return resultBanner;
        }

        private IPredicate GetCBannerCondition(int? id, string name)
        {
            List<IPredicate> predicates = new List<IPredicate>();

            #region 基本条件
            if (id.HasValue)
            {
                predicates.Add(PredicateFactory.Equal<CBannerPO>(t => t.Id, id));
            }
            if (!string.IsNullOrWhiteSpace(name))
            {
                predicates.Add(PredicateFactory.Like<CBannerPO>(t => t.Name, name.ToSafeSQLLike()));
            }
            #endregion

            #region 数量条件
            #endregion
            return PredicateFactory.And(predicates.ToArray());
        }


        #region 自动生成
        public CBannerPO GetById(int id)
        {
            return CBannerPOManager.Instance.GetById(id);
        }

        public CBannerPO GetByIdWithContext(IEntityContext entityContext, int id)
        {
            return CBannerPOManager.Instance.GetByIdWithContext(entityContext, id);
        }


        public int AddWithContext(IEntityContext entityContext, CBannerPO po, string operatorName)
        {
            if (po == null)
                throw ErrorCodeHelper.CBannerNull.ToException();

            EnumOperationLogAction enumOperationLogAction = EnumOperationLogAction.Add;
            CBannerPO oldObject = null;
            CBannerPO newObject = po;
            
            newObject.CreateName =  operatorName;
            newObject.CreateTime =  DateTime.Now;
            newObject.UpdateName =  operatorName;
            newObject.UpdateTime =  DateTime.Now;
            
            CBannerPOManager.Instance.AddWithContext(entityContext, newObject);
            po.Id = newObject.Id;

            COperationLogManager.Instance.AddOperationLog(po.Id, EnumOperationLogType.CBanner, enumOperationLogAction, oldObject, newObject, operatorName);
            return po.Id;
        }
        
        public int UpdateWithContext(IEntityContext entityContext, CBannerPO po, string operatorName)
        {
            if (po == null)
                throw ErrorCodeHelper.CBannerNull.ToException();

            EnumOperationLogAction enumOperationLogAction = EnumOperationLogAction.Update;
            CBannerPO oldObject = null;
            CBannerPO newObject = po;
                        
            newObject.UpdateName =  operatorName;
            newObject.UpdateTime =  DateTime.Now;
            
            oldObject = GetByIdWithContext(entityContext, po.Id);  
            if (oldObject == null)
                throw ErrorCodeHelper.CBannerNotExist.ToException();  
            CBannerPOManager.Instance.UpdateWithContext(entityContext, newObject);

            COperationLogManager.Instance.AddOperationLog(po.Id, EnumOperationLogType.CBanner, enumOperationLogAction, oldObject, newObject, operatorName);
            return po.Id;
        }
        
        public int Add(CBannerPO po, string operatorName)
        {
            using (IEntityContext entityContext = CBannerPOManager.Instance.CreateEntityContext())
            {
                po.Id = AddWithContext(entityContext, po, operatorName);
                return po.Id;
            }
        }
        
        public int Update(CBannerPO po, string operatorName)
        {
            using (IEntityContext entityContext = CBannerPOManager.Instance.CreateEntityContext())
            {
                po.Id = UpdateWithContext(entityContext, po, operatorName);
                return po.Id;
            }
        }

       
        public int AddCBanner(AddCBannerDTO cBanner, string operatorName)
        {
            if (cBanner == null)
               throw ErrorCodeHelper.CBannerNull.ToException();
            if (string.IsNullOrWhiteSpace(operatorName))
                throw ErrorCodeHelper.OperatorNameNull.ToException();

            CBannerPO cBannerPO = new CBannerPO();
            
            cBannerPO.ImgURL = cBanner.ImgURL;
            cBannerPO.Name = cBanner.Name;
            cBannerPO.BannerLink = cBanner.BannerLink;
            cBannerPO.Sort = cBanner.Sort;
            return Add(cBannerPO, operatorName);
        }
        
        public int UpdateCBanner(UpdateCBannerDTO cBanner, string operatorName)
        {            
        
            if (cBanner == null)
               throw ErrorCodeHelper.CBannerNull.ToException();
            if (string.IsNullOrWhiteSpace(operatorName))
                throw ErrorCodeHelper.OperatorNameNull.ToException();

            CBannerPO cBannerPO = CBannerManager.Instance.GetById(cBanner.Id);
            if (cBannerPO == null)
                    throw ErrorCodeHelper.CBannerNotExist.ToException();
            
            if (cBanner.Id <= 0)
            {
                throw ErrorCodeHelper.IdLessErr.ToException();
            }
            
            cBannerPO.Id = cBanner.Id;
            cBannerPO.ImgURL = cBanner.ImgURL;
            cBannerPO.Name = cBanner.Name;
            cBannerPO.BannerLink = cBanner.BannerLink;
            cBannerPO.Sort = cBanner.Sort;
            return Update(cBannerPO, operatorName);
        }
        
        #endregion
    }
}
