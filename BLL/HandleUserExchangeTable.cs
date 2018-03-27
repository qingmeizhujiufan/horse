using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BLL
{
    public class HandleUserExchangeTable
    {
        /* 通过用户名, 查plant_user_exchange表
        * username：string  用户名
        */
        public static DataTable QueryUserExchangeTableByUserName(string username)
        {
            string strSql = @"select * from dbo.plant_user_exchange 
                              where user_name='{0}'";
            strSql = string.Format(strSql, username);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            return dt;
        }

        /* 通过用户名，交易对象，交易果实数量 更新plant_user_exchange表
         * username：string  用户名
         * tousername: string 交易对象
         * sum: string 交易果实数量
        */
        public static DataTable UpdateUserExchangeTable(string username, string tousername, string sum)
        {
            string strSql = @"update dbo.plant_user_exchange  set exchange_number='{0}' where user_name= '{1}' and user_exchangename='{2}'";
            strSql = string.Format(strSql, sum, username, tousername);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            return dt;
        }

        /* 通过用户名，树类型，树id，果实数量 插入plant_user_exchange表
         * username：string  用户名
         * tousername: string 交易对象
         * sum: string 交易果实数量
         * sum: 数量
        */
        public static DataTable InsertUserExchangeTable(string username, string tousername, string sum)
        {
            string strSql = @"insert into dbo.plant_user_exchange (user_name,user_exchangename,exchange_number) values('{0}','{1}','{2}')";
            strSql = string.Format(strSql, username, tousername, sum);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            return dt;
        }

        /* 通过用户名，删除plant_user_exchange表 
         * username：string 用户名
         */
        public static DataTable DeleteUserExchangeTableByUserName(string username)
        {
            string strSql = @"delete dbo.plant_user_exchange 
                              where user_name='{0}'";
            strSql = string.Format(strSql, username);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            return dt;
        }

        // 计算交易中的实际费用
        public double CalcRealSum(int sum)
        {
            return Convert.ToDouble((Double)sum + (Double)sum * 0.1);
        }
    }
}
