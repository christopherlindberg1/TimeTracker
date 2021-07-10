using CoreLibrary.DataAccess.Excel.Models;
using CoreLibrary.DataAccess.Excel.Private;
using CoreLibrary.Models;
using System.Collections.Generic;

namespace CoreLibrary.DataAccess.Excel
{
    /// <summary>
    /// Class used to provide access to time log data that is read from an Excel file.
    /// </summary>
    public class ExcelTimeLogRepository : ITimeLogRepository
    {
        private readonly ExcelDataReader excelDataReader;
        private KeyCellLocations keyCellLocations;

        public ExcelTimeLogRepository(ExcelDataReader excelDataReader)
        {
            this.excelDataReader = excelDataReader;
            keyCellLocations = new KeyCellLocations();
        }

        public IEnumerable<MonthTimeLogModel> GetTimeLogDataForMonth(Month month)
        {

            throw new System.NotImplementedException();
        }

        


    }
}
