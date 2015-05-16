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
    public class KindOfExpensesController : Controller
    {
        private DataContext db = new DataContext();

        // GET: KindOfExpenses
        public ActionResult Index(string sortOrder, int? page)
        {
            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            var kindOfExpenses = from k in db.KindsOfExpenses select k;

            switch (sortOrder)
            {
                case "name_desc":
                    kindOfExpenses = kindOfExpenses.OrderByDescending(k => k.Name);
                    break;

                default:
                    kindOfExpenses = kindOfExpenses.OrderBy(k => k.Name);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(kindOfExpenses.ToPagedList(pageNumber, pageSize));
        }

        // GET: KindOfExpenses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KindOfExpenses kindOfExpenses = db.KindsOfExpenses.Find(id);
            if (kindOfExpenses == null)
            {
                return HttpNotFound();
            }
            return View(kindOfExpenses);
        }

        // GET: KindOfExpenses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KindOfExpenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Description")] KindOfExpenses kindOfExpenses)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.KindsOfExpenses.Add(kindOfExpenses);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(kindOfExpenses);
        }

        // GET: KindOfExpenses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KindOfExpenses kindOfExpenses = db.KindsOfExpenses.Find(id);
            if (kindOfExpenses == null)
            {
                return HttpNotFound();
            }
            return View(kindOfExpenses);
        }

        // POST: KindOfExpenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description")] KindOfExpenses kindOfExpenses)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(kindOfExpenses).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(kindOfExpenses);
        }

        // GET: KindOfExpenses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KindOfExpenses kindOfExpenses = db.KindsOfExpenses.Find(id);
            if (kindOfExpenses == null)
            {
                return HttpNotFound();
            }
            return View(kindOfExpenses);
        }

        // POST: KindOfExpenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KindOfExpenses kindOfExpenses = db.KindsOfExpenses.Find(id);
            db.KindsOfExpenses.Remove(kindOfExpenses);
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
