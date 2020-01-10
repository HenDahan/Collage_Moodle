using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Collage_Moodle.Models
{
    public class StudentModel
    {
        [Key, Column(Order = 0)]
        public string Courses_cName { get; set; }

        [Key, Column(Order = 1)]

        public int Users_userID { get; set; }

        public int? grade { get; set; }

    }
}