using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BLL
{
    public class HandleMultiTable
    {
        // 种植后更新表
        public static bool HandlePlantTree(string username, int treetype, int treeid, int treesum, double storesum)
        {
            bool bRet = false;
            List<String> SQLStrList = new List<String>();
            string strSql = @"insert into dbo.plant_user_tree (user_name,tree_type,tree_id,fruit_number,update_time) values('{0}',{1},{2},{3},'{4}') ";
            strSql = string.Format(strSql, username, treetype, treeid, treesum, System.DateTime.Now.ToString());
            SQLStrList.Add(strSql);
            strSql = @"update dbo.plant_user_income  set user_store={0} where user_name= '{1}' ";
            strSql = string.Format(strSql, storesum,username);
            SQLStrList.Add(strSql);
            strSql = @"insert into dbo.plant_user_plan (user_name,peach_type,peach_id,peach_number) values('{0}',{1},{2},{3}) ";
            strSql = string.Format(strSql, username, treetype, treeid, treesum, username);
            SQLStrList.Add(strSql);
            int iRes = DBHelper.SqlHelper.ExecuteSqlTran(SQLStrList);
            if (iRes > 0)
                bRet = true;
            return bRet;
        }

        // 增种后更新表
        public static bool HandlePlantPeachToTree(string username, int treetype, int treeid, int treesum, double storesum, int plansum, int plantover)
        {
            bool bRet = false;
            List<String> SQLStrList = new List<String>();
            string strSql = @"update dbo.plant_user_tree  set fruit_number={0}, update_time='{1}' where tree_type={2} and tree_id={3}  and user_name= '{4}'";
            strSql = string.Format(strSql, treesum, System.DateTime.Now.ToString(), treetype, treeid, username);
            SQLStrList.Add(strSql);
            strSql = @"update dbo.plant_user_income  set user_store={0} where user_name= '{1}' ";
            strSql = string.Format(strSql, storesum, username);
            SQLStrList.Add(strSql);
            strSql = @"insert into dbo.plant_user_plan (user_name,peach_type,peach_id,peach_number,peach_number_over) values('{0}',{1},{2},{3},{4}) ";
            strSql = string.Format(strSql, username, treetype, treeid, plansum, plantover);
            SQLStrList.Add(strSql);
            int iRes = DBHelper.SqlHelper.ExecuteSqlTran(SQLStrList);
            if (iRes > 0)
                bRet = true;
            return bRet;
        }
        // 更新采集后相关表
        public static bool HandleGetPeachFromTreeTable(string username, int treetype, int treeid, double userstore, double usertotalgains, double treetotalgains, double curincome)
        {
            bool bRet = false;
            List<String> SQLStrList = new List<String>();
            string strSql = @"update dbo.plant_user_tree  set user_total_gains={0}, user_gains={1}, update_time='{2}' where tree_type={3} and tree_id={4}  and user_name= '{5}'";
            strSql = string.Format(strSql, treetotalgains, curincome, System.DateTime.Now.ToString(), treetype, treeid, username);
            SQLStrList.Add(strSql);
            strSql = @"update dbo.plant_user_income  set user_store={0},user_totalgains={1} where user_name= '{2}' ";
            strSql = string.Format(strSql, userstore, usertotalgains, username);
            SQLStrList.Add(strSql);
            strSql = @"insert into dbo.plant_user_pick (user_name,peach_type,peach_id,peach_number) values('{0}',{1},{2},{3}) ";
            strSql = string.Format(strSql, username, treetype, treeid, curincome);
            SQLStrList.Add(strSql);
            int iRes = DBHelper.SqlHelper.ExecuteSqlTran(SQLStrList);
            if (iRes > 0)
                 bRet = true;
            return bRet;
        }
        // 更新从树上取下果实后的相关信息表
        public static bool HandleGetTreeRestPeachesTable(string username, int treetype, int treeid,int fruitnumber, double userstore, int pickrestnumber)
        {
            bool bRet = false;
            List<String> SQLStrList = new List<String>();
            string strSql = @"update dbo.plant_user_tree  set fruit_number={0} where tree_type={1} and tree_id={2}  and user_name= '{3}'";
            strSql = string.Format(strSql, fruitnumber, treetype, treeid, username);
            SQLStrList.Add(strSql);
            strSql = @"update dbo.plant_user_income  set user_store={0} where user_name= '{1}' ";
            strSql = string.Format(strSql, userstore, username);
            SQLStrList.Add(strSql);
            strSql = @"insert into dbo.plant_pick_tree_rest (user_name,tree_type,tree_id,pick_rest_sum) values('{0}',{1},{2},{3}) ";
            strSql = string.Format(strSql, username, treetype, treeid, pickrestnumber);
            SQLStrList.Add(strSql);
            int iRes = DBHelper.SqlHelper.ExecuteSqlTran(SQLStrList);
            if (iRes > 0)
                bRet = true;
            return bRet;
        }

        // 更新发起交易的表
        public static bool HandleLaunchExchangeTable(string poster, string exchanger, int sum, double userstore)
        {
            bool bRet = false;
            List<String> SQLStrList = new List<String>();
            string strSql = @"insert into dbo.plant_user_exchangeinfo (user_name,user_exchangename,state,user_exchangenumber,delstate) values('{0}', '{1}', {2}, {3},{4})";
            strSql = string.Format(strSql, poster,exchanger, 0, sum, 0);
            SQLStrList.Add(strSql);
            strSql = @"update dbo.plant_user_income  set user_store= user_store-{0},able_sell_sum=able_sell_sum-{0} where user_name= '{1}' ";
            strSql = string.Format(strSql, userstore, poster);
            SQLStrList.Add(strSql);
            int iRes = DBHelper.SqlHelper.ExecuteSqlTran(SQLStrList);
            if (iRes > 0)
                bRet = true;
            return bRet;
        }

        // 删除发起的交易更新相关表
        public static bool HandleDeleteExchangeInfoTable(string id,string username,double userstor)
        {
            bool bRet = false;
            List<String> SQLStrList = new List<String>();
            string strSql = @"delete dbo.plant_user_exchangeinfo 
                              where id='{0}'";
            strSql = string.Format(strSql, id);
            SQLStrList.Add(strSql);
            strSql = @"update dbo.plant_user_income  set user_store={0} where user_name= '{1}' ";
            strSql = string.Format(strSql, userstor, username);
            SQLStrList.Add(strSql);
            int iRes = DBHelper.SqlHelper.ExecuteSqlTran(SQLStrList);
            if (iRes > 0)
                bRet = true;
            return bRet;
        }

        // 更新取消相关表
        public static bool UpdateHandleCancleCallMeExchange(string id, string username, double userstore)
        {
            bool bRet = false;
            List<String> SQLStrList = new List<String>();
            string strSql = @"update dbo.plant_user_exchangeinfo set state=2 where id= '{0}'";
            strSql = string.Format(strSql, id);
            SQLStrList.Add(strSql);
            strSql = @"update dbo.plant_user_income  set user_store={0} where user_name= '{1}' ";
            strSql = string.Format(strSql, userstore, username);
            SQLStrList.Add(strSql);
            int iRes = DBHelper.SqlHelper.ExecuteSqlTran(SQLStrList);
            if (iRes > 0)
                bRet = true;
            return bRet;
        }

        // 交易完成更新相关的表
        public static bool UpdateHandleExchangeSuccess(string id, string username, string user_exchangename, int user_exchangenumber, double exchangenamestore, int myablesellsum, int exchangeablesellsum)
        {
            bool bRet = false;
            List<String> SQLStrList = new List<String>();
            string strSql = @"update dbo.plant_user_exchangeinfo set state=3 where id= '{0}'";
            strSql = string.Format(strSql, id);
            SQLStrList.Add(strSql);
//             strSql = @"update dbo.plant_user_income  set able_sell_sum={0} where user_name= '{1}' ";
//             strSql = string.Format(strSql, myablesellsum, username);
//             SQLStrList.Add(strSql);
            strSql = @"update dbo.plant_user_income  set user_store={0},able_sell_sum={1} where user_name= '{2}' ";
            strSql = string.Format(strSql, exchangenamestore, exchangeablesellsum, user_exchangename);
            SQLStrList.Add(strSql);
            strSql = @"insert into dbo.plant_user_exchange (user_name,user_exchangename,exchange_number) values('{0}', '{1}', {2})";
            strSql = string.Format(strSql, username, user_exchangename, user_exchangenumber);
            SQLStrList.Add(strSql);
            int iRes = DBHelper.SqlHelper.ExecuteSqlTran(SQLStrList);
            if (iRes > 0)
                bRet = true;
            return bRet;
        }

        // 交易取消更新相关的表
        public static bool UpdateHandleCancleMyExchange(string id, string username, double sellsum)
        {
            bool bRet = false;
            List<String> SQLStrList = new List<String>();
            string strSql = @"update dbo.plant_user_exchangeinfo set state=4 where id= '{0}'";
            strSql = string.Format(strSql, id);
            SQLStrList.Add(strSql);
            strSql = @"update dbo.plant_user_income  set user_store=user_store + {0},able_sell_sum=able_sell_sum+{0} where user_name= '{1}' ";
            strSql = string.Format(strSql, sellsum, username);
            SQLStrList.Add(strSql);
            int iRes = DBHelper.SqlHelper.ExecuteSqlTran(SQLStrList);
            if (iRes > 0)
                bRet = true;
            return bRet;
        }

        public static bool InsertStartHangeSellTable(string username, int sellsum)
        {
            bool bRet = false;
            List<String> SQLStrList = new List<String>();
            string strSql = @"insert into dbo.plant_user_hangsell (user_name,hangsell_number,hangsellstate) values('{0}',{1}, {2})";
            strSql = string.Format(strSql, username, sellsum,1);
            SQLStrList.Add(strSql);
//             strSql = @"update dbo.plant_user_income  set user_store={0} where user_name= '{1}' ";
//             strSql = string.Format(strSql, userstore, username);
//             SQLStrList.Add(strSql);
            int iRes = DBHelper.SqlHelper.ExecuteSqlTran(SQLStrList);
            if (iRes > 0)
                bRet = true;
            return bRet;
        }

        public static bool UpdateRemoveMyHangeSellTable(string id,string username)
        {
            bool bRet = false;
            List<String> SQLStrList = new List<String>();
            string strSql = @"update dbo.plant_user_hangsell set hangsellstate=0 where id='{0}'";
            strSql = string.Format(strSql, id);
            SQLStrList.Add(strSql);
//             strSql = @"update dbo.plant_user_income  set user_store={0} where user_name= '{1}' ";
//             strSql = string.Format(strSql, userstore, username);
//             SQLStrList.Add(strSql);
            int iRes = DBHelper.SqlHelper.ExecuteSqlTran(SQLStrList);
            if (iRes > 0)
                bRet = true;
            return bRet;
        }

        public static bool UpdateInviteTable(string id, string pname)
        {
            bool bRet = false;
            List<String> SQLStrList = new List<String>();
            string strSql = @"update dbo.plant_user set is_remove=0 where id='{0}'";
            strSql = string.Format(strSql, id);
            SQLStrList.Add(strSql);

            int iRes = DBHelper.SqlHelper.ExecuteSqlTran(SQLStrList);
            if (iRes > 0)
                bRet = true;
            return bRet;
        }

        public static DataTable QueryPoint(string userid)
        {
            string strSql = @"select  a.user_name as user_name,
                                      ISNULL(b.user_points, 0) as user_points,
                                      ISNULL(b.user_store, 0) as user_store
                                      from dbo.plant_user as a
                                      left join dbo.plant_user_income as b on a.user_name=b.user_name
                                      where a.id='{0}'";
            strSql = string.Format(strSql, userid);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            return dt;
        }

        public static int GetTotalPoint(string id)
        {
            string strSql = @"select  a.user_name as user_name,
                                      ISNULL(b.user_totalpoints, 0) as user_totalpoints
                                      from dbo.plant_user as a
                                      left join dbo.plant_user_income as b on a.user_name=b.user_name
                                      where a.id='{0}'";
            strSql = string.Format(strSql, id);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            if (dt.Rows.Count < 1)
                return -1;
            return Convert.ToInt32(dt.Rows[0]["user_totalpoints"]);
        }

        public static int GetFirstFriendSum(string id)
        {
            string strSql = @"select   u.id,
                                       u.pid,
                                       u.user_name,
                                       u.user_weixin,
                                       COUNT(t.id) as tree_sum
                                 from dbo.plant_user as u
                                 left join dbo.plant_user_tree as t
                                 on u.user_name = t.user_name
                                 where u.pid = '{0}' and u.pid <> u.id
                                 group by u.id, u.pid, u.user_name, u.user_weixin";
            strSql = string.Format(strSql, id);
            //选择出一级
            DataTable dt_first_level = DBHelper.SqlHelper.GetDataTable(strSql);
            return dt_first_level.Rows.Count;
        }
    }
}
