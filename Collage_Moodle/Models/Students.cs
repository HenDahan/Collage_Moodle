using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Collage_Moodle.Models
{
    public class Students
    {
        [Key, Column(Order = 0)]
        [Required]
        [StringLength(50, ErrorMessage = "Course name should be no more than 50 characters.")]
        public string Courses_cName { get; set; }

        [Key, Column(Order = 1)]
        [Required]
        [Range(000001, 999999999, ErrorMessage ="Please enter an ID between 6 to 9 digits.")]
        public int Users_userID{ get; set; }
        
        public int? grade { get; set; }


    }
}