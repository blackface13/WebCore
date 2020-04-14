using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIGame.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using APIUnoGame.Infrastructure.Entities;

namespace APIGame.CoreBase
{
    public class GameDBContext : DbContext
    {
        public GameDBContext(DbContextOptions<GameDBContext> options)
    : base(options)
        { }

        public DbSet<HourRewardEntity> HourReward { get; set; }//Nhận thưởng hàng ngày
        public DbSet<InventoryEntity> Inventory { get; set; }//Nhận thưởng hàng ngày
        public DbSet<Player> Player { get; set; }//Nhận thưởng hàng ngày
        public DbSet<APIUnoGame.Infrastructure.Entities.Ranking> Ranking { get; set; }
    }
}
