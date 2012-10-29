using System.Data;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace HiLand.Utility.Office
{
    /// <summary>
    /// Excel操作辅助类
    /// </summary>
    public static class ExcelHelper
    {
        /// <summary>
        /// 根据数据写入Excel文件(本方法返回的是Excel的流数据，请调用相应方法进行文件保存)
        /// </summary>
        /// <param name="sourceTable">源数据</param>
        /// <returns></returns>
        public static Stream WriteExcel(DataTable sourceTable)
        {
            return WriteExcel(sourceTable, true);
        }

        /// <summary>
        /// 根据数据写入Excel文件(本方法返回的是Excel的流数据，请调用相应方法进行文件保存)
        /// </summary>
        /// <param name="sourceTable">源数据</param>
        /// <param name="isDisplayHeader">是否显示表头</param>
        /// <returns></returns>
        public static Stream WriteExcel(DataTable sourceTable, bool isDisplayHeader)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            MemoryStream ms = new MemoryStream();
            ISheet sheet = workbook.CreateSheet();

            int rowIndex = 0;

            //处理表头部分
            if (isDisplayHeader == true)
            {
                IRow headerRow = sheet.CreateRow(rowIndex);
                foreach (DataColumn column in sourceTable.Columns)
                {
                    headerRow.CreateCell(column.Ordinal).SetCellValue(column.Caption);
                }
                headerRow = null;
                rowIndex++;
            }

            //处理数据部分
            foreach (DataRow row in sourceTable.Rows)
            {
                IRow dataRow = sheet.CreateRow(rowIndex);
                foreach (DataColumn column in sourceTable.Columns)
                {
                    dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                }
                rowIndex++;
            }

            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;
            sheet = null;
            workbook = null;

            return ms;
        }

        /// <summary>
        /// 读取Excel文件(第一个shell)
        /// </summary>
        /// <param name="excelFileStream">Excel文件流</param>
        /// <param name="headerRowNumber">表头行的索引号(从1开始计数)</param>
        /// <returns></returns>
        public static DataTable ReadExcel(Stream excelFileStream, int headerRowNumber)
        {
            return ReadExcel(excelFileStream, 1, headerRowNumber);
        }

        /// <summary>
        /// 读取Excel文件
        /// </summary>
        /// <param name="excelFileStream">Excel文件流</param>
        /// <param name="sheetNumber">工作表的索引号(从1开始计数)</param>
        /// <param name="headerRowNumber">表头行的索引号(从1开始计数)</param>
        /// <returns></returns>
        public static DataTable ReadExcel(Stream excelFileStream, int sheetNumber, int headerRowNumber)
        {
            sheetNumber -= 1;
            headerRowNumber -= 1;

            if (sheetNumber < 0)
            {
                sheetNumber = 0;
            }

            if (headerRowNumber < 0)
            {
                headerRowNumber = 0;
            }

            HSSFWorkbook workbook = new HSSFWorkbook(excelFileStream);
            ISheet sheet = workbook.GetSheetAt(sheetNumber);

            DataTable table = GetDataTable(sheet, headerRowNumber);

            //excelFileStream.Close();
            workbook = null;
            sheet = null;
            return table;
        }

        /// <summary>
        /// 读取Excel文件（流）
        /// </summary>
        /// <param name="excelFileStream">Excel文件流</param>
        /// <param name="sheetName">工作表的名称</param>
        /// <param name="headerRowNumber">表头行的索引号(从1开始计数)</param>
        /// <returns></returns>
        public static DataTable ReadExcel(Stream excelFileStream, string sheetName, int headerRowNumber)
        {
            headerRowNumber -= 1;

            if (headerRowNumber < 0)
            {
                headerRowNumber = 0;
            }

            HSSFWorkbook workbook = new HSSFWorkbook(excelFileStream);
            ISheet sheet = workbook.GetSheet(sheetName);

            DataTable table = GetDataTable(sheet, headerRowNumber);

            //excelFileStream.Close();
            workbook = null;
            sheet = null;
            return table;
        }

        /// <summary>
        /// 从工作表获取DataTable
        /// </summary>
        /// <param name="sheet">工作表</param>
        /// <param name="headerRowIndex">表头行的索引号(从0开始计数)</param>
        /// <returns></returns>
        private static DataTable GetDataTable(ISheet sheet, int headerRowIndex)
        {
            DataTable table = new DataTable();

            int cellCount = 0;

            if (sheet != null)
            {
                //1.抽取表头部分，成为DataTable的列名称
                IRow headerRow = sheet.GetRow(headerRowIndex);
                if (headerRow != null)
                {
                    cellCount = headerRow.LastCellNum;

                    for (int i = headerRow.FirstCellNum; i < cellCount; i++)
                    {
                        DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                        table.Columns.Add(column);
                    }
                }

                //2.抽取表体数据部分，成为DataTable的内容
                int rowCount = sheet.LastRowNum;

                for (int i = (headerRowIndex + 1); i <= rowCount; i++)
                {
                    IRow row = sheet.GetRow(i);
                    if (row == null)
                    {
                        continue;
                    }

                    DataRow dataRow = table.NewRow();

                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                        {
                            dataRow[j] = row.GetCell(j).ToString();
                        }
                    }

                    table.Rows.Add(dataRow);
                }
            }

            return table;
        }
    }
}
