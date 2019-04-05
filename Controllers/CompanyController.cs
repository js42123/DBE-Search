using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DBESearch.Models;


namespace DBESearch.Controllers
{
    public class CompanyController : Controller
    {

        private DBESearchDirectoryEntities db = new DBESearchDirectoryEntities();

        // GET: Company
        public ActionResult Index()
        {
          
            return View(db.DBECompanies.ToList());
        }

        [HttpPost]
        public JsonResult CompanyList(int jtStartIndex = 0, int JtPageSize =0, string jtSorting = null)
        {
            try
            {
                var CompanyList = db.DBECompanies.Select(c => new
                {
                    CompanyId = c.CompanyId,
                    CompanyName = c.CompanyName,
                    DBAName = c.DBAName,
                    OwnersFirstName = c.OwnersFirstName,
                    OwnersLastName = c.OwnersLastName,
                    CompanyAddress = c.CompanyAddress,
                    City = c.City,
                    State = c.State,
                    Zip = c.Zip,
                    Phone = c.Phone,
                    District = c.District,
                    DBE = c.DBE,
                    ACDBE = c.ACDBE,
                    SPP = c.SBP,
                    MBE = c.MBE,
                    Certified = c.Certified

                }).OrderBy(c => c.CompanyName).Skip(jtStartIndex).Take(JtPageSize).ToList();

                int recordCount = db.DBECompanies.Count();

                return Json(new { Result = "OK", Records = CompanyList, TotalRecordCount = recordCount });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult codelist(int id)
        {
            int codecount = db.CompanyItemCodes.Count();

            var CompanyItemCodes = db.CompanyItemCodes.Where(c => c.CompanyID == id).Select(c => new { ItemCode = c.ItemCode, Comments = c.Comments }).ToArray();

            return Json(new { Result = "OK", Records = CompanyItemCodes });
        }

        public ActionResult Details(int id)
        {
            DBECompany dbecompany = db.DBECompanies.Find(id);
            List<CompanyItemCode> myList = db.CompanyItemCodes.Where(c => c.CompanyID == id).Select(c => new CompanyItemCode { ItemCode = c.ItemCode, Comments = c.Comments }).ToList();

            if (dbecompany == null)
            {
                return HttpNotFound();
            }
            return View(dbecompany);
        }
       
    }
}