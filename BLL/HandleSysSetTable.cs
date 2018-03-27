using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BLL
{
    public class HandleSysSetTable
    {
        public static bool QueryIsSellNeedBuy()
        {
            string strSql = @"select  ISNULL(sell_need_buy,0) as sell_need_buy from dbo.plant_sys_set";
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            if(dt.Rows.Count < 1)
                return false;
            if(string.Compare(dt.Rows[0]["sell_need_buy"].ToString(),"1") == 0)
                return true;
            return false;
        }

        public static int QuerySellPercent()
        {
            string strSql = @"select ISNULL(sell_percent,100) as  sell_percent from dbo.plant_sys_set";
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            if (dt.Rows.Count < 1)
                return 100;
            return Convert.ToInt32(dt.Rows[0]["sell_percent"].ToString());
        }

        public static bool QueryHasData()
        {
            string strSql = @"select * from dbo.plant_sys_set";
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            if (dt.Rows.Count < 1)
                return false;
            return true;
        }

        public static bool InsertData(int state)
        {
            string strSql = @"insert into dbo.plant_sys_set(sell_need_buy) values({0})";
            strSql = string.Format(strSql, state);
            int flag = DBHelper.SqlHelper.ExecuteSql(strSql);
            if (flag < 1)
                return false;
            return true;
        }

        public static bool UpdateSellNeedBuyState(int state)
        {
           string strSql = @"update dbo.plant_sys_set  set sell_need_buy={0}";
            strSql = string.Format(strSql, state);
            int flag = DBHelper.SqlHelper.ExecuteSql(strSql);
            if (flag > 0)
                return true;
            return false;
        }

        public static bool InsertSellPercentData(int percent)
        {
            string strSql = @"insert into dbo.plant_sys_set(sell_percent) values({0})";
            strSql = string.Format(strSql, percent);
            int flag = DBHelper.SqlHelper.ExecuteSql(strSql);
            if (flag < 1)
                return false;
            return true;
        }

        public static bool UpdateSellPercentState(int percent)
        {
            string strSql = @"update dbo.plant_sys_set  set sell_percent={0}";
            strSql = string.Format(strSql, percent);
            int flag = DBHelper.SqlHelper.ExecuteSql(strSql);
            if (flag > 0)
                return true;
            return false;
        }
    }
}
