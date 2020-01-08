using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Collage_Moodle.Models
{
    public class AssignStudent
    {
        [Key, Column(Order = 0)]
        public string courseName { get; set; }

        [Key, Column(Order = 1)]
        public int studentID { get; set; }


        

    }
}