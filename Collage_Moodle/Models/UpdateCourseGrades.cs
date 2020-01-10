using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Collage_Moodle.Models
{
    public class UpdateCourseGrades
    {
        [Key, Column(Order = 0)]
        [Required(ErrorMessage = "Course name cannot be empty.")]
        [StringLength(50, ErrorMessage = "Course name should be no more than 50 characters.")]
        public string course_name { get; set; }

        [Key, Column(Order = 1)]
        [Required(ErrorMessage = "Student ID cannot be empty.")]
        [Range(100000, 999999999, ErrorMessage = "Student ID must be a number between 6 to 9 digits.")]
        public int student_ID { get; set; }

        [Required(ErrorMessage = "Grade cannot be empty.")]
        [Range(0, 100, ErrorMessage = "The grade must be between 0 - 100.")]
        public int grade { get; set; }
    }
}