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

        public ActionResult SearchResault(int CID, string keyword, int priceFrom, int priceTo)
        {
            List<Product> lstProduct = rep.GetProductsList();
            List<Product> lstProductSearch = new List<Product>();
            lstProductSearch = (from n in lstProduct
                                where n.Name.Contains(keyword) && n.CatalogueId == CID && n.Price >= priceFrom && n.Price <= priceTo
                                select n
                                  ).ToList();
            ViewBag.ProductSearchList = lstProductSearch;
            return View();
        }

        //[HttpPost]
        //public ActionResult SearchResault(int idCatalogue, string keyword, int priceFrom, int priceTo)
        //{
        //    List<Product> lstProduct = rep.GetProductsList();
        //    List<Product> lstProductSearch = new List<Product>();
        //    lstProductSearch = (from n in lstProduct
        //                          where n.Name.Contains(keyword) && n.CatalogueId == idCatalogue && n.Price > priceFrom && n.Price < priceTo
        //                          select n
        //                          ).ToList();
        //    ViewBag.ProductSearchList = lstProductSearch;
        //    return View();
        //}

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
