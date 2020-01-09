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
        [RegularExpression("[A-B]$", ErrorMessage = "The moed should be only 'A' or 'B'.")]
        public string moed { get; set; }

        [Required(ErrorMessage = "The date cannot be empty.")]
        [RegularExpression("([0-2][0-9]|3[01])/[01][0-9]/20[0-9][0-9]$", ErrorMessage = "The date should be VALID for example: '12/03/2020'.")]
        public string date { get; set; }

        [Required(ErrorMessage = "The hour cannot be empty.")]
        [RegularExpression("([0-1][0-9]|2[0-3]):[0-5][0-9]-([0-1][0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "The hour should be VALID for exmple: '09:00-14:00'.")]
        public string hour { get; set; }

        [Required(ErrorMessage = "The classroom cannot be empty.")]
        [StringLength(50, ErrorMessage = "the classroom should be no more than 50 characters.")]
        public string classroom { get; set; }
    }
}
