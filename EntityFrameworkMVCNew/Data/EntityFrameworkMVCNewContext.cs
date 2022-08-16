using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EntityFrameworkMVCNew.Models;

namespace EntityFrameworkMVCNew.Data
{
    public class EntityFrameworkMVCNewContext : DbContext
    {
        public EntityFrameworkMVCNewContext (DbContextOptions<EntityFrameworkMVCNewContext> options)
            : base(options)
        {
        }

        public DbSet<EntityFrameworkMVCNew.Models.CricketPlayer> CricketPlayers { get; set; }
    }
}
