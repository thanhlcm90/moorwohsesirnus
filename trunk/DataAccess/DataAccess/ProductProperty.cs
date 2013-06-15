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
        /// Lấy danh sách property
        /// </summary>
        /// <param name="id">Mã sản phẩm</param>
        /// <returns></returns>
        public List<ProductProperty> GetPropertyProductList(int id)
        {
            try
            {
                var item = from p in _dataContext.ProductProperties
                           where p.ProductId == id
                           select p;
                // Trả về 1 giá trị hoặc mặc định (null)
                return item.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool updatePropertyProductList(int id, IDictionary<String, Object> propertyData)
        {
            try
            {
                var item = from p in _dataContext.ProductProperties
                           where p.ProductId == id
                           select p;
                // Xóa giá trị cũ
                _dataContext.ProductProperties.DeleteAllOnSubmit(item);
                List<ProductProperty> listInsert = new List<ProductProperty>();
                foreach (String sKey in propertyData.Keys)
                {
                    var itemInsert = new ProductProperty();
                    itemInsert.ProductId = id;
                    itemInsert.PropertyId = Convert.ToInt32(sKey.Substring(1));
                    itemInsert.Value = propertyData[sKey].ToString();
                    listInsert.Add(itemInsert);
                }
                _dataContext.ProductProperties.InsertAllOnSubmit(listInsert);
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
