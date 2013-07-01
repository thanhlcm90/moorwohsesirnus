using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Showroom.Models
{
    [MetadataType(typeof(Property.PropertyMetadata))]
    public partial class Property
    {
        internal sealed class PropertyMetadata
        {
            [Display(Name = "Mã")]
            public int Id { get; set; }

            [Required(ErrorMessage = "Bạn phải nhập tên thuộc tính.")]
            [Display(Name = "Tên")]
            [StringLength(255)]
            public string Name { get; set; }

            [Display(Name = "Tên không dấu")]
            [StringLength(255)]
            public string NameEn { get; set; }

            [Display(Name = "Bộ thông số")]
            [Required(ErrorMessage = "Bạn phải nhập tên bộ tông số.")]
            public int? CatelogueId { get; set; }

            [Display(Name = "Nhóm thông số")]
            [Required(ErrorMessage = "Bạn phải nhập tên nhóm tông số.")]
            public int? SubCatelogueId { get; set; }

            [Display(Name = "Hiển thị trên tóm tắt sản phẩm")]
            public char? ShowMain { get; set; }

            [Display(Name = "Loại thuộc tính")]
            public int? PropertyType { get; set; }

            [Display(Name = "Trạng thái")]
            public char Actflg { get; set; }

        }
    }
}
