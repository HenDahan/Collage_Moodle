using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Collage_Moodle.Models
{
    public class ManageCourseSchedule
    {

        [Key]
        [Required(ErrorMessage = "Course name cannot be empty.")]
        [StringLength(50, ErrorMessage = "Course name should be no more than 50 characters.")]
        public string course_name { get; set; }

        [Required(ErrorMessage = "The day cannot be empty.")]
        [RegularExpression("(Sunday|Monday|Tuesday|Wednesday|Thursday|Friday|Saturday)$", ErrorMessage = "The day must be Sunday Monday, Tuesday, Wednesday, Thursday, Friday or Saturday.")]
        public string day { get; set; }

        [Required(ErrorMessage = "The hour cannot be empty.")]
        [RegularExpression("([0-1][0-9]|2[0-3]):[0-5][0-9]-([0-1][0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "The hour should be VALID for exmple: '09:00-14:00'.")]
        public string hour { get; set; }

        [Required(ErrorMessage = "The classroom cannot be empty.")]
        [StringLength(50, ErrorMessage = "the classroom should be no more than 50 characters.")]
        public string classroom { get; set; }

        [Required(ErrorMessage = "Lecturer ID cannot be empty.")]
        [Range(100000, 999999999, ErrorMessage = "Lecturer ID must be a number between 6 to 9 digits.")]
        public int Lecturer_id { get; set; }
    }
}