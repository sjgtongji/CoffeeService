using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMS.Core.Data;

namespace XMS.Inner.Coffee.Data
{
    public class OperationLogShardingStrategy : IShardingStrategy
    {

        public IConnectionString GetConnectionString(IEntityContainer entityContainer, IEntityMapper mapper, object entity)
        {
            // 从实体容器中配置的默认连接字符串自动生成连接字符串 这适用于使用同一个用户名和密码管理各切片数据库的场景
            return entityContainer.DefaultConnectionString.AcquireConnectionString(
                entityContainer.DefaultConnectionString.DataSource,
                entityContainer.DefaultConnectionString.Database + DateTime.Today.Year.ToString()
            );
        }

        public IConnectionString GetConnectionString(IEntityContainer entityContainer, IEntityMapper mapper, IShardingParams shardingParams)
        {
            // 从实体容器中配置的默认连接字符串自动生成连接字符串 这适用于使用同一个用户名和密码管理各切片数据库的场景
            // 通过传入的ShardingParams对象去的到分表信息 然后调用EmployeePOManager类中的GetDatabaseName方法来组合出数据库名称
            return entityContainer.DefaultConnectionString.AcquireConnectionString(
                entityContainer.DefaultConnectionString.DataSource,
                entityContainer.DefaultConnectionString.Database + DateTime.Today.Year.ToString()
            );
        }

        public string GetPhysicalTableName(IEntityContainer entityContainer, IEntityMapper mapper, IShardingParams shardingParams)
        {
            return "COperationLog";
        }

        public string GetPhysicalTableName(IEntityContainer entityContainer, IEntityMapper mapper, object entity)
        {
            return "COperationLog";
        }
    }
}
