using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace APIGame.Infrastructure.Entities
{
    public class Player
    {
        [Key]
        public string UserID { get; set; }
        public string UserName { get; set; }
        public int Golds { get; set; }
        public int Gems { get; set; }
        public int InventorySlot { get; set; }
        public int BattleWin { get; set; }
        public int BattleLose { get; set; }
        public int NumberSpined { get; set; } //Số lượt đã quay vòng quay may mắn
        public string ItemUserForBattle { get; set; }
        public bool IsAutoBattle { get; set; }
        public string EnemyFutureMap { get; set; }
        public string DifficultMap { get; set; }
    }
}
