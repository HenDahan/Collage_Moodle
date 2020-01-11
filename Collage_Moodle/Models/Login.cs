using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Collage_Moodle.Models
{
    public class Login
    {
        [Key]
        [Required(ErrorMessage = "User name cannot be empty.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "User name must be at least 2 characters.")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Password cannot be empty.")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; }

    }
}