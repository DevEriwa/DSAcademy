using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class TestResult: BaseModel
    {
        public int? CourseId { get; set; }
        [ForeignKey("CourseId")]
        public virtual TrainingCourse? Course { get; set; }
        public int? ResultOne { get; set; }
        public bool TestOneChecker { get; set; }
        public int? ResultTwo { get; set; }
        public bool TestTwoChecker { get; set; }
        public int? Total { get; set; }
        [NotMapped]
        public string? Title { get; set; }
    }
}
