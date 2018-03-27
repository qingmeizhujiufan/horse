using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace horse.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserDetailInfo(string id) {
            ViewData["id"] = id;
            return View();
        }

        public ActionResult Employee()
        {
            return View();
        }

        public ActionResult EmployeeDetailInfo(string id)
        {
            ViewData["id"] = id;
            return View();
        }
    }
}
