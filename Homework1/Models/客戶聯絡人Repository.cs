using System;
using System.Linq;
using System.Collections.Generic;
	
namespace Homework1.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{
        public override IQueryable<客戶聯絡人> All() {

            return base.All().Where(cc => !cc.是否已刪除 && !cc.客戶資料.是否已刪除);
        }

        public IQueryable<客戶聯絡人> GetQueryData(string contactName, string contactTitle) {
            var data = this.All();
            if (!string.IsNullOrEmpty(contactName)) {
                data = data.Where(c => c.姓名.Contains(contactName));
            }
            if (!string.IsNullOrEmpty(contactTitle)) {
                data = data.Where(c => c.職稱.Contains(contactTitle));
            }
            return data;
        }

        public 客戶聯絡人 Find(int? id) {
            return this.All().FirstOrDefault(c => c.Id == id);
        }

        public override void Delete(客戶聯絡人 entity) {
            entity.是否已刪除 = true;
        }
    }

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}