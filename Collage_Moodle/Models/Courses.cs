using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Collage_Moodle.Models
{
    public class Courses
    {
        [Key]
        [Required]
        [StringLength(50, ErrorMessage = "Course name should be no more than 50 characters.")]
        public string courseName { get; set; }
        [Required]
         public string day { get; set; }
        [Required]
        public string hour { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Classroom should be no more than 50 characters.")]
        public string classroom { get; set; }
        [Required]
        public int Users_lecturerID { get; set; }

    }
}