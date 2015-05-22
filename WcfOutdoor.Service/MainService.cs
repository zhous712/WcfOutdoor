using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Threading;
using System.ServiceModel;
using AutoRadio.RadioSmart.Common;
using WcfOutdoor;

namespace WcfOutdoor.Service
{
    public partial class MainService : ServiceBase
    {
        private ServiceHost serviceHost = null;
        private ServiceHost serviceHostAsync = null;
        public MainService()
        {
            InitializeComponent();

            this.ServiceName = Program.SVC_NAME;
            this.EventLog.Source = Program.SVC_NAME;
            this.EventLog.Log = Program.SVC_LOG;

            // These Flags set whether or not to handle that specific
            //  type of event. Set to true if you need it, false otherwise.
            this.CanHandlePowerEvent = true;
            this.CanHandleSessionChangeEvent = false;
            this.CanPauseAndContinue = false;
            this.CanShutdown = false;
            this.CanStop = true;

            if (!EventLog.SourceExists(Program.SVC_NAME)) EventLog.CreateEventSource(Program.SVC_NAME, Program.SVC_LOG);
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                Loger.Current.Write("MainService.OnStart() begin");
                SchtaskService.Instance.Start();
                serviceHost = new ServiceHost(typeof(OutdoorService));
                serviceHost.Open();
            }
            catch (Exception e)
            {
                Loger.Current.Write(e.ToString());
            }
        }

        protected override void OnStop()
        {
            Loger.Current.Write("MainService.OnStop() begin");
            SchtaskService.Instance.Stop();
            if (serviceHost != null)
            {
                serviceHost.Close();
                serviceHost = null;
            }
            if (serviceHostAsync != null)
            {
                serviceHostAsync.Close();
                serviceHostAsync = null;
            }
        }
    }
}
