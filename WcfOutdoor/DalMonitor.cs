using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AutoRadio.RadioSmart.Common.Data;
using AutoRadio.RadioSmart.Common;
using System.Configuration;
using System.Data.SqlClient;

namespace WcfOutdoor
{
    public class DalMonitor
    {
        private string OutdoorMonitor = "OutdoorMonitor";
        /// <summary>
        /// 获取应该解锁的任务
        /// </summary>
        /// <returns></returns>
        public DataTable GetUnLockTaskList()
        {
            var sql = string.Format(@"SELECT tpur.TPId,tpur.TPUId FROM dbo.TaskProject tp INNER JOIN dbo.TaskProjectUserRelation tpur ON tp.TPId = tpur.TPId WHERE tp.Status=1 AND Relation=0 
AND DATEDIFF(HOUR,UserBeginWorkTime,GETDATE())>={0}", ConfigurationManager.AppSettings["UnLockTime"]);
            return SqlHelper.ExecuteDataset(DBConnectionString.Get(OutdoorMonitor), CommandType.Text, sql).Tables[0];
        }

        /// <summary>
        /// 对过期不进行任务进行解锁
        /// </summary>
        /// <param name="tpids"></param>
        /// <param name="tpuids"></param>
        /// <returns></returns>
        public int UnLockTask(string tpids, string tpuids)
        {
            string sql = @"UPDATE dbo.TaskProject SET Status=0 WHERE TPId IN(" + tpids + ");UPDATE dbo.TaskProjectUserRelation SET Relation=3 WHERE TPUId IN(" + tpuids + ")";
            return SqlHelper.ExecuteNonQuery(DBConnectionString.Get(OutdoorMonitor), CommandType.Text, sql);
        }
    }
}
