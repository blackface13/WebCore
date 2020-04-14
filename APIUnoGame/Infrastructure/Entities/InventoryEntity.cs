using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIGame.Infrastructure.Entities
{
    public class InventoryEntity
    {
        [Key]
        public string UserID { get; set; }
        public string Data { get; set; }
        public int LastUpdate { get; set; }
        public string HWID { get; set; }
    }
}