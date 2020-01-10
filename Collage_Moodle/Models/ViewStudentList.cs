using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Collage_Moodle.Models
{
    public class ViewStudentList
    {
        [Key]
        public int Id { get; set; }

        public string course_name { get; set; }

        public Users user { get; set; }

        public List<StudentModel> students { get; set; }
    }
}