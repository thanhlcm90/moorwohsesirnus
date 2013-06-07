using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Showroom.Models.DataBusiness;

namespace Showroom.Models.DataAccess
{
    public partial class ShowroomRepository : IShowroomRepository
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


    }
}
