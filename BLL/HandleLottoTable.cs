using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BLL
{
    public class HandleLottoTable
    {
        public static bool IsFirstGuess(string userId)
        {
            string strSql = @"select* from plant_lotto where userId='{0}'";
            strSql = string.Format(strSql, userId);
            DataTable dt = DBHelper.SqlHelper.GetDataTable(strSql);
            if (dt.Rows.Count > 0)
                return false;
            return true;
        }

        public static int RandomLottoType()
        {
            Random ran = new Random();
            int n = ran.Next(0, 100);
            if (n < 62)
                return 0;
            else if (n < 65)
                return 1;
            else if (n < 70)
                return 2;
            else
                return 3;
        }

        public static int RandomLottoCase()
        {
            return 1;
        }
    }

    
}
