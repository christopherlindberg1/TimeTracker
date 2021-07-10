using CoreLibrary.Models;
using System.Collections.Generic;

namespace CoreLibrary.DataAccess
{
    /// <summary>
    /// Interface for classes that provide access to time log data.
    /// </summary>
    interface ITimeLogRepository
    {
        IEnumerable<MonthTimeLogModel> GetTimeLogDataForMonth(Month month);
    }
}
