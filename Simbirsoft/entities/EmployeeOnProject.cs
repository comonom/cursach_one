using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simbirsoft.entities
{
    [Table ("employee_has_projects")]
    public class EmployeeOnProject
    {
        [Column("id_employee")]
        public int EmpolyeeId { get; set; }

        [ForeignKey("EmpolyeeId")]
        public virtual Employee Employee { get; set; }

        [Column("id_projects")]
        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }

        [Column("salary_for_company")]
        public decimal SalaryForCompany { get; set; }

        [Column("datestart")]
        public DateTime DateStart { get; set; }

        [Column("dateend")]
        public DateTime? DateEnd { get; set; }
    }
}
