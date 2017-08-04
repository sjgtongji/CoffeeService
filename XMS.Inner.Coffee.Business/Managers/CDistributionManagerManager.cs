
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
    public class CDistributionManagerManager: DataMapperManagerBase<CDistributionManagerPO, CDistributionManagerDTO>, IManagerBase<CDistributionManagerPO>
    {
        public static readonly CDistributionManagerManager Instance = new CDistributionManagerManager();
        private CDistributionManagerManager() { }

        #region 自动生成
        public CDistributionManagerPO GetById(int id)
        {
            return CDistributionManagerPOManager.Instance.GetById(id);
        }

        public CDistributionManagerPO GetByIdWithContext(IEntityContext entityContext, int id)
        {
            return CDistributionManagerPOManager.Instance.GetByIdWithContext(entityContext, id);
        }

        public CDistributionManagerPO CheckDistributionExsit(string passWord, string mobile)
        {
            List<CDistributionManagerPO> listDistribution = GetCDistributionManagerList(null, null, passWord, null, new List<string> { mobile });
            if (listDistribution == null || listDistribution.Count == 0)
                throw new BusinessException("骑手不存在");

            return listDistribution[0];
        }

        public List<CDistributionManagerPO> GetCDistributionManagerList(int? id, string name, string passWord, int? userLevel, List<string> mobile)
        {
            List<CDistributionManagerPO> listResult = new List<CDistributionManagerPO>();
            int count = 1;
            while(true)
            {
                Core.Data.QueryResult<CDistributionManagerPO> result = GetCDistributionManager(id, name, passWord, userLevel, mobile, count, 1000);
                if (result.Items != null && result.Items.Length > 0)
                    listResult.AddRange(result.Items);
                count = count + 1000;
                if (result.TotalCount <= (count - 1))
                    break;
            }

            return listResult;
        }
        
        public Core.Data.QueryResult<CDistributionManagerPO> GetCDistributionManager(int? id, string name, string passWord, int? userLevel, List<string> mobile,  int startIndex, int count)
        {
            IPredicate predicate = this.GetCDistributionManagerCondition(id, name, passWord, userLevel, mobile);
            Core.Data.QueryResult<CDistributionManagerPO> resultRest = CDistributionManagerPOManager.Instance.GetCDistributionManagerWithTotalCountByPredicate(predicate, startIndex, count, null);
            return resultRest;
        }

        private IPredicate GetCDistributionManagerCondition(int? id, string name, string passWord, int? userLevel, List<string> mobile)
        {
            List<IPredicate> predicates = new List<IPredicate>();

            #region 基本条件
            if (id.HasValue)
            {
                predicates.Add(PredicateFactory.Equal<CDistributionManagerPO>(t => t.Id, id));
            }
            if (!string.IsNullOrWhiteSpace(name))
            {
                predicates.Add(PredicateFactory.Equal<CDistributionManagerPO>(t => t.Name, name));
            }   
            if (!string.IsNullOrWhiteSpace(passWord))
            {
                predicates.Add(PredicateFactory.Equal<CDistributionManagerPO>(t => t.PassWord, passWord));
            }   
            if (userLevel.HasValue)
            {
                predicates.Add(PredicateFactory.Equal<CDistributionManagerPO>(t => t.UserLevel, userLevel));
            }
            if (mobile != null && mobile.Count > 0)
            {
                predicates.Add(PredicateFactory.In<CDistributionManagerPO>(t => t.Mobile, mobile.ToArray()));
            }
            #endregion

            #region 数量条件
            #endregion
            return PredicateFactory.And(predicates.ToArray());
        }

        public int AddWithContext(IEntityContext entityContext, CDistributionManagerPO po, string operatorName)
        {
            if (po == null)
                throw ErrorCodeHelper.CDistributionManagerNull.ToException();
             if (string.IsNullOrWhiteSpace(po.Name))
                throw ErrorCodeHelper.CDistributionManagerNameNotSet.ToException();           
             if (string.IsNullOrWhiteSpace(po.PassWord))
                throw ErrorCodeHelper.CDistributionManagerPassWordNotSet.ToException();           
             if (string.IsNullOrWhiteSpace(po.Mobile))
                throw ErrorCodeHelper.CDistributionManagerMobileNotSet.ToException();           

            EnumOperationLogAction enumOperationLogAction = EnumOperationLogAction.Add;
            CDistributionManagerPO oldObject = null;
            CDistributionManagerPO newObject = po;
            
            newObject.CreateName =  operatorName;
            newObject.CreateTime =  DateTime.Now;
            newObject.UpdateName =  operatorName;
            newObject.UpdateTime =  DateTime.Now;
            
            CDistributionManagerPOManager.Instance.AddWithContext(entityContext, newObject);
            po.Id = newObject.Id;

            COperationLogManager.Instance.AddOperationLog(po.Id, EnumOperationLogType.CDistributionManager, enumOperationLogAction, oldObject, newObject, operatorName);
            return po.Id;
        }
        
        public int UpdateWithContext(IEntityContext entityContext, CDistributionManagerPO po, string operatorName)
        {
            if (po == null)
                throw ErrorCodeHelper.CDistributionManagerNull.ToException();
             if (string.IsNullOrWhiteSpace(po.Name))
                throw ErrorCodeHelper.CDistributionManagerNameNotSet.ToException();          
             if (string.IsNullOrWhiteSpace(po.PassWord))
                throw ErrorCodeHelper.CDistributionManagerPassWordNotSet.ToException();          
             if (string.IsNullOrWhiteSpace(po.Mobile))
                throw ErrorCodeHelper.CDistributionManagerMobileNotSet.ToException();          

            EnumOperationLogAction enumOperationLogAction = EnumOperationLogAction.Update;
            CDistributionManagerPO oldObject = null;
            CDistributionManagerPO newObject = po;
                        
            newObject.UpdateName =  operatorName;
            newObject.UpdateTime =  DateTime.Now;
            
            oldObject = GetByIdWithContext(entityContext, po.Id);  
            if (oldObject == null)
                throw ErrorCodeHelper.CDistributionManagerNotExist.ToException();  
            CDistributionManagerPOManager.Instance.UpdateWithContext(entityContext, newObject);

            COperationLogManager.Instance.AddOperationLog(po.Id, EnumOperationLogType.CDistributionManager, enumOperationLogAction, oldObject, newObject, operatorName);
            return po.Id;
        }
        
        public int Add(CDistributionManagerPO po, string operatorName)
        {
            using (IEntityContext entityContext = CDistributionManagerPOManager.Instance.CreateEntityContext())
            {
                po.Id = AddWithContext(entityContext, po, operatorName);
                return po.Id;
            }
        }
        
        public int Update(CDistributionManagerPO po, string operatorName)
        {
            using (IEntityContext entityContext = CDistributionManagerPOManager.Instance.CreateEntityContext())
            {
                po.Id = UpdateWithContext(entityContext, po, operatorName);
                return po.Id;
            }
        }

       
        public int AddCDistributionManager(AddCDistributionManagerDTO cDistributionManager, string operatorName)
        {
            if (cDistributionManager == null)
               throw ErrorCodeHelper.CDistributionManagerNull.ToException();
            if (string.IsNullOrWhiteSpace(operatorName))
                throw ErrorCodeHelper.OperatorNameNull.ToException();

            CDistributionManagerPO cDistributionManagerPO = new CDistributionManagerPO();
            
            cDistributionManagerPO.Name = cDistributionManager.Name;
            cDistributionManagerPO.PassWord = cDistributionManager.PassWord;
            cDistributionManagerPO.UserLevel = cDistributionManager.UserLevel;
            cDistributionManagerPO.Mobile = cDistributionManager.Mobile;
            
            return Add(cDistributionManagerPO, operatorName);
        }
        
        public int UpdateCDistributionManager(UpdateCDistributionManagerDTO cDistributionManager, string operatorName)
        {            
        
            if (cDistributionManager == null)
               throw ErrorCodeHelper.CDistributionManagerNull.ToException();
            if (string.IsNullOrWhiteSpace(operatorName))
                throw ErrorCodeHelper.OperatorNameNull.ToException();

            CDistributionManagerPO cDistributionManagerPO = CDistributionManagerManager.Instance.GetById(cDistributionManager.Id);
            if (cDistributionManagerPO == null)
                    throw ErrorCodeHelper.CDistributionManagerNotExist.ToException();
            
            if (cDistributionManager.Id <= 0)
            {
                throw ErrorCodeHelper.IdLessErr.ToException();
            }
            
            cDistributionManagerPO.Id = cDistributionManager.Id;
            cDistributionManagerPO.Name = cDistributionManager.Name;
            cDistributionManagerPO.PassWord = cDistributionManager.PassWord;
            cDistributionManagerPO.UserLevel = cDistributionManager.UserLevel;
            cDistributionManagerPO.Mobile = cDistributionManager.Mobile;

            return Update(cDistributionManagerPO, operatorName);
        }
        
        #endregion
    }
}
