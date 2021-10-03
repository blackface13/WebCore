using Microsoft.EntityFrameworkCore;
using SkullTribalIntrusionServer.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkullTribalIntrusionServer.CoreBase
{
    public class GameDBContext : DbContext
    {
        public GameDBContext(DbContextOptions<GameDBContext> options)
    : base(options)
        { }

        public DbSet<PlayerEntity> Players { get; set; }
        public DbSet<ItemEntity> PlayerItems { get; set; }
    }
}
