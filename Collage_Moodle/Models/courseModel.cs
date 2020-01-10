using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Collage_Moodle.Models
{
    public class CourseModel
    {
        [Key]
        public string courseName { get; set; }

        public string day { get; set; }

        public string hour { get; set; }

        public string classroom { get; set; }

        public int lecturerID { get; set; }
        
    }
}