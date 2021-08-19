using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkullTribalIntrusionServer.Infrastructure.Entities
{
    public class PlayerModel
    {
        public int Level { get; set; }
        public double Exp { get; set; }
        public float Attack { get; set; }
        public float Defense { get; set; }
        public float Health { get; set; }
        public float HealingPerSecond { get; set; }//Hồi máu mỗi giây
        public float BreakDefense { get; set; }
        public float Critical { get; set; }
        public float AtkSpeed { get; set; }//Tốc độ dương cung
        public float WalkSpeed { get; set; }
        public List<ItemModel> ItemsData { get; set; }//Dữ liệu item 

        #region Các chỉ số của người chơi (ko phải chỉ số nhân vật)
        [Key]
        public Guid PlayerId { get; set; }
        public string PlayerName { get; set; }
        public long Golds { get; set; }
        public long Silver { get; set; }
        public int RoundNumber { get; set; }//Số trận đã chơi
        public long ShotNumber { get; set; }//Số lần đã bắn mũi tên ra
        public long ShotHitted { get; set; }//Số lần đã trúng mục tiêu
        public long ShotToHead { get; set; }//Số lần bắn trúng đầu
        public long ShotHighAngle { get; set; }//Số lần bắn trúng góc cao
        public long ShotSuperHighAngle { get; set; }//Số lần bắn trúng góc siêu cao
        public long HittedNumber { get; set; }//Số lần bị bắn
        public long HittedHeadNumber { get; set; }//Số lần bị bắn trúng đàu 
        public float DamageCreated { get; set; }//Số dmg gây ra
        public float DamageReceived { get; set; }//Số dmg nhận vào
        public DateTime StartedDate { get; set; }//Ngày bắt đầu chơi
        public long ArrowHeight { get; set; }//Số lượng mũi tên góc cao
        public long ArrowSuperHeight { get; set; }////Số lượng mũi tên góc siêu cao
        #endregion

        public List<int> ArrowsBag { get; set; }
        public List<int> ArrowsBuyed { get; set; }

        public PlayerModel Clone()
        {
            return (PlayerModel)MemberwiseClone();
        }
    }
}
