using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Utils
{
    public static class ExcelHelper
    {
        public static string ReadCellValue(ExcelWorksheet worksheet, int row, int column)
        {
            var result = worksheet.Cells[row, column].Value?.ToString();
            if (result == null) throw new Exception($"Field ({row},{column}) is requite");
            return result;
        }
        public static (int Rows, int Columns) GetMergedCellSize(ExcelWorksheet worksheet, int row, int column)
        {
            var cell = worksheet.Cells[row, column];
            if (!cell.Merge)
                return (1, 1);
            var mergedAddress = worksheet.MergedCells[row, column];
            if (string.IsNullOrEmpty(mergedAddress))
                return (1, 1);
            var range = new ExcelAddress(mergedAddress);
            int rows = range.End.Row - range.Start.Row + 1;
            int columns = range.End.Column - range.Start.Column + 1;
            return (rows, columns);
        }
        public static bool CheckedCellIsNull(ExcelWorksheet worksheet, int row, int column)
        {
            var result = worksheet.Cells[row, column].Value?.ToString();
            if (result == null) return true;
            return false;
        }
    }
}