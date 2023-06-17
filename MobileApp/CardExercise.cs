using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MobileApp
{
    public class CardExercise
    {
        [PrimaryKey, AutoIncrement]
        public int  ID { get; set; }
        public string Name { get; set; }
        public string GIF { get; set; }
        public string Description { get; set; }
        public PartBody PartBody { get; set; }
    }
}
