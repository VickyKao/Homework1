using System;
using System.Linq;
using System.Collections.Generic;
	
namespace Homework1.Models
{   
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
	{
        public override IQueryable<客戶銀行資訊> All() {
            return base.All().Where(b => !b.是否已刪除 && !b.客戶資料.是否已刪除);
        }

        public IQueryable<客戶銀行資訊> GetQueryData(string bankName) {
            var data = this.All();
            if (!string.IsNullOrEmpty(bankName)) {
                data = data.Where(b => b.銀行名稱.Contains(bankName));
            }
            return data;
        }

        public 客戶銀行資訊 Find(int? id) {
            return this.All().FirstOrDefault(b => b.Id == id);
        }

        public override void Delete(客戶銀行資訊 entity) {
            entity.是否已刪除 = true;
        }
    }

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{

	}
}