using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Showroom.Models.DataAccess;
using Showroom.Models;
using System.IO;
using System.Collections;
using System.Web.Script.Serialization;
namespace SunriseShowroom.Controllers
{
    public class AdminProductController : Controller
    {
        private ShowroomRepository rep = new ShowroomRepository();
        //
        // GET: /Product/
        [Authorize]
        public ActionResult Index()
        {
            // Action GetList, dùng cho Grid load danh sách dữ liệu
            var ProductList = rep.GetProductsList();
            return View(ProductList);
        }

        /// <summary>
        /// Thêm mới/ Sửa sản phẩm (Load data)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult Edit(int id)
        {
            Product product;
            //Lấy danh mục nhóm sản phẩm
            var list = rep.GetProductCatalogueList();
            ViewBag.CatalogueList = (from p in list
                                     where p.Actflg == 'A'
                                     select new SelectListItem
                                     {
                                         Value = p.Id.ToString(),
                                         Text = p.Name,
                                     }).ToList();
            //sửa product
            if (id != 0)
            {
                product = rep.GetProductInfo(id);
            }
            else //id =0 là thêm mới product
            {
                product = new Product();
            }
            return View(product);
        }


        /// <summary>
        /// Thêm mới/ Sửa sản phẩm (Save data)
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(Product product)
        {
            if (product.Id == 0)//Trường hợp thêm mới product
            {
                if (ModelState.IsValid)
                {
                    // product.CatalogueId = 1;
                    product.NameEn = Code.Utilities.ConvertToUnSign(product.Name);
                  var id=  rep.InsertProduct(product);// Insert và trả về id vừa mới insert xong.
                  return RedirectToAction("EditProductProperties", new { id });
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    // product.CatalogueId = 1;
                    product.NameEn = Code.Utilities.ConvertToUnSign(product.Name);
                    rep.UpdateProducts(product);
                    return RedirectToAction("EditProductProperties", new { product.Id });
                }
            }

            //Lấy danh mục nhóm sản phẩm
            var list = rep.GetProductCatalogueList();
            ViewBag.CatalogueList = (from p in list
                                     where p.Actflg == 'A'
                                     select new SelectListItem
                                     {
                                         Value = p.Id.ToString(),
                                         Text = p.Name,
                                     }).ToList();
            return View(product);
        }

        /// <summary>
        /// Gen các control lên form để edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult EditProductProperties(int id)
        {
            //var model = rep.GetProductInfo(id);

            //Get các thuộc tính
            var PropertyList = rep.GetPropertyList();
            ViewBag.PropertyList = PropertyList;
            //Get các group thuộc tính
            var ProCate = rep.GetPropertyCatalogueList();
            ViewBag.PropertyCatalogueList = ProCate;
            //Get các nhóm thuộc tính
            var ProSubCate = rep.GetPropertySubCatalogueList();
            ViewBag.PropertySubCatalogueList = ProSubCate;
            //Get các thuộc tính của sản phẩm
            var ProductProperty = rep.GetPropertyProductList(id);
            ViewBag.ProductPropertyList = ProductProperty;
            ViewBag.ProductId = id;
            return View();
        }


        /// <summary>
        /// Sửa các thuộc tính cho sản phẩm
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public ActionResult EditProductProperties(Product product)
        {
            var json_serializer = new JavaScriptSerializer();
            var json = (IDictionary<String, Object>)json_serializer.DeserializeObject(product.jsondata);
            rep.updatePropertyProductList(product.Id, json);
            return RedirectToAction("EditProductImage", new { product.Id });
        }

        /// <summary>
        /// Load form
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult EditProductImage(int id)
        {
            var list = new ArrayList();
            var product = new Product();
            product.Id = id;

          var productFolder = Server.MapPath(@"~/Images/Product/" + id);
          var dir = new DirectoryInfo(productFolder);
            if (Directory.Exists(productFolder))
            {
                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo file in files)
                {
                    var extension = file.Extension.ToString().ToLower();
                    if (extension == ".jpg" || extension == ".jpeg" || extension == ".gif" || extension == ".png")
                    {
                        list.Add("/Images/Product/"+ id +"/" + file.Name);
                    }
                }
            }
            ViewBag.ImageList = list;
            return View(product);
        }

        /// <summary>
        /// Sửa ảnh của sản phẩm
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public ActionResult EditProductImage(Product product)
        {
           // var productFolder = Server.MapPath("Images/Product/" + product.Id);
            var productFolder = Server.MapPath(@"~/Images/Product/"+product.Id);

            // If directory does not exist, don't even try 
            if (!Directory.Exists(productFolder))
            {
                Directory.CreateDirectory(productFolder);
            }
            //Save ảnh vào thư mục
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i];
                string path = System.IO.Path.Combine(productFolder, System.IO.Path.GetFileName(file.FileName));
                if (!System.IO.File.Exists(path) && file.ContentLength > 0)
                {
                    file.SaveAs(path);
                }
            }
            return RedirectToAction("EditProductImage", new { product.Id });
        }

        /// <summary>
        /// Xóa image của sản phẩm
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult DeleteImage(string image, string productId)
        {
            var productFolder = Server.MapPath(@""+ image);
            if (System.IO.File.Exists(productFolder))
            {
                try
                {
                    System.IO.File.Delete(productFolder);
                }
                catch (System.IO.IOException e)
                {
                }
            }
            if (!String.IsNullOrEmpty(productId))
            {
                return RedirectToAction("EditProductImage", new { id=productId });
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Xóa sản phẩm
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public  ActionResult Delete(int id)
        {
            rep.DeleteProducts(id);
            return RedirectToAction("Index");
        }
    }
}
