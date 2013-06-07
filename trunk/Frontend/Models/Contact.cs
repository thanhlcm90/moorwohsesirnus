using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace Frontend.Models
{
    public class Contact
    {
        //Email
        [Required(ErrorMessage = "Email chưa được nhập")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        //Mobile
        [Required(ErrorMessage = "Mobile chưa được nhập")]
        [Display(Name = "Mobile")]
        public string Mobile { get; set; }

        //Detail
        [Required(ErrorMessage = "Detail chưa được nhập")]
        [Display(Name = "Detail")]
        public string Detail { get; set; }
    }
}
