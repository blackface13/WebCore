using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIGame.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace APIGame.CoreBase
{
    public class GameDBContext : DbContext
    {
        public GameDBContext(DbContextOptions<GameDBContext> options)
    : base(options)
        { }

        public DbSet<BreakBallEntity> BreakBall { get; set; }//Đập bóng
        public DbSet<HourRewardEntity> HourReward { get; set; }//Nhận thưởng hàng ngày
        public DbSet<Player> Player { get; set; }//Nhận thưởng hàng ngày
    }
}
