using ComputerAPIs.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerAPIs.Data
{
    public class ComputerDbContext : DbContext
    {
        private IConfiguration config;
        public ComputerDbContext(IConfiguration config)
        {
            this.config= config;
        }
        public DbSet<Computer> Computers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(config.GetConnectionString("ComputerDb"));
        }

    }
}
