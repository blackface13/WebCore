﻿using AutoMapper;
using System;
using System.Collections;

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
                result = item.ToString() + ";";
            return result;
        }
    }
}
