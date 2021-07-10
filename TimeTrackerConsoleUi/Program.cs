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

            var excelReader = new ExcelDataReader();
            var excelTimeLogRepository = new ExcelTimeLogRepository(excelReader);

            var a = await excelReader.GetKeyCellLocations(new FileInfo(FilePaths.TimeLogFilePath), 0);
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
