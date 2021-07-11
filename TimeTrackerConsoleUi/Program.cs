using CoreLibrary;
using CoreLibrary.DataAccess.Excel;
using CoreLibrary.DataAccess.Excel.Private;
using System.IO;
using System.Threading.Tasks;
using OfficeOpenXml;

namespace TimeTrackerConsoleUi
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ConfigureApplication();

            var excelDataReader = new ExcelDataReader();
            var excelTimeLogRepository = new ExcelTimeLogRepository(excelDataReader);

            var fileInfo = new FileInfo(FilePaths.TimeLogFilePath);

            var data = await excelTimeLogRepository.GetTimeLogDataForMonthAsync(Month.July);

            //var data = await excelDataReader.GetColumnDataAsync(fileInfo, Month.July, "B3:B33");
            //var d = await excelTimeLogRepository.GetTimeLogDataForMonthAsync(Month.July);


            //var a = await excelReader.GetKeyCellLocations(new FileInfo(FilePaths.TimeLogFilePath), 0);
        }

        private static void WriteToConsole()
        {
            
        }

        private static void ConfigureApplication()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }
    }
}
