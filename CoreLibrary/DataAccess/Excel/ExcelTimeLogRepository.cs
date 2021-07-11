using CoreLibrary.DataAccess.Excel.Private;
using CoreLibrary.Models;
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
        private readonly ExcelDataReader excelDataReader;

        public ExcelTimeLogRepository(ExcelDataReader excelDataReader)
        {
            this.excelDataReader = excelDataReader;
        }

        public async Task<IEnumerable<MonthTimeLogModel>> GetTimeLogDataForMonthAsync(Month month)
        {
            var keyCellLocations = await excelDataReader.GetKeyCellLocationsAsync(new FileInfo(FilePaths.TimeLogFilePath), month);

            // Temporary code designed for the month of july
            var timeLogData = new List<TimeLogModel>();

            int firstRow = int.Parse(keyCellLocations.DateHeaderExcelAddress[1].ToString()) + 1;
            int lastRow = int.Parse(keyCellLocations.DateHeaderExcelAddress[1].ToString()) + 31;

            string dateCellAddressRange
                = $"{keyCellLocations.DateHeaderExcelAddress[0]}{firstRow}:"
                + $"{keyCellLocations.DateHeaderExcelAddress[0]}{lastRow}";

            string startTimeCellAddressRange
                = $"{keyCellLocations.StartHeaderExcelAddress[0]}{keyCellLocations.StartHeaderExcelAddress[1] + 1}:"
                  + $"{keyCellLocations.StartHeaderExcelAddress[0]}{keyCellLocations.StartHeaderExcelAddress[1] + 30}";

            string endTimeCellAddressRange
                = $"{keyCellLocations.EndHeaderExcelAddress[0]}{keyCellLocations.EndHeaderExcelAddress[1] + 1}:"
                  + $"{keyCellLocations.EndHeaderExcelAddress[0]}{keyCellLocations.EndHeaderExcelAddress[1] + 30}";

            string lunchTimeCellAddressRange
                = $"{keyCellLocations.LunchBreakHeaderExcelAddress[0]}{keyCellLocations.LunchBreakHeaderExcelAddress[1] + 1}:"
                  + $"{keyCellLocations.LunchBreakHeaderExcelAddress[0]}{keyCellLocations.LunchBreakHeaderExcelAddress[1] + 30}";

            var allDates =
                excelDataReader.GetColumnDataAsync(new FileInfo(FilePaths.TimeLogFilePath), month, dateCellAddressRange);

            return null;
        }
    }
}
