using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Showroom.Models.DataAccess;
using Showroom.Models;

namespace Frontend.Controllers
{
    public class HomeController : Controller
    {
        ShowroomRepository rep = new ShowroomRepository();

        public ActionResult Index()
        {
            // Danh sách dữ liệu sản phẩm
            ViewBag.ProductList = rep.GetProductsList();

            return View();
        }

        [HttpPost]
        public ActionResult Index(string strOlala)
        {
            HttpContext.Response.Write("Olala");
            return View();
        }

        public ActionResult Contact()
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
        public ActionResult Contact(Contact models)
        {
            models.PostDate = DateTime.Now;
            models.Actflg = '1';
            rep.InsertContacts(models);
            return View();
        }
    }
}
