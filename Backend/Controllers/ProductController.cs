using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Showroom.Models.DataAccess;
using Showroom.Models;

namespace SunriseShowroom.Controllers
{
    public class ProductController : Controller
    {
        private ShowroomRepository rep = new ShowroomRepository();
        //
        // GET: /Product/

        public ActionResult Index()
        {
            // Action GetList, dùng cho Grid load danh sách dữ liệu
            var ProductList = rep.GetProductsList();
            return View(ProductList);
        }

        public ActionResult Edit(int id)
        {
            Product product = rep.GetProductInfo(id);
            var list = rep.GetProductCatalogueList();
            ViewBag.CatalogueList = (from p in list
                                     select new SelectListItem
                                     {
                                         Value = p.Id.ToString(),
                                         Text = p.Name,
                                     }).ToList();
            //model.listCatalogue = rep
            ViewBag.ProductList = rep.GetProductsList();
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
               // product.CatalogueId = 1;
                product.NameEn = "Nay thi name en";
                rep.UpdateProducts(product);
                return RedirectToAction("EditProductProperties", new { product.Id });
            }
            return View(product);
        }

        public ActionResult EditProductProperties(int id)
        {
            Product product = rep.GetProductInfo(id);
            var list = rep.GetProductCatalogueList();
            ViewBag.CatalogueList = (from p in list
                                     select new SelectListItem
                                     {
                                         Value = p.Id.ToString(),
                                         Text = p.Name,
                                     }).ToList();
            //model.listCatalogue = rep
            ViewBag.ProductList = rep.GetProductsList();
            return View(product);
        }


    }
}
