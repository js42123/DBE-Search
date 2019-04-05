using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DBESearch.Models;
//using AHTD.Linq;
using DBESearch.ViewModels;
using System.Data;
using System.Data.Entity;
using System.Web.UI.WebControls;
using System.IO;
using OfficeOpenXml;

namespace DBESearch.Controllers
{
    public class DBECompanyController : Controller
    {
        private DBESearchDirectoryEntities db = new DBESearchDirectoryEntities();
        // GET: DBECompany
        public ActionResult Index()
        {
            

            var zipcodes = db.DBECompanies.OrderBy(z => z.Zip).Select(z => z.Zip).Distinct();
            ViewBag.ZipCodeList = zipcodes.ToList();

            var states = db.DBECompanies.OrderBy(s => s.State).Select(s => s.State).Distinct();
            ViewBag.StateList = states.ToList();

            var naicss = db.NAICSCodes.OrderBy(z => z.NAICSCode1).Select(s =>  s.NAICSCode1 + " - " + s.Description).Distinct();
            ViewBag.NAICSList = naicss.ToList();

            var items = db.ItemCodes.OrderBy(z => z.ItemCode1).Select(s => s.ItemCode1 + " - " + s.Description).Distinct();
            ViewBag.ItemCodesList = items.ToList();

            var cities = db.DBECompanies.OrderBy(z => z.City).Select(s => s.City).Distinct();
            ViewBag.CityList = cities.ToList();

            var counties = db.DBECompanies.OrderBy(c => c.County).Select(c => c.County).Distinct();
            ViewBag.counties = counties.ToList();

            List<string> DistrictList = new List<string>();
            DistrictList.Add("District 1 - Crittenden, Cross, St. Francis, Lee, Monroe, Phillips, Woodruff");
            DistrictList.Add("District 2 - Arkansas, Ashley, Chicot, Drew, Grant, Jefferson, Lincoln");
            DistrictList.Add("District 3 - Hempstead, Howard, Lafayette, Little River, Miller, Nevada, Pike, Sevier");
            DistrictList.Add("District 4 - Crawford, Franklin, Logan, Polk, Scott, Sebastian");
            DistrictList.Add("District 5 - Cleburne, Fulton, Independence, Izard, Jackson, Sharp, Stone, White");
            DistrictList.Add("District 6 - Garland, Hot Springs, Lonoke, Prairie, Pulaski, Saline");
            DistrictList.Add("District 7 - Bradley, Calhoun, Clark, Cleveland, Dallas, Ouachita, Union");
            DistrictList.Add("District 8 - Conway, Faulkner, Johnson, Montgomery, Perry, Pope, Van Buren, Yell");
            DistrictList.Add("District 9 - Baxter, Boone, Carrol, Madison, Marion, Newton, Searcy");
            DistrictList.Add("District 10 - Clay, Craighead, Greene, Lawrence, Mississippi, Poinsett, Randolph");
            ViewBag.Districts = DistrictList;

            return View();
        }

