using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Showroom.Models.DataAccess
{
    public partial class ShowroomRepository
    {
        /// <summary>
        /// Lấy toàn bộ danh sách thuộc tính sản phẩm
        /// </summary>
        /// <returns>List of Catelogue</returns>
        public List<Product> GetProductsList()
        {
            try
            {
                var list = from p in _dataContext.Products select p;
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Lấy thông tin của 1  sản phẩm
        /// </summary>
        /// <param name="id">Mãsản phẩm</param>
        /// <returns></returns>
        public Product GetProductInfo(int id)
        {
            try
            {
                var item = from p in _dataContext.Products
                           where p.Id == id
                           select p;
                // Trả về 1 giá trị hoặc mặc định (null)
                return item.SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Lấy List Product by Catalogue ID
        /// </summary>
        /// <param name="IdCatalogue"></param>
        /// <returns></returns>
        public List<Product> GetListProductSame(int IdCatalogue)
        {
            try
            {
                var list = from p in _dataContext.Products
                           where p.CatalogueId == IdCatalogue
                           select p;
                return list.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool UpdateProducts(Product item)
        {
            try
            {
                // Tìm kiếm đối tượng có Id cần sửa đổi
                Product itemUpdate = (from p in _dataContext.Products where p.Id == item.Id select p).SingleOrDefault();

                // Nếu không tìm thấy thì trả về False
                if (itemUpdate == null) return false;

                // Copy toàn bộ giá trị từ item sang itemUpdate. Submit thay đổi
                item.CopyProperties(itemUpdate);
                // itemUpdate.CatalogueId = item.CatalogueId; //nó ko copy được thằng relationship
                _dataContext.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertProduct(Product product)
        {
            try
            {
                _dataContext.Products.InsertOnSubmit(product);
                _dataContext.SubmitChanges();
                return product.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteProducts(int id)
        {
            try
            {
                // Tìm kiếm đối tượng có Id cần sửa đổi
                Product itemDelete = (from p in _dataContext.Products where p.Id == id select p).SingleOrDefault();

                // Nếu không tìm thấy thì trả về False
                if (itemDelete == null) return false;
                //Xóa các thuộc tính của sản phẩm.
                var properties = (from p in _dataContext.ProductProperties where p.ProductId == itemDelete.Id select p).ToList();
                foreach (var prop in properties)
                {
                    _dataContext.ProductProperties.DeleteOnSubmit(prop);
                }
                // Xóa và Submit thay đổi
                _dataContext.Products.DeleteOnSubmit(itemDelete);
                _dataContext.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Lấy Model của Product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProductProperty GetModelOfProduct(int id)
        {
            var pModel = (from p in _dataContext.ProductProperties where p.ProductId == id && p.PropertyId == 2 select p).FirstOrDefault();
            return pModel;
        }

        public List<Product> SearchProduct(int hangXe, string model, string tinhTrang)
        {
            if (string.IsNullOrEmpty(model) || model == "tat-ca") model = "0"; //Lấy tất cả
            if (string.IsNullOrEmpty(tinhTrang) || tinhTrang == "tat-ca") tinhTrang = "0";//Lấy tất cả
            if (tinhTrang == "xe-cu") tinhTrang = "Xe cũ";
            if (tinhTrang == "xe-moi") tinhTrang = "Xe mới";
            return (from p in _dataContext.Products
                    from q in _dataContext.ProductProperties
                    from t in _dataContext.ProductProperties
                    where p.Id == q.ProductId && p.Id == t.ProductId && (p.CatalogueId == hangXe || hangXe == 0) && (q.Value.ToLower().Contains(model.ToLower()) || model == "0") && (t.Value.ToLower().Contains(tinhTrang.ToLower()) || tinhTrang == "0")
                    select p).Distinct().ToList();
        }
    }
}
