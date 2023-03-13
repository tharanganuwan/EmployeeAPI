using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeAPI.Models;

namespace EmployeeAPI.DataAccess
{
    public class EmployeeDbContext : DbContext
    {
        public DbSet<Employee> Employee { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Server=localhost; Database=Employee; User Id=sa; password=5503;";
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
