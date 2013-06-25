using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Showroom.Models.DataAccess;
using Showroom.Models;
namespace SunriseShowroom.Controllers
{
    public class AdminNewsCatalogueController : Controller
    {
        private ShowroomRepository rep = new ShowroomRepository();
        //
        // GET: /Catalogue/
        [Authorize]
        public ActionResult Index()
        {
            // Action GetList, dùng cho Grid load danh sách dữ liệu
            var newsCatalogueList = rep.GetNewsCatalogueList();
            return View(newsCatalogueList);
        }

        /// <summary>
        /// Lấy thông tin bind lên view để edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult Edit(int id)
        {
            NewsCatalogue newsCatalogue;
            //sửa product
            if (id != 0)
            {
                newsCatalogue = rep.GetNewsCatalogueInfo(id);
            }
            else //id =0 là thêm mới product
            {
                newsCatalogue = new NewsCatalogue();
            }
            return View(newsCatalogue);
        }


        /// <summary>
        /// Lưu thông tin vừa edit
        /// </summary>
        /// <param name="catalogue"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(NewsCatalogue newsCatalogue)
        {
            if (newsCatalogue.Id == 0) //Trường hợp thêm mới product
            {
                if (ModelState.IsValid)
                {
                    // product.CatalogueId = 1;
                    newsCatalogue.NameEn = Code.Utilities.ConvertToUnSign(newsCatalogue.Name);
                    var id = rep.InsertNewsCatalogue(newsCatalogue); // Insert và trả về id vừa mới insert xong.
                    return RedirectToAction("Index");
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    newsCatalogue.NameEn = Code.Utilities.ConvertToUnSign(newsCatalogue.Name);
                    rep.UpdateNewsCatalogue(newsCatalogue);
                    return RedirectToAction("Index");
                }
            }
            return View(newsCatalogue);
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
                rep.DeleteNewsCatalogue(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}
