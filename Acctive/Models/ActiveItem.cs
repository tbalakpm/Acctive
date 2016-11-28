using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acctive.Models
{
    [Serializable]
    public class ActiveItem
    {
        public int CompanyId { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public int PeriodId { get; set; }
        public string PeriodCode { get; set; }

        public override string ToString()
        {
            var displayString = CompanyName;
            displayString += string.IsNullOrWhiteSpace(PeriodCode) ? string.Empty : string.Format(" [{0}]", PeriodCode);
            return displayString;
        }
    }
}