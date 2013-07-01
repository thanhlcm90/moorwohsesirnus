using System;
using System.Linq;
using System.Web.Mvc;
using Showroom.Models.DataAccess;
using Showroom.Models;
namespace SunriseShowroom.Controllers
{
    public class AdminPropertySubCatalogueController : Controller
    {
        private ShowroomRepository rep = new ShowroomRepository();
        //
        // GET: /propertySubCatalogue/
        [Authorize]
        public ActionResult Index()
        {
            // Action GetList, dùng cho Grid load danh sách dữ liệu
            var propertySubCatalogueList = rep.GetPropertySubCatalogueList();
            return View(propertySubCatalogueList);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            PropertySubCatalogue propertySubCatalogueInfo;
            //sửa product
            if (id != 0)
            {
                propertySubCatalogueInfo = rep.GetPropertySubCatalogueInfo(id);
            }
            else //id =0 là thêm mới product
            {
                propertySubCatalogueInfo = new PropertySubCatalogue();
            }
            return View(propertySubCatalogueInfo);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(PropertySubCatalogue propertySubCatalogue)
        {
            if (ModelState.IsValid)
            {
                propertySubCatalogue.NameEn = Code.Utilities.ConvertToUnSign(propertySubCatalogue.Name);
                rep.UpdatePropertySubCatalogue(propertySubCatalogue);
                return RedirectToAction("Index");
            }
            return View(propertySubCatalogue);
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            try
            {
                rep.DeletePropertySubCatalogue(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

    }
}
