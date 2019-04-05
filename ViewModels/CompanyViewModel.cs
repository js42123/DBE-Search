using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBESearch.ViewModels;

namespace DBESearch.ViewModels
{
    public class CompanyViewModel
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string DBAName { get; set; }
        public string OwnersFirstName { get; set; }
        public string OwnersLastName { get; set; }
        public string CompanyAddress { get; set; }
        public string Email { get; set; }
        public string AltEmail { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string County { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string AltFax { get; set; }
        public string District { get; set; }
        public Nullable<bool> DBE { get; set; }
        public Nullable<bool> ACDBE { get; set; }
        public Nullable<bool> SBP { get; set; }
        public Nullable<bool> MBE { get; set; }
        public Nullable<bool> Certified { get; set; }
        public Nullable<System.DateTime> CertificationDate { get; set; }
        public Nullable<System.DateTime> DecertificationDate { get; set; }
        public string DecertReason { get; set; }
        public Nullable<bool> Suspended { get; set; }
        public string Website { get; set; }

        public List<CompanyItemCodeDesc> CompanyItemCodesList { get; set; }
        public List<CompanyNAICSCodeDesc> CompanyNAICSCodesList { get; set; }

    }


}