using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace horse.Controllers
{
    public class AdminManageController : Controller
    {
        //
        // GET: /AdminManage/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Top(string section)
        {
            string lihtml = "";
            Dictionary<string, string> dicHtml = new Dictionary<string, string>();
            dicHtml.Add("0", "<li {0}><a  href=\"/Home/Index\"><i class=\"icon-home\"></i>首页概况</a></li>");
            dicHtml.Add("1", "<li {0}><a  href=\"/User/Index\"><i class=\"icon-user\"></i>人员管理</a></li>");
            dicHtml.Add("2", "<li {0}><a  href=\"/BaseInfo/Index\"><i class=\"icon-envelope\"></i>基本信息管理</a></li>");
            dicHtml.Add("3", "<li {0}><a  href=\"/Complain/Index\"><i class=\"icon-bell-alt\"></i>投诉管理</a></li>");
            dicHtml.Add("4", "<li {0}><a  href=\"/Operation/Index\"><i class=\"icon-sitemap\"></i>运营管理</a></li>");
            dicHtml.Add("5", "<li {0}><a  href=\"/SysComment/Index\"><i class=\"icon-comments\"></i>系统消息管理</a></li>");
            dicHtml.Add("6", "<li {0}><a  href=\"/SysSetting/Index\"><i class=\"icon-cog\"></i>系统设置</a></li>");
            dicHtml.Add("7", "<li {0}><a  href=\"/TypeCommand/Index\"><i class=\"icon-tags\"></i>类别管理</a></li>");
            dicHtml.Add("8", "<li {0}><a  href=\"/StatisticalAnalysis/Index\"><i class=\"icon-bar-chart\"></i>统计分析</a></li>");
            //dicHtml.Add("9", "<li {0}><a  href=\"/StatisticalUser/Index\"><i class=\"icon-cloud\"></i>用户统计管理</a></li>");

            string strTemp = string.Empty;
            foreach (string key in dicHtml.Keys)
            {
                strTemp = string.Empty;
                if (key == section)
                {
                    strTemp = string.Format(dicHtml[key], "class=\"active\"");
                }
                else
                {
                    strTemp = string.Format(dicHtml[key], "");
                }
                lihtml += strTemp;
            }
            ViewData["lihtml"] = lihtml;

            return View();
        }

        public ActionResult Aside(string menu_id)
        {
            ViewData["menu_id"] = menu_id;
            return View(ViewData);
        }

    }
}
