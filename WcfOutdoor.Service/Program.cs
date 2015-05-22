using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace WcfOutdoor.Service
{
    static class Program
    {
        public static readonly string SVC_NAME = "WcfOutdoor";
        public static readonly string SVC_DSNAME = "AutoRadio WcfOutdoor Service";
        public static readonly string SVC_DESC = "车语盟电台户外监测服务";
        public static readonly string SVC_LOG = "Application";

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
#if DEBUG
            Test();
#else
            RunService();
#endif
        }

        static void Test()
        {
            Console.Read();
        }

        static void RunService()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new MainService() 
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
