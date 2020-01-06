using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Collage_Moodle.Models
{
    public class LogIn
    {
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Error 1  Name in to short.")]
        public string UserName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Error 2 Password is mast to be at list 6 digits.")]
        public string Password { get; set; }

    }
}