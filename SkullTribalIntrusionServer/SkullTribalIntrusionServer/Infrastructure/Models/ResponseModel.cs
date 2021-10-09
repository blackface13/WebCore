using SkullTribalIntrusionServer.CoreBase;

namespace SkullTribalIntrusionServer.Models
{
    public class ResponseModel
    {
        public Systems.State Res { get; set; }

        /// <summary>
        /// Mã lỗi
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Lỗi trả về từ server
        /// </summary>
        public string Messages { get; set; }

        /// <summary>
        /// Dữ liệu trả về từ server
        /// </summary>
        public object Results { get; set; }
    }
    //ErrorCode
    //1: Thời gian đồng bộ hoá ở server mới hơn client => hỏi client xem có get data từ server về ko
    //2: Không tìm thấy kết quả cần có ở server
    //3: Khác HWID => hỏi client xem có đồng bộ đè dữ liệu lên không
}
