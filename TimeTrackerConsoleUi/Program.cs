using CoreLibrary;
using CoreLibrary.DataAccess.Excel;
using CoreLibrary.DataAccess.Excel.Private;
using CoreLibrary.Models;
using CoreLibrary.TimeCalculations;
using OfficeOpenXml;
using System;
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
            var timeCalculator = new TimeCalculator();

            var timeLogModel = new TimeLogModel
            {
                Date = DateTime.Now,
                StartTime = new TimeSpan(8, 0, 0),
                EndTime = new TimeSpan(17, 0, 0),
                LunchInMinutes = 30
            };

            //Console.WriteLine(timeLogModel.IsDayOfWeekend);

            var data = await excelTimeLogRepository.GetTimeLogDataForMonthAsync((Month)DateTime.Now.Month);

            TimeSpan currentBalanceForMonth = timeCalculator.GetTimeBalanceForMonth(data);

            Console.WriteLine(currentBalanceForMonth);
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
