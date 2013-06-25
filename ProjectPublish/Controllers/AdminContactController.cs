using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Showroom.Models.DataAccess;
using Showroom.Models;

namespace SunriseShowroom.Controllers
{
    public class AdminContactController : Controller
    {
        private ShowroomRepository rep = new ShowroomRepository();
        //
        // GET: /Contact/

        public ActionResult Index()
        {
            // Action GetList, dùng cho Grid load danh sách dữ liệu
            var ContactList = rep.GetContactsList();
            return View(ContactList);
        }

        /// <summary>
        /// Xem thông tin liên hệ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult View(int id)
        {
            Contact contact = rep.GetContactsInfo(id);
            return View(contact);
        }

        /// <summary>
        /// Xóa thông tin liên hệ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult Delete(int id)
        {
            try
            {
                rep.DeleteContacts(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}
