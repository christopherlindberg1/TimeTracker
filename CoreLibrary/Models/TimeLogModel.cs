using System;

namespace CoreLibrary.Models
{
    public class TimeLogModel
    {
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int LunchInMinutes { get; set; }
    }
}
