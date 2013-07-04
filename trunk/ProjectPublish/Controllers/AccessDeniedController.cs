using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectPublish.Controllers
{
    public class AccessDeniedController : Controller
    {
        //
        // GET: /AccessDenied/

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

    }
}
