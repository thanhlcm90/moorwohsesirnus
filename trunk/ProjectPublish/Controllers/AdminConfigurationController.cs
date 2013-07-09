using System;
using System.Linq;
using System.Web.Mvc;
using Showroom.Models.DataAccess;
using Showroom.Models;
using System.Collections.Generic;
namespace SunriseShowroom.Controllers
{
    public class AdminConfigurationController : Controller
    {
        private ShowroomRepository rep = new ShowroomRepository();

        [Authorize]
        public ActionResult Info()
        {
            // Action GetList, dùng cho Grid load danh sách dữ liệu
            var list = rep.GetSystemInfoList();
            return View(list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Info(String CONTACT_INFO, String EMAIL_INFO, String FACEBOOK_INFO, String PHONE_INFO, String SKYPE_INFO, String TWITTER_INFO, String YAHOO_INFO, String YOUTUBE_INFO)
        {
            List<SystemInfo> list = new List<SystemInfo>();
            list.Add(new SystemInfo { Code = SystemInfo.CONTACT_INFO, Value = CONTACT_INFO });
            list.Add(new SystemInfo { Code = SystemInfo.EMAIL_INFO, Value = EMAIL_INFO });
            list.Add(new SystemInfo { Code = SystemInfo.FACEBOOK_INFO, Value = FACEBOOK_INFO });
            list.Add(new SystemInfo { Code = SystemInfo.PHONE_INFO, Value = PHONE_INFO });
            list.Add(new SystemInfo { Code = SystemInfo.SKYPE_INFO, Value = SKYPE_INFO });
            list.Add(new SystemInfo { Code = SystemInfo.TWITTER_INFO, Value = TWITTER_INFO });
            list.Add(new SystemInfo { Code = SystemInfo.YAHOO_INFO, Value = YAHOO_INFO });
            list.Add(new SystemInfo { Code = SystemInfo.YOUTUBE_INFO, Value = YOUTUBE_INFO });
            rep.UpdateSystemInfo(list);
            return View(list);
        }
    }
}
