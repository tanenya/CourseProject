using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourseProject.DAL;
using CourseProject.Models;

namespace CourseProject.Controllers
{
    public class ReportsController : Controller
    {
        private DataContext db = new DataContext();
        
        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GroupByEmployees(Reports report)
        {
            if (report.StartDate != null && report.EndDate != null)
            {
                if (report.StartDate.CompareTo(report.EndDate) > 0)
                    ModelState.AddModelError("EndDate", "Дата начала должна быть раньше конечной даты.");

                var expenses = db.Expenses.Where(e => e.Date >= report.StartDate && e.Date <= report.EndDate).ToList();

                report.ExpensesByEmployee = expenses.GroupBy(e => e.Employee);

                return View(report);
            }
            
            return View();
        }

        public ActionResult GroupByKindOfExpenses(Reports report)
        {
            if (report.StartDate != null && report.EndDate != null)
            {
                if (report.StartDate.CompareTo(report.EndDate) > 0)
                    ModelState.AddModelError("EndDate", "Дата начала должна быть раньше конечной даты.");

                var expenses = db.Expenses.Where(e => e.Date >= report.StartDate && e.Date <= report.EndDate).ToList();

                report.ExpensesByKindOfExpenses = expenses.GroupBy(e => e.KindOfExpenses);

                return View(report);
            }

            return View();
        }
    }
}