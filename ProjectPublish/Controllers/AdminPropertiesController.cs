using System;
using System.Linq;
using System.Web.Mvc;
using Showroom.Models.DataAccess;
using Showroom.Models;
namespace SunriseShowroom.Controllers
{
    public class AdminPropertiesController : Controller
    {
        private ShowroomRepository rep = new ShowroomRepository();
        //
        // GET: /Property/
        [Authorize]
        public ActionResult Index()
        {
            // Action GetList, dùng cho Grid load danh sách dữ liệu
            var propertyList = rep.GetPropertyList();
            return View(propertyList);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {

            Property propertyInfo;
            var list = rep.GetPropertyCatalogueList();
            ViewBag.PropertyCatalogueList = (from p in list
                                             where p.Actflg == 'A'
                                             select new SelectListItem
                                             {
                                                 Value = p.Id.ToString(),
                                                 Text = p.Name,
                                             }).ToList();
            var listSub = rep.GetPropertySubCatalogueList();
            ViewBag.PropertySubCatalogueList = (from p in listSub
                                                where p.Actflg == 'A'
                                                select new SelectListItem
                                                {
                                                    Value = p.Id.ToString(),
                                                    Text = p.Name,
                                                }).ToList();
            if (id != 0)
            {
                propertyInfo = rep.GetPropertyInfo(id);
            }
            else
            {
                propertyInfo = new Property();
            }
            //Lấy danh mục nhóm thuộc tính sản phẩm

            return View(propertyInfo);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(Property property)
        {
            if (ModelState.IsValid)
            {
                property.NameEn = Code.Utilities.ConvertToUnSign(property.Name);
                rep.UpdateProperty(property);
                return RedirectToAction("Index");
            }
            return View(property);
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            try
            {
                rep.DeleteProperty(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

    }
}
