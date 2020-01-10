using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Collage_Moodle.Models
{
    public class ExamModel
    {
        [Key, Column(Order = 0)]
        public string Courses_cName { get; set; }

        [Key, Column(Order = 1)]
        public string moed { get; set; }

        public string date { get; set; }

        public string hour { get; set; }

        public string classroom { get; set; }
    }
}