using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using Common;
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

        public ActionResult SearchProduct(int CID, string ModelID, string TYPEID)
        {
            List<Product> lstProduct = rep.SearchProduct(CID, ModelID, TYPEID);

            ViewBag.ProductSearchList = lstProduct;

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
            if (ModelState.IsValid)
            {
                models.PostDate = DateTime.Now;
                models.Actflg = '1';
                rep.InsertContacts(models);
            } else
            {
                ModelState.AddModelError("", "Bạn phải nhập đầy đủ các thông tin.");
            }
            return View();
        }

        [WebMethod]
        public JsonResult LoadModelByCatalogueId(string cataID)
        {
            //Select tất cả các product của Catalogue đó --> Select tất cả các model của các product đó.
            var productByCatalogue = rep.GetListProductSame(clsHelper.fncCnvNullToInt(cataID));
            var arlModels = new ArrayList();
            
            foreach (var product in productByCatalogue)
            {
                //Select Model của sản phẩm đó.
                var pModel = rep.GetModelOfProduct(product.Id);
                if (pModel !=null)
                {
                    pModel = (ProductProperty) pModel;
                    if (!arlModels.Contains(new { Value = pModel.Value, Display = pModel.Value }) && pModel.Value!="")
                    {
                        arlModels.Add(new { Value = pModel.Value, Display = pModel.Value });
                    }
                 
                }
            }
            return new JsonResult { Data = arlModels };
        }
    }
}
