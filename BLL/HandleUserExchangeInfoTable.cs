using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BLL
{
    public class HandleUserExchangeInfoTable
    {
        // 查询今天是否有卖出
        public static bool IsHasSellOutToday(string username)
        {
            string strSql = @"select count(a.id) as count
                              from dbo.plant_user_exchangeinfo as a
                              left join dbo.plant_user_exchange as b on a.user_name=b.user_name
                              where a.state=3 and CONVERT(varchar(100), b.create_time, 23) = CONVERT(varchar(100), getdate(), 23) 
                              and a.user_name='{0}'
                              group by CONVERT(varchar(100), a.create_time, 23)";
            strSql = string.Format(strSql, username);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            if (dt.Rows.Count < 1)
                return false;
            return true;
        }

        // 查询今天是否有没有处理的交易
        public static bool IsHasLanchSellToday(string username)
        {
            string strSql = @"select count(id) as count
                              from dbo.plant_user_exchangeinfo 
                              where (state = 0 or state = 1)and CONVERT(varchar(100), create_time, 23) = CONVERT(varchar(100), getdate(), 23) 
                              and user_name='{0}'
                              group by CONVERT(varchar(100), create_time, 23)";
            strSql = string.Format(strSql, username);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            if (dt.Rows.Count < 1)
                return false;
            return true;
        }

        // 
        public static DataTable QueryMyExchangeInfoTable(string username)
        {
            string strSql = @"select id,
                              user_name,
                              user_exchangename,
                              user_exchangenumber,
                              CONVERT(varchar(16), create_time, 120) as create_time,
                              state
                              from dbo.plant_user_exchangeinfo
                              where user_name='{0}' and (delstate=0 or delstate=2)
                              order by create_time DESC";
            strSql = string.Format(strSql, username);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            return dt;
        }

        public static DataTable QueryCallMeExchangeInfoTable(string username)
        {
            string strSql = @"select id,
                              user_name,
                              user_exchangename,
                              user_exchangenumber,
                              CONVERT(varchar(16), create_time, 120) as create_time,
                              state
                              from dbo.plant_user_exchangeinfo
                              where user_exchangename='{0}' and (delstate=0 or delstate=1)
                              order by create_time DESC";
            strSql = string.Format(strSql, username);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            return dt;
        }

        // 通过id查询交易信息
        public static DataTable QueryExchangeInfoById(string id,int state)
        {
            string strSql = @"select* from dbo.plant_user_exchangeinfo where id='{0}' and state={1}";
            strSql = string.Format(strSql, id, state);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            return dt;
        }
        // 通过id查询交易信息
        public static DataTable QueryExchangeInfoById(string id)
        {
            string strSql = @"select* from dbo.plant_user_exchangeinfo where id='{0}' and state < 3";
            strSql = string.Format(strSql, id);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            return dt;
        }

        // 通过id删除交易信息
        public static bool DeleteExchangeInfoById(string id)
        {
            string strSql = @"delete dbo.plant_user_exchangeinfo 
                              where id='{0}'";
            strSql = string.Format(strSql, id);
            int count = DBHelper.SqlHelper.ExecuteSql(strSql);
            if (count < 1)
                return false;
            return true;
        }

        // 通过id修改删除状态字段
        public static bool DeleteExchangeInfoByDelState(string id,int delstate)
        {
            string strSql = @"update dbo.plant_user_exchangeinfo set delstate={0} where id= '{1}'";
            strSql = string.Format(strSql, delstate,id);
            int count = DBHelper.SqlHelper.ExecuteSql(strSql);
            if (count < 1)
                return false;
            return true;
        }

        // 通过id设置交易状态
        public static bool SetExchangeInfoStae(string id,int state)
        {
            string strSql = @"update dbo.plant_user_exchangeinfo set state={0} where id= '{1}'";
            strSql = string.Format(strSql, state,id);
            int count = DBHelper.SqlHelper.ExecuteSql(strSql);
            if (count < 1)
                return false;
            return true;
        }

        // 查询所有交易信息
        public static DataTable QueryAllExchangeInfo()
        {
            string strSql = @"select* from dbo.plant_user_exchangeinfo";
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            return dt; ;
        }
    }
}
