using CoreLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreLibrary.DataAccess
{
    /// <summary>
    /// Interface for classes that provide access to time log data.
    /// </summary>
    interface ITimeLogRepository
    {
        Task<IEnumerable<TimeLogModel>> GetTimeLogDataForMonthAsync(Month month);
    }
}
