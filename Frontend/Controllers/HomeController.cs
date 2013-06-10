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
            ViewBag.Message = "Home Showroom car";
            var list = rep.GetProductCatalogueList();
            ViewBag.CatalogueListDrop = (from p in list
                                     select new SelectListItem
                                     {
                                         Value=p.Id.ToString(),
                                         Text=p.Name,
                                     }).ToList();
            // Danh sách dữ liệu danh mục
            ViewBag.CataloguesList = rep.GetProductCatalogueList();
            // Danh sách dữ liệu sản phẩm
            ViewBag.ProductList = rep.GetProductsList();
            return View();
        }

        public ActionResult Search()
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
        public ActionResult Search(Contact models)
        {
            models.PostDate = DateTime.Now;
            models.Actflg = '1';
            rep.InsertContacts(models);
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact()
        {
            return View();
        }

        //public ActionResult List()
        //{
        //    var list = rep.GetProductCatelogueList();
        //    return View(list);
        //}
    }
}
