using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;

namespace ProjectPublish.Controllers
{
    public class AdminSevirceController : Controller
    {
        //
        // GET: /AdminSevirce/

        public static class basConst
        {
            //Đường dẫn lưu file
            public const string Linkservices = "~/SaveFile/Service.txt";
        }

        public ActionResult Index()
        {
            clsHelper.ReadFile(basConst.Linkservices);
            return View();
        }

        [Authorize]
        [HttpPost, ValidateInput(false)]
        public ActionResult Index(string Detail)
        {
            clsHelper.WriteFile(Detail, basConst.Linkservices);

            return RedirectToAction("Index");
        }

    }
}
