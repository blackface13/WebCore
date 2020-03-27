using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace APIGame.Infrastructure.Entities
{
    public class BreakBallEntity
    {
        [Key]
        public string UserID { get; set; }
        public DateTime LastUpdate { get; set; }
        public int HammerRemaining { get; set; }
    }
}
