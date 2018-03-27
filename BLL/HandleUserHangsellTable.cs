using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BLL
{
    public class HandleUserHangsellTable
    {
        /* 查plant_user_hangsell表 正在挂买的数据
      * username：string  用户名
      */
        public static DataTable QueryUserHangsellTableAllOnSale()
        {
            string strSql = @"select  a.id, 
                                      a.user_name,
                                      a.hangsell_number,
                                      CONVERT(varchar(16), a.create_time, 120) as create_time,
                                      a.hangsellstate,
                                      b.user_weixin as weixin
                                      from dbo.plant_user_hangsell as a
                                      left join dbo.plant_user as b on a.user_name=b.user_name
                                      where a.hangsellstate=1 order by create_time desc";
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            return dt;
        }

        /* 查plant_user_hangsell表 已买出的数据
      * username：string  用户名
      */
//         public static DataTable QueryUserHangsellTableAllNoSale()
//         {
//             string strSql = @"select * from dbo.plant_user_hangsell 
//                               where plant_user_hangsell='0'";
//             DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
//             return dt;
//         }
        /* 通过用户名, 查plant_user_hangsell表
       * username：string  用户名
       */
        public static DataTable QueryUserHangsellTableByUserName(string username)
        {
            string strSql = @"select * from dbo.plant_user_hangsell 
                              where user_name='{0}'";
            strSql = string.Format(strSql, username);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            return dt;
        }

        public static DataTable QueryUserHangsellTable(string username, string sellid, int sum)
        {
            string strSql = @"select * from dbo.plant_user_hangsell 
                              where id='{1}' and user_name = '{0}' and hangsell_number={2}";
            strSql = string.Format(strSql, username, sellid, sum);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            return dt;
        }

        /* 通过用户名, 查plant_user_hangsell表中挂买的数据
       * username：string  用户名
       */
        public static DataTable QueryUserHangsellTableSaleByUserName(string username)
        {
            string strSql = @"select  a.id, 
                                      a.user_name,
                                      a.hangsell_number,
                                      CONVERT(varchar(16), a.create_time, 120) as create_time,
                                      a.hangsellstate,
                                      b.user_weixin as weixin
                                      from dbo.plant_user_hangsell as a
                                      left join dbo.plant_user as b on a.user_name=b.user_name
                                      where a.user_name = '{0}' order by create_time desc";
            strSql = string.Format(strSql, username);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            return dt;
        }
        /* 通过用户名, 查plant_user_hangsell表中已买出的数据
      * username：string  用户名
      */
        public static DataTable QueryUserHangsellTableNoSaleByUserName(string username)
        {
            string strSql = @"select * from dbo.plant_user_hangsell 
                              where user_name='{0}' and is_hangsell = '0'";
            strSql = string.Format(strSql, username);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            return dt;
        }
        /* 通过用户名，交易对象，交易果实数量 更新plant_user_hangsell表
         * username：string  用户名
         * hangsellnumber: string 交易果实数
        */
        public static DataTable UpdateUserHangsellTable(string username, string hangsellnumber)
        {
            string strSql = @"update dbo.plant_user_hangsell  set hangsell_number='{0}' where user_name= '{1}'";
            strSql = string.Format(strSql, hangsellnumber, username);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            return dt;
        }

        /* 通过用户名，交易果实数 插入plant_user_hangsell表
         * username：string  用户名
         * hangsellnumber: string 交易果实数
        */
        public static DataTable InsertUserExchangeTable(string username, string hangsellnumber)
        {
            string strSql = @"insert into dbo.plant_user_hangsell (user_name,hangsell_number,is_hangsell) values('{0}','{1}','1'";
            strSql = string.Format(strSql, username, hangsellnumber);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            return dt;
        }

        /* 通过用户名，删除plant_user_exchange表  删除已买出的数据
         * username：string 用户名
         */
        public static DataTable DeleteUserTreeTableSaleOutByUserName(string username)
        {
            string strSql = @"delete dbo.plant_user_exchange 
                              where user_name='{0}' and is_hangsell = '0'";
            strSql = string.Format(strSql, username);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            return dt;
        }
        /* 通过用户名，删除plant_user_exchange表  正在买出的数据
         * username：string 用户名
         */
        public static DataTable DeleteUserTreeTableAllNoSaleByUserName(string username)
        {
            string strSql = @"delete dbo.plant_user_exchange 
                              where user_name='{0}' and is_hangsell = '1'";
            strSql = string.Format(strSql, username);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            return dt;
        }
        /* 通过用户名，删除plant_user_exchange表  删除所有买买数据
         * username：string 用户名
         */
        public static DataTable DeleteUserTreeTableAllSaleByUserName(string username)
        {
            string strSql = @"delete dbo.plant_user_exchange 
                              where user_name='{0}' and";
            strSql = string.Format(strSql, username);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            return dt;
        }
    }
}
