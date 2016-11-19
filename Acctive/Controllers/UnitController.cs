using Acctive.Models;
using Acctive.Models.Context;
using Acctive.Models.Inventory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Acctive.Controllers
{
    public class UnitController : Controller
    {
        private AcctiveDbContext db = new AcctiveDbContext();

        // GET: Unit
        public async Task<ActionResult> Index()
        {
            //var unit = db.Unit.Include(u => u.Company).Include(u => u.Parent);

            //var item = Session["ActiveItem"] as ActiveItem;
            //int companyId = item != null ? item.CompanyId : 0;
            //var units = db.Unit.Where(u => u.CompanyId == companyId);
            return View(await db.Unit.ToListAsync());
        }

        // GET: Unit/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unit unit = await db.Unit.FindAsync(id);
            if (unit == null)
            {
                return HttpNotFound();
            }
            return View(unit);
        }

        // GET: Unit/Create
        public ActionResult Create()
        {
            //ViewBag.ParentId = new SelectList(db.Unit, "Id", "Code");
            return View(new Unit());
        }

        // POST: Unit/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Code,Name,Description,ImageFilePath,Active")] Unit unit)
        {
            if (ModelState.IsValid)
            {
                //int companyId = ((ActiveItem)Session["ActiveItem"]).CompanyId;
                //unit.CompanyId = companyId;

                db.Unit.Add(unit);
                await db.SaveChangesAsync();
                return RedirectToAction("Create");
            }

            //ViewBag.CompanyId = new SelectList(db.Company, "Id", "Code", unit.CompanyId);
            //ViewBag.ParentId = new SelectList(db.Unit, "Id", "Code", unit.ParentId);
            return View(unit);
        }

        // GET: Unit/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unit unit = await db.Unit.FindAsync(id);
            if (unit == null)
            {
                return HttpNotFound();
            }
            //ViewBag.CompanyId = new SelectList(db.Company, "Id", "Code", unit.CompanyId);
            //ViewBag.ParentId = new SelectList(db.Unit, "Id", "Code", unit.ParentId);
            return View(unit);
        }

        // POST: Unit/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Code,Name,Description,ImageFilePath,Active")] Unit unit)
        {
            if (ModelState.IsValid)
            {
                //int companyId = ((ActiveItem)Session["ActiveItem"]).CompanyId;
                //unit.CompanyId = companyId;
                db.Entry(unit).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            //ViewBag.CompanyId = new SelectList(db.Company, "Id", "Code", unit.CompanyId);
            //ViewBag.ParentId = new SelectList(db.Unit, "Id", "Code", unit.ParentId);
            return View(unit);
        }

        // GET: Unit/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unit unit = await db.Unit.FindAsync(id);
            if (unit == null)
            {
                return HttpNotFound();
            }
            return View(unit);
        }

        // POST: Unit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Unit unit = await db.Unit.FindAsync(id);
            db.Unit.Remove(unit);
            await db.SaveChangesAsync();
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