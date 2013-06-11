using System;
using System.Linq;
using System.Web.Mvc;
using Showroom.Models.DataAccess;
using Showroom.Models;
using System.Collections.Generic;
namespace SunriseShowroom.Controllers
{
    public class SystemInfoController : Controller
    {
        private ShowroomRepository rep = new ShowroomRepository();

        [Authorize]
        public ActionResult Index()
        {
            // Action GetList, dùng cho Grid load danh sách dữ liệu
            var list = rep.GetSystemInfoList();
            return View(list);
        }

        [HttpPost]
        public ActionResult Index(String ContactInfo, String PhoneInfo, String EmailInfo, String SkypeInfo, String YahooInfo, String TwitterInfo, String FacebookInfo)
        {
            List<SystemInfo> list = new List<SystemInfo>();
            list.Add(new SystemInfo { Code = SystemInfo.CONTACT_INFO, Value = ContactInfo });
            list.Add(new SystemInfo { Code = SystemInfo.EMAIL_INFO, Value = EmailInfo });
            list.Add(new SystemInfo { Code = SystemInfo.FACEBOOK_INFO, Value = FacebookInfo });
            list.Add(new SystemInfo { Code = SystemInfo.PHONE_INFO, Value = PhoneInfo });
            list.Add(new SystemInfo { Code = SystemInfo.SKYPE_INFO, Value = SkypeInfo });
            list.Add(new SystemInfo { Code = SystemInfo.TWITTER_INFO, Value = TwitterInfo });
            list.Add(new SystemInfo { Code = SystemInfo.YAHOO_INFO, Value = YahooInfo });
            rep.UpdateSystemInfo(list);
            return View(list);
        }
    }
}
