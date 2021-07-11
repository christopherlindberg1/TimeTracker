using CoreLibrary.DataAccess.Excel.Private;
using CoreLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CoreLibrary.DataAccess.Excel
{
    /// <summary>
    /// Class used to provide access to time log data that is read from an Excel file.
    /// </summary>
    public class ExcelTimeLogRepository : ITimeLogRepository
    {
        private readonly ExcelDataReader _excelDataReader;

        public ExcelTimeLogRepository(ExcelDataReader excelDataReader)
        {
            _excelDataReader = excelDataReader;
        }

        public async Task<IEnumerable<TimeLogModel>> GetTimeLogDataForMonthAsync(Month month)
        {
            var keyCellLocations = await _excelDataReader.GetKeyCellLocationsAsync(new FileInfo(FilePaths.TimeLogFilePath), month);

            int daysOfMonth = Convert.ToInt32(month);

            int firstRow = int.Parse(keyCellLocations.DateHeaderExcelAddress[1].ToString()) + 1;
            int lastRow =
                int.Parse(keyCellLocations.DateHeaderExcelAddress[1].ToString())
                + DateTime.DaysInMonth(DateTime.Now.Year, daysOfMonth);

            string dateCellAddressRange
                = $"{keyCellLocations.DateHeaderExcelAddress[0]}{firstRow}:"
                + $"{keyCellLocations.DateHeaderExcelAddress[0]}{lastRow}";

            string startTimeCellAddressRange
                = $"{keyCellLocations.StartHeaderExcelAddress[0]}{firstRow}:"
                  + $"{keyCellLocations.StartHeaderExcelAddress[0]}{lastRow}";

            string endTimeCellAddressRange
                = $"{keyCellLocations.EndHeaderExcelAddress[0]}{firstRow}:"
                  + $"{keyCellLocations.EndHeaderExcelAddress[0]}{lastRow}";

            string lunchBreakCellAddressRange
                = $"{keyCellLocations.LunchBreakHeaderExcelAddress[0]}{firstRow}:"
                  + $"{keyCellLocations.LunchBreakHeaderExcelAddress[0]}{lastRow}";

            var allDates =
                await _excelDataReader.GetColumnDataAsync(new FileInfo(FilePaths.TimeLogFilePath), month, dateCellAddressRange);

            var allStartTimes =
                await _excelDataReader.GetColumnDataAsync(new FileInfo(FilePaths.TimeLogFilePath), month, startTimeCellAddressRange);

            var allEndTimes =
                await _excelDataReader.GetColumnDataAsync(new FileInfo(FilePaths.TimeLogFilePath), month, endTimeCellAddressRange);

            var allLunchBreaks =
                await _excelDataReader.GetColumnDataAsync(new FileInfo(FilePaths.TimeLogFilePath), month, lunchBreakCellAddressRange);

            var timeLogDataForMonth = new List<TimeLogModel>();

            for (int i = 0; i < allDates.Count; i++)
            {
                string startTimeRawData = (i < allStartTimes.Count) ? allStartTimes[i] : null;
                string endTimeRawData = (i < allEndTimes.Count) ? allEndTimes[i] : null;
                string lunchBreakTimeRawData = (i < allLunchBreaks.Count) ? allLunchBreaks[i] : null;

                DateTime date = DateTime.Parse(allDates[i]);

                var timeLog = new TimeLogModel
                {
                    Date = date,
                    StartTime = string.IsNullOrWhiteSpace(startTimeRawData) ? null : TimeSpan.Parse(startTimeRawData),
                    EndTime = string.IsNullOrWhiteSpace(endTimeRawData) ? null : TimeSpan.Parse(endTimeRawData),
                    LunchInMinutes = string.IsNullOrWhiteSpace(lunchBreakTimeRawData) ? null : int.Parse(lunchBreakTimeRawData)
                };

                timeLogDataForMonth.Add(timeLog);
            }

            return timeLogDataForMonth;
        }
    }
}
