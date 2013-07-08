using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Showroom.Models.DataAccess;
using Showroom.Models;
namespace SunriseShowroom.Controllers
{
    public class AdminNewsController : Controller
    {
        private ShowroomRepository rep = new ShowroomRepository();
        //
        // GET: /News/
        private static string newsImage = "";
        public ActionResult Index()
        {
            // Action GetList, dùng cho Grid load danh sách dữ liệu
            var newsList = rep.GetNewsList();
            return View(newsList);
        }

        public ActionResult Edit(int id)
        {
            //Lấy danh mục nhóm sản phẩm
            var list = rep.GetNewsCatalogueList();
            ViewBag.CatelogueList = (from p in list
                                     where p.Atcflg.Trim() == "A"
                                     select new SelectListItem
                                     {
                                         Value = p.Id.ToString(),
                                         Text = p.Name,
                                     }).ToList();
            News news;
            //sửa product
            if (id != 0)
            {
                news = rep.GetNewsInfo(id);
                if (news != null)
                {
                    newsImage = news.Image;
                }
            }
            else //id =0 là thêm mới product
            {
                news = new News();
                news.PostDate = DateTime.Now;
            }
            return View(news);
        }

        /// <summary>
        /// Lưu thông tin vừa edit
        /// </summary>
        /// <param name="catalogue"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(News objNews)
        {
            if (objNews.Id == 0) //Trường hợp thêm mới product
            {
                if (ModelState.IsValid)
                {
                    objNews.TitleEn = Code.Utilities.ConvertToUnSign(objNews.Title);
                    HttpPostedFileBase file = Request.Files[0];
                    if (file.ContentLength > 0)
                    {
                        objNews.Image = file.FileName;
                    }
                    var id = rep.InsertNews(objNews); // Insert và trả về id vừa mới insert xong.

                    var imageFolder = Server.MapPath(@"~/Images/News/" + objNews.Id);
                    // If directory does not exist, don't even try 
                    if (!Directory.Exists(imageFolder))
                    {
                        Directory.CreateDirectory(imageFolder);
                    }
                    string path = System.IO.Path.Combine(imageFolder, System.IO.Path.GetFileName(file.FileName));
                    if (!System.IO.File.Exists(path) && file.ContentLength > 0)
                    {
                        //Xóa ảnh cũ
                        Code.Utilities.DeleteFiles(imageFolder);
                        file.SaveAs(path);
                        objNews.Image = file.FileName;
                    }
                    return RedirectToAction("Index");
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    objNews.TitleEn = Code.Utilities.ConvertToUnSign(objNews.Title);
                    var imageFolder = Server.MapPath(@"~/Images/News/" + objNews.Id);
                    HttpPostedFileBase file = Request.Files[0];
                    // If directory does not exist, don't even try 
                    if (!Directory.Exists(imageFolder))
                    {
                        Directory.CreateDirectory(imageFolder);
                    }
                    string path = System.IO.Path.Combine(imageFolder, System.IO.Path.GetFileName(file.FileName));
                    if (!System.IO.File.Exists(path) && file.ContentLength > 0)
                    {
                        //Xóa ảnh cũ
                        Code.Utilities.DeleteFiles(imageFolder);
                        file.SaveAs(path);
                        objNews.Image = file.FileName;
                    }else
                    {
                        objNews.Image = newsImage;
                    }
                    rep.UpdateNews(objNews);
                    return RedirectToAction("Index");
                }
            }
            //Lấy danh mục nhóm sản phẩm
            var list = rep.GetNewsCatalogueList();
            ViewBag.CatelogueList = (from p in list
                                     where p.Atcflg.Trim() == "A"
                                     select new SelectListItem
                                     {
                                         Value = p.Id.ToString(),
                                         Text = p.Name,
                                     }).ToList();
            return View(objNews);
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
                rep.DeleteNews(id);
                //Xóa folder ảnh
                var newImageFolder = AppDomain.CurrentDomain.BaseDirectory + "Images\\News\\" + id;
                // If directory does not exist, don't even try 
                if (Directory.Exists(newImageFolder))
                {
                    Directory.Delete(newImageFolder, true);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }


    }
}
