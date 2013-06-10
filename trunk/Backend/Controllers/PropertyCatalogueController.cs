using System;
using System.Linq;
using System.Web.Mvc;
using Showroom.Models.DataAccess;
using Showroom.Models;
namespace SunriseShowroom.Controllers
{
    public class PropertyCatalogueController : Controller
    {
        private ShowroomRepository rep = new ShowroomRepository();
        //
        // GET: /PropertyCatalogue/

        public ActionResult Index()
        {
            // Action GetList, dùng cho Grid load danh sách dữ liệu
            var propertyCatalogueList = rep.GetPropertyCatalogueList();
            return View(propertyCatalogueList);
        }

        public ActionResult Edit(int id)
        {
            PropertyCatalogue propertyCatalogueInfo = rep.GetPropertyCatalogueInfo(id);
            return View(propertyCatalogueInfo);
        }

        [HttpPost]
        public ActionResult Edit(PropertyCatalogue propertyCatalogue)
        {
            if (ModelState.IsValid)
            {
                rep.UpdatePropertyCatalogue(propertyCatalogue);
                return RedirectToAction("Index");
            }
            return View(propertyCatalogue);
        }

        public ActionResult Delete(int id)
        {
            try
            {
                rep.DeletePropertyCatalogue(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

    }
}
