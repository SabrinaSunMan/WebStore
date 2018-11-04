using PagedList;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Models.ViewModel
{
    /// <summary>
    /// 產品ViewModel 包括 [表頭] 以及 [PageList]
    /// </summary>
    public class ProductViewModel
    {
        public ProductListHeaderViewModel Header { get; set; }

        public IPagedList<ProductListContentViewModel> Content_List { get; set; }

        public int page { get; set; }
    }

    /// <summary>
    /// 搜尋 [Product] 條件式
    /// </summary>
    public class ProductListHeaderViewModel
    {
        /// <summary>
        /// 商品名稱.
        /// </summary>
        [DisplayName("商品名稱")]
        [MinLength(3, ErrorMessage = "最少不得輸入少於 {1}")]
        public string ProductName { get; set; }

        /// <summary>
        /// 商品數量.
        /// </summary>
        [DisplayName("商品數量")]
        [Range(0, int.MaxValue, ErrorMessage = "請輸入有效的數字")]
        public int Qty { get; set; }

        /// <summary>
        /// 建立日期.
        /// </summary>
        [DisplayName("建立日期")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public string CreateTime { get; set; }
    }

    /// <summary>
    /// 呈現 [Product] 搜尋結果
    /// </summary>
    public class ProductListContentViewModel
    {
        /// <summary>
        /// ID.
        /// </summary>
        [DisplayName("ID")]
        public string ProductID { get; set; }

        /// <summary>
        /// 商品名稱.
        /// </summary>
        [DisplayName("商品名稱")]
        public string ProductName { get; set; }

        /// <summary>
        /// 狀態. False = 刪除,True = 啟用中
        /// </summary>
        public string _ProducStatus;

        [DisplayName("上架狀態")]
        public string Status //{ get; set; }
        {
            get { return Convert.ToBoolean(_ProducStatus) == true ? "上架中" : "下架"; }
            set { _ProducStatus = value; }
        }

        /// <summary>
        /// 建立日期.
        /// </summary>
        private DateTime _createTime;

        public string CreateTime
        {
            get { return _createTime.ToString("yyyy/MM/dd"); }
            set { DateTime.TryParse(value, out _createTime); }
        }
    }

    /// <summary>
    /// 呈現 [Product] 檢視頁面 時
    /// </summary>
    public class ProductDetailViewModel
    {
        /// <summary>
        /// ID.
        /// </summary>
        [DisplayName("ID")]
        public string ProductID { get; set; }

        /// <summary>
        /// 商品名稱.
        /// </summary>
        [Required]
        [DisplayName("商品名稱")]
        [MinLength(3, ErrorMessage = "最少不得輸入少於 {1}")]
        [StringLength(10, ErrorMessage = (" 不得超過長度 {1}"))]
        public string ProductName { get; set; }

        /// <summary>
        /// 狀態. False = 刪除,True = 啟用中
        /// </summary>
        [Required]
        [DisplayName("上架狀態")]
        public bool Status { get; set; }

        [DisplayName("使用者建立時間")]
        public DateTime CreateTime { get; set; }

        [DisplayName("建立者")]
        public string CreateUser { get; set; }

        [DisplayName("更新時間")]
        public DateTime UpdateTime { get; set; }

        [DisplayName("更新者")]
        public string UpdateUser { get; set; }

        /// <summary>
        /// 排序.
        /// </summary>
        public int sort { get; set; }
    }
}