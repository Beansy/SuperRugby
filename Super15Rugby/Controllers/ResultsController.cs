using Super15Rugby.DataLayer;
using Super15Rugby.Models;
using Super15Rugby.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Super15Rugby.Controllers
{
    public class ResultsController : Controller
    {
        // GET: Results
        public ActionResult Index()
        {
            var yearsToQueryFor = GetYearsToQueryFor();
            var resultDataLayer = new ResultsDataLayer();
            var viewModel = new ResultsViewModel();

            foreach (var year in yearsToQueryFor) 
            {
                viewModel.years.Add(resultDataLayer.GetSuperRugbyResults(year));
            }
            
            return View("AllResults", viewModel);
        }

        private List<int> GetYearsToQueryFor() 
        {
            var latestSuperRugbyYear = Int32.Parse(ConfigurationManager.AppSettings["LatestSuperRugbyYear"]);
            var yearsToGet = new List<int>();
            yearsToGet.Add(2006);

            while (yearsToGet.Max() < latestSuperRugbyYear)
            {
                yearsToGet.Add(yearsToGet.Max() + 1);
            }

            return yearsToGet;
        }
    }
}