﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkullTribalIntrusionServer.Infrastructure.Entities
{
    public class PlayerEntity
    {
        public int Level { get; set; }
        public float Exp { get; set; }
        public List<ItemEntity> ItemsData { get; set; }//Dữ liệu item 
        public string HWID { get; set; }//Mã phần cứng thiết bị

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
        public long TotalBattleTime { get; set; }//Tổng số thời gian trong trận, tính theo giây
        public int LastTimeSync { get; set; }//Thời gian cuối cùng đồng bộ
        #endregion

        //public List<int> ArrowsBag { get; set; }
        //public List<int> ArrowsBuyed { get; set; }

        public string ArrowsBagValues { get; set; }
        public string ArrowsBuyedValues { get; set; }

        public PlayerEntity Clone()
        {
            return (PlayerEntity)MemberwiseClone();
        }
    }
}
