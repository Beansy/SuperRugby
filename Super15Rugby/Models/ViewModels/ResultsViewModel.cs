using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Super15Rugby.Models.ViewModels
{
    public class YearResults 
    {
        public List<DBResult> results;
        public int year;

        public YearResults() 
        {
            results = new List<DBResult>();
        }
    }
    public class ResultsViewModel
    {
        public List<SuperRugbyYear> years;
        
        public ResultsViewModel() 
        {
            years = new List<SuperRugbyYear>();
        }
    }
}