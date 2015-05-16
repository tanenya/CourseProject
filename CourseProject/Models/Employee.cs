using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CourseProject.Models
{
    public class Employee
    {
        public int ID { get; set; }
        [Display(Name = "Имя")]
        [StringLength(50)]
        [Required]
        public string FirstName { get; set; }
        [Display(Name = "Фамилия")]
        [StringLength(50)]
        [Required]
        public string LastName { get; set; }
        [Display(Name = "Отчество")]
        [StringLength(50)]
        public string MiddleName { get; set; }
        public int DepartmentID { get; set; }

        public virtual Department Department { get; set; }
    }
}