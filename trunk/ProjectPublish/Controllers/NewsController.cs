using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Showroom.Models.DataAccess;
using Common;

namespace Frontend.Controllers
{
    public class NewsController : Controller
    {
        ShowroomRepository rep = new ShowroomRepository();
        int maxNewInPage = 20;
        //
        // GET: /News/

        public ActionResult NewsAll(int page)
        {
            //Lấy danh sách tất cả tin tức trên hệ thống
            var lstNews = rep.GetNewsList();
            ViewBag.lstTopNews = rep.GetNewsList();
            ViewBag.lstCatalogueNews = rep.GetNewsCatalogueList();
            ViewBag.MaxPage = lstNews.Count() / maxNewInPage +1;
            ViewBag.Curenpage = page;
            lstNews = lstNews.Skip(maxNewInPage * (page - 1)).Take(maxNewInPage).ToList();
            ViewBag.lstNews = lstNews;
            ViewBag.CurrentPage = "news";
            return View();
        }

        public ActionResult NewsbyCatalogue(string id1, string id2)
        {
            var model = rep.GetNewsCatalogueInfo(clsHelper.fncCnvNullToInt(id1));
            var lstNews = rep.GetListNewsByCATAID(clsHelper.fncCnvNullToInt(id1));
            ViewBag.lstTopNews = rep.GetNewsList();
            ViewBag.lstCatalogueNews = rep.GetNewsCatalogueList();
            ViewBag.MaxPage = lstNews.Count() / maxNewInPage + 1;
            ViewBag.Id = clsHelper.fncCnvNullToInt(id1);
            ViewBag.Curenpage = clsHelper.fncCnvNullToInt(id2);
            lstNews = lstNews.Skip(maxNewInPage * (clsHelper.fncCnvNullToInt(id2) - 1)).Take(maxNewInPage).ToList();
            ViewBag.lstNews = lstNews;
            ViewBag.CurrentPage = "news";
            return View(model);
        }

        public ActionResult NewsDetails(int id)
        {
            var model = rep.GetNewsInfo(id);
            ViewBag.lstTopNews = rep.GetNewsList();
            ViewBag.lstCatalogueNews = rep.GetNewsCatalogueList();
            ViewBag.lstNewsSame = rep.GetListNewsByCATAID(model.CatelogueId);
            ViewBag.CurrentPage = "news";
            return View(model);
        }


    }
}
