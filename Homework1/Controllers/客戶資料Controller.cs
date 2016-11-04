using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Homework1.Models;

namespace Homework1.Controllers
{
    public class 客戶資料Controller : Controller
    {
        //private 客戶資料Entities db = new 客戶資料Entities();

        客戶資料Repository 客戶repo = RepositoryHelper.Get客戶資料Repository();
        vw_ClientInfoRepository ClientInforepo = RepositoryHelper.Getvw_ClientInfoRepository();

        // GET: 客戶資料
        public ActionResult Index(string clientName, string clientType)
        {
            //var data = db.客戶資料.Where(c => !c.是否已刪除);
            var data = 客戶repo.GetQueryData(clientName, clientType);
            //if (!string.IsNullOrEmpty(clientName)) {
            //    data = data.Where(c => c.客戶名稱.Contains(clientName));
            //}
            //if (!string.IsNullOrEmpty(clientType)) {
            //    data = data.Where(c => c.客戶分類 == clientType);
            //}

            //客戶分類(下拉選項)
            //string[] type = new string[] { "A", "B", "C", "D", "E"};
            //string[] type = 客戶repo.Get客戶分類(); 
            List<SelectListItem> type = 客戶repo.Get客戶分類List();
            ViewBag.clientType = type;

            return View(data);
        }

        public ActionResult ClientInfoList() {
            //var data = db.vw_ClientInfo;
            var data = ClientInforepo.All();
            return View(data);
        }

        // GET: 客戶資料/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //客戶資料 客戶資料 = db.客戶資料.Find(id);
            客戶資料 客戶資料 = 客戶repo.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Create
        public ActionResult Create()
        {
            ViewBag.clientType = 客戶repo.Get客戶分類List();

            return View();
        }

        // POST: 客戶資料/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email, 帳號, 密碼, 客戶分類")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                //db.客戶資料.Add(客戶資料);
                //db.SaveChanges();
                客戶repo.Add(客戶資料);
                客戶repo.UnitOfWork.Commit();

                return RedirectToAction("Index");
            }
            ViewBag.clientType = 客戶repo.Get客戶分類List();

            return View(客戶資料);
        }

        // GET: 客戶資料/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //客戶資料 客戶資料 = db.客戶資料.Find(id);
            客戶資料 客戶資料 = 客戶repo.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            ViewBag.clientType = 客戶repo.Get客戶分類List();
            return View(客戶資料);
        }

        // POST: 客戶資料/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email, 帳號, 密碼, 客戶分類")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(客戶資料).State = EntityState.Modified;
                //db.SaveChanges();
                var db = 客戶repo.UnitOfWork.Context;
                db.Entry(客戶資料).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.clientType = 客戶repo.Get客戶分類List();
            return View(客戶資料);
        }

        // GET: 客戶資料/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //客戶資料 客戶資料 = db.客戶資料.Find(id);
            客戶資料 客戶資料 = 客戶repo.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //客戶資料 客戶資料 = db.客戶資料.Find(id);
            客戶資料 客戶資料 = 客戶repo.Find(id);
            //db.客戶資料.Remove(客戶資料);
            //客戶資料.是否已刪除 = true;
            //db.SaveChanges();
            客戶repo.Delete(客戶資料);
            客戶repo.UnitOfWork.Commit();

            ViewBag.clientType = 客戶repo.Get客戶分類List();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
                客戶repo.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
