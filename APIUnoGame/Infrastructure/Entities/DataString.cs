using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGame.Infrastructure.Entities
{
    public class DataString
    {
        //0 = Thông tin tài khoản
        //1 = Danh sách tướng
        //2 = Inventory
        public int DataType { get; set; }
        public string UserID { get; set; }
        public string Values { get; set; }
    }
}
