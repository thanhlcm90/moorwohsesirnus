using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Showroom.Models;

namespace Frontend.Controllers
{
    public class ContactController : Controller
    {
        //
        // GET: /Contact/
        Showroom.Models.DataAccess.ShowroomRepository rep = new Showroom.Models.DataAccess.ShowroomRepository();
        public ActionResult Send()
        {
            return View();
        }

        /// <summary>
        /// Insert New Contact
        /// Author: ThuanNH
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Send(Contact models)
        {
            models.PostDate = DateTime.Now;
            models.Actflg = '1';
            rep.InsertContacts(models);
            ViewBag.CurrentPage = "contact";
            return View();
        }
    }
}
