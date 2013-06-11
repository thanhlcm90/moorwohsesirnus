using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Showroom.Models.DataAccess;
using Showroom.Models;
namespace SunriseShowroom.Controllers
{
    public class CatalogueController : Controller
    {
        private ShowroomRepository rep = new ShowroomRepository();
        //
        // GET: /Catalogue/
        [Authorize]
        public ActionResult Index()
        {
            // Action GetList, dùng cho Grid load danh sách dữ liệu
            var CatalogueList = rep.GetProductCatalogueList();
            return View(CatalogueList);
        }

        /// <summary>
        /// Lấy thông tin bind lên view để edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult Edit(int id)
        {
            Catalogue catalogue = rep.GetProductCatalogueInfo(id);
            return View(catalogue);
        }


        /// <summary>
        /// Lưu thông tin vừa edit
        /// </summary>
        /// <param name="catalogue"></param>
        /// <returns></returns>
        [Authorize]
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


        /// <summary>
        /// Xóa catalogue
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult Delete(int id)
        {
            try
            {
                rep.DeleteProductCatalogue(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}
