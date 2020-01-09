using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Collage_Moodle.Models
{
    public class Exams
    {
        [Key, Column(Order = 0)]
        [Required]
        [StringLength(50, ErrorMessage = "Course name should be no more than 50 characters.")]
        public string Courses_cName { get; set; }
        [Key, Column(Order = 1)]
        [Required]
        public string moed { get; set; }
        [Required]
        public string date { get; set; }

        [Required]
        public string hour { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Classroom should be no more than 50 characters.")]
        public string classroom { get; set; }
    }
}