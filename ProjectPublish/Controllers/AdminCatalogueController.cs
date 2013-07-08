using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Showroom.Models.DataAccess;
using Showroom.Models;
using System.IO;
namespace SunriseShowroom.Controllers
{
    public class AdminCatalogueController : Controller
    {
        private ShowroomRepository rep = new ShowroomRepository();
        private static string catalogueLogo = "";
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
            Catalogue catalogue;
            //sửa product
            if (id != 0)
            {
                catalogue = rep.GetProductCatalogueInfo(id);
                catalogueLogo = catalogue.Image;//Lưu ảnh cũ vào biến
            }
            else //id =0 là thêm mới product
            {
                catalogue = new Catalogue();
            }
            return View(catalogue);
        }


        /// <summary>
        /// Lưu thông tin vừa edit
        /// </summary>
        /// <param name="catalogue"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(Catalogue catalogue)
        {
            if (catalogue.Id == 0) //Trường hợp thêm mới product
            {
                if (ModelState.IsValid)
                {
                    // product.CatalogueId = 1;
                    catalogue.NameEn = Code.Utilities.ConvertToUnSign(catalogue.Name);
                   

                    HttpPostedFileBase file = Request.Files[0];
                    if (file.ContentLength > 0)
                    {
                        catalogue.Image = file.FileName;
                    }
                    rep.InsertProductCatalogue(catalogue);  
                    //Lưu ảnh
                    var imageFolder = Server.MapPath(@"~/Images/Catalogue/" + catalogue.Id);
                    // If directory does not exist, don't even try 
                    if (!Directory.Exists(imageFolder))
                    {
                        Directory.CreateDirectory(imageFolder);
                    }
                    string path = System.IO.Path.Combine(imageFolder, System.IO.Path.GetFileName(file.FileName));
                    if (file.ContentLength > 0)
                    {
                        //Xóa ảnh cũ
                        Code.Utilities.DeleteFiles(imageFolder);
                        file.SaveAs(path);
                        catalogue.Image = file.FileName;
                    }
                    return RedirectToAction("Index");
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    catalogue.NameEn = Code.Utilities.ConvertToUnSign(catalogue.Name);
                    var imageFolder = Server.MapPath(@"~/Images/Catalogue/" + catalogue.Id);
                    HttpPostedFileBase file = Request.Files[0];
                    // If directory does not exist, don't even try 
                    if (!Directory.Exists(imageFolder))
                    {
                        Directory.CreateDirectory(imageFolder);
                    }
                    string path = System.IO.Path.Combine(imageFolder, System.IO.Path.GetFileName(file.FileName));
                    if ( file.ContentLength > 0)
                    {
                        //Xóa ảnh cũ
                        Code.Utilities.DeleteFiles(imageFolder);
                        file.SaveAs(path);
                        catalogue.Image = file.FileName;
                    }
                    else
                    {
                        catalogue.Image = catalogueLogo;//gán lại ảnh cũ.
                    }
                    rep.UpdateProductCatalogue(catalogue);
                    return RedirectToAction("Index");
                }
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
