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
                    var id = rep.InsertNews(objNews); // Insert và trả về id vừa mới insert xong.
                    //Save image vào hệ thống
                    return RedirectToAction("Edit", new { id });
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    objNews.TitleEn = Code.Utilities.ConvertToUnSign(objNews.Title);
                    if (Request.Files.Count > 0)// ko chọn file upload nhưng Request.File vẫn trả về 1 item
                    {
                        HttpPostedFileBase file = Request.Files[0];
                        if (file.ContentLength > 0)
                        {
                            //Xử lý thêm ảnh
                            var newImageFolder = AppDomain.CurrentDomain.BaseDirectory + "Images\\News\\" + objNews.Id;
                            // If directory does not exist, don't even try 
                            if (!Directory.Exists(newImageFolder))
                            {
                                Directory.CreateDirectory(newImageFolder);
                            }
                            //Save ảnh vào thư mục

                            string path = System.IO.Path.Combine(newImageFolder,
                                                                 System.IO.Path.GetFileName(file.FileName));
                            try
                            {
                                //check file.ContentLength >0 vì người ta ko chọn file nhưng Request.File vẫn trả về 1 item.

                                //Xóa ảnh cũ
                                Code.Utilities.DeleteFiles(newImageFolder);
                                file.SaveAs(path);
                                objNews.Image = file.FileName;
                                // Copy ảnh qua front end;
                                var newsImageFolder = AppDomain.CurrentDomain.BaseDirectory + "Images\\News\\" + objNews.Id;
                                var newsImageFrontEndFolder = newsImageFolder.Replace("Backend", "Frontend");
                                //Lưu ảnh cũ đồng thời xóa ảnh mới.
                                Code.Utilities.PublishImages(newImageFolder, newsImageFrontEndFolder, true);

                            }
                            catch (Exception)
                            {
                            }
                        }
                        else
                        {
                            //Nếu ko có ảnh upload thì lấy ảnh cũ;
                            objNews.Image = newsImage;
                        }

                    }

                    rep.UpdateNews(objNews);
                    return RedirectToAction("Index");
                }
            }
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
