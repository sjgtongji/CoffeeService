using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XMS.Core;
using XMS.Core.Logging;
using XMS.Core.Configuration;
using XMS.Core.WCF;
using XMS.Inner.Coffee.Service;
using XMS.Core.Messaging;
using XMS.Inner.Coffee.Model;

namespace XMS.Inner.Coffee.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //XMS.Inner.Coffee.Business.TEL_Message message = new XMS.Inner.Coffee.Business.TEL_Message();
                //开启消息监听
                MessageSubscribeHost.Instance.Start();

                //ManageableServiceHostManager.Instance.RegisterService(typeof(SMSService));

                //ManageableServiceHostManager.Instance.Start();

                Console.WriteLine("");
                Console.WriteLine("***************************************");
                Console.WriteLine("Running......");
                Console.WriteLine("***************************************");
                Console.WriteLine("");

                while (true)
                {
                    // 让线程一直等待下去
                    System.Threading.Thread.Sleep(Int32.MaxValue);
                    continue;
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(" ########## 异常:" + err.Message);
                Console.Read();
                //XMS.Core.Container.LogService.Error(err);
            }
        }
    }

    //public class TEL_SMSLogMessageHandler : IMessageHandler<XMS.Inner.Coffee.Business.TEL_Message>
    //{
    //    /// <summary>
    //    /// 这是一个callback函数 收到新消息的时候本方法会被调用
    //    /// </summary>
    //    /// <param name="messageContext">消息的上下文 包括了消息相关的所有信息</param>
    //    /// <param name="message">这是消息Body反序列化后的对象</param>
    //    public void Handle(IMessageContext messageContext, XMS.Inner.Coffee.Business.TEL_Message message)
    //    {
    //        if ((message != null) && (!string.IsNullOrEmpty(message.Mobile)))
    //            Console.WriteLine("接收到TEL_Message消息：ID：" + message.Mobile);
    //        else
    //            Console.WriteLine("接收到空消息");
    //    }
    //}
}
