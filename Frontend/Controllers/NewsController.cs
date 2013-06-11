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
        int maxNewInPage = 2;
        //
        // GET: /News/

        public ActionResult NewsAll(int page)
        {
            //Lấy danh sách tất cả tin tức trên hệ thống
            var lstNews = rep.GetNewsList();
            ViewBag.MaxPage = lstNews.Count() / maxNewInPage +1;
            ViewBag.Curenpage = page;
            lstNews = lstNews.Skip(maxNewInPage * (page - 1)).Take(maxNewInPage).ToList();
            ViewBag.lstNews = lstNews;
            return View();
        }

        public ActionResult NewsbyCatalogue(int id, int page)
        {
            var lstNews = rep.GetListNewsByCATAID(id);
            ViewBag.MaxPage = lstNews.Count() / maxNewInPage + 1;
            ViewBag.Id = id;
            ViewBag.Curenpage = page;
            lstNews = lstNews.Skip(maxNewInPage * (page - 1)).Take(maxNewInPage).ToList();
            ViewBag.lstNews = lstNews;
            return View();
        }


    }
}
