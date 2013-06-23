using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SunriseShowroom.Controllers
{
    public class ImageSlideController : Controller
    {
        //
        // GET: /ImageSlide/

        [Authorize]
        public ActionResult Index()
        {
            var list = Membership.GetAllUsers();
            string controller = ControllerContext.RouteData.GetRequiredString("controller");
            if (!User.IsInRole("admin")) return RedirectToAction("ErrorPermission", "Account", null);
            var listTop = new ArrayList();
            var imageFolder = AppDomain.CurrentDomain.BaseDirectory + "Images\\Slide\\Top";
            var producImagePath = "/Images/Slide/Top";
            var dir = new DirectoryInfo(imageFolder);
            if (Directory.Exists(imageFolder))
            {
                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo file in files)
                {
                    if (file.Extension == ".jpg" || file.Extension == ".jpeg" || file.Extension == ".gif" || file.Extension == ".png")
                    {
                        listTop.Add(producImagePath + "/" + file.Name);
                    }
                }
            }
            ViewBag.ImageListTop = listTop;

            var listBottom = new ArrayList();
            imageFolder = AppDomain.CurrentDomain.BaseDirectory + "Images\\Slide\\Bottom";
            producImagePath = "/Images/Slide/Bottom";
            dir = new DirectoryInfo(imageFolder);
            if (Directory.Exists(imageFolder))
            {
                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo file in files)
                {
                    if (file.Extension == ".jpg" || file.Extension == ".jpeg" || file.Extension == ".gif" || file.Extension == ".png")
                    {
                        listBottom.Add(producImagePath + "/" + file.Name);
                    }
                }
            }
            ViewBag.ImageListBottom = listBottom;
            return View();
        }

        [Authorize(Roles = "ImageSlide")]
        [HttpPost]
        public ActionResult AddTop()
        {
            var imageFolder = AppDomain.CurrentDomain.BaseDirectory + "Images\\Slide\\Top";

            // If directory does not exist, don't even try 
            if (!Directory.Exists(imageFolder))
            {
                Directory.CreateDirectory(imageFolder);
            }
            //Save ảnh vào thư mục
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i];
                string path = System.IO.Path.Combine(imageFolder, System.IO.Path.GetFileName(file.FileName));
                if (!System.IO.File.Exists(path) && file.ContentLength > 0)
                {
                    file.SaveAs(path);
                }
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "ImageSlide")]
        [HttpPost]
        public ActionResult AddBottom()
        {
            var imageFolder = AppDomain.CurrentDomain.BaseDirectory + "Images\\Slide\\Bottom";

            // If directory does not exist, don't even try 
            if (!Directory.Exists(imageFolder))
            {
                Directory.CreateDirectory(imageFolder);
            }
            //Save ảnh vào thư mục
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i];
                string path = System.IO.Path.Combine(imageFolder, System.IO.Path.GetFileName(file.FileName));
                if (!System.IO.File.Exists(path) && file.ContentLength > 0)
                {
                    file.SaveAs(path);
                }
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "ImageSlide")]
        [HttpPost]
        public ActionResult DeleteImage(string image)
        {
            var imagePath = image.Replace("/", "\\");
            var productImageFolder = AppDomain.CurrentDomain.BaseDirectory + imagePath;
            if (System.IO.File.Exists(productImageFolder))
            {
                try
                {
                    System.IO.File.Delete(productImageFolder);
                }
                catch (System.IO.IOException e)
                {
                }
            }
            return RedirectToAction("Index");
        }
    }
}
