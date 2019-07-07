using System;

namespace DataPlus.Entities.Models
{
    public class Log
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Message { get; set; }
        public string Level { get; set; }
        public string Logger { get; set; }
    }
}
