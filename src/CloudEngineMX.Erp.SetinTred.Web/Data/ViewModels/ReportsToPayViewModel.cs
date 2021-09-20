using System.Collections.Generic;
using System.Linq;

namespace CloudEngineMX.Erp.SetinTred.Web.Data.ViewModels
{
    public class ReportsToPayViewModel
    {
        public string SupplierKey { get; set; }
        public string FiscalName { get; set; }
        public string Applicant { get; set; }
        public string CurrencyName { get; set; }
        public decimal Amount { get; set; }

        public decimal TotalPesos => Pays.Where(x => x.CurrencyName.Equals("PESOS")).Sum(x => x.Amount);
        public decimal TotalUsd => Pays.Where(x => x.CurrencyName.Equals("DOLARES")).Sum(x => x.Amount);
        public List<ReportsToPayViewModel> Pays { get; set; }
    }
}
