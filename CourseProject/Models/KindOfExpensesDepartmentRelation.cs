using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseProject.Models
{
    public class KindOfExpensesDepartmentRelation
    {
        public int ID { get; set; }
        [Index("IX_KindAndDepartment", 1, IsUnique = true)]
        public int KindOfExpensesID { get; set; }
        [Index("IX_KindAndDepartment", 2, IsUnique = true)]
        public int DepartmentID { get; set; }
        [Display(Name = "Лимит")]
        public decimal Limit { get; set; }

        public virtual KindOfExpenses KindOfExpenses { get; set; }
        public virtual Department Department { get; set; }
    }
}