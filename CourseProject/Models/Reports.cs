using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CourseProject.Models;
using System.ComponentModel.DataAnnotations;

namespace CourseProject.Models
{
    public class Reports
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Начальная дата")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Конечная дата")]
        public DateTime EndDate { get; set; }

        public virtual IEnumerable<IGrouping<Employee, Expense>> ExpensesByEmployee { get; set; }
        public virtual IEnumerable<IGrouping<KindOfExpenses, Expense>> ExpensesByKindOfExpenses { get; set; }
    }
}