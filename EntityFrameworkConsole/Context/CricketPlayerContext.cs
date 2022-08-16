using EntityFrameworkConsole.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkConsole.Context
{
    class CricketPlayerContext : DbContext
    {
        public DbSet<CricketPlayer> CricketPlayers { get; set; }
        public DbSet<Team> Teams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=EPINHYDW05C1\\MSSQLSERVER1;Initial Catalog=CricketDB;Integrated Security=True;"); //+
               // "Trusted_Connections=True;");
        }
    }
}
