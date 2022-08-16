using EntityFrameworkConsole.Context;
using EntityFrameworkConsole.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EntityFrameworkConsole
{
    class Program
    {
        public static async void AddTempTeamRecords()
        {
            using var db = new CricketPlayerContext();
            await db.Teams.AddRangeAsync(
                                //new Team { Id = 1, Name = "India" },
                                //new Team { Id = 2, Name = "Australia" },
                                //new Team { Id = 3, Name = "England" },
                                //new Team { Id = 4, Name = "New Zealand" }
                                new Team { Name = "India" },
                new Team {  Name = "Australia" },
                new Team { Name = "England" },
                new Team { Name = "New Zealand" }
                );
            db.SaveChanges();
        }

        public static async Task<Team> GetTeam(int id, CricketPlayerContext cricketPlayerContext)
        {
            return await cricketPlayerContext.Teams.FirstOrDefaultAsync(t => t.Id == id);
        }

        public static async void AddTempCricketPlayerRecords()
        {
            using var db = new CricketPlayerContext();
            await db.CricketPlayers.AddRangeAsync(
                new CricketPlayer {  Name = "Sachin Tendular", Age = 35, Team = await GetTeam(1, db) },
                new CricketPlayer { Name = "Rohit Sharma", Age = 30, Team = await GetTeam(1, db) },
                new CricketPlayer {Name = "Virat Kohli", Age = 29, Team = await GetTeam(1, db) },
                new CricketPlayer {Name = "Steve Smith", Age = 29, Team = await GetTeam(2, db) },
                new CricketPlayer { Name = "Glenn Maxwell", Age = 28, Team = await GetTeam(2, db) },
                new CricketPlayer {  Name = "Jos Buttler", Age = 27, Team = await GetTeam(3, db) },
                new CricketPlayer {  Name = "Kane Williamson", Age = 28, Team = await GetTeam(4, db) },
                new CricketPlayer { Name = "Trent Boult", Age = 26, Team = await GetTeam(4, db) }
                );
            db.SaveChanges();
        }



        static void Main(string[] args)
        {
            //AddTempTeamRecords();
            //AddTempCricketPlayerRecords();
            Console.WriteLine("Records Inserted");
            Console.ReadLine();
        }
    }
}
