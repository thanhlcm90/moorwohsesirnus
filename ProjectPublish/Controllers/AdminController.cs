using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SunriseShowroom.Controllers
{
    public class AdminController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Message = "Chào mừng đến với trang quản lý của hệ thống xe Showromm Sunrise!";

            return View();
        }

        [Authorize]
        public ActionResult About()
        {
            return View();
        }
    }
}
