using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CourseProject.DAL;
using CourseProject.Models;
using PagedList;

namespace CourseProject.Controllers
{
    public class ExpensesController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Expenses
        public ActionResult Index(string sortOrder, int? page)
        {
            ViewBag.IDSortParam = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewBag.EmployeeSortParam = sortOrder == "employee" ? "employee_desc" : "employee";
            ViewBag.KindOfExpenseSortParam = sortOrder == "kind_of_exp" ? "kind_of_exp_desc" : "kind_of_exp";
            ViewBag.DateSortParam = sortOrder == "date" ? "date_desc" : "date";
            ViewBag.SummSortParam = sortOrder == "summ" ? "summ_desc" : "summ";

            var expenses = from e in db.Expenses select e;

            switch (sortOrder)
            {
                case "id_desc":
                    expenses = expenses.OrderByDescending(e => e.ID);
                    break;

                case "employee":
                    expenses = expenses.OrderBy(e => e.Employee.LastName);
                    break;

                case "employee_desc":
                    expenses = expenses.OrderByDescending(e => e.Employee.LastName);
                    break;

                case "kind_of_exp":
                    expenses = expenses.OrderBy(e => e.KindOfExpenses.Name);
                    break;

                case "kind_of_exp_desc":
                    expenses = expenses.OrderByDescending(e => e.KindOfExpenses.Name);
                    break;

                case "date":
                    expenses = expenses.OrderBy(e => e.Date);
                    break;

                case "date_desc":
                    expenses = expenses.OrderByDescending(e => e.Date);
                    break;

                case "summ":
                    expenses = expenses.OrderBy(e => e.Summ);
                    break;

                case "summ_desc":
                    expenses = expenses.OrderByDescending(e => e.Summ);
                    break;

                default:
                    expenses = expenses.OrderBy(e => e.ID);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(expenses.ToPagedList(pageNumber, pageSize));
        }

        // GET: Expenses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = db.Expenses.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            return View(expense);
        }

        // GET: Expenses/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeID = new SelectList(db.Employees, "ID", "LastName");
            ViewBag.KindOfExpensesID = new SelectList(db.KindsOfExpenses, "ID", "Name");
            return View();
        }

        // POST: Expenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeID,KindOfExpensesID,Date,Summ")] Expense expense)
        {
            try
            {
                if (!isMoneyEnough(expense))
                    ModelState.AddModelError("Summ", "Превышен лимит средств, которые могут быть потрачены " +
                    "по данному виду расходов для данного отдела в месяц.");

                if (ModelState.IsValid)
                {
                    db.Expenses.Add(expense);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            ViewBag.EmployeeID = new SelectList(db.Employees, "ID", "FirstName", expense.EmployeeID);
            ViewBag.KindOfExpensesID = new SelectList(db.KindsOfExpenses, "ID", "Name", expense.KindOfExpensesID);
            return View(expense);
        }

        // GET: Expenses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = db.Expenses.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "ID", "FirstName", expense.EmployeeID);
            ViewBag.KindOfExpensesID = new SelectList(db.KindsOfExpenses, "ID", "Name", expense.KindOfExpensesID);
            return View(expense);
        }

        // POST: Expenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,EmployeeID,KindOfExpensesID,Date,Summ")] Expense expense)
        {
            try
            {
                if (!isMoneyEnough(expense))
                    ModelState.AddModelError("Summ", "Превышен лимит средств, которые могут быть потрачены " +
                    "по данному виду расходов для данного отдела в месяц.");
                
                if (ModelState.IsValid)
                {
                    db.Entry(expense).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            ViewBag.EmployeeID = new SelectList(db.Employees, "ID", "FirstName", expense.EmployeeID);
            ViewBag.KindOfExpensesID = new SelectList(db.KindsOfExpenses, "ID", "Name", expense.KindOfExpensesID);
            return View(expense);
        }

        // GET: Expenses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = db.Expenses.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            return View(expense);
        }

        // POST: Expenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Expense expense = db.Expenses.Find(id);
            db.Expenses.Remove(expense);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public Boolean isMoneyEnough(Expense expense)
        {
            var departmentID = db.Employees.Where(e => e.ID == expense.EmployeeID).Single().DepartmentID;
            var kindOfExpenseID = expense.KindOfExpensesID;
            var limit = 0m;
            
            try
            {
                limit = db.KindOfExpensesDepartmentRelations.Where(kind =>
                kind.DepartmentID.Equals(departmentID) && kind.KindOfExpensesID.Equals(kindOfExpenseID)).Single().Limit;
            }
            catch (Exception)
            {
                ModelState.AddModelError("Summ", "Для данного вида расходов по отделу сотрудника лимит не установлен." + 
                    "Перед внесением расхода необходимо задать лимит.");
            }

            var expenses = db.Expenses.Where(e => e.KindOfExpensesID.Equals(kindOfExpenseID) 
                && e.Employee.DepartmentID.Equals(departmentID) && e.Date.Month.Equals(DateTime.Today.Month)).ToList();
            var spent = expenses.Sum(e => e.Summ);

            return limit > spent + expense.Summ;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
