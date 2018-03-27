using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BLL
{
    public class HandleUserTree
    {
        /* 通过用户名，树类型，树id 查plant_user_tree表
         * username：string  用户名
         * treetype: string 树类型
         * treeid: string 树id
        */
        public static DataTable QueryUserTreeTable(string username, int treetype, int treeid)
        {
            string strSql = @"select ISNULL(a.user_name, '') as user_name,
                              ISNULL(a.tree_type, 0) as tree_type,
                              ISNULL(a.tree_id, 0) as tree_id,
                              ISNULL(a.fruit_number, 0) as fruit_number,
                              ISNULL(a.user_total_gains, 0) as user_total_gains,
                              ISNULL(a.user_gains, 0) as user_gains,
                              ISNULL(a.update_time, 0) as update_time
                              from dbo.plant_user_tree as a
                              where user_name='{0}' and tree_type={1} and tree_id={2}";
            strSql = string.Format(strSql, username, treetype, treeid);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            return dt;
        }
        // 查询个数种植树的个数
        public static int QueryUserTreeTable(string username)
        {
            string strSql = @"select ISNULL(a.user_name, '') as user_name,
                              ISNULL(a.tree_type, 0) as tree_type,
                              ISNULL(a.tree_id, 0) as tree_id,
                              ISNULL(a.fruit_number, 0) as fruit_number,
                              ISNULL(a.user_total_gains, 0) as user_total_gains,
                              ISNULL(a.user_gains, 0) as user_gains,
                              ISNULL(a.update_time, 0) as update_time
                              from dbo.plant_user_tree as a
                              where user_name='{0}'";
            strSql = string.Format(strSql, username);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            return dt.Rows.Count;
        }

        // 查询数种植树的个数
        public static DataTable QueryUserTreeTableByTreetype(string username)
        {
            string strSql = @"Select
                            Sum(Case When tree_type=1 Then 1 Else 0 End) As tree_type1,
                            Sum(Case When tree_type=2 Then 1 Else 0 End) As tree_type2,
                            Sum(Case When tree_type=3 Then 1 Else 0 End) As tree_type3
                            from dbo.plant_user_tree 
                            where user_name='{0}'";
            strSql = string.Format(strSql, username);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            return dt;
        }

        /* 通过用户名，树类型，树id，果实数量 更新plant_user_tree表
         * username：string  用户名
         * treetype: string 树类型
         * treeid: string 树id
         * sum: 数量
        */
        public static DataTable UpdateUserTreeTable(string username, int treetype, int treeid, int sum)
        {
            string strSql = @"update dbo.plant_user_tree  set fruit_number={0}, tree_type={1}, tree_id={2} where user_name= '{3}' ";
            strSql = string.Format(strSql, sum, treetype, treeid, username);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            return dt;
        }

        /* 通过用户名，树类型，树id，果实数量 插入plant_user_tree表
         * username：string  用户名
         * treetype: string 树类型
         * treeid: string 树id
         * sum: 数量
        */
        public static bool InsertUserTreeTable(string username, int treetype, int treeid, int sum)
        {
            bool isTrue = false;
            string strSql = @"insert into dbo.plant_user_tree (user_name,tree_type,tree_id,fruit_number) values('{0}', {1}, {2}, {3});
                              update dbo.plant_user_income set user_store = user_store - {3} where user_name = '{0}'";
            strSql = string.Format(strSql, username, treetype, treeid, sum);
            int flag = DBHelper.SqlHelper.ExecuteSql(strSql);
            if (flag > 0)
                isTrue = true;
            return isTrue;
        } 
     
        /* 通过用户名，删除plant_user_tree表 
         * username：string 用户名
         */
        public static DataTable DeleteUserTreeTableByUserName(string username)
        {
            string strSql = @"delete dbo.plant_user_tree 
                              where user_name='{0}'";
            strSql = string.Format(strSql, username);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            return dt;
        }
    }
}
