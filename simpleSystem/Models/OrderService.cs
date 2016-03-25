using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using simpleSystem.Models;

namespace Models
{
    public class OrderService
    {
        /// <summary>
        /// 新增訂單
        /// </summary>
        /// <param name="order"></param>
        public void InsertOrder(simpleSystem.Models.Order order)
        {
            //todo
        }
        /// <summary>
        /// 依照Id 取得訂單資料
        /// </summary>
        /// <returns></returns>
        public simpleSystem.Models.Order GetOrderById(string orderId)
        {
            //todo
            return new Order();
        }
        /// <summary>
        /// 依照條件取得訂單資料
        /// </summary>
        /// <returns></returns>
        public List<simpleSystem.ViewModels.Order> GetOrderByCondtioin(simpleSystem.ViewModels.Order order)
        {
            //todo
            List<simpleSystem.ViewModels.Order> result = new List<simpleSystem.ViewModels.Order>();
            //依照給的order資訊下條件

            SimpleDB db = new SimpleDB();
            var a = db.Orders.Select(x => x.OrderID).ToList();
            
            /*
             result = db.Orders.Select(x => new Order {
                                                         CustId = x.CustId,
                                                         CustName = x.CustName ,
                                                         EmpId = x.EmpId,
                                                         ShipperName = x.ShipperName,
                                                         ShipperId = x.ShipperId,
                                                         EmpName = x.EmpName,
                                                         Orderdate = x.Orderdate,
                                                         ShippedDate = x.ShippedDate
                                                      } ).ToList();
             */





             result.Add(new simpleSystem.ViewModels.Order() { CustId = "001", CustName = "叡揚資訊", EmpId = 1, ShipperName = "哈哈公司", ShipperId = 12, EmpName = "王小明", Orderdate = DateTime.Parse("2015/11/08"), ShippedDate = DateTime.Parse("2015/11/09") });
             result.Add(new simpleSystem.ViewModels.Order() { CustId = "002", CustName = "網軟資訊", EmpId = 2, ShipperName="嘻嘻公司",ShipperId=14, EmpName = "李小華", Orderdate = DateTime.Parse("2015/11/01"), ShippedDate = DateTime.Parse("2015/11/09") });
            return result;
        }
        /// <summary>
        /// 刪除訂單
        /// </summary>
        public void DeleteOrderById(string orderId)
        {
            //todo
        }
        /// <summary>
        /// 更新訂單
        /// </summary>
        public void UpdateOrder(simpleSystem.Models.Order order)
        {
            //todo
        }

    }
}
