using Acctive.Models.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Acctive.ViewModels
{
    public class CreateCompanyViewModel
    {
        public CreateCompanyViewModel()
        {
            DateTime today = DateTime.Today;
            if (today.Month >= 4 && today.Month <= 12)
            {
                PeriodCode = string.Format("AY{0:yyyy}-{1:yy}", today, today.AddYears(1));
                BeginDate = new DateTime(today.Year, 4, 1);
                EndDate = new DateTime(today.Year + 1, 3, 31);
            }
            else
            {
                PeriodCode = string.Format("AY{0:yyyy}-{1:yy}", today.AddYears(-1), today);
                BeginDate = new DateTime(today.Year - 1, 4, 1);
                EndDate = new DateTime(today.Year, 3, 31);
            }
        }

        public CreateCompanyViewModel(Company company)
        {
            Id = company.Id;
            Code = company.Code;
            Name = company.Name;
            Description = company.Description;
            ImageFilePath = company.ImageFilePath;
            RegdNumber = company.RegdNumber;
            SalesTaxNumber = company.SalesTaxNumber;
            CstNumber = company.CstNumber;

            foreach (var addr in company.Addresses)
            {
                AddressId = addr.Id;
                ContactName = addr.ContactName;
                ContactTitle = addr.ContactTitle;
                Line1 = addr.Line1;
                Line2 = addr.Line2;
                City = addr.City;
                State = addr.State;
                Country = addr.Country;
                Pincode = addr.Pincode;
                ContactNumber = addr.ContactNumber;
                Email = addr.Email;
                Website = addr.Website;

                break;
            }
            foreach (var year in company.Periods)
            {
                PeriodId = year.Id;
                PeriodCode = year.Code;
                BeginDate = year.BeginDate;
                EndDate = year.EndDate;
                break;
            }
        }

        #region Company

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Company code cannot be empty")]
        [StringLength(15)]
        public string Code { get; set; }

        [Required(ErrorMessage = "Company name cannot be empty")]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [Display(Name = "Image")]
        [StringLength(255)]
        public string ImageFilePath { get; set; }

        [Display(Name = "Regd.No.")]
        [StringLength(50)]
        public string RegdNumber { get; set; }

        [Display(Name = "TIN")]
        [StringLength(50)]
        public string SalesTaxNumber { get; set; }

        [Display(Name = "CST No.")]
        [StringLength(50)]
        public string CstNumber { get; set; }

        #endregion Company

        #region Address

        public int AddressId { get; set; }

        [Display(Name = "Contact Name")]
        [StringLength(50)]
        public string ContactName { get; set; }

        [Display(Name = "Contact Title")]
        [StringLength(50)]
        public string ContactTitle { get; set; }

        [Display(Name = "Address Line1")]
        [StringLength(100)]
        public string Line1 { get; set; }

        [Display(Name = "Address Line2")]
        [StringLength(100)]
        public string Line2 { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string State { get; set; }

        [StringLength(10)]
        public string Pincode { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        [Display(Name = "Contact No.")]
        [StringLength(25)]
        public string ContactNumber { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(100)]
        public string Website { get; set; }

        #endregion Address

        #region Period

        public int PeriodId { get; set; }

        [Display(Name = "Year Code")]
        [Required(ErrorMessage = "Period code cannot be empty")]
        [StringLength(15)]
        public string PeriodCode { get; set; }

        [Display(Name = "Begin Date")]
        public DateTime BeginDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        #endregion Period

        public T Get<T>() where T : new()
        {
            T result = default(T);

            if (typeof(T) == typeof(Company))
                //Func<CreateCompanyViewModel, Company> del = cc => new Company { };  //(T) GetCompany;
                return (T)(object)GetCompany();
            //else if (typeof(T) == typeof(Address))
            //    return (T)(object)GetAddress();
            //else if (typeof(T) == typeof(Period))
            //    return (T)(object)GetPeriod();

            return result;
        }

        public Company GetCompany()
        {
            var comp = new Company
            {
                Id = Id,
                Code = this.Code,
                Name = this.Name,
                Description = this.Description,
                Active = true,
                ImageFilePath = this.ImageFilePath,
                RegdNumber = this.RegdNumber,
                SalesTaxNumber = this.SalesTaxNumber,
                CstNumber = this.CstNumber
            };

            comp.Addresses.Add(GetAddress(comp));
            comp.Periods.Add(GetPeriod(comp));

            return comp;
        }

        public Address GetAddress(Company company)
        {
            if (IsAddressEmpty())
                return null;

            var addr = new Address
            {
                Id = AddressId,
                ContactName = this.ContactName,
                ContactTitle = this.ContactTitle,
                Line1 = this.Line1,
                Line2 = this.Line2,
                City = this.City,
                State = this.State,
                Country = this.Country,
                Pincode = this.Pincode,
                ContactNumber = this.ContactNumber,
                Email = this.Email,
                Website = this.Website,
                Active = true,
                AddressType = AddressType.Default
            };
            addr.Companies.Add(company);

            return addr;
        }

        public Period GetPeriod(Company company)
        {
            return new Period
            {
                Id = PeriodId,
                Code = this.PeriodCode,
                BeginDate = this.BeginDate,
                EndDate = this.EndDate,
                Active = true,
                Company = company,
                CompanyId = Id
            };
        }

        public bool IsAddressEmpty()
        {
            return string.IsNullOrWhiteSpace(ContactName) &&
                string.IsNullOrWhiteSpace(ContactTitle) &&
                string.IsNullOrWhiteSpace(Line1) &&
                string.IsNullOrWhiteSpace(Line2) &&
                string.IsNullOrWhiteSpace(City) &&
                string.IsNullOrWhiteSpace(State) &&
                string.IsNullOrWhiteSpace(Pincode) &&
                string.IsNullOrWhiteSpace(Country) &&
                string.IsNullOrWhiteSpace(ContactNumber) &&
                string.IsNullOrWhiteSpace(Email) &&
                string.IsNullOrWhiteSpace(Website);
        }
    }
}