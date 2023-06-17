using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using SQLite;

namespace MobileApp
{
    public class curProgram
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public ProgramType Program { get; set; }
    }
}
