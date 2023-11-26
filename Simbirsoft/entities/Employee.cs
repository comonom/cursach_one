using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simbirsoft.entities
{
    [Table ("employee")]
    public class Employee
    {
        [Column ("id_employee")]
        public int Id { get; set; }

        [Column("surname")]
        public string Surname { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("patronymic")]
        public string Patronymic { get; set; }

        [Column("salary")]
        public decimal Salary { get; set; }

        [Column("login")]
        public string Login { get; set; }

        [Column("password")]
        public string Password { get; set; }

        public string Fullname => $"{Surname} {Name} {Patronymic}";

        public virtual List<EmployeeOnProject> EmployeeOnProjects { get; set; }
        public virtual List<Service> Services { get; set; }
    }
}
