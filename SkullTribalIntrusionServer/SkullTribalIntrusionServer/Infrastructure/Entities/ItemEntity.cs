using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkullTribalIntrusionServer.Infrastructure.Entities
{
    public class ItemEntity
    {
        /// <summary>
        /// 0: tài nguyên
        /// 1: sử dụng
        /// 2: nhiệm vụ
        /// </summary>
        public int ItemTypeMode { get; set; }
        [Key]
        public Guid ItemGuidId { get; set; } //Mã item để đồng bộ với server
        public Guid PlayerId { get; set; }
        public int ItemId { get; set; }
        public int Index { get; set; }//Vị trí sắp xếp
        public int Quantity { get; set; }
        //public UnityAction<ItemModel> Action { get; set; }//Action sẽ ko lưu trên server
        public ItemEntity Clone()
        {
            return (ItemEntity)this.MemberwiseClone();
        }
    }
}
