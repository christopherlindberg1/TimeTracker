using CoreLibrary.DataAccess.Excel.Models;
using OfficeOpenXml;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CoreLibrary.DataAccess.Excel.Private
{
    /// <summary>
    /// Class responsible for reading data from an Excel file.
    /// </summary>
    public class ExcelDataReader
    {
        private const int LastRowForSearch = 10;
        private const int LastColForSearch = 10;

        public async Task<KeyCellLocations> GetKeyCellLocations(FileInfo fileInfo, int sheetIndex)
        {
            try
            {
                var keyCellLocations = new KeyCellLocations();

                using var package = new ExcelPackage();

                await package.LoadAsync(fileInfo);

                var workSheet = package.Workbook.Worksheets[sheetIndex];

                for (int currentRow = 1; currentRow < LastRowForSearch; currentRow++)
                {
                    for (int currentCol = 1; currentCol < LastColForSearch; currentCol++)
                    {
                        if (workSheet.Cells[currentRow, currentCol].Text == "Datum")
                            keyCellLocations.DateHeaderExcelAddress = workSheet.Cells[currentRow, currentCol].Address;

                        else if (workSheet.Cells[currentRow, currentCol].Text == "Start")
                            keyCellLocations.StartHeaderExcelAddress = workSheet.Cells[currentRow, currentCol].Address;

                        else if (workSheet.Cells[currentRow, currentCol].Text == "Slut")
                            keyCellLocations.EndHeaderExcelAddress = workSheet.Cells[currentRow, currentCol].Address;

                        else if (workSheet.Cells[currentRow, currentCol].Text == "Lunchtid")
                            keyCellLocations.LunchBreakHeaderExcelAddress = workSheet.Cells[currentRow, currentCol].Address;
                    }

                    if (!string.IsNullOrWhiteSpace(keyCellLocations.DateHeaderExcelAddress)
                        && !string.IsNullOrWhiteSpace(keyCellLocations.StartHeaderExcelAddress)
                        && !string.IsNullOrWhiteSpace(keyCellLocations.EndHeaderExcelAddress)
                        && !string.IsNullOrWhiteSpace(keyCellLocations.LunchBreakHeaderExcelAddress))
                    {
                        break;
                    }
                }

                return keyCellLocations;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string[,]> GetExcelDataAsync(FileInfo fileInfo, int sheetIndex, string address)
        {
            using var package = new ExcelPackage();

            await package.LoadAsync(fileInfo);

            var workSheet = package.Workbook.Worksheets[sheetIndex];


            //for (int currentRow = fromRow; currentRow < toRow; currentRow++)
            //{
            //    for (int currentCol = fromCol; currentCol < toCol; currentCol++)
            //    {
            //        dataToReturn[currentRow - fromRow, currentCol - fromCol] = workSheet.Cells[currentRow, currentCol].Text;
            //    }
            //}

            return null;
        }

        private AddressType GetAddressType(string address)
        {
            return AddressType.InvalidAddress;
            // FIX WITH REGEX

            if (String.IsNullOrWhiteSpace(address)
                || address.Length < 2)
            {
                return AddressType.InvalidAddress;
            }

            if (address.Length > 2)
                // VALIDATE
                return AddressType.MultipleCells;

            int placeholder;

            Regex.IsMatch(address, @"[a-zA-Z]\d");

        }

        /// <summary>
        /// Returns a string with the value from a single cell.
        /// </summary>
        public async Task<string> GetExcelDataAsync(
            FileInfo fileInfo, int sheetIndex, int row, int col)
        {
            try
            {
                using var package = new ExcelPackage();
                
                await package.LoadAsync(fileInfo);

                var workSheet = package.Workbook.Worksheets[sheetIndex];

                return workSheet.Cells[Row: row, Col: col].Text;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Returns a 2D array with the values from a range of cells.
        /// </summary>
        public async Task<string[,]> GetExcelDataRangeAsync(
            FileInfo fileInfo, int sheetIndex, int fromRow, int fromCol, int toRow, int toCol)
        {
            try
            {
                string[,] dataToReturn = new string[toRow - fromRow, toCol - fromCol];

                using var package = new ExcelPackage();
                
                await package.LoadAsync(fileInfo);

                var workSheet = package.Workbook.Worksheets[sheetIndex];

                for (int currentRow = fromRow; currentRow < toRow; currentRow++)
                {
                    for (int currentCol = fromCol; currentCol < toCol; currentCol++)
                    {
                        dataToReturn[currentRow - fromRow, currentCol - fromCol] = workSheet.Cells[currentRow, currentCol].Text;
                    }
                }

                return dataToReturn;
            }
            catch (Exception)
            {
                throw;
            }
        }

        
    }
}
