using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Simbirsoft.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simbirsoft.entities
{
    public class ModelContext : DbContext
    {
        private static ModelContext _instance = null;

        public static ModelContext Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ModelContext();

                return _instance;
            }
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeOnProject> EmployeeOnProjects { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<TypeService> TypeServices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(App.Configuration.GetConnectionString("db"),
                new MySqlServerVersion(App.Configuration.GetSection("versionMySql").Value));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeOnProject>().HasKey(k => new { k.EmpolyeeId, k.ProjectId });
        }


    }
}
