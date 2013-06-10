using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Showroom.Models.DataAccess;

namespace Frontend.Controllers
{
    public class NewsController : Controller
    {
        ShowroomRepository rep = new ShowroomRepository();
        int maxProductInPage = 2;
        //
        // GET: /News/

        public ActionResult NewsAll(int page)
        {
            //Lấy danh sách tất cả tin tức trên hệ thống
            var lstNews = rep.GetNewsList();
            ViewBag.MaxPage = lstNews.Count() / 1;
            ViewBag.Curenpage = page;
            lstNews = lstNews.Skip(maxProductInPage * (page - 1)).Take(maxProductInPage).ToList();
            ViewBag.lstNews = lstNews;
            return View();
        }

    }
}
