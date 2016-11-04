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

        public IQueryable<客戶資料> GetQueryData(string clientName, string clientType) {
            var data = this.All();

            if (!string.IsNullOrEmpty(clientName)) {
                data = data.Where(c => c.客戶名稱.Contains(clientName));
            }
            if (!string.IsNullOrEmpty(clientType)) {
                data = data.Where(c => c.客戶分類 == clientType);
            }
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