using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BLL
{
    public class HandleUserUserIncomeTable
    {
        /*查询通过用户名查询plant_user_income表
         * username:string 用户名
        */
        public static DataTable QueryUserIncomeTableByUserName(string username)
        {
            string strSql = @"select ISNULL(a.user_name, '') as user_name,
                              ISNULL(a.user_store, 0) as user_store,
                              ISNULL(a.user_totalgains, 0) as user_totalgains,
                              ISNULL(a.user_curgains, 0) as user_curgains,
                              ISNULL(a.able_sell_sum, 0) as able_sell_sum
                              from dbo.plant_user_income as a
                              where user_name='{0}'";
            strSql = string.Format(strSql, username);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            return dt;
        }
        /* 通过用户名，仓库可用数，总收益，当前收益，更新plant_user_income表 
         * username：string 用户名
         * userstore：string 仓库可用数
         * usertotalgains：总收益
         * usercurgains：当前收益
         * sum：数量
         */
        public static DataTable UpdateUserIncomeTable(string username, string userstore, string usertotalgains, string usercurgains)
        {
            string strSql = @"update dbo.plant_user_income  set userstore='{0}', usertotalgains='{1}', usercurgains='{2}' where user_name= '{3}' ";
            strSql = string.Format(strSql, userstore, usertotalgains, usercurgains, username);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            return dt;
        }
        /* 通过用户名，仓库数 更新plant_user_income表 
         * username：string 用户名
         * userstore：string 仓库可用数
         */
        public static DataTable UpdateUserIncomeTable(string username, string userstore)
        {
            string strSql = @"update dbo.plant_user_income  set userstore='{0}' where user_name= '{1}' ";
            strSql = string.Format(strSql, userstore, username);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            return dt;
        }

        /* 通过用户名，仓库可用数，总收益，当前收益，插入plant_user_income表 
         * username：string 用户名
         * userstore：string 仓库可用数
         * usertotalgains：总收益
         * usercurgains：当前收益
         * sum：数量
         */
        public static DataTable InsertUserIncomeTable(string username, string userstore, string usertotalgains, string usercurgains)
        {
            string strSql = @"insert into dbo.plant_user_income (user_name,user_store,user_totalgains,user_curgains) values('{0}','{1}','{2}','{3}')";
            strSql = string.Format(strSql, username, userstore, usertotalgains, usercurgains);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            return dt;
        }
        /* 通过用户名，删除plant_user_income表 
         * username：string 用户名
         */
        public static DataTable DeleteUserIncomeTableByUserName(string username)
        {
            string strSql = @"delete dbo.plant_user_income 
                              where user_name='{0}'";
            strSql = string.Format(strSql, username);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            return dt;
        }

        // 充值
        public static bool UpdateUserIncomeTable(string username, int userstore, int ablesellsum)
        {
            string strSql = @"update dbo.plant_user_income  set user_store=ISNULL(user_store,0)+{0},able_sell_sum=ISNULL(able_sell_sum,0)+{1} where user_name= '{2}' ";
            strSql = string.Format(strSql, userstore, ablesellsum,username);
            int flag = DBHelper.SqlHelper.ExecuteSql(strSql);
            return flag > 0;
        }

        public static DataTable QueryUserIncomeTableByUserId(string userId)
        {
            string strSql = @"select ISNULL(a.user_name, '') as user_name,
                              ISNULL(a.user_store, 0) as user_store,
                              ISNULL(a.user_points, 0) as user_points
                              from dbo.plant_user_income as a
                              left join plant_user as b on a.user_name = b.user_name
                              where b.id='{0}'";
            strSql = string.Format(strSql, userId);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            return dt;
        }
    }
}