        [HttpGet]
        public JsonResult GetBusinessDescription(String term)
        {
                var t = db.ItemCodes.Where(c => c.Description.Contains(term))
                                        .Select(c => new { c.Description });
                return Json(t.Select(i => new { label = i.Description, data = i }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult companies(String term)
        {

            //var t = db.DBECompanies.Where(c => c.CompanyName.Contains(term) || c.DBAName.Contains(term)).Where(c => c.Certified == true && c.Suspended != true)
            //                        .Select(c => new { c.CompanyId, c.CompanyName, c.DBAName });
            //return Json(t.Select(i => new { label = i.CompanyName + "    " + i.DBAName, data = i }), JsonRequestBehavior.AllowGet);

            var t = db.DBECompanies.Where(c => c.CompanyName.Contains(term) || c.DBAName.Contains(term)).Where(c => c.Certified == true && c.Suspended != true)
                                    .Select(c => new { c.CompanyId, c.CompanyName, c.DBAName });

            return Json(t.Select(i => new { label = i.CompanyName, data = i }), JsonRequestBehavior.AllowGet);

        }

        public JsonResult lastName(String term)
        {
            object[] lastNames = db.DBECompanies.Where(c => c.OwnersLastName.ToUpper().Contains(term.ToUpper())).Select(c => c.OwnersLastName).Distinct().ToArray();
            return Json(lastNames, JsonRequestBehavior.AllowGet);
        }

        public JsonResult firstName(String term)
        {
            var t = db.DBECompanies.Where(c => c.OwnersFirstName.Contains(term)).Select(c => c.OwnersFirstName).Distinct();
            return Json(t.Select( i => new { label = i, data = i}), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Partialform()
        {

            var results = db.NAICSCodes.ToList();
            return PartialView("~/Views/DBECompany/_NAICS.cshtml", results);
        }

        public ActionResult ItemPartialform()
        {
            var results = db.ItemCodes.ToList();
            return PartialView("~/Views/DBECompany/_ItemCodes.cshtml", results);
        }

        public ActionResult SearchResults(CompanySearchViewModel searchData)
        {
            //IQueryable<DBECompany> searchquery = db.DBECompanies.Where(s => s.CompanyName != "000");

            IQueryable<DBECompany> searchquery = db.DBECompanies.Where(s => s.CompanyName == "000");
            //IQueryable<DBECompany> empty = Enumerable.Empty<DBECompany>() as IQueryable<DBECompany>;

            if (searchData != null)
            {

                IQueryable<DBECompany> DBE = db.DBECompanies;
                IQueryable<DBECompany> ACDBE = db.DBECompanies;
                IQueryable<DBECompany> MBE = db.DBECompanies;
                IQueryable<DBECompany> SBP = db.DBECompanies;
                if ((searchData.DBE))
                {
                    DBE = DBE.Where(s => s.DBE == true);
                }
                else
                {
                    DBE = DBE.Where(s => s.CompanyName == "000");
                }
                if ((searchData.ACDBE))
                {
                    ACDBE = ACDBE.Where(s => s.ACDBE == true);
                }
                else
                {
                    ACDBE = ACDBE.Where(s => s.CompanyName == "000");
                }
                if ((searchData.SBP))
                {
                    SBP = SBP.Where(s => s.SBP == true);
                }
                else
                {
                    SBP = SBP.Where(s => s.CompanyName == "000");
                }
                if ((searchData.MBE))
                {
                    MBE = MBE.Where(s => s.MBE == true);
                }
                else
                {
                    MBE = MBE.Where(s => s.CompanyName == "000");
                }

                //searchquery = searchquery.Concat( DBE).Concat(ACDBE).Concat(SBP).Concat(MBE).Distinct();

                searchquery = searchquery.Concat(DBE).Concat(ACDBE).Concat(SBP).Concat(MBE).Distinct();

                if (!String.IsNullOrEmpty(searchData.Company))
                {
                    //searchquery = searchquery.Where(c => c.CompanyName.Contains(searchData.Company)).Select(c => c);
                    searchquery = searchquery.Where(c => c.CompanyName.Contains(searchData.Company) || c.DBAName.Contains(searchData.Company)   ).Select(c => c);
                }

                if (!String.IsNullOrEmpty(searchData.BusinessDescription))
                {
                    IQueryable<DBECompany> dBECompanies = searchquery.Join(db.CompanyNAICSCodes, a => a.CompanyId, b => b.Companyid, (a, b) => new { a, b })
                                            .Join(db.NAICSCodes, c => c.b.NAICSCode, d => d.NAICSCode1, (c, d) => new { c, d })
                                            .Where(e => e.d.Description.Contains(searchData.BusinessDescription))
                                            .Select(g => g.c.a);
                    IQueryable<DBECompany> NAICSCompanies = searchquery.Join(db.CompanyItemCodes, a => a.CompanyId, b => b.CompanyID, (a, b) => new { a, b })
                                           .Join(db.ItemCodes, c => c.b.ItemCode, d => d.ItemCode1, (c, d) => new { c, d })
                                           .Where(e => e.d.Description.Contains(searchData.BusinessDescription))
                                           .Select(g => g.c.a);
                    searchquery = dBECompanies.Union(NAICSCompanies);
                }

                if (searchData.NAICS != null && searchData.NAICS.Count > 0 && !String.IsNullOrEmpty(searchData.NAICS[0]))
                {
                    searchquery = from company in searchquery
                                  join compNAICSCode in db.CompanyNAICSCodes on company.CompanyId equals compNAICSCode.Companyid
                                  join searchCode in searchData.NAICS on compNAICSCode.NAICSCode equals searchCode
                                  select company;
                }

                if (searchData.ItemCode != null && searchData.ItemCode.Count > 0 && !string.IsNullOrEmpty(searchData.ItemCode[0]))
                {
                    searchquery = from DBECompany in searchquery
                                  join companyItemCode in db.CompanyItemCodes on DBECompany.CompanyId equals companyItemCode.CompanyID
                                  join searchedcodes in searchData.ItemCode on companyItemCode.ItemCode.ToString() equals searchedcodes
                                  select DBECompany;

                }
                if (!String.IsNullOrEmpty(searchData.OwnerFirstName))
                {
                    searchquery = searchquery.Where(c => c.OwnersFirstName.Contains(searchData.OwnerFirstName)).Select(c => c);
                }
                if (!String.IsNullOrEmpty(searchData.OwnerLastName))
                {
                    searchquery = searchquery.Where(c => c.OwnersLastName.Contains(searchData.OwnerLastName)).Select(c => c);
                }
                if (searchData.Cities != null && searchData.Cities.Count > 0 && !String.IsNullOrEmpty(searchData.Cities[0]))
                {
                    searchquery = searchquery.Join(searchData.Cities, a => a.City, b => b, (a, b) => new { a }).Select(c => c.a);
                }
                if (searchData.States != null && searchData.States.Count > 0 && !String.IsNullOrEmpty(searchData.States[0]))
                {
                    searchquery = searchquery.Join(searchData.States, a => a.State, b => b, (a, b) => new { a }).Select(c => c.a);
                }
                if (searchData.OwnerZipCodes != null && searchData.OwnerZipCodes.Count > 0 && !String.IsNullOrEmpty(searchData.OwnerZipCodes[0]))
                {
                    searchquery = searchquery.Join(searchData.OwnerZipCodes, a => a.Zip, b => b, (a, b) => new { a }).Select(c => c.a);
                }
                if (searchData.Districts != null && searchData.Districts.Count > 0 && !String.IsNullOrEmpty(searchData.Districts[0]))
                {
                    searchquery = searchquery.Join(searchData.Districts, a => a.District, b => b, (a, b) => new { a }).Select(c => c.a);
                }
                if (!string.IsNullOrEmpty(searchData.AreaCode))
                {
                    searchquery = searchquery.Where(c => c.Phone.StartsWith(searchData.AreaCode));
                }
                if (searchData.County != null && searchData.County.Count > 0 && !String.IsNullOrEmpty(searchData.County[0]))
                {
                    searchquery = searchquery.Join(searchData.County, a => a.County, b => b, (a, b) => new { a }).Select(c => c.a);
                }

                searchquery = searchquery.Where(s => s.Suspended != true && s.Certified == true).OrderBy(c => c.CompanyName);

                //done with the querybuilding
                //List<DBECompany> results = searchquery.Distinct().ToList();
                List<DBECompany> results = searchquery.Distinct().OrderBy(c => c.CompanyName).ToList();
                return PartialView("~/Views/DBECompany/_SearchResults.cshtml", results);
            }
            return View();
        }
        private IQueryable<DBECompany> GetSearchResults(Models.DBECompany data, DBESearchDirectoryEntities db)
        {
            IQueryable<Models.DBECompany> query = db.DBECompanies;
            query = query.Where(s => s.Suspended != true && s.Certified == true).OrderBy(c => c.CompanyName);

            return query;
        }

        public ActionResult ExportDatatoExcel()
        {
            try
            {
                IQueryable<Models.DBECompany> query;
                List<Models.DBECompany> CompanyList;
                using (var db = new Models.DBESearchDirectoryEntities())
                {
                    Models.DBECompany data = (Models.DBECompany)Session["query"];
                    query = GetSearchResults(data, db);

                    query = query
                        .Select(c => c);
                    CompanyList = query.ToList();
                }
                GridView gv = new GridView();
                gv.DataSource = CompanyList;
                gv.DataBind();

                var fileName = "Company_List-" + DateTime.Now.ToString("yyyyMMdd_hhss") + ".xlsx";
                var file = new FileInfo(fileName);
                ExcelPackage excel = new ExcelPackage();
                var workSheet = excel.Workbook.Worksheets.Add("List");
                var totalCols = gv.Rows[0].Cells.Count;
                var totalRows = gv.Rows.Count;
                var headerRow = gv.HeaderRow;
                for (var i = 1; i <= totalCols; i++)
                {
                    workSheet.Cells[1, i].Value = headerRow.Cells[i - 1].Text;
                }
                for (var j = 1; j <= totalRows; j++)
                {
                    for (var i = 1; i <= totalCols; i++)
                    {
                        var a = CompanyList.ElementAt(j - 1);
                        workSheet.Cells[j + 1, i].Value = a.GetType().GetProperty(headerRow.Cells[i - 1].Text).GetValue(a);
                    }
                }
                using (var memoryStream = new MemoryStream())
                {
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;  filename=" + fileName);
                    excel.SaveAs(memoryStream);
                    memoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
                return RedirectToAction("Search");
            }
            catch (Exception)
            {
                ViewBag.Message = "Your data is too Large to Export to Excel. Please filter your Search to avoid the error. Thank you!";
                return View();
            }
        }

        public ActionResult Contact()
        {
            return PartialView("~/Views/DBECompany/_SearchResults.cshtml");
        }

        public JsonResult cities(String term)
        {            
            var t = db.DBECompanies.Where(c => c.City.Contains(term)).Select(c => new { c.City }).Distinct();
            return Json(t.Select(i => new { label = i.City, data = i }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DisplayAll()
        {
            ViewBag.Message = "List all DBE Companies here.";
            return View();
        }

        /// 
        /// This is the view that is used to show the company contact information, NAICS codes, and departmental codes for a given company.
        /// The company id comes from the row click of the search results on Search page.
        /// 
        public ActionResult companyDetails(int? id)
        {
            List<CompanyViewModel> companyVM = new List<CompanyViewModel>();

            CompanyViewModel company = new CompanyViewModel();

            var companydatalist = db.DBECompanies.Where(c => c.CompanyId == id).Select(d => new
            {
                CompanyName = d.CompanyName,
                DBAName = d.DBAName,
                OwnersFirstName = d.OwnersFirstName,
                OwnersLastName = d.OwnersLastName,
                CompanyAddress = d.CompanyAddress,
                City = d.City,
                State = d.State,
                zip = d.Zip,
                Phone = d.Phone,
                County = d.County,
                Email = d.Email,
                //AltEmail = d.AltEmail,
                Fax = d.Fax,
                //AltFax = d.AltFax,
                District = d.District,
                DBE = d.DBE,
                ACDBE = d.ACDBE,
                SBP = d.SBP,
                MBE = d.MBE,
                Suspended = d.Suspended,
                Certified = d.Certified,
                CertificationDate = d.CertificationDate,
                DecertificationDate = d.DecertificationDate
            }).ToList();

            foreach (var item in companydatalist)
            {
                company.CompanyName = item.CompanyName;
                company.DBAName = item.DBAName;
                company.OwnersFirstName = item.OwnersFirstName;
                company.OwnersLastName = item.OwnersLastName;
                company.CompanyAddress = item.CompanyAddress + ", " + item.City + ", " + item.State + " " + item.zip ;
                company.City = item.City;
                company.State = item.State;
                company.Zip = item.zip;
                company.Phone = item.Phone;
                company.County = item.County;
                company.Email = item.Email;
                //company.AltEmail = item.AltEmail;
                company.Fax = item.Fax;
                //company.AltFax = item.AltFax;
                company.District = item.District;
                company.DBE = item.DBE;
                company.ACDBE = item.ACDBE;
                company.SBP = item.SBP;
                company.MBE = item.MBE;
                company.Suspended = item.Suspended;
                company.Certified = item.Certified;
                company.CertificationDate = item.CertificationDate;
                company.DecertificationDate = item.DecertificationDate;
                companyVM.Add(company);
            }

            //build me a list of the Departmental item codes for a given company and put the list in the object
            var codes = db.CompanyItemCodes.Where(c => c.CompanyID == id).Include("ItemCode1").ToList();

            List<CompanyItemCodeDesc> codelist = new List<CompanyItemCodeDesc>();

            foreach (var item in codes)
            {
                CompanyItemCodeDesc companycode = new CompanyItemCodeDesc();
                companycode.ItemCode = item.ItemCode;
                companycode.Description = item.ItemCode1.Description;
                companycode.Comments = item.Comments;
                codelist.Add(companycode);
            }

            company.CompanyItemCodesList = codelist;

            //build me a list of the NAICS codes for a given company and put the list in the object
            List<CompanyNAICSCodeDesc> NAICSList = new List<CompanyNAICSCodeDesc>();

            var naicsquery = (from comp in db.DBECompanies
                              join compNaics in db.CompanyNAICSCodes on comp.CompanyId equals compNaics.Companyid
                              join naics in db.NAICSCodes on compNaics.NAICSCode equals naics.NAICSCode1
                              where comp.CompanyId == id
                              select new { comp.CompanyId, naics.NAICSCode1, naics.Description }).ToList();

            foreach (var x in naicsquery)
            {
                CompanyNAICSCodeDesc naicsitem = new CompanyNAICSCodeDesc();
                naicsitem.NAICSCode = x.NAICSCode1;
                naicsitem.Description = x.Description;
                NAICSList.Add(naicsitem);
            }

            company.CompanyNAICSCodesList = NAICSList;

            return View(companyVM);
        }

    }
}

