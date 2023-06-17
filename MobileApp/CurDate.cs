using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MobileApp
{
    public class CurDate
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int CurDay { get; set; }
        public int CurWeek { get; set; }
        public int Time { get; set; }
    }
}
