using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace APIGame.Infrastructure.Entities
{
    public class Player
    {
        [Key]
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string PassCode { get; set; }//Dùng để đổi thiết bị
        public double NumberSpined { get; set; } //Số lượt đã quay vòng quay may mắn
        public string HWID { get; set; }
        public string Country { get; set; }
        public byte UnoBGColorR { get; set; }
        public byte UnoBGColorG { get; set; }
        public byte UnoBGColorB { get; set; }
        public bool UnoSettingFastPush { get; set; }
        public bool UnoSettingFastPass { get; set; }
        public bool UnoSettingImgSupport { get; set; }
        public float UnoBasicWinRound { get; set; }
        public float UnoBasicLoseRound { get; set; }
        public float UnoBasicPoint { get; set; }
        public float UnoExtensionWinRound { get; set; }
        public float UnoExtensionLoseRound { get; set; }
        public float UnoExtensionPoint { get; set; }
        public bool IsLoginFB { get; set; }
        public int UnoTypePlay { get; set; }//Kiểu chơi cơ bản hoặc nâng cao
        public bool IsChangeDevice { get; set; }//Biến này xác định có chuyển thiết bị hay ko. = true thì ko cho phép đồng bộ lên, chỉ cho phép đồng bộ xuống, sau khi đồng bộ xuống thì đẩy lại dữ liệu lên và set = false. Nếu = false thì ko phải là trạng thái đăng ký chuyển thiết bị, trùng IDHW mới cho phép đồng bộ
    }
}
