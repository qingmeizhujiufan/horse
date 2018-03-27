using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BLL
{
    public class HandleUsertPlanTable
    {
        /*查询通过用户名查询plant_user_plan表
         * username:string 用户名
        */
        public static DataTable QueryUserPlanTableByUserName(string username)
        {
            string strSql = @"select * from dbo.plant_user_plan 
                              where user_name='{0}'";
            strSql = string.Format(strSql, username);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            return dt;
        }
        /* 通过用户名，树类型，树id，种植桃数，更新plant_user_plan表 
         * username：string 用户名
         * peachtype：string 仓库可用数
         * peachid：总收益
         * sum：数量
         */
        public static DataTable UpdateUserPlanTable(string username, string peachtype, string peachid, string sum)
        {
            string strSql = @"update dbo.plant_user_plan  set peach_type='{0}', peach_id='{1}', peach_number='{2}' where user_name= '{3}' ";
            strSql = string.Format(strSql, peachtype, peachid, sum, username);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            return dt;
        }

        /* 通过用户名，树类型，树id，种植桃数，插入plant_user_plan表 
         * username：string 用户名
         * peachtype：string 仓库可用数
         * peachid：总收益
         * sum：数量
         */
        public static DataTable InsertUserPlantTable(string username, string peachtype, string peachid, string sum)
        {
            string strSql = @"insert into dbo.plant_user_plan (user_name,peach_type,peach_id,peach_number) values('{0}','{1}','{2}','{3}')";
            strSql = string.Format(strSql, username, peachtype, peachid, sum);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            return dt;
        }
        /* 通过用户名，删除plant_user_plan表 
         * username：string 用户名
         */
        public static DataTable DeleteUserPlanTableByUserName(string username)
        {
            string strSql = @"delete dbo.plant_user_plan 
                              where user_name='{0}'";
            strSql = string.Format(strSql, username);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            return dt;
        }
    }
}
