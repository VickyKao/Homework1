using System;
using System.Linq;
using System.Collections.Generic;
using Homework1.Models.ViewModels;
	
namespace Homework1.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{
        public override IQueryable<客戶聯絡人> All() {

            return base.All().Where(cc => !cc.是否已刪除 && !cc.客戶資料.是否已刪除);
        }

        public IQueryable<客戶聯絡人> GetQueryData(string contactName, string contactTitle, string sort) {
            var data = this.All();
            if (!string.IsNullOrEmpty(contactName)) {
                data = data.Where(c => c.姓名.Contains(contactName));
            }
            if (!string.IsNullOrEmpty(contactTitle)) {
                data = data.Where(c => c.職稱.Contains(contactTitle));
            }

            #region 排序處理
            switch (sort) {
                case "姓名_desc":
                    data = data.OrderByDescending(c => c.姓名);
                    break;
                case "職稱_desc":
                    data = data.OrderByDescending(c => c.職稱);
                    break;
                case "職稱":
                    data = data.OrderBy(c => c.職稱);
                    break;
                case "Email_desc":
                    data = data.OrderByDescending(c => c.Email);
                    break;
                case "Email":
                    data = data.OrderBy(c => c.Email);
                    break;
                case "手機_desc":
                    data = data.OrderByDescending(c => c.手機);
                    break;
                case "手機":
                    data = data.OrderBy(c => c.手機);
                    break;
                case "電話_desc":
                    data = data.OrderByDescending(c => c.電話);
                    break;
                case "電話":
                    data = data.OrderBy(c => c.電話);
                    break;
                case "客戶名稱_desc":
                    data = data.OrderByDescending(c => c.客戶資料.客戶名稱);
                    break;
                case "客戶名稱":
                    data = data.OrderBy(c => c.客戶資料.客戶名稱);
                    break;
                default:
                    data = data.OrderBy(c => c.姓名);
                    break;
            }
            #endregion

            return data;
        }

        public 客戶聯絡人 Find(int? id) {
            return this.All().FirstOrDefault(c => c.Id == id.Value);
        }

        public override void Delete(客戶聯絡人 entity) {
            entity.是否已刪除 = true;
        }

        //public List<客聯ViewModel> GetContactBatchList(int clientId) {
        //    var contactData = this.Where(c => c.客戶Id == clientId);
        //    List<客聯ViewModel> data = new List<客聯ViewModel>();
        //    foreach (var item in contactData) {
        //        data.Add(
        //            new 客聯ViewModel { 
        //                Id = item.Id, 
        //                職稱=item.職稱, 
        //                姓名=item.姓名, 
        //                Email= item.Email, 
        //                電話=item.電話, 
        //                手機= item.手機
        //            });
        //    }
        //    return data;
        //}
        
        public IQueryable<客戶聯絡人> GetContactBatchList(int clientId) {
            var data = this.Where(c => c.客戶Id == clientId && !c.是否已刪除);
            return data;
        }

    }

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}