using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using SportsTeamManager.Models;
using System.Text;

namespace SportsTeamManager.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<SportsTeam> SportsTeams => Set<SportsTeam>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\MSSQLLocalDB;Database=SportsTeamDB;Trusted_Connection=True;");
        }
    }
}
