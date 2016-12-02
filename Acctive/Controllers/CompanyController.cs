using Acctive.Models;
using Acctive.Models.Application;
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
    public class CompanyController : Controller
    {
        private AcctiveDbContext db = new AcctiveDbContext();

        // GET: Company
        public async Task<ActionResult> Index()
        {
            //var comps = db.Company.Where(x => x.Active);
            return View(await db.Company.ToListAsync());
        }

        // GET: Company/Open
        // GET: Company/Open/5
        public async Task<ActionResult> Open(int? id)
        {
            if (id == null)
            {
                var comps = db.Company.Where(x => x.Active);
                if (comps.Count() == 1)
                    id = comps.FirstOrDefault().Id;
                else
                    return View(await comps.ToListAsync());
            }
            Company company = await db.Company.FindAsync(id);
            var active = new ActiveItem
            {
                CompanyId = company.Id,
                CompanyCode = company.Code,
                CompanyName = company.Name,
                PeriodId = company.Periods.Count > 0 ? company.Periods[0].Id : 0,
                PeriodCode = company.Periods.Count > 0 ? company.Periods[0].Code : string.Empty
            };
            Session["ActiveItem"] = active;

            return RedirectToAction("Index", "Home");
        }

        // GET: Company/Create
        public ActionResult Create()
        {
            var model = new Company(); //CreateCompanyViewModel();
            if (model.Addresses.Count == 0)
                model.Addresses.Add(new Address());
            if (model.Periods.Count == 0)
                model.Periods.Add(new Period());
            return View(model);
        }

        // POST: Company/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // [Bind(Include = "Id,Code,Name,Description,ImageFilePath,RegdNumber,SalesTaxNumber,CstNumber,Active")]
        public async Task<ActionResult> Create(Company model)
        {
            if (ModelState.IsValid)
            {
                //var com = company.Get<Company>();
                db.Company.Add(model);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Company/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = await db.Company.FindAsync(id);
            if (model.Addresses.Count == 0)
                model.Addresses.Add(new Address());
            if (model.Periods.Count == 0)
                model.Periods.Add(new Period());

            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Company/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Company model)
        {
            if (ModelState.IsValid)
            {
                var company = await db.Company.FindAsync(model.Id);

                #region Company

                company.Code = model.Code;
                company.Name = model.Name;
                company.Description = model.Description;
                company.ImageFilePath = model.ImageFilePath;
                company.RegdNumber = model.RegdNumber;
                company.SalesTaxNumber = model.SalesTaxNumber;
                company.CstNumber = model.CstNumber;
                company.Active = model.Active;

                #endregion Company

                #region Address

                var modelAddr = model.Addresses[0];
                if (company.Addresses.Count == 0)
                    company.Addresses.Add(modelAddr);
                else
                {
                    var compAddr = company.Addresses[0];
                    compAddr.ContactName = modelAddr.ContactName;
                    compAddr.ContactTitle = modelAddr.ContactTitle;
                    compAddr.Line1 = modelAddr.Line1;
                    compAddr.Line2 = modelAddr.Line2;
                    compAddr.City = modelAddr.City;
                    compAddr.State = modelAddr.State;
                    compAddr.Country = modelAddr.Country;
                    compAddr.Pincode = modelAddr.Pincode;
                    compAddr.ContactNumber = modelAddr.ContactNumber;
                    compAddr.Email = modelAddr.Email;
                    compAddr.Website = modelAddr.Website;
                }

                #endregion Address

                #region Period

                var modelPeriod = model.Periods[0];
                if (company.Periods.Count == 0)
                    company.Periods.Add(modelPeriod);
                else
                {
                    var compPeriod = company.Periods[0];
                    compPeriod.Code = modelPeriod.Code;
                    compPeriod.BeginDate = modelPeriod.BeginDate;
                    compPeriod.EndDate = modelPeriod.EndDate;
                }

                #endregion Period

                db.Entry(company).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Company/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = await db.Company.FindAsync(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Company/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Company company = await db.Company.FindAsync(id);
            db.Company.Remove(company);
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