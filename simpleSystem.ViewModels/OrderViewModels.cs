using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpleSystem.ViewModels
{
    public class Order
    {
        /// <summary>
        /// 訂單編號
        /// </summary>

        [DisplayName("訂單編號")]
        public int OrderDd { get; set; }

        /// <summary>
        /// 客戶代號
        /// </summary>
        [DisplayName("客戶代號")]
        public int CustId { get; set; }

        /// <summary>
        /// 客戶名稱
        /// </summary>
        [DisplayName("客戶名稱")]
        public string CustName { get; set; }

        /// <summary>
        /// 業務(員工)代號
        /// </summary>
        [DisplayName("業務(員工)代號")]
        public int EmpId { get; set; }

        /// <summary>
        /// 業務(員工姓名)
        /// </summary>
        [DisplayName("業務(員工姓名)")]
        public string EmpName { get; set; }

        /// <summary>
        /// 訂單日期
        /// </summary>
        [DisplayName("訂單日期")]
        public DateTime? OrderDate { get; set; }

        /// <summary>
        /// 訂單日期fromat
        /// </summary>
        public String OrderdateStr { get; set; }

        /// <summary>
        /// 需要日期
        /// </summary>
        [DisplayName("需要日期")]
        public DateTime? RequireDdate { get; set; }

        /// <summary>
        /// 出貨日期
        /// </summary> 
        [DisplayName("出貨日期")]
        public DateTime? ShippedDate { get; set; }

        /// <summary>
        /// 出貨日期fromat
        /// </summary>
        public String ShippedDateStr { get; set; }

        /// <summary>
        /// 出貨公司代號
        /// </summary>
        [DisplayName("出貨公司代號")]
        public int ShipperId { get; set; }

        /// <summary>
        /// 出貨公司名稱
        /// </summary>
        [DisplayName("出貨公司名稱")]
        public String ShipperName { get; set; }

        /// <summary>
        /// 運費
        /// </summary>
        /// 
        [DisplayName("運費")]
        public double Freight { get; set; }

        /// <summary>
        /// 出貨說明
        /// </summary>
        public string ShipName { get; set; }

        /// <summary>
        /// 出貨地址
        /// </summary>
        public string ShipAddress { get; set; }

        /// <summary>
        /// 出貨程式
        /// </summary>
        public string ShipCity { get; set; }

        /// <summary>
        /// 出貨地區
        /// </summary>
        public string ShipRegion { get; set; }

        /// <summary>
        /// 郵遞區號
        /// </summary>
        public string ShipPostalCode { get; set; }

        /// <summary>
        /// 出貨國家
        /// </summary>
        public string ShipCountry { get; set; }

        /// <summary>
        /// 第幾頁資料
        /// </summary>
        public int Page { get; set; }
    }
}
