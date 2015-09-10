using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Super15Rugby.Models
{
    public class Result
    {
        public int id { get; set; }
        public string TeamOne { get; set; }
        public string TeamOneScore { get; set; }
        public string TeamTwo { get; set; }
        public string TeamTwoScore { get; set; }
        public string MatchDate { get; set; }
    }

    public class DBResult : Result 
    {
        public int Round;
    }

    public class Week
    {
        public int weekNumber { get; set; }
        public List<DBResult> results { get; set; }

        public Week() 
        {
            results = new List<DBResult>();
        }
    }

    public class SuperRugbyYear
    {
        public int year { get; set; }
        public List<Week> weeks { get; set; }

        public SuperRugbyYear() 
        {
            weeks = new List<Week>();
        }
    }
}