namespace Homework1.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(客戶聯絡人MetaData))]
    public partial class 客戶聯絡人 : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {
            var db = new 客戶資料Entities();

            if (this.Id == 0) {
                //Create
                if (db.客戶聯絡人.Where(ct => ct.客戶Id == this.客戶Id && ct.Email == this.Email).Any()) {
                    yield return new ValidationResult(
                        "同一個客戶下的聯絡人，其 Email 不能重複。", new string[] { "Email" });
                }
            }
            else {
                //Update
                if (db.客戶聯絡人.Where(ct => ct.Id != this.Id && ct.客戶Id == this.客戶Id && ct.Email == this.Email).Any()) {
                    yield return new ValidationResult(
                        "同一個客戶下的聯絡人，其 Email 不能重複。", new string[] { "Email" });
                }
            }

            yield return ValidationResult.Success;  //回傳驗證成功
            //throw new NotImplementedException();
        }
    }
    
    public partial class 客戶聯絡人MetaData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int 客戶Id { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 職稱 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 姓名 { get; set; }
        
        [StringLength(250, ErrorMessage="欄位長度不得大於 250 個字元")]
        [Required]
        [EmailAddress(ErrorMessage="Email格式錯誤")]
        public string Email { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [RegularExpression(@"\d{4}-\d{6}",ErrorMessage="手機號碼格式應為 09xx-xxxxxx")]
        [Required]
        public string 手機 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string 電話 { get; set; }
        [Required]
        public bool 是否已刪除 { get; set; }
    
        public virtual 客戶資料 客戶資料 { get; set; }
    }
}
