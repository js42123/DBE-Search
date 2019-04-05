using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DBESearch.ViewModels
{
    public class CompanySearchViewModel
    {
        [Display(Name = "Disadvantaged Business Enterprise (DBE)")]
        public bool DBE { get; set; }
        [Display(Name = "Airport Concessionaire Disadvantaged Business Enterprise (ACDBE)")]
        public bool ACDBE { get; set; }
        [Display(Name = "Small Business Provision (SBP)")]
        public bool SBP { get; set; }
        [Display(Name = "Minority Business Enterprise (MBE)")]
        public bool MBE { get; set; }
        [Display(Name = "Company Name")]
        public List<string> CompanyDescriptions { get; set; }
        public string Company { get; set; }
        public int CompanyId { get; set; }
        [Display(Name = "City")]
        public List<string> Cities { get; set; }
        [Display(Name = "Business Description")]
        public string BusinessDescription { get; set; }
        [Display(Name = "Dept. Codes")]
        public List<string> DepartmentCodes { get; set; }
        [Display(Name = "Contact's First Name")]
        public string OwnerFirstName { get; set; }
        [Display(Name = "Contact's Last Name")]
        public string OwnerLastName { get; set; }
        [Display(Name = "Zip Codes")]
        public List<string> OwnerZipCodes { get; set; }
        [Display(Name = "States")]
        public List<string> States { get; set; }
        [Display(Name = "County")]
        public List<string> County { get; set; }
        [Display(Name = "Area Code")]
        public string AreaCode { get; set; }
        [Display(Name = "Item Codes")]
        public List<string> ItemCode { get; set; }
        [Display(Name = "NAICS Codes")]
        public List<string> NAICS { get; set; }
        [Display(Name = "Currently Suspended")]
        public bool Suspended { get; set; }
        [Display(Name = "District")]
        public List<string> Districts { get; set; }

       
       



    }
}