using System.Net;
using Microsoft.Reporting.WebForms;

namespace CloudEngineMX.Erp.SetinTred.Reporting.Models
{
    public class CustomReportCredentials : IReportServerCredentials
    {
        private readonly string _userName;
        private readonly string _password;
        private readonly string _domainName;

        public CustomReportCredentials(string userName, string password, string dominio)
        {
            _userName = userName;
            _password = password;
            _domainName = dominio;
        }

        public bool GetFormsCredentials(out System.Net.Cookie authCookie, out string userName, out string password, out string authority)
        {
            authCookie = null;
            userName = password = authority = null;
            return false;
        }

        public System.Security.Principal.WindowsIdentity ImpersonationUser
        {
            get { return null; }
        }

        public System.Net.ICredentials NetworkCredentials
        {
            get { return new NetworkCredential(_userName, _password, _domainName); }
        }
    }
}
