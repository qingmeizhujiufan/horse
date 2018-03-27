using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BLL
{
    public class Order
    {
        /// <summary>
        /// 获取所有订单
        /// </summary>     
        /// <returns></returns>
        public DataTable GetAllOrder()
        {
            DataTable dt = new DataTable();
            string strSql = @"select    id,
                                        openid,
                                        telephone,
                                        amount,
                                        CAST(ISNULL(paymoney, 0) as decimal(18,2)) as paymoney,
                                        area,
                                        company,
                                        state,
                                        create_time                              
                                from dbo.cz_payorder
                                order by create_time asc ";
            strSql = string.Format(strSql);
            dt = DBHelper.SqlHelper.GetDataTable(strSql);

            return dt;
        }

    }
}
