using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simbirsoft.entities
{
    [Table("services")]
    public class Service
    {
        [Column("id_services")]
        public int Id { get; set; }

        [Column("id_employee")]
        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }

        [Column("id_projects")]
        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Column("datestart")]
        public DateTime DateStart { get; set; }
        
        [Column("id_type_services")]
        public int TypeServiceId { get; set; }

        [ForeignKey("TypeServiceId")]
        public virtual TypeService TypeService { get; set; }
    }
}
