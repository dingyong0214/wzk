using NPOI.HSSF.UserModel;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;

namespace WZK.Common
{
    public class ExcelHelper
    {
        #region 从DataTable导出到Excel
        /// <summary>
        /// 从DataTable导出到Excel
        /// </summary>
        /// <param name="dtSource">源数据.</param>
        /// <param name="head">表头</param>
        /// <returns>是否成功</returns>
        /// <remarks>add by dingyong,2016-10-27 10:20:09 </remarks>
        public static bool ExportExcel(DataTable dtSource, string[] head)
        {
            if (dtSource != null)
            {
                var workbook = new HSSFWorkbook();
                var sheet = workbook.CreateSheet("Sheet 1");
                //添加表头
                var excelHead = sheet.CreateRow(0);
                for (int i = 0; i < head.Length; i++)
                {
                    excelHead.CreateCell(i).SetCellValue(head[i]);
                }

                for (int i = 1; i <= dtSource.Rows.Count; i++)
                {
                    var row = sheet.CreateRow(i);
                    for (int j = 0; j < dtSource.Columns.Count; j++)
                    {
                        row.CreateCell(j).SetCellValue(dtSource.Rows[i - 1][j].ToString());
                    }
                }
                using (MemoryStream ms = new MemoryStream())
                {
                    workbook.Write(ms);
                    long fileSize = ms.Length;
                    // 设置编码和附件格式   

                    HttpContext curContext = HttpContext.Current;
                    curContext.Response.Clear();

                    curContext.Response.ContentType = "application/vnd.ms-excel";
                    curContext.Response.ContentEncoding = Encoding.Default;
                    curContext.Response.Charset = "";
                    HttpContext.Current.Response.AddHeader("Content-Length", fileSize.ToString());

                    curContext.Response.AppendHeader("Content-Disposition", "attachment;filename=" + DateTime.Now.ToShortDateString() + ".xls");
                    HttpContext.Current.Response.BinaryWrite(ms.GetBuffer());
                    curContext.Response.End();
                }
            }
            return true;
        }
        #endregion
    }
}
