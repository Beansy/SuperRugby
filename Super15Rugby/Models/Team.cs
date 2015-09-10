using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Super15Rugby.Models
{
    public enum Conference { Australia, NewZealand, SouthAfrica}

    public class Team
    {
        //public string TeamName { get; set; }
        //public Conference Conference { get; set; }
        //public int Played { get; set; }
        //public int Wins { get; set; }
        //public int Losses { get; set; }
        //public int Draws { get; set; }
        //public int PointsFor { get; set; }
        //public int PointsAgainst { get; set; }
        //public int PointsDifferential { get; set; }
        //public int BonusPoints { get; set; }
        //public int TablePoints { get; set; }
        public string ConferenceName;
        public string TeamName;
        public int Played;
        public int Wins;
        public int Losses;
        public int Draws;
        public int PointsFor;
        public int PointsAgainst;
        public int PointsDifferential;
        public int BonusPoints;
        public int TablePoints;
    }
}