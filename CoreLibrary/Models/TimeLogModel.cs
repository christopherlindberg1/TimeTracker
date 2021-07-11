using System;

namespace CoreLibrary.Models
{
    public class TimeLogModel
    {
        public DateTime Date { get; set; }

        public TimeSpan? StartTime { get; set; }
        
        public TimeSpan? EndTime { get; set; }
        
        public int? LunchInMinutes { get; set; }

        public DayOfWeek DayOfWeek => Date.DayOfWeek; 

        public bool HasCompleteData => StartTime != null && EndTime != null && LunchInMinutes != null;

        public bool IsDayOfWeekend => DayOfWeek == DayOfWeek.Saturday || DayOfWeek == DayOfWeek.Sunday;
    }
}
