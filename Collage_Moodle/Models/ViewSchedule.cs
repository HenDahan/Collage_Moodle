using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Collage_Moodle.Models
{
    public class ViewSchedule
    {
        [Key]
        public int Id { get; set; }

        public Users user { get; set; }

        public List<CourseModel> courses { get; set; }
    }
}