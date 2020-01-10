using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Collage_Moodle.Models
{
    public class ViewExamSchedule
    {
        [Key]
        public int Id { get; set; }

        public Users user { get; set; }

        public List<ExamModel> exams { get; set; }

    }
}