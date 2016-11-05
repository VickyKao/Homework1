using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using System.IO;
using System.Data;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
	
namespace Homework1.Models
{   
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{
        public override IQueryable<客戶資料> All() {
            return base.All().Where(c => !c.是否已刪除);
        }

        public IQueryable<客戶資料> GetQueryData(string clientName, string clientType, string sort) {
            var data = this.All();

            if (!string.IsNullOrEmpty(clientName)) {
                data = data.Where(c => c.客戶名稱.Contains(clientName));
            }
            if (!string.IsNullOrEmpty(clientType)) {
                data = data.Where(c => c.客戶分類 == clientType);
            }

            #region 排序處理
            switch (sort) {
                case "客戶名稱_desc":
                    data = data.OrderByDescending(c => c.客戶名稱);
                    break;
                case "統一編號_desc":
                    data = data.OrderByDescending(c => c.統一編號);
                    break;
                case "統一編號":
                    data = data.OrderBy(c => c.統一編號);
                    break;
                case "電話_desc":
                    data = data.OrderByDescending(c => c.電話);
                    break;
                case "電話":
                    data = data.OrderBy(c => c.電話);
                    break;
                case "傳真_desc":
                    data = data.OrderByDescending(c => c.傳真);
                    break;
                case "傳真":
                    data = data.OrderBy(c => c.傳真);
                    break;
                case "地址_desc":
                    data = data.OrderByDescending(c => c.地址);
                    break;
                case "地址":
                    data = data.OrderBy(c => c.地址);
                    break;
                case "Email_desc":
                    data = data.OrderByDescending(c => c.Email);
                    break;
                case "Email":
                    data = data.OrderBy(c => c.Email);
                    break;
                case "客戶分類_desc":
                    data = data.OrderByDescending(c => c.客戶分類);
                    break;
                case "客戶分類":
                    data = data.OrderBy(c => c.客戶分類);
                    break;
                default:
                    data = data.OrderBy(c => c.客戶名稱);
                    break;
            }
            #endregion

            return data;
        }

        public List<SelectListItem> Get客戶分類List() {
            string[] clientOption = new string[] { "A", "B", "C", "D", "E" };
            List<SelectListItem> clientType = new List<SelectListItem>();
            foreach (var item in clientOption) {
                clientType.Add(new SelectListItem() {
                    Text = item + "分類",
                    Value = item
                });
            }
            return clientType;
        }

        public 客戶資料 Find(int? id) {
            return this.All().FirstOrDefault(c=>c.Id == id);
        }

        public override void Delete(客戶資料 entity) {
            entity.是否已刪除 = true;
            //base.Delete(entity);
        }

        public MemoryStream ExportExcel(List<客戶資料> exportData, HSSFWorkbook book) {
            DataTable dt = new DataTable();
            dt.Columns.Add("客戶名稱", typeof(string));
            dt.Columns.Add("統一編號", typeof(string));
            dt.Columns.Add("電話", typeof(string));
            dt.Columns.Add("傳真", typeof(string));
            dt.Columns.Add("地址", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("客戶分類", typeof(string));
            for (int i = 0; i < exportData.Count; i++) {
                dt.Rows.Add(exportData[i].客戶名稱, exportData[i].統一編號,
                    exportData[i].電話, exportData[i].傳真,
                    exportData[i].地址, exportData[i].Email,
                    exportData[i].客戶分類);
            }
            HSSFSheet sheet = (HSSFSheet)book.CreateSheet();
            HSSFCellStyle headCellStyle = (HSSFCellStyle)book.CreateCellStyle();
            HSSFCellStyle dataCellStyle = (HSSFCellStyle)book.CreateCellStyle();

            HSSFFont font = (HSSFFont)book.CreateFont();
            font.FontHeightInPoints = 12;
            font.FontName = "微軟正黑體";
            font.Color = HSSFColor.Blue.Index;  //字的顏色
            headCellStyle.SetFont(font);

            HSSFFont dataFont = (HSSFFont)book.CreateFont();
            font.FontName = "微軟正黑體";
            dataCellStyle.SetFont(dataFont);

            var hRow = sheet.CreateRow(0);
            //表頭
            for (int h = 0; h < dt.Columns.Count; h++) {
                HSSFRow r = (HSSFRow)sheet.GetRow(0);
                r.CreateCell(h).SetCellValue(dt.Columns[h].ColumnName.ToString());
                r.GetCell(h).CellStyle = headCellStyle;
            }
            //表身
            for (int j = 0; j < dt.Rows.Count; j++) {
                var dRow = sheet.CreateRow(j + 1);
                HSSFRow dr = (HSSFRow)sheet.GetRow(j + 1);
                for (int k = 0; k < dt.Columns.Count; k++) {
                    dr.CreateCell(k).SetCellValue(dt.Rows[j][k].ToString());
                }
            }
            MemoryStream output = new MemoryStream();
            book.Write(output);
            return output;
        }
    }

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}