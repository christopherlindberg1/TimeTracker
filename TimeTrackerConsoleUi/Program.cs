using CoreLibrary;
using CoreLibrary.DataAccess.Excel;
using CoreLibrary.DataAccess.Excel.Private;
using OfficeOpenXml;
using System.IO;
using System.Threading.Tasks;

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
