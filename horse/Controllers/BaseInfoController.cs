using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace horse.Controllers
{
    public class BaseInfoController : Controller
    {
        //
        // GET: /BseInfo/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CompanyInfo()
        {
            return View();
        }

        public ActionResult ApplicantInfo()
        {
            return View();
        }

    }
}
