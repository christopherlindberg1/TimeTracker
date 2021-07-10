using System.Collections.Generic;

namespace CoreLibrary.Models
{
    public class MonthTimeLogModel
    {
        public IEnumerable<TimeLogModel> DayLogs { get; }
    }
}
