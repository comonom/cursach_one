using Microsoft.EntityFrameworkCore;
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
            var userId = Settings.Default.db_user;
            var password = Settings.Default.db_pass;
            var database = Settings.Default.db_name;
            var server = Settings.Default.db_server;

            optionsBuilder.UseMySql($"user Id={userId}; password={password}; database={database}; server={server}; charset=utf-8",
                new MySqlServerVersion("8.0.33"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeOnProject>().HasKey(k => new { k.EmpolyeeId, k.ProjectId });
        }


    }
}
