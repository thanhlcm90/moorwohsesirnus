using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Showroom.Models.DataAccess;
using Common;

namespace Frontend.Controllers
{
    public class ProductController : Controller
    {
        ShowroomRepository rep = new ShowroomRepository();
        int maxProductInPage = 20;
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

        public ActionResult DetailProduct(int id)
        {
            var model = rep.GetProductInfo(id);
            //Lấy danh sách property catalogue
            var PropertyCatalogue = rep.GetPropertyCatalogueList();
            ViewBag.PropertyCatalogue = PropertyCatalogue;

            // Lấy danh sách property
            var PropertyList = rep.GetPropertyList();
            ViewBag.PropertyList = PropertyList;

            //Get các nhóm thuộc tính
            var ProSubCate = rep.GetPropertySubCatalogueList();
            ViewBag.PropertySubCatalogueList = ProSubCate;

            // Lấy danh sách thuộc tính của product theo ID của product
            var ProductProperty = rep.GetPropertyProductList(id);
            ViewBag.ProductPropertyList = ProductProperty;

            return View(model);
        }

        public ActionResult Product(string id1, string id2)
        {
            var model = rep.GetProductCatalogueInfo(clsHelper.fncCnvNullToInt(id1));
            var ProductByCatalogue = rep.GetListProductSame(clsHelper.fncCnvNullToInt(id1));
            ViewBag.MaxPage = ProductByCatalogue.Count() / maxProductInPage +1;
            ViewBag.Id = clsHelper.fncCnvNullToInt(id1);
            ViewBag.Curenpage = clsHelper.fncCnvNullToInt(id2); ;
            ProductByCatalogue = ProductByCatalogue.Skip(maxProductInPage * (clsHelper.fncCnvNullToInt(id2) - 1)).Take(maxProductInPage).ToList();
            ViewBag.ProductByCatalogue = ProductByCatalogue;
            return View(model);
        }

    }
}
