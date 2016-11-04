using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
	
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
    }

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}