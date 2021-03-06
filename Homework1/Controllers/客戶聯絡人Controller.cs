﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Homework1.Models;
using Homework1.Models.ViewModels;

namespace Homework1.Controllers
{
    public class 客戶聯絡人Controller : Controller
    {
        客戶資料Repository 客戶repo = RepositoryHelper.Get客戶資料Repository();
        客戶聯絡人Repository 客聯repo = RepositoryHelper.Get客戶聯絡人Repository();

        // GET: 客戶聯絡人
        public ActionResult Index(string contactName, string contactTitle, string sortOrder)
        {
            ViewBag.sort姓名 = string.IsNullOrEmpty(sortOrder) ? "姓名_desc" : "";
            ViewBag.sort職稱 = sortOrder == "職稱" ? "職稱_desc" : "職稱";
            ViewBag.sortEmail = sortOrder == "Email" ? "Email_desc" : "Email";
            ViewBag.sort手機 = sortOrder == "手機" ? "手機_desc" : "手機";
            ViewBag.sort電話 = sortOrder == "電話" ? "電話_desc" : "電話";
            ViewBag.sort客戶名稱 = sortOrder == "客戶名稱" ? "客戶名稱_desc" : "客戶名稱";

            var 客戶聯絡人 = 客聯repo.GetQueryData(contactName, contactTitle, sortOrder);
            return View(客戶聯絡人);
        }

        public ActionResult ContactList(int clientId) {
            return View(客聯repo.GetContactBatchList(clientId));
        }

        [HttpPost]
        public ActionResult ContactBatchUpdate(客聯ViewModel[] items, int? clientId) {
            if (ModelState.IsValid) {
                if (items != null && items.Count() > 0) {
                    foreach (var contact in items) {
                        var 客戶聯絡人 = 客聯repo.Find(contact.Id);
                        客戶聯絡人.職稱 = contact.職稱;
                        客戶聯絡人.手機 = contact.手機;
                        客戶聯絡人.電話 = contact.電話;
                    }
                    客聯repo.UnitOfWork.Commit();
                }
            }

            return RedirectToAction("Index", "客戶資料");
        }

        // GET: 客戶聯絡人/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = 客聯repo.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(客戶repo.All()  , "Id", "客戶名稱");
            return View();
        }

        // POST: 客戶聯絡人/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                客聯repo.Add(客戶聯絡人);
                客聯repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(客戶repo.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = 客聯repo.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(客戶repo.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                var db = 客聯repo.UnitOfWork.Context;
                db.Entry(客戶聯絡人).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(客戶repo.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = 客聯repo.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶聯絡人 客戶聯絡人 = 客聯repo.Find(id);
            客聯repo.Delete(客戶聯絡人);
            客聯repo.UnitOfWork.Commit();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                客聯repo.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
