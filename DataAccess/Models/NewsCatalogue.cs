using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Showroom.Models
{
    [MetadataType(typeof(NewsCatalogue.NewsCatalogueMetadata))]
    public partial class NewsCatalogue
    {
        internal sealed class NewsCatalogueMetadata
        {
            [Display(Name = "Mã")]
            public int Id { get; set; }

            [Required(ErrorMessage = "Bạn phải nhập tên chuyên mục.")]
            [Display(Name = "Tên")]
            [StringLength(100)]
            public string Name { get; set; }

            [Display(Name = "Tên không dấu")]
            [StringLength(255)]
            public string NameEn { get; set; }

            [Display(Name = "Logo")]
            public int Image { get; set; }

            [Display(Name = "Trạng thái")]
            public char Atcflg { get; set; }
        }
    }
}
