using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

using XMS.Core;
using XMS.Core.Logging;
using XMS.Core.Configuration;
using XMS.Core.WCF;
using XMS.Core.Tasks;

using XMS.Inner.Coffee.Service;

namespace XMS.Inner.Coffee.Host
{
    public partial class Service : ServiceBase
    {
        public Service()
        {
            InitializeComponent();
            ManageableServiceHostManager.Instance.RegisterService(typeof(RestCoffeeService));

            // 向服务管理器中注册服务
            ManageableServiceHostManager.Instance.RegisterService(typeof(CoffeeService));

            //注册集群MasterChanged事件
            Core.Container.ClusterService.MasterChanged += ClusterService_MasterChanged;
        }

        protected override void OnStart(string[] args)
        {
            //XMS.Inner.Coffee.Business.TaskManager.Instance.Start();

            // 开启集群服务
            Core.Container.ClusterService.Start();

            // 启动服务管理器
            ManageableServiceHostManager.Instance.Start();


            base.OnStart(args);
        }

        protected override void OnStop()
        {
            // 停止服务管理器
            ManageableServiceHostManager.Instance.Stop();

            // 停止集群服务
            Core.Container.ClusterService.Stop();

            //XMS.Inner.Coffee.Business.TaskManager.Instance.Stop();

            base.OnStop();
        }

        private void ClusterService_MasterChanged(object sender, EventArgs e)
        {
            if (Core.Container.ClusterService.Current.Key == Core.Container.ClusterService.Master.Key)
            {
                Core.Container.LogService.Info("\r\n集群MasterChanged事件执行，Master = true\r\n");
            }
            else
            {
                Core.Container.LogService.Info("\r\n集群MasterChanged事件执行，Master = false\r\n");
            }
        }
    }
}
