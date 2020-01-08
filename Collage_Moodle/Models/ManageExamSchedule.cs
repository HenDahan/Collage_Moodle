using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Collage_Moodle.Models
{
    public class ManageExamSchedule
    {

        [Key, Column(Order = 0)]
        [Required(ErrorMessage = "Course name cannot be empty.")]
        [StringLength(50, ErrorMessage = "Course name should be no more than 50 characters.")]
        public string course_name { get; set; }

        [Key, Column(Order = 1)]
        [Required(ErrorMessage = "The moed cannot be empty.")]
        [StringLength(1, ErrorMessage = "the moed is either 'A' or 'B'.")]
        public string moed { get; set; }

        [Required(ErrorMessage = "The date cannot be empty.")]
        [StringLength(50, ErrorMessage = "the date should be no more than 50 characters.")]
        public string new_date { get; set; }

        [Required(ErrorMessage = "The classroom cannot be empty.")]
        [StringLength(50, ErrorMessage = "the classroom should be no more than 50 characters.")]
        public string new_classroom { get; set; }
    }
}
