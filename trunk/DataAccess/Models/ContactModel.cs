using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Showroom.Models
{
    [MetadataType(typeof(Contact.CatalogueMetadata))]
    public partial class Contact
    {
        internal sealed class CatalogueMetadata
        {
            [Display(Name = "Mã")]
            public int Id { get; set; }

            [Required(ErrorMessage = "Bạn phải nhập email.")]
            [Display(Name = "Email")]
            [StringLength(200)]
            public string Email { get; set; }

            [Display(Name = "Điện thoại")]
            [StringLength(55)]
            public string Mobile { get; set; }

            [Display(Name = "Chi tiết")]
            public string Detail { get; set; }

            [Display(Name = "Ngày gửi")]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public DateTime PostDate { get; set; }

            [Display(Name = "Trạng thái")]
            public DateTime Actflg { get; set; }
        }
    }
}
