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
    public class EmployeesController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Employees
        public ActionResult Index(string sortOrder, int? page)
        {
            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DepartmentSortParam = sortOrder == "department" ? "department_desc" : "department";
            ViewBag.LastNameSortParam = sortOrder == "last_name" ? "last_name_desc" : "last_name";
            ViewBag.MiddleNameSortParam = sortOrder == "middle_name" ? "middle_name_desc" : "middle_name";

            var employees = from e in db.Employees select e;

            switch (sortOrder)
            {
                case "name_desc":
                    employees = employees.OrderByDescending(e => e.FirstName);
                    break;

                case "department":
                    employees = employees.OrderBy(e => e.Department.Name);
                    break;

                case "department_desc":
                    employees = employees.OrderByDescending(e => e.Department.Name);
                    break;

                case "last_name":
                    employees = employees.OrderBy(e => e.LastName);
                    break;

                case "last_name_desc":
                    employees = employees.OrderByDescending(e => e.LastName);
                    break;

                case "middle_name":
                    employees = employees.OrderBy(e => e.MiddleName);
                    break;

                case "middle_name_desc":
                    employees = employees.OrderByDescending(e => e.MiddleName);
                    break;

                default:
                    employees = employees.OrderBy(e => e.FirstName);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(employees.ToPagedList(pageNumber, pageSize));
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentID = new SelectList(db.Departments, "ID", "Name");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,LastName,MiddleName,DepartmentID")] Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Employees.Add(employee);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            ViewBag.DepartmentID = new SelectList(db.Departments, "ID", "Name", employee.DepartmentID);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "ID", "Name", employee.DepartmentID);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,MiddleName,DepartmentID")] Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(employee).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "ID", "Name", employee.DepartmentID);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
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
