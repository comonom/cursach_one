using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simbirsoft.entities
{
    [Table("customers")]
    public class Customer
    {
        [Column("id_customers")]
        public int Id { get; set; }

        [Column("name_company")]
        public string NameCompany { get; set; }

        [Column("address")]
        public string Address { get; set; }        [Column("BIK")]        public int BIK { get; set; }        [Column("INN")]        public long INN { get; set; }

        public virtual List<Project> Projects { get; set; }
    }
}
