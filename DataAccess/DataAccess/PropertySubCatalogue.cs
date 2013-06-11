using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Showroom.Models.DataAccess
{
    public partial class ShowroomRepository
    {
        /// <summary>
        /// Lấy toàn bộ danh sách nhóm thuộc tính con sản phẩm
        /// </summary>
        /// <returns>List of Catelogue</returns>
        public List<PropertySubCatalogue> GetPropertySubCatalogueList()
        {
            try
            {
                var list = from p in _dataContext.PropertySubCatalogues select p;
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Lấy thông tin của 1 nhóm thuộc tính con sản phẩm
        /// </summary>
        /// <param name="id">Mã nhóm thuộc tính con sản phẩm</param>
        /// <returns></returns>
        public PropertySubCatalogue GetPropertySubCatalogueInfo(int id)
        {
            try
            {
                var item = from p in _dataContext.PropertySubCatalogues
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
        /// Thêm mới 1 nhóm thuộc tính con sản phẩm
        /// </summary>
        /// <param name="item">Đối tượng cần thêm</param>
        /// <returns>True: Thành công; False: Thất bại</returns>
        public bool InsertPropertySubCatalogue(PropertySubCatalogue item)
        {
            try
            {
                _dataContext.PropertySubCatalogues.InsertOnSubmit(item);
                _dataContext.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Thay đổi thông tin của đối tượng theo Id chứa trong Object
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool UpdatePropertySubCatalogue(PropertySubCatalogue item)
        {
            try
            {
                // Tìm kiếm đối tượng có Id cần sửa đổi
                PropertySubCatalogue itemUpdate = (from p in _dataContext.PropertySubCatalogues where p.Id == item.Id select p).SingleOrDefault();

                // Nếu không tìm thấy thì trả về False
                if (itemUpdate == null) return false;

                // Copy toàn bộ giá trị từ item sang itemUpdate. Submit thay đổi
                item.CopyProperties(itemUpdate);
                _dataContext.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeletePropertySubCatalogue(int Id)
        {
            try
            {
                // Tìm kiếm đối tượng có Id cần sửa đổi
                PropertySubCatalogue itemDelete = (from p in _dataContext.PropertySubCatalogues where p.Id == Id select p).SingleOrDefault();

                // Nếu không tìm thấy thì trả về False
                if (itemDelete == null) return false;

                // Xóa và Submit thay đổi
                _dataContext.PropertySubCatalogues.DeleteOnSubmit(itemDelete);
                _dataContext.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
