using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Showroom.Models
{
    [MetadataType(typeof(PropertySubCatalogue.PropertySubCatalogueMetadata))]
    public partial class PropertySubCatalogue
    {
        internal sealed class PropertySubCatalogueMetadata
        {
            [Display(Name = "Mã")]
            public int Id { get; set; }

            [Required(ErrorMessage = "Bạn phải nhập tên nhóm thông số.")]
            [Display(Name = "Tên")]
            [StringLength(255)]
            public string Name { get; set; }

            [Display(Name = "Tên không dấu")]
            [StringLength(255)]
            public string NameEn { get; set; }

            [Display(Name = "Trạng thái")]
            public char Actflg { get; set; }

        }
    }
}
