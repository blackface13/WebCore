using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIGame.Infrastructure.Entities
{
    public class HourRewardEntity
    {
        [Key]
        public string UserID { get; set; }
        public DateTime LastUpdate { get; set; }
        public int UpdateTime { get; set; }
        public int UpdateRound { get; set; }
        public string NextReward { get; set; }
    }
}
