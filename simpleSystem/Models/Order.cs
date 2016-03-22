using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Models
{
    public class Order
    {
        /// <summary>
        /// 訂單編號
        /// </summary>
        
        [DisplayName("訂單編號")]
        [Required()]
        public int OrderDd {get;set;}

        /// <summary>
        /// 客戶代號
        /// </summary>
        [MaxLength(3)]
        [DisplayName("客戶代號")]
        public string CustId {get;set;}

        /// <summary>
        /// 客戶名稱
        /// </summary>
        public string CustName { get; set; }

        /// <summary>
        /// 業務(員工)代號
        /// </summary>
        public int EmpId {get;set;}

        /// <summary>
        /// 業務(員工姓名)
        /// </summary>
        public string EmpName { get; set; }

        /// <summary>
        /// 訂單日期
        /// </summary>
        public DateTime ? Orderdate {get;set;}

        /// <summary>
        /// 需要日期
        /// </summary>
        public DateTime? RequireDdate { get; set; }

        /// <summary>
        /// 出貨日期
        /// </summary>
        public DateTime?  ShippedDate { get; set; }

        /// <summary>
        /// 出貨公司代號
        /// </summary>
        public int ShipperId {get;set;}

        /// <summary>
        /// 出貨公司名稱
        /// </summary>
        public int ShipperName { get; set; }

        /// <summary>
        /// 運費
        /// </summary>
        /// 
        [Range(0,double.MaxValue)]
        [DisplayName("運費")]
        public double Freight {get;set;}

        /// <summary>
        /// 出貨說明
        /// </summary>
        public string ShipName {get;set;}

        /// <summary>
        /// 出貨地址
        /// </summary>
        public string ShipAddress {get;set;}

        /// <summary>
        /// 出貨程式
        /// </summary>
        public string ShipCity {get;set;}

        /// <summary>
        /// 出貨地區
        /// </summary>
        public string ShipRegion {get;set;}

        /// <summary>
        /// 郵遞區號
        /// </summary>
        public string ShipPostalCode {get;set;}

        /// <summary>
        /// 出貨國家
        /// </summary>
        public string ShipCountry { get; set; }
    }
}
