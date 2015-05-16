using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CourseProject.Models
{
    public class KindOfExpensesDepartmentRelation
    {
        public int ID { get; set; }
        public int KindOfExpensesID { get; set; }
        public int DepartmentID { get; set; }
        [Display(Name = "Лимит")]
        public decimal Limit { get; set; }

        public virtual KindOfExpenses KindOfExpenses { get; set; }
        public virtual Department Department { get; set; }
    }
}