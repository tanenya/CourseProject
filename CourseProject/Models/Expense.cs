using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace CourseProject.Models
{
    public class Expense
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public int KindOfExpensesID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата")]
        public DateTime Date { get; set; }
        [Display(Name = "Сумма")]
        public decimal Summ { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual KindOfExpenses KindOfExpenses { get; set; }
    }
}