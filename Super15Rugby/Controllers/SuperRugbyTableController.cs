using Newtonsoft.Json;
using Super15Rugby.DataLayer;
using Super15Rugby.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Super15Rugby.Controllers
{
    public class SuperRugbyTableController : Controller
    {
        // GET: Table
        public ActionResult Index()
        {
            
            return View(new SuperRugbyTableDataLayer().GetTable());
        }
    }
}