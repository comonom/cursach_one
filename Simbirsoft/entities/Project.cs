using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simbirsoft.entities
{
    [Table ("projects")]
    public class Project
    {
        [Column("id_projects")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("id_customers")]
        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        [Column("datestart")]
        public DateTime DateStart { get; set; }

        public virtual List<EmployeeOnProject> EmployeeOnProjects { get; set; }
        public virtual List<Service> Services { get; set; }
    }
}
