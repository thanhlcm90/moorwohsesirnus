using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Showroom.Models.DataAccess;

namespace Frontend.Controllers
{
    public class ProductController : Controller
    {
        ShowroomRepository rep = new ShowroomRepository();
        int maxProductInPage = 3;
        //
        // GET: /Product/

        public ActionResult Details(int id)
        {
           var model =  rep.GetProductInfo(id);
            // Lấy danh sách Show main
           var PropertyList = rep.GetPropertyOFProduct(model.Id);
           ViewBag.PropertyList = PropertyList;

            // Lấy danh sách chuyên mục
           var ProCate = rep.GetPropertyCatalogueList();
           ViewBag.PropertyCatalogueList = ProCate;

            // Lấy danh sách Sub Cate
           var ProSubCate = rep.GetPropertySubCatalogueList();
           ViewBag.PropertySubCatalogueList = ProSubCate;

            // Lấy danh sách thuộc tính của product theo ID của sản phẩm
           var ProductProperty = rep.GetPropertyProductList(id);
           ViewBag.ProductPropertyList = ProductProperty;

           return View(model);
        }

        public ActionResult Product(int id, int page)
        {
            var ProductByCatalogue = rep.GetListProductSame(id);
            ViewBag.MaxPage = ProductByCatalogue.Count()/3;
            ViewBag.Id = id;
            ViewBag.Curenpage = page;
            ProductByCatalogue = ProductByCatalogue.Skip(maxProductInPage * (page-1)).Take(maxProductInPage).ToList();
            ViewBag.ProductByCatalogue = ProductByCatalogue;
            return View();
        }

    }
}
