using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Showroom.Models
{
    [MetadataType(typeof(Product.CatalogueMetadata))]
    public partial class Product
    {
        public String jsondata { get; set; }

        internal sealed class CatalogueMetadata
        {
            [Display(Name = "Mã")]
            public int Id { get; set; }

            [Required(ErrorMessage = "Bạn phải nhập mã sản phẩm.")]
            [Display(Name = "Mã sản phẩm")]
            [StringLength(55)]
            public string Code { get; set; }

            [Required(ErrorMessage = "Bạn phải nhập tên sản phẩm.")]
            [Display(Name = "Tên sản phẩm")]
            [StringLength(255)]
            public string Name { get; set; }

            [Required(ErrorMessage = "Bạn phải nhập giá sản phẩm.")]
            [Display(Name = "Giá")]
            public string Price { get; set; }

            [Required(ErrorMessage = "Bạn phải nhập số lượng sản phẩm.")]
            [Display(Name = "Số lượng")]
            public string Amount { get; set; }

            [Required(ErrorMessage = "Bạn phải chọn nhóm sản phẩm.")]
            [Display(Name = "Nhóm sản phẩm")]
            public string CatalogueId { get; set; }

            [Required(ErrorMessage = "Bạn phải nhập mô tả chi tiết.")]
            [Display(Name = "Mô tả chi tiết")]
            public string Description { get; set; }

            [Display(Name = "Trạng thái")]
            public DateTime Actflg { get; set; }
        }
    }
}
