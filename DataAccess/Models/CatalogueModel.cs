﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Showroom.Models
{
    [MetadataType(typeof(Catalogue.CatalogueMetadata))]
    public partial class Catalogue
    {
        internal sealed class CatalogueMetadata
        {
            [Display(Name = "Mã")]
            public int Id { get; set; }

            [Required(ErrorMessage = "Bạn phải nhập tên danh mục.")]
            [Display(Name = "Tên")]
            [StringLength(100)]
            public string Name { get; set; }

            [Display(Name = "Tên không dấu")]
            [StringLength(255)]
            public string NameEn { get; set; }

            [Display(Name = "Logo")]
            [Required(ErrorMessage = "Bạn phải chọn ảnh Logo.")]
            public string Image { get; set; }

            [Display(Name = "Độ ưu tiên")]
            [RegularExpression(@"^\d+$", ErrorMessage = "Bạn phải nhập vào 1 số nguyên.")]
            public int? Priority { get; set; }

            [Display(Name = "Trạng thái")]
            public char Actflg { get; set; }
        }
    }
}
