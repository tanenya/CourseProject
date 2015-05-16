using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CourseProject.Models
{
    public class Department
    {
        public int ID { get; set; }
        [Display(Name = "Название отдела")]
        [StringLength(50)]
        [Required]
        public string Name { get; set; }
    }
}