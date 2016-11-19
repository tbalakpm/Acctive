using Acctive.Models.Accounting;
using Acctive.Models.Context;
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
    public class LedgersController : Controller
    {
        private AcctiveDbContext db = new AcctiveDbContext();

        // GET: Ledgers
        public async Task<ActionResult> Index()
        {
            //var account = db.Account.Include(a => a.Category).Include(a => a.Company).Include(a => a.Parent);
            return View(await db.Account.ToListAsync());
        }

        // GET: Ledgers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = await db.Account.FindAsync(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // GET: Ledgers/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.AccountCategory, "Id", "Code");
            ViewBag.CompanyId = new SelectList(db.Company, "Id", "Code");
            ViewBag.ParentId = new SelectList(db.Account, "Id", "Code");
            return View();
        }

        // POST: Ledgers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Code,Name,Description,ParentId,SequenceNumber,IsGroup,CategoryId,ImageFilePath,CompanyId,Active")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Account.Add(account);
                await db.SaveChangesAsync();
                return RedirectToAction("Create");
            }

            ViewBag.CategoryId = new SelectList(db.AccountCategory, "Id", "Code", account.CategoryId);
            ViewBag.CompanyId = new SelectList(db.Company, "Id", "Code", account.CompanyId);
            ViewBag.ParentId = new SelectList(db.Account, "Id", "Code", account.ParentId);
            return View(account);
        }

        // GET: Ledgers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = await db.Account.FindAsync(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.AccountCategory, "Id", "Code", account.CategoryId);
            ViewBag.CompanyId = new SelectList(db.Company, "Id", "Code", account.CompanyId);
            ViewBag.ParentId = new SelectList(db.Account, "Id", "Code", account.ParentId);
            return View(account);
        }

        // POST: Ledgers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Code,Name,Description,ParentId,SequenceNumber,IsGroup,CategoryId,ImageFilePath,CompanyId,Active")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.AccountCategory, "Id", "Code", account.CategoryId);
            ViewBag.CompanyId = new SelectList(db.Company, "Id", "Code", account.CompanyId);
            ViewBag.ParentId = new SelectList(db.Account, "Id", "Code", account.ParentId);
            return View(account);
        }

        // GET: Ledgers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = await db.Account.FindAsync(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Ledgers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Account account = await db.Account.FindAsync(id);
            db.Account.Remove(account);
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