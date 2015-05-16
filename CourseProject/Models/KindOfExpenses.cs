using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CourseProject.Models
{
    public class KindOfExpenses
    {
        public int ID { get; set; }
        [Display(Name = "Вид расходов")]
        [StringLength(50)]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Описание")]
        [StringLength(150)]
        public string Description { get; set; }
    }
}