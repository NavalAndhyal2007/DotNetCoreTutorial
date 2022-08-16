using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkConsole.Models
{
    //Data Source=EPINHYDW05C1\\MSSQLSERVER1;Initial Catalog=EcommerceDB;Integrated Security=True
    class CricketPlayer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Team Team { get; set; }

    }
}
