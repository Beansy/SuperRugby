using Super15Rugby.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Super15Rugby.DataLayer
{
    public class SuperRugbyTableDataLayer
    {

        public Table GetTable()
        {
            var table = new Table() { Teams = QueryTable().ToList() };

            foreach (var t in table.Teams)
            {
                t.PointsDifferential = t.PointsFor - t.PointsAgainst;
            }

            table.Teams = SortTeams(table.Teams);

            return table;
        }

        private IEnumerable<Team> QueryTable() 
        {
            var con = new SqlConnection(ConfigurationManager.ConnectionStrings["SuperRugbyData"].ToString());
            using (con)
            {
                const string query = "SELECT TConference.ConferenceName, TTeam.TeamName, dbo.TSuperRugbyTable.Played, dbo.TSuperRugbyTable.Wins, dbo.TSuperRugbyTable.Losses, dbo.TSuperRugbyTable.Draws, dbo.TSuperRugbyTable.PointsFor, dbo.TSuperRugbyTable.PointsAgainst, dbo.TSuperRugbyTable.PointsDifferential, dbo.TSuperRugbyTable.BonusPoints, dbo.TSuperRugbyTable.TablePoints " +
                      "FROM dbo.TSuperRugbyTable " + 
                      "INNER JOIN dbo.TTeam " +
                      "ON TSuperRugbyTable.TeamID=TTeam.TeamId " + 
                      "INNER JOIN dbo.TConference " +
                      "ON TTeam.ConferenceID=TConference.ConferenceId " +
                      "ORDER BY TTeam.TeamName";
                return con.Query<Team>(query);
            }
        }

        private List<Team> DoSuperRugbyTableSortAlgorithm(List<Team> teams) 
        {
            return teams.OrderByDescending(t => t.TablePoints).ThenByDescending(t => t.Wins).ThenByDescending(t => t.PointsDifferential).ToList();
        }

        private List<Team> SortTeams(List<Team> teams)
        {
            var conferences = new List<string>();
            conferences.Add("New Zealand");
            conferences.Add("Australia");
            conferences.Add("South Africa");

            var sortedTable = new List<Team>();

            foreach (var conference in conferences) 
            {
                var bestTeamOfConference = DoSuperRugbyTableSortAlgorithm(teams.Where(t => t.ConferenceName == conference).ToList()).First();
                sortedTable.Add(bestTeamOfConference);
            }

            sortedTable = DoSuperRugbyTableSortAlgorithm(sortedTable);

            foreach (var team in sortedTable) 
            {
                teams.RemoveAll(t => t.TeamName == team.TeamName);
            }
            teams = DoSuperRugbyTableSortAlgorithm(teams);
            
            foreach (var team in teams) 
            {
                sortedTable.Add(team);
            }
            return sortedTable;
        }
    }
}