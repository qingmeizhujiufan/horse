using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BLL
{
    public class HandleUserPickTable
    {
        /*查询通过用户名查询plant_user_pick表
         * username:string 用户名
        */
        public static DataTable QueryUserPickTableByUserName(string username)
        {
            string strSql = @"select * from dbo.plant_user_pick 
                              where user_name='{0}'";
            strSql = string.Format(strSql, username);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            return dt;
        }

        /* 通过用户名，树类型，树id，果实数，更新plant_user_pick表 
         * username：string 用户名
         * treetype：string 仓库可用数
         * treeid：string 树id
         * sum：数量
         */
        public static DataTable UpdateUserPickTable(string username, string treetype, string treeid, string sum)
        {
            string strSql = @"update dbo.plant_user_pick  set peach_type='{0}', peach_id='{1}', peach_number='{2}' where user_name= '{3}' ";
            strSql = string.Format(strSql, treetype, treeid, sum, username);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            return dt;
        }

        /* 通过用户名，树类型，树id，果实数，更新plant_user_pick表 
         * username：string 用户名
         * treetype：string 仓库可用数
         * treeid：string 树id
         * sum：数量
         */
        public static DataTable InsertUserPickTable(string user, string treetype, string treeid, string sum)
        {
            string strSql = @"insert into dbo.plant_user_pick (user_name,peach_type,peach_id,peach_number) values('{0}','{1}','{2}','{3}')";
            strSql = string.Format(strSql, user, treetype, treeid, sum);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            return dt;
        }

        /* 通过用户名，删除plant_user_pick表 
         * username：string 用户名
         */
        public static DataTable DeleteUserPickTableByUserName(string username)
        {
            string strSql = @"delete dbo.plant_user_pick 
                              where user_name='{0}'";
            strSql = string.Format(strSql, username);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            return dt;
        }
    }
}
