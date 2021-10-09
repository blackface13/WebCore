using AutoMapper;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SkullTribalIntrusionServer.CoreBase
{
    public static class Systems
    {
        public static Mapper Mapper;

        public enum State//Trạng thái request
        {
            Waiting, //Trạng thái chờ kết nối server
            Success, //Thao tác thành công với server
            Failed, //Thao tác thất bại, hoặc server từ chối yêu cầu
            Connected, //Kết nối tới server thành công
            LostConnected, //Không thể kết nối tới server
            SyncFailedByHWID,//Không thể đồng bộ do đăng ký chơi trên thiết bị này nhưng đồng bộ trên thiết bị khác
        }
        public static State Response;
        /// <summary>
        /// Trả về số thời gian hiện tại theo dạng int
        /// </summary>
        public static int GetTimeNowToInteger()
        {
            return (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
        }

        /// <summary>
        /// Chuyển list -> chuỗi ngăn cách bởi ';', dành cho lưu vào data
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ConvertListToSplitString(IList input)
        {
            if (input == null || input.Count <= 0)
                return null;

            string result = "";
            foreach (var item in input)
                result += item.ToString() + ";";
            return result;
        }

        /// <summary>
        /// Chuyển đổi về dạng list với string ngăn cách bởi ';'
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static List<T> ConvertStringSplitToList<T>(string input)
        {
            List<T> result = new List<T>();
            var arrayTemp = input.Split(';');
            for (int i = 0; i < arrayTemp.Length; i++)
            {
                if (!string.IsNullOrEmpty(arrayTemp[i]))
                    result.Add((T)Convert.ChangeType(arrayTemp[i], typeof(T)));
            }
            return result;
        }
    }
}
