using SkullTribalIntrusionServer.CoreBase;

namespace SkullTribalIntrusionServer.Models
{
    public class ResponseModel
    {
        public Systems.State Res { get; set; }
        public string ResName { get => Res.ToString(); }

        /// <summary>
        /// Mã lỗi
        /// </summary>
        public int ErrorCode { get; set; }
        public string Messages { get; set; }
    }

    //ErrorCode
    //001: Thời gian đồng bộ hoá ở server mới hơn client => hỏi client xem có get data từ server về ko
}
