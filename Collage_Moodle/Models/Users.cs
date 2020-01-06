using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Collage_Moodle.Models
{
    public class Users
    {
        [Key]
        public string userName { get; set; }
        public string password { get; set; }
        public int permission { get; set; }
    }
}