using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGame.Infrastructure.FunctionsSetting
{
    public class HourRewardSetting
    {
        public static int HourDelayTime = 0;//Thời gian delay giữa 2 lần nhận thưởng 60 = 1 giờ
        public static bool HourDelayTimePlusAllow = true;//Thời gian delay giữa 2 lần có tăng dần lên không
        public static int HourDelayTimePlus = 1;//Số phút tăng dần mỗi lần delay
        public static int ValuePerRound = 1;//Giá trị mỗi lần
        public static bool ValuePlusPerRoundAllow = true;//Cho phép giá trị tăng mỗi lần
        public static int ValuePlusPerRound = 1;//Giá trị tăng mỗi lần

    }
}
