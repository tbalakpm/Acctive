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
    public class ProductCategoriesController : Controller
    {
        private AcctiveDbContext db = new AcctiveDbContext();

        // GET: ProductCategories
        public async Task<ActionResult> Index()
        {
            //var productCategory = db.ProductCategory.Include(p => p.Company);

            int companyId = GetCompanyId();
            if (companyId == 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            else
            {
                var prodcats = db.ProductCategory.Where(x => x.CompanyId == companyId);
                return View(await prodcats.ToListAsync());    //db.Product.ToListAsync()
            }
        }

        //// GET: ProductCategories/Details/5
        //public async Task<ActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ProductCategory productCategory = await db.ProductCategory.FindAsync(id);
        //    if (productCategory == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(productCategory);
        //}

        // GET: ProductCategories/Create
        public ActionResult Create()
        {
            //ViewBag.CompanyId = new SelectList(db.Company, "Id", "Code");
            int companyId = GetCompanyId();
            if (companyId == 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            else
            {
                return View(new ProductCategory());
            }
        }

        // POST: ProductCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Code,Name,Description,ImageFilePath,Active")] ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                int companyId = GetCompanyId();
                productCategory.CompanyId = companyId;
                db.ProductCategory.Add(productCategory);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            //ViewBag.CompanyId = new SelectList(db.Company, "Id", "Code", productCategory.CompanyId);
            return View(productCategory);
        }

        // GET: ProductCategories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategory productCategory = await db.ProductCategory.FindAsync(id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            int companyId = GetCompanyId();
            if (companyId == 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            else
            {
                return View(productCategory);
            }
        }

        // POST: ProductCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Code,Name,Description,ImageFilePath,Active")] ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                int companyId = GetCompanyId();
                productCategory.CompanyId = companyId;
                db.Entry(productCategory).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            //ViewBag.CompanyId = new SelectList(db.Company, "Id", "Code", productCategory.CompanyId);
            return View(productCategory);
        }

        // GET: ProductCategories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategory productCategory = await db.ProductCategory.FindAsync(id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            int companyId = GetCompanyId();
            if (companyId == 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            else
            {
                return View(productCategory);
            }
        }

        // POST: ProductCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ProductCategory productCategory = await db.ProductCategory.FindAsync(id);
            db.ProductCategory.Remove(productCategory);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private int GetCompanyId()
        {
            var item = Session["ActiveItem"] as ActiveItem;
            return (item != null) ? item.CompanyId : 0;
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