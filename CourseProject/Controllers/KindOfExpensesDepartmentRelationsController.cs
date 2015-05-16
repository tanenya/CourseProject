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
    public class KindOfExpensesDepartmentRelationsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: KindOfExpensesDepartmentRelations
        public ActionResult Index(string sortOrder, int? page)
        {
            ViewBag.DepartmentSortParam = String.IsNullOrEmpty(sortOrder) ? "department_desc" : "";
            ViewBag.KindOfExpenseSortParam = sortOrder == "kind_of_exp" ? "kind_of_exp_desc" : "kind_of_exp";
            ViewBag.LimitSortParam = sortOrder == "limit" ? "limit_desc" : "limit";

            var kindOfExpensesDepartmentRelations = from k in db.KindOfExpensesDepartmentRelations select k;

            switch (sortOrder)
            {
                case "department_desc":
                    kindOfExpensesDepartmentRelations = kindOfExpensesDepartmentRelations.OrderByDescending(k => k.Department.Name);
                    break;

                case "kind_of_exp":
                    kindOfExpensesDepartmentRelations = kindOfExpensesDepartmentRelations.OrderBy(k => k.KindOfExpenses.Name);
                    break;

                case "kind_of_exp_desc":
                    kindOfExpensesDepartmentRelations = kindOfExpensesDepartmentRelations.OrderByDescending(k => k.KindOfExpenses.Name);
                    break;

                case "limit":
                    kindOfExpensesDepartmentRelations = kindOfExpensesDepartmentRelations.OrderBy(k => k.Limit);
                    break;

                case "limit_desc":
                    kindOfExpensesDepartmentRelations = kindOfExpensesDepartmentRelations.OrderByDescending(k => k.Limit);
                    break;

                default:
                    kindOfExpensesDepartmentRelations = kindOfExpensesDepartmentRelations.OrderBy(k => k.Department.Name);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(kindOfExpensesDepartmentRelations.ToPagedList(pageNumber, pageSize));
        }

        // GET: KindOfExpensesDepartmentRelations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KindOfExpensesDepartmentRelation kindOfExpensesDepartmentRelation = db.KindOfExpensesDepartmentRelations.Find(id);
            if (kindOfExpensesDepartmentRelation == null)
            {
                return HttpNotFound();
            }
            return View(kindOfExpensesDepartmentRelation);
        }

        // GET: KindOfExpensesDepartmentRelations/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentID = new SelectList(db.Departments, "ID", "Name");
            ViewBag.KindOfExpensesID = new SelectList(db.KindsOfExpenses, "ID", "Name");
            return View();
        }

        // POST: KindOfExpensesDepartmentRelations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KindOfExpensesID,DepartmentID,Limit")] KindOfExpensesDepartmentRelation kindOfExpensesDepartmentRelation)
        {
            try
            {
                if (isExist(kindOfExpensesDepartmentRelation, false))
                    ModelState.AddModelError("Limit", "Лимит для такого сочетания вида расходов и отдела уже существует.");
                
                if (ModelState.IsValid)
                {
                    db.KindOfExpensesDepartmentRelations.Add(kindOfExpensesDepartmentRelation);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            ViewBag.DepartmentID = new SelectList(db.Departments, "ID", "Name", kindOfExpensesDepartmentRelation.DepartmentID);
            ViewBag.KindOfExpensesID = new SelectList(db.KindsOfExpenses, "ID", "Name", kindOfExpensesDepartmentRelation.KindOfExpensesID);
            return View(kindOfExpensesDepartmentRelation);
        }

        // GET: KindOfExpensesDepartmentRelations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KindOfExpensesDepartmentRelation kindOfExpensesDepartmentRelation = db.KindOfExpensesDepartmentRelations.Find(id);
            if (kindOfExpensesDepartmentRelation == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "ID", "Name", kindOfExpensesDepartmentRelation.DepartmentID);
            ViewBag.KindOfExpensesID = new SelectList(db.KindsOfExpenses, "ID", "Name", kindOfExpensesDepartmentRelation.KindOfExpensesID);
            return View(kindOfExpensesDepartmentRelation);
        }

        // POST: KindOfExpensesDepartmentRelations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,KindOfExpensesID,DepartmentID,Limit")] KindOfExpensesDepartmentRelation kindOfExpensesDepartmentRelation)
        {
            try
            {
                if (isExist(kindOfExpensesDepartmentRelation, true))
                    ModelState.AddModelError("Limit", "Лимит для такого сочетания вида расходов и отдела уже существует.");
                
                if (ModelState.IsValid)
                {
                    db.Entry(kindOfExpensesDepartmentRelation).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "ID", "Name", kindOfExpensesDepartmentRelation.DepartmentID);
            ViewBag.KindOfExpensesID = new SelectList(db.KindsOfExpenses, "ID", "Name", kindOfExpensesDepartmentRelation.KindOfExpensesID);
            return View(kindOfExpensesDepartmentRelation);
        }

        // GET: KindOfExpensesDepartmentRelations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KindOfExpensesDepartmentRelation kindOfExpensesDepartmentRelation = db.KindOfExpensesDepartmentRelations.Find(id);
            if (kindOfExpensesDepartmentRelation == null)
            {
                return HttpNotFound();
            }
            return View(kindOfExpensesDepartmentRelation);
        }

        // POST: KindOfExpensesDepartmentRelations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KindOfExpensesDepartmentRelation kindOfExpensesDepartmentRelation = db.KindOfExpensesDepartmentRelations.Find(id);
            db.KindOfExpensesDepartmentRelations.Remove(kindOfExpensesDepartmentRelation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public Boolean isExist(KindOfExpensesDepartmentRelation kindOfExpRel, Boolean isEdit)
        {
            var kindOfExpensesID = kindOfExpRel.KindOfExpensesID;
            var departmentID = kindOfExpRel.DepartmentID;
            var existingKindOfExpRel = db.KindOfExpensesDepartmentRelations.Where(rel => rel.KindOfExpensesID.Equals(kindOfExpensesID) && rel.DepartmentID.Equals(departmentID));
            //var rel = from r in db.KindOfExpensesDepartmentRelations where r.DepartmentID == departmentID && r.KindOfExpensesID == kindOfExpensesID select r;
            //var isUnique = rel1.Count() == 0;

            if (isEdit)
                return existingKindOfExpRel.Count() > 0 && existingKindOfExpRel.Count(r => r.ID == kindOfExpRel.ID) == 0;

            return existingKindOfExpRel.Count() > 0;
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
