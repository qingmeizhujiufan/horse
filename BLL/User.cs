using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BLL
{
    public class User
    {

        private Common commonBll = null;
        public User()
        {
            this.commonBll = new Common();
        }

        public DataTable GetUserBaseInfo(string id)
        {
            string strSql = @"select    a.id, 
                                        a.user_name,
                                        ISNULL(a.user_realname, '') as user_realname,
                                        ISNULL(a.user_level, 0) as user_level,
                                        ISNULL(a.telephone, '') as telephone,
                                        ISNULL(a.qq, '') as qq,
                                        ISNULL(a.weixin, '') as weixin,
                                        ISNULL(b.user_store, 0) as user_store,
                                        ISNULL(b.user_gains, 0) as user_gains,
                                        ISNULL(b.user_split_rate, 0) as user_split_rate,
                                        ISNULL(b.peach_1_1, 0) as peach_1_1,
                                        ISNULL(b.peach_1_2, 0) as peach_1_2,
                                        ISNULL(b.peach_1_3, 0) as peach_1_3,
                                        ISNULL(b.peach_1_4, 0) as peach_1_4,
                                        ISNULL(b.peach_1_5, 0) as peach_1_5,
                                        ISNULL(b.peach_1_6, 0) as peach_1_6,
                                        ISNULL(b.peach_1_7, 0) as peach_1_7,
                                        ISNULL(b.peach_1_8, 0) as peach_1_8,
                                        ISNULL(b.peach_1_9, 0) as peach_1_9,
                                        ISNULL(b.peach_1_10, 0) as peach_1_10,
                                        ISNULL(b.peach_2_1, 0) as peach_2_1,
                                        ISNULL(b.peach_2_2, 0) as peach_2_2,
                                        ISNULL(b.peach_2_3, 0) as peach_2_3,
                                        ISNULL(b.peach_2_4, 0) as peach_2_4,
                                        ISNULL(b.peach_2_5, 0) as peach_2_5,
                                        ISNULL(b.peach_3_1, 0) as peach_3_1
                                        from dbo.plant_user as a
                                        left join dbo.plant_user_tree as b on a.user_name=b.user_name
                                        where a.id = '{0}'";
            strSql = string.Format(strSql, id);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            return dt;
        }

        public bool UpdateUserInfo(string userID, string fieldName, string fieldValue)
        {
            bool bRtn = false;

            if (string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(fieldName) || string.IsNullOrEmpty(fieldValue))
            {
                bRtn = false;
                return bRtn;
            }

            string strSql = "update dbo.hzw_user set {0} = '{1}' where user_openid = '{2}'";
            strSql = string.Format(strSql, fieldName, fieldValue, userID);
            int tag = DBHelper.SqlHelper.ExecuteSql(strSql);
            if (tag > 0)
            {
                bRtn = true;
            }

            return bRtn;
        }

        public bool CheckLogin(string i, string b)
        {
            return true;
        }
        
        public DataTable GetUserInfoList()
        {
            string strSql = @"select a.user_name,
                            ISNULL(a.user_realname, '') as user_realname,
                            ISNULL(a.is_remove, '') as is_remove,
                            ISNULL(c.tree_type, '0') as tree_type,
                            ISNULL(c.tree_id, '0') as tree_id,
                            ISNULL(c.fruit_number, '0') as fruit_number,
                            ISNULL(b.user_store, '0') as user_store,
                            ISNULL(b.user_totalgains, '0') as user_totalgains
                            from plant_user as a
                            left join dbo.plant_user_income as b on a.user_name = b.user_name
                            left join dbo.plant_user_tree as c on a.user_name = c.user_name
                            where a.user_name is not NULL                            
                            order by a.user_name desc";
            strSql = string.Format(strSql);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            return dt;
        }

        // 得到用户多级父节点相关信息
        public void GetUserMultilevelParentInfo(string user,int level,ref Dictionary<string, string> dic)
        {
            string username = user;
            int plevel = 1;
            while (plevel <= level)
            {
                DataTable dt = BLL.HandleUserInfoTable.QueryUserInfoByUserName(username);
                if (dt.Rows.Count < 1)
                    break;
                string plevelstr = Convert.ToString(plevel);
                string pname = dt.Rows[0]["p_name"].ToString();
                if (pname == string.Empty)
                {
                    if (dic.ContainsKey(plevelstr) == false)
                        dic.Add(plevelstr, username);
                    break;
                }
                if (dic.ContainsKey(plevelstr) == false)
                    dic.Add(plevelstr, username);
                plevel++;
            }
        }

        // 得到下级用户字典
        // id: 用户id
        // dic:保存的字典
        // sum:当前子节点数
        // sumlevel: 总等级
        // level: 下级等级
        public void GetUserChildSum(string id, ref Dictionary<string, int> dic, ref int childsum, int sumlevel, int level, bool childover)
        {
            string pepolelevel;
            if (level == 0)
                return;
            if (childover)
            {
                pepolelevel = "people_sum_" + level;
                if (dic.ContainsKey(pepolelevel) == false)
                    dic.Add(pepolelevel, childsum);
                else
                    dic[pepolelevel] = dic[pepolelevel] + childsum;
                return;
            }
            DataTable dt_user = GetUserChildTable(id);
            int levelsum = dt_user.Rows.Count;
            int reallevel = sumlevel - level + 1;   
            if (levelsum < 1 || reallevel < 1)
                return;
            childsum = 0;
            string tempid = string.Empty;
            for (int i = 0; i < levelsum; i++)
            {
                tempid = string.Empty;
                tempid = dt_user.Rows[i]["id"].ToString();
                GetUserChildSum(tempid, ref dic, ref childsum, sumlevel, level - 1,false);
            }
            GetUserChildSum(tempid, ref dic, ref childsum, sumlevel, level - 1,true);
            pepolelevel = "people_sum_" + reallevel;
            if (dic.ContainsKey(pepolelevel) == false)
                dic.Add(pepolelevel, levelsum);
            else
                dic[pepolelevel] = dic[pepolelevel] + levelsum;
        }

        public DataTable GetUserChildTable(string id)
        {
            string strSql = @"select * from dbo.plant_user where pid='{0}' and pid <> id";
            strSql = string.Format(strSql, id);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            return dt;
        }

        public float CalcUserSplit(string userame)
        {
            float split = 0;
            float sysSplitRate = BLL.HandleSysSplitRate.QuerySysSplitRateTable();
            int treesum = BLL.HandleUserTree.QueryUserTreeTable(userame);
            if (treesum < 2)
                split = sysSplitRate;
            else
                split = sysSplitRate + BLL.EnumData.EnumTreeSplit[treesum - 1];
            return split;
        }

        // 计算收益
        public double CalcTreeCurIncome(int fruitnumber, string username, int treetype, double totalgains, DataTable treeTypeSumDb)
        {
            double income = 0;
            switch (treetype)
            {
                case 1:
                    {
                        if (totalgains > BLL.EnumData.EnumTreeTypeIncomeSum[0] * Convert.ToDouble(treeTypeSumDb.Rows[0]["tree_type1"].ToString()))
                            return -1;
                    }
                    break;
                case 2:
                    {
                        if (totalgains > 9000 + BLL.EnumData.EnumTreeTypeIncomeSum[1] * Convert.ToDouble(treeTypeSumDb.Rows[0]["tree_type2"].ToString()))
                            return -1;
                    }
                    break;
                case 3:
                    {
                        if (totalgains > 24000 + BLL.EnumData.EnumTreeTypeIncomeSum[2])
                            return -1;
                    }
                    break;
                default:
                    break;
            }

            double sysSplitRate = CalcUserSplit(username);
            if (sysSplitRate > 0)
            {
                income = fruitnumber * sysSplitRate;
                income = Math.Round(income, 2, MidpointRounding.AwayFromZero);
            }
            if (treetype == 1 && totalgains + income > BLL.EnumData.EnumTreeTypeIncomeSum[0] * Convert.ToDouble(treeTypeSumDb.Rows[0]["tree_type1"].ToString()))
                income = BLL.EnumData.EnumTreeTypeIncomeSum[0] * Convert.ToDouble(treeTypeSumDb.Rows[0]["tree_type1"].ToString()) - totalgains;
            else if (treetype == 2 && totalgains + income > 9000 + BLL.EnumData.EnumTreeTypeIncomeSum[1])
                income = 9000 + BLL.EnumData.EnumTreeTypePlantSum[1] - totalgains;
            else if (treetype == 3 && totalgains + income > 24000 + BLL.EnumData.EnumTreeTypeIncomeSum[2])
                income = 24000 + BLL.EnumData.EnumTreeTypePlantSum[2] - totalgains;
            return (double)income;
        }

        public bool IsAbleIncome(string tableupdatetime)
        {
            bool bRet = false;
            DateTime curDate = System.DateTime.Now;
            string Date = string.Format("{0:d}", curDate);
            if (string.Compare(tableupdatetime, Date) == 0)
                return bRet;
            //             switch (treetype)
            //             {
            //                 case 1:
            //                     {
            //                         if (usertotalincome <= BLL.EnumData.EnumTreeTypeIncomeSum[0] * Convert.ToDouble(treeTypeSumDb.Rows[0]["tree_type1"].ToString()))
            //                             bRet = true;
            //                     }
            //                     break;
            //                 case 2:
            //                     {
            //                         if (usertotalincome <= 9000 + BLL.EnumData.EnumTreeTypeIncomeSum[1] * Convert.ToDouble(treeTypeSumDb.Rows[0]["tree_type2"].ToString()))
            //                             bRet = true;
            //                     }
            //                     break;
            //                 case 3:
            //                     {
            //                         if (usertotalincome <= 24000 + BLL.EnumData.EnumTreeTypeIncomeSum[2])
            //                             bRet = true;
            //                     }
            //                     break;
            //                 default:
            //                     break;
            //             }
            return true;
        }

        // 通过用户名得到数据库中的登录数据
        public DataTable GetUserInfo(string username)
        {
            username = CommonTool.SqlValueCheck.ChangeEscapecode(username);
            DataTable UserTable = QueryUserInfo(username);
            if (UserTable.Rows.Count < 1)
                return UserTable;
            //得到会员显示table
            string strval;
            DataTable UserInfo = CreateUserInfoTable();
            DataRow NewRow = UserInfo.NewRow();
            InitNewRow(ref NewRow);
            //遍历数据库中得到的table转化为显示的table
            for (int i = 0; i < UserTable.Rows.Count; i++)
            {
                strval = UserTable.Rows[i]["user_name"].ToString();
                NewRow["user_name"] = strval;
                // 取真实姓名
                strval = UserTable.Rows[i]["user_realname"].ToString();
                NewRow["user_realname"] = strval;
                // 取果树类型
                GetUserInfoRow(ref UserTable, i, ref NewRow);
                // 取仓库过实数
                strval = UserTable.Rows[i]["user_store"].ToString();
                //strval = string.Format()
                NewRow["user_store"] = Math.Floor(100 * Convert.ToDouble(strval)) / 100.0;
                strval = UserTable.Rows[i]["user_totalgains"].ToString();
                NewRow["user_totalgains"] = Math.Floor(100 * Convert.ToDouble(strval)) / 100.0;
                NewRow["user_enablesell_sum"] = CalcEnableSellSum(username);
            }
            UserInfo.Rows.Add(NewRow);
            return UserInfo;
        }

        // 向会员table中填数据
        // dt_usersRow: 数据库中得到的table
        // dusersRow: 遍历dt_usersRow的行数
        // dusersRow: 会员显示table的一行数据
        public void GetUserInfoRow(ref DataTable dt_usersRow, int dusersRow, ref DataRow newRow)
        {
            int strtype = Convert.ToInt32(dt_usersRow.Rows[dusersRow]["tree_type"].ToString());
            int strid = Convert.ToInt32(dt_usersRow.Rows[dusersRow]["tree_id"].ToString());
            int fruit_number = Convert.ToInt32(dt_usersRow.Rows[dusersRow]["fruit_number"].ToString());
            int total_gains = Convert.ToInt32(dt_usersRow.Rows[dusersRow]["user_total_gains"].ToString());
            string username = dt_usersRow.Rows[dusersRow]["user_name"].ToString();
            string treeupdatetime = dt_usersRow.Rows[dusersRow]["tree_update_time"].ToString();
            string tableUpdateDate = string.Format("{0:d}", Convert.ToDateTime(treeupdatetime));
            DataTable treeTypeSumDb = BLL.HandleUserTree.QueryUserTreeTableByTreetype(username);
            // 是否计算用户果实增长数
            bool isCalcCurIncome = IsAbleIncome(tableUpdateDate);
            string key = "peach_";
            key = key + strtype + "_" + strid;
            string keycurincome = key + "_CurIncome";
            if (!newRow.Table.Columns.Contains(keycurincome))
                return;
            newRow[key] = fruit_number;
            if (isCalcCurIncome)
            {
                double sysSplitRate = CalcUserSplit(username);
                double income = 0;
                if (sysSplitRate > 0)
                {
                    income = fruit_number * sysSplitRate;
                    income = Math.Round(income, 2, MidpointRounding.AwayFromZero);
                }
                newRow[keycurincome] = income;
            }

        }

        int CalcEnableSellSum(string username)
        {
            int iEableSell = 0;
            bool isNeedBuy = BLL.HandleSysSetTable.QueryIsSellNeedBuy();
            bool isSellOnce = false;
            int percent = BLL.HandleSysSetTable.QuerySellPercent();
            if (percent < 100)
                isSellOnce = true;
            if (isSellOnce)
            {
                if (BLL.HandleUserExchangeInfoTable.IsHasSellOutToday(username))
                    return 0;
            }
            DataTable user_income = BLL.HandleUserUserIncomeTable.QueryUserIncomeTableByUserName(username);
            if (user_income.Rows.Count < 1)
            {
                return 0;
            }
            double store = Convert.ToDouble(user_income.Rows[0]["user_store"].ToString());
            double ableselltotal = store * (double)percent / 100.0;
            if (isNeedBuy)
            {
                int ablesellsum = Convert.ToInt32(user_income.Rows[0]["able_sell_sum"].ToString());
                if (ablesellsum <= (int)ableselltotal)
                {
                    iEableSell = ablesellsum;
                }
            }
            iEableSell = (int)ableselltotal;

            iEableSell = iEableSell - iEableSell % 10;
            return iEableSell;
        }

        // 创建返回个人登录数据的table
        public DataTable CreateUserInfoTable()
        {
            DataTable users_info = new DataTable();
            users_info.Columns.Add("user_name", typeof(String));
            users_info.Columns.Add("user_realname", typeof(String));
            users_info.Columns.Add("peach_1_1", typeof(String));
            users_info.Columns.Add("peach_1_1_CurIncome", typeof(String));
            users_info.Columns.Add("peach_1_2", typeof(String));
            users_info.Columns.Add("peach_1_2_CurIncome", typeof(String));
            users_info.Columns.Add("peach_1_3", typeof(String));
            users_info.Columns.Add("peach_1_3_CurIncome", typeof(String));
            users_info.Columns.Add("peach_1_4", typeof(String));
            users_info.Columns.Add("peach_1_4_CurIncome", typeof(String));
            users_info.Columns.Add("peach_1_5", typeof(String));
            users_info.Columns.Add("peach_1_5_CurIncome", typeof(String));
            users_info.Columns.Add("peach_1_6", typeof(String));
            users_info.Columns.Add("peach_1_6_CurIncome", typeof(String));
            users_info.Columns.Add("peach_1_7", typeof(String));
            users_info.Columns.Add("peach_1_7_CurIncome", typeof(String));
            users_info.Columns.Add("peach_1_8", typeof(String));
            users_info.Columns.Add("peach_1_8_CurIncome", typeof(String));
            users_info.Columns.Add("peach_1_9", typeof(String));
            users_info.Columns.Add("peach_1_9_CurIncome", typeof(String));
            users_info.Columns.Add("peach_1_10", typeof(String));
            users_info.Columns.Add("peach_1_10_CurIncome", typeof(String));
            users_info.Columns.Add("peach_2_1", typeof(String));
            users_info.Columns.Add("peach_2_1_CurIncome", typeof(String));
            users_info.Columns.Add("peach_2_2", typeof(String));
            users_info.Columns.Add("peach_2_2_CurIncome", typeof(String));
            users_info.Columns.Add("peach_2_3", typeof(String));
            users_info.Columns.Add("peach_2_3_CurIncome", typeof(String));
            users_info.Columns.Add("peach_2_4", typeof(String));
            users_info.Columns.Add("peach_2_4_CurIncome", typeof(String));
            users_info.Columns.Add("peach_2_5", typeof(String));
            users_info.Columns.Add("peach_2_5_CurIncome", typeof(String));
            users_info.Columns.Add("peach_3_1", typeof(String));
            users_info.Columns.Add("peach_3_1_CurIncome", typeof(String));
            users_info.Columns.Add("user_enablesell_sum", typeof(String));
            users_info.Columns.Add("user_store", typeof(String));
            users_info.Columns.Add("user_totalgains", typeof(String));
            return users_info;
        }
        // 初始化会个人数据table
        public void InitNewRow(ref DataRow newRow)
        {
            newRow["peach_1_1"] = 0;
            newRow["peach_1_1_CurIncome"] = 0;
            newRow["peach_1_2"] = 0;
            newRow["peach_1_2_CurIncome"] = 0;
            newRow["peach_1_3"] = 0;
            newRow["peach_1_3_CurIncome"] = 0;
            newRow["peach_1_4"] = 0;
            newRow["peach_1_4_CurIncome"] = 0;
            newRow["peach_1_5"] = 0;
            newRow["peach_1_5_CurIncome"] = 0;
            newRow["peach_1_6"] = 0;
            newRow["peach_1_6_CurIncome"] = 0;
            newRow["peach_1_7"] = 0;
            newRow["peach_1_7_CurIncome"] = 0;
            newRow["peach_1_8"] = 0;
            newRow["peach_1_8_CurIncome"] = 0;
            newRow["peach_1_9"] = 0;
            newRow["peach_1_9_CurIncome"] = 0;
            newRow["peach_1_10"] = 0;
            newRow["peach_1_10_CurIncome"] = 0;
            newRow["peach_2_1"] = 0;
            newRow["peach_2_1_CurIncome"] = 0;
            newRow["peach_2_2"] = 0;
            newRow["peach_2_2_CurIncome"] = 0;
            newRow["peach_2_3"] = 0;
            newRow["peach_2_3_CurIncome"] = 0;
            newRow["peach_2_4"] = 0;
            newRow["peach_2_4_CurIncome"] = 0;
            newRow["peach_2_5"] = 0;
            newRow["peach_2_5_CurIncome"] = 0;
            newRow["peach_3_1"] = 0;
            newRow["peach_3_1_CurIncome"] = 0;
            newRow["user_store"] = 0;
            newRow["user_totalgains"] = 0;
        }

        public DataTable QueryUserInfo(string username)
        {
            string strSql = @"select a.user_name,
                            ISNULL(a.user_realname, '') as user_realname,
                            ISNULL(c.tree_type, '0') as tree_type,
                            ISNULL(c.tree_id, '0') as tree_id,
                            ISNULL(c.fruit_number, '0') as fruit_number,
                            ISNULL(b.user_store, '0') as user_store,
                            ISNULL(c.update_time, '2017-01-01 0:00:00') as tree_update_time,
                            ISNULL(c.user_total_gains, '0') as user_total_gains,
                            ISNULL(b.user_totalgains, '0') as user_totalgains
                            from plant_user as a
                            left join dbo.plant_user_income as b on a.user_name = b.user_name
                            left join dbo.plant_user_tree as c on a.user_name = c.user_name
                            where a.user_name='{0}'";
            strSql = string.Format(strSql, username);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            return dt;
        }
    }
}
