using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Showroom.Models.DataAccess;
using System.Linq;
using Showroom.Models;
namespace SunriseShowroom.Controllers
{
    public class CatalogueController : Controller
    {
        private ShowroomRepository rep = new ShowroomRepository();
        //
        // GET: /Catalogue/
       
        public ActionResult Index()
        {
            // Action GetList, dùng cho Grid load danh sách dữ liệu
            var CatalogueList = rep.GetProductCatalogueList();
            return View(CatalogueList);
        }

        public ActionResult Edit(int id)
        {
            Catalogue catalogue = rep.GetProductCatalogueInfo(id);
            return View(catalogue);
        }

        [HttpPost]
        public ActionResult Edit(Catalogue catalogue)
        {
            if (ModelState.IsValid)
            {
                catalogue.NameEn = "Nay thi name en";
                rep.UpdateProductCatalogue(catalogue);
                return RedirectToAction("Index");
            }
            return View(catalogue);
        }

        public ActionResult Delete(int id)
        {
            try
            {
                rep.DeleteProductCatalogue(id);
                return RedirectToAction("Index");
            }
            catch {
                return RedirectToAction("Index");
            }            
        }
    }
}
