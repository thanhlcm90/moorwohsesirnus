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
        [Authorize]
        public ActionResult Index()
        {
            // Action GetList, dùng cho Grid load danh sách dữ liệu
            var ProductList = rep.GetProductsList();
            return View(ProductList);
        }

        [Authorize]
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

        [Authorize]
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
               // product.CatalogueId = 1;
                product.NameEn = "Nay thi name en";
               // rep.UpdateProducts(product);
                return RedirectToAction("EditProductProperties", new { product.Id });
            }
            return View(product);
        }

        /// <summary>
        /// Gen các control lên form để edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult EditProductProperties(int id)
        {
            //var model = rep.GetProductInfo(id);
           
            //Get các thuộc tính
            var PropertyList = rep.GetPropertyList();
            ViewBag.PropertyList = PropertyList;
            //Get các group thuộc tính
            var ProCate = rep.GetPropertyCatalogueList();
            ViewBag.PropertyCatalogueList = ProCate;
            //Get các nhóm thuộc tính
            var ProSubCate = rep.GetPropertySubCatalogueList();
            ViewBag.PropertySubCatalogueList = ProSubCate;
            //Get các thuộc tính của sản phẩm
            var ProductProperty = rep.GetPropertyProductList(id);
            ViewBag.ProductPropertyList = ProductProperty;

            return View();
        }


        /// <summary>
        /// Sửa các thuộc tính cho sản phẩm
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public ActionResult EditProductProperties(Product product)
        {
            return RedirectToAction("EditProductImage", new { product.Id });
        }

        /// <summary>
        /// Load form
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult EditProductImage(int id)
        {
            return View();
        }

    }
}
