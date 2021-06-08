using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkullTribalIntrusionServer.Infrastructure.Entities
{
    public class Players
    {
        [Key]
        public Guid PlayerId { get; set; }
        public string PlayerName { get; set; }
        public long Golds { get; set; }
        public long Gems { get; set; }
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
    }
}
