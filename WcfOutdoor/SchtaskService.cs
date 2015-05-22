using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using AutoRadio.RadioSmart.Common;
using System.Data;

namespace WcfOutdoor
{
    public class SchtaskService
    {
        private Timer timer;
        private DalMonitor monitor;
        private static SchtaskService singleton;

        public static SchtaskService Instance
        {
            get
            {
                if (singleton == null) singleton = new SchtaskService();
                return singleton;
            }
        }

        public SchtaskService()
        {
            timer = new Timer();
            timer.Interval = 60000; //一分钟
            timer.Elapsed += new ElapsedEventHandler(onTimedEvent);
            monitor = new DalMonitor();
        }

        /// <summary>
        /// 开始
        /// </summary>
        public void Start()
        {
            timer.Start();
            Loger.Current.Write("SchtaskService.Start()");
        }

        /// <summary>
        /// 结束
        /// </summary>
        public void Stop()
        {
            timer.Close();
            Loger.Current.Write("SchtaskService.Stop()");
        }

        private void onTimedEvent(object source, ElapsedEventArgs e)
        {
            if (DateTime.Now.ToShortTimeString() == "22:00" )
            {
                try
                {
                    DataTable dt = monitor.GetUnLockTaskList();
                    string tpids = string.Empty, tpuids = string.Empty;
                    foreach (DataRow dr in dt.Rows)
                    {
                        tpids += dr["TPId"] + ",";
                        tpuids += dr["TPUId"] + ",";
                    }
                    if (!string.IsNullOrEmpty(tpids.Trim(',')) && !string.IsNullOrEmpty(tpuids.Trim(',')))
                    {
                        monitor.UnLockTask(tpids, tpuids);
                    }
                }
                catch (Exception err)
                {
                    Loger.Current.Write("Client.OnTimedEvent() err=" + err.Message);
                }
            }
        }
    }
}
