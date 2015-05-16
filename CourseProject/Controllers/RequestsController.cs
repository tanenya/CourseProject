using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourseProject.DAL;
using CourseProject.Models;

namespace CourseProject.Controllers
{
    public class RequestsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ExpensesByDepartment(int? id)
        {
            var departments = db.Departments.ToList();
            ViewBag.Departments = new SelectList(departments, "ID", "Name");

            if (id != null)
            {
                var expenses = db.Expenses.Where(e => e.Employee.DepartmentID == id).ToList();
                return View(expenses);
            }
            
            return View();
        }

        public ActionResult EmployeeByExpense(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                var expenses = db.Expenses.Where(e => e.ID.ToString().Equals(id)).ToList();
                var employees = new List<Employee>();
                expenses.ForEach(e => employees.Add(e.Employee));

                return View(employees);
            }
            
            return View();
        }

        public ActionResult ExpensesInThisMonth()
        {
            var expenses = db.Expenses.Where(e => e.Date.Month.Equals(DateTime.Today.Month)).OrderBy(e => e.Date).ToList();

            return View(expenses);
        }
    }
}