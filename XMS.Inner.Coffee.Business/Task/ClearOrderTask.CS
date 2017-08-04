using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMS.Core;

namespace XMS.Inner.Coffee.Business.Task
{
    public class ClearOrderTask : TriggerTaskBase
    {
        public ClearOrderTask()
            : base(Guid.NewGuid().ToString(), "清除未支付订单")
        {
            Container.LogService.Info("清除未支付订单任务已注册");

            // NextExecuteTime时间小于等于当前时间, 那么这个任务会马上执行
            this.NextExecuteTime = DateTime.Now;
        }

        public override void Execute(DateTime? lastExecuteTime)
        {
            Container.LogService.Info("清除未支付订单任务准备执行");
            // 为了让多个本程序实例中 同时只有一个实例运行下面业务逻辑
            if (Container.ClusterService.Master == Container.ClusterService.Current)
            {
                Container.LogService.Info("清除未支付订单任务开始执行");
                // 业务逻辑
                //Warn();
            }

            // 如果希望停止这个任务那么把NextExecuteTime设为null
            this.NextExecuteTime = DateTime.Now.AddMinutes(5);
        }
    }
}
