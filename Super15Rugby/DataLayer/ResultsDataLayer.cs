using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using Super15Rugby.Models;

namespace Super15Rugby.DataLayer
{
    public enum SuperRugbyTeamID { Hurricanes = 1, Crusaders = 2, Chiefs = 3, Highlanders = 4, Blues = 5, Waratahs = 6, Brumbies = 7, Reds = 8, Force = 9, Rebels = 10, Bulls = 11, Sharks = 12, Cheetahs = 13, Stormers = 14, Lions = 15, Cats = 15, Kings = 16}
    public class ResultsDataLayer
    {
        public void InsertJsonParsedResult(int id, int weekNo, Result result)
        {
            result.TeamOne = result.TeamOne.Trim();
            result.TeamTwo = result.TeamTwo.Trim();
            result.TeamOne = (result.TeamOne == "W. Force" || result.TeamOne == "W Force" || result.TeamOne == "Western Force") ? "Force" : result.TeamOne;
            result.TeamTwo = (result.TeamTwo == "W. Force" || result.TeamTwo == "W Force" || result.TeamTwo == "Western Force") ? "Force" : result.TeamTwo;
            result.TeamOne = (result.TeamOne == "Melbourne Rebels") ? "Rebels" : result.TeamOne;
            result.TeamTwo = (result.TeamTwo == "Melbourne Rebels") ? "Rebels" : result.TeamTwo;
            result.TeamOne = (result.TeamOne == "S.Kings") ? "Kings" : result.TeamOne;
            result.TeamTwo = (result.TeamTwo == "S.Kings") ? "Kings" : result.TeamTwo;

            var teamOneEnum = (SuperRugbyTeamID)Enum.Parse(typeof(SuperRugbyTeamID), result.TeamOne);
            var teamTwoEnum = (SuperRugbyTeamID)Enum.Parse(typeof(SuperRugbyTeamID), result.TeamTwo);
            var teamOneId = (int)teamOneEnum;
            var teamTwoId = (int)teamTwoEnum;
            var matchDate = DateTime.Parse(result.MatchDate);
            var formattedMatchDate = matchDate.ToString("yyyy-MM-dd");
            var con = new SqlConnection(ConfigurationManager.ConnectionStrings["SuperRugbyData"].ToString());
            using (con)
            {
                string query = "INSERT INTO Results " + 
                    "VALUES ('" + id + "', '" + formattedMatchDate + "', '" + weekNo + "', '" + teamOneId + "', '" + result.TeamOneScore + 
                    "', '" + teamTwoId + "', '" + result.TeamTwoScore + "')";
                con.Query<int>(query);
            }
        }

        public SuperRugbyYear GetSuperRugbyResults(int year) 
        {
            var yearResults = QueryResult(year);
            var superRugbyYear = new SuperRugbyYear { year = year};
            var weekCounter = 1;
            var highestWeekNumber = yearResults.Max(r => r.Round);
            
            while (weekCounter <= highestWeekNumber) 
            {
                var week = new Week
                {
                    weekNumber = weekCounter,
                    results = yearResults.Where(y => y.Round == weekCounter).ToList()
                };
                superRugbyYear.weeks.Add(week);
                weekCounter++;
            }
            return superRugbyYear;
        }


        private IEnumerable<DBResult> QueryResult(int year) 
        {
            var yearStr = year.ToString();
            var con = new SqlConnection(ConfigurationManager.ConnectionStrings["SuperRugbyData"].ToString());
            using (con)
            {
                string query = "SELECT " +
                    "TeamOne = Team1Ref.TeamName, Results.TeamOneScore, " +
                    "TeamTwo = Team2Ref.TeamName, Results.TeamTwoScore, " + 
                    "Results.MatchDate, Results.Round " +
                    "FROM Results " +
                    "JOIN TTeam Team1Ref ON Results.TeamOneID = Team1Ref.TeamId " +
                    "JOIN TTeam Team2Ref ON Results.TeamTwoID = Team2Ref.TeamId " +
                    "WHERE Results.MatchDate BETWEEN '" + yearStr + "0101' AND '" + yearStr + "1231'" +
                    "ORDER BY Results.MatchDate ASC";
                return con.Query<DBResult>(query);
            }
        }
    }
}