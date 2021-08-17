using SkullTribalIntrusionServer.CoreBase;

namespace SkullTribalIntrusionServer.Models
{
    public class ResponseModel
    {
        public Systems.State Res { get; set; }
        public string ResName { get => Res.ToString(); }
        public string Messages { get; set; }
    }
}
