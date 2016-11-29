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
    public class ProductsController : Controller
    {
        private AcctiveDbContext db = new AcctiveDbContext();

        // GET: Products
        public async Task<ActionResult> Index()
        {
            //var product = db.Product.Include(p => p.Category).Include(p => p.Company).Include(p => p.Parent);

            var item = Session["ActiveItem"] as ActiveItem;
            int companyId = item != null ? item.CompanyId : 0;
            var products = db.Product.Where(x => x.CompanyId == companyId);
            return View(await products.ToListAsync());    //db.Product.ToListAsync()
        }

        //// GET: Products/Details/5
        //public async Task<ActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Product product = await db.Product.FindAsync(id);
        //    if (product == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(product);
        //}

        // GET: Products/Create
        public ActionResult Create()
        {
            int companyId = ((ActiveItem)Session["ActiveItem"]).CompanyId;

            ViewBag.CategoryId = new SelectList(db.ProductCategory.Where(x => x.CompanyId == companyId), "Id", "Code");
            ViewBag.ParentId = new SelectList(db.Product.Where(x => x.CompanyId == companyId && x.IsGroup), "Id", "Code");
            return View(new Product());
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Code,Name,Description,ParentId,IsGroup,CategoryId,CostPrice,ProfitPercent,SellingPrice,TaxPercent,Surcharge,Freight,MinimumQuantity,MaximumQuantity,ReorderLevelQuantity,ImageFilePath,Active")] Product product)
        {
            int companyId = ((ActiveItem)Session["ActiveItem"]).CompanyId;
            if (ModelState.IsValid)
            {
                product.CompanyId = companyId;

                db.Product.Add(product);
                await db.SaveChangesAsync();
                return RedirectToAction("Create");
            }

            ViewBag.CategoryId = new SelectList(db.ProductCategory.Where(x => x.CompanyId == companyId), "Id", "Code", product.CategoryId);
            ViewBag.ParentId = new SelectList(db.Product.Where(x => x.CompanyId == companyId && x.IsGroup), "Id", "Code", product.ParentId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Product.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            int companyId = ((ActiveItem)Session["ActiveItem"]).CompanyId;

            ViewBag.CategoryId = new SelectList(db.ProductCategory.Where(x => x.CompanyId == companyId), "Id", "Code", product.CategoryId);
            ViewBag.ParentId = new SelectList(db.Product.Where(x => x.CompanyId == companyId && x.Id != product.Id && x.IsGroup), "Id", "Code", product.ParentId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Code,Name,Description,ParentId,IsGroup,CategoryId,CostPrice,ProfitPercent,SellingPrice,TaxPercent,Surcharge,Freight,MinimumQuantity,MaximumQuantity,ReorderLevelQuantity,ImageFilePath,Active")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            int companyId = ((ActiveItem)Session["ActiveItem"]).CompanyId;

            ViewBag.CategoryId = new SelectList(db.ProductCategory.Where(x => x.CompanyId == companyId), "Id", "Code", product.CategoryId);
            ViewBag.ParentId = new SelectList(db.Product.Where(x => x.CompanyId == companyId && x.Id != product.Id && x.IsGroup), "Id", "Code", product.ParentId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Product.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Product product = await db.Product.FindAsync(id);
            db.Product.Remove(product);
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