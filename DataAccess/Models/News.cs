using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Showroom.Models
{
    [MetadataType(typeof(News.NewsMetadata))]
    public partial class News
    {
        internal sealed class NewsMetadata
        {
            [Display(Name = "Mã")]
            public int Id { get; set; }

            [Required(ErrorMessage = "Bạn phải nhập tiêu đề")]
            [Display(Name = "Tiêu đề")]
            [StringLength(255)]
            public string Title { get; set; }

            public string TitleEn { get; set; }

            [Display(Name = "Ảnh Thumb")]
            [StringLength(255)]
            public string Image { get; set; }
            

            [Required(ErrorMessage = "Bạn phải nhập chi tiết tin.")]
            [Display(Name = "Chi tiết")]
            public string Detail { get; set; }

            [Display(Name = "Lượt xem")]
            public int? Views { get; set; }

            [Display(Name = "Ngày đăng")]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}", NullDisplayText = "")]
            public DateTime PostDate { get; set; }

            [Display(Name = "Hiển thị trên slide")]
            public char? ShowSlide { get; set; }

            [Display(Name = "Chuyên mục")]
            public int? CatelogueId { get; set; }

            [Display(Name = "Trạng thái")]
            public char? Actflg { get; set; }
        }
    }
}
