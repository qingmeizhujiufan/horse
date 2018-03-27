using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BLL
{
    public class HandleSysSplitRate
    {
        /* 查sys_split_raten表获得拆分率
      */
        public static float QuerySysSplitRateTable()
        {
            float m_SysSplitRate = 0;
            string strSql = @"select sys_split_rate from dbo.plant_sys_split_rate";
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            if (dt.Rows.Count < 1)
                return 0;
            try
            {
                m_SysSplitRate = float.Parse(dt.Rows[0]["sys_split_rate"].ToString());
                return m_SysSplitRate;
            }
            catch (System.Exception ex)
            {
                return 0;
            }
        }

        /* 设置拆分率
     */
        public static bool UpdateSysSplitRateTable(string splitrate)
        {
            string strSql = @"update dbo.plant_sys_split_rate  set sys_split_rate='{0}'";
            strSql = string.Format(strSql, splitrate);
            int flag = DBHelper.SqlHelper.ExecuteSql(strSql);
            if (flag > 0)
            {
                return true;
            }
            return false;
        }
    }
}
