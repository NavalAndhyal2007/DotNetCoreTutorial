using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFrameworkMVC.Models
{
    class CricketPlayer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Team Team { get; set; }

    }
}
