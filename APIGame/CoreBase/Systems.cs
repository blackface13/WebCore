using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGame.CoreBase
{
    public static class Systems
    {
        /// <summary>
        /// Trả về số thời gian hiện tại theo dạng int
        /// </summary>
        public static int GetTimeNowToInteger()
        {
            return (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
        }
    }
}
