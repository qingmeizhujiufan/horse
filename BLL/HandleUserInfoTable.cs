using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BLL
{
    public class HandleUserInfoTable
    {
        // 查询用户表
        public static DataTable QueryUserInfoByUserName(string username)
        {
            string strSql = @"select * from dbo.plant_user where user_name = '{0}'";
            strSql = string.Format(strSql, username);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            return dt;
        }

        // 更新用户信息表
        public static bool UpdateUserInfoTable(string user_name, string user_pwd,
             string user_secondpwd, string user_realname, string user_telephone,
             string user_qq, string user_weixin, string user_alipay)
        {
            bool bRet = false;
            string strSql = @"update dbo.plant_user set user_pwd='{0}',
                                                        user_secondpwd='{1}',
                                                        user_realname='{2}',
                                                        user_telephone='{3}',
                                                        user_qq='{4}',
                                                        user_weixin='{5}',
                                                        user_alipay='{6}'
                                                        where user_name= '{7}' ";
            strSql = string.Format(strSql, user_pwd, user_secondpwd, user_realname,
                user_telephone, user_qq, user_weixin, user_alipay, user_name);
            int count = DBHelper.SqlHelper.ExecuteSql(strSql);
            if (count > 0)
                bRet = true;
            return bRet;
        }

        // 冻结用户更新表
        public static bool UpdateFreezeUserInfoTable(string user_name, int state)
        {
            bool bRet = false;
            string strSql = @"update dbo.plant_user set is_remove={0} where user_name='{1}'";
            strSql = string.Format(strSql, state, user_name);
            int count = DBHelper.SqlHelper.ExecuteSql(strSql);
            if (count > 0)
                bRet = true;
            return bRet;
        }

        // 查询用户是否存在
        public static bool IsUserExist(string username)
        {
            bool bRet = false;
            string strSql = @"select * from dbo.plant_user 
                              where user_name='{0}'";
            strSql = string.Format(strSql, username);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            if (dt.Rows.Count > 0)
                bRet = true;
            return bRet;
        }
        // 查询用户是否冻结
        public static bool IsUserFreeze(string username)
        {
            bool bRet = false;
            string strSql = @"select * from dbo.plant_user 
                              where user_name='{0}' and is_remove = 1";
            strSql = string.Format(strSql, username);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            if (dt.Rows.Count > 0)
                bRet = true;
            return bRet;
        }

        // 查询用户二级密码是否正确
        public static bool IsUserSecondPwdRight(string username,string secondpwd)
        {
            bool bRet = false;
            string strSql = @"select * from dbo.plant_user 
                              where user_name='{0}' and user_secondpwd='{1}'";
            strSql = string.Format(strSql, username, secondpwd);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            if (dt.Rows.Count > 0)
                bRet = true;
            return bRet;
        }

        // 查询会员增长信息
        public static DataTable QueryUserGrowInfo(int betweenday)
        {
            string strSql = @"select CONVERT(varchar(100), create_time, 23) as time,
	                                   count(id) as count 
                                from dbo.plant_user 
                                where CONVERT(varchar(100), create_time, 23) > dateadd(day,-{0},getdate()) and 
                                      CONVERT(varchar(100), create_time, 23) < GETDATE()
                                group by CONVERT(varchar(100), create_time, 23)
                                order by CONVERT(varchar(100), create_time, 23)";
            strSql = string.Format(strSql, betweenday);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            return dt;
        }
    }
}
