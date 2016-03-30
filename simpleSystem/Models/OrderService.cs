using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using simpleSystem.Models;
using PagedList;

using System.Data.Entity;
using System.Diagnostics;
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
        /// Order/index主畫面的 Shipper預設下拉式選單
        /// </summary>
        /// <returns></returns>
        public List<Shipper> GetShipper()
        {
            List<Shipper> result = new List<Shipper>();
            SimpleDB db = new SimpleDB();

            var tmpShipper = db.Shippers.Select(x => new { CompanyName = x.CompanyName, ShipperID = x.ShipperID }).ToList();
            foreach (var Shipper in tmpShipper)
            {
                result.Add(new Shipper { CompanyName = Shipper.CompanyName, ShipperID = Shipper.ShipperID });
            } return result;
        }

        /// <summary>
        ///  Order/index主畫面的 Emp預設下拉式選單
        /// </summary>
        /// <returns></returns>
        public List<Employee> GetEmp()
        {
            List<Employee> result = new List<Employee>();
            SimpleDB db = new SimpleDB();
            var tmpEmp = db.Employees.Select(x => new { EmployeeID = x.EmployeeID, FirstName = x.FirstName, LastName = x.LastName }).ToList();

            foreach (var emp in tmpEmp)
            {
                result.Add(new Employee { EmployeeID = emp.EmployeeID, FirstName = emp.FirstName, LastName = emp.LastName });
            }
            return result;
        }

        /// <summary>
        /// 一頁需要幾筆資料
        /// </summary>
        private int pageSize = 5;

        /// <summary>
        /// 依照條件取得訂單資料
        /// </summary>
        /// <returns></returns>
        public IPagedList<simpleSystem.ViewModels.Order> GetOrderByCondtion(simpleSystem.ViewModels.Order order, int page = 1)
        {
            //IPagedList<simpleSystem.ViewModels.Order> result = new IPagedList<simpleSystem.ViewModels.Order>();

            DateTime reDate = (Convert.ToDateTime(order.RequireDdate)).Date;
            DateTime orDate = (Convert.ToDateTime(order.OrderDate)).Date;
            DateTime shDate = (Convert.ToDateTime(order.ShippedDate)).Date;
            
           


            using (SimpleDB db = new SimpleDB())
            {
                int currentPage = page < 1 ? 1 : page;
                //依照給的order資訊下條件
                var dbResult = db.Orders.Join(db.Customers,
                                     orders => orders.CustomerID,
                                     customers => customers.CustomerID,
                                     (orders, customers) => new { Customers = customers, Orders = orders })
                                     .Where(y => (
                                         (order.OrderDd == y.Orders.OrderID || order.OrderDd == 0)
                                         &&
                                         (order.EmpId == y.Orders.EmployeeID || order.EmpId == 0)
                                         &&
                                         (y.Customers.CompanyName.Contains(order.CustName) || order.CustName == null)
                                         &&
                                         (order.ShipperId == y.Orders.ShipperID || order.ShipperId == 0)
                                         &&//order.RequireDdate == y.Orders.RequiredDate
                                         (DbFunctions.TruncateTime(y.Orders.RequiredDate) == reDate || order.RequireDdate == null)
                                         &&// (order.Orderdate == y.Orders.OrderDate || order.Orderdate == null)
                                         (DbFunctions.TruncateTime(y.Orders.OrderDate) == orDate || order.OrderDate == null)
                                         &&
                                         (DbFunctions.TruncateTime(y.Orders.ShippedDate) == shDate || order.ShippedDate == null)
                                     ))

                                     .Select(x => new simpleSystem.ViewModels.Order
                                     {
                                         OrderDd = x.Orders.OrderID,
                                         CustName = x.Customers.CompanyName,
                                         OrderDate = x.Orders.OrderDate,
                                         ShippedDate = x.Orders.ShippedDate,

                                     })
                                     .OrderBy(all => all.OrderDd);
                //OrderByDescending
                IPagedList<simpleSystem.ViewModels.Order> orderDbResult = dbResult.ToPagedList(currentPage, pageSize);
                switch (order.OrderByid)
                {
                    case "OrderDd":
                        orderDbResult = dbResult.OrderBy(all => all.OrderDd).ToPagedList(currentPage, pageSize);
                        break;
                    case "CustName":
                        orderDbResult = dbResult.OrderBy(all => all.CustName).ToPagedList(currentPage, pageSize);
                        break;
                    case "OrderdateStr":
                        orderDbResult = dbResult.OrderBy(all => all.OrderDate).ToPagedList(currentPage, pageSize);
                        break;
                    case "ShippedDateStr":
                        orderDbResult = dbResult.OrderBy(all => all.ShippedDate).ToPagedList(currentPage, pageSize);
                        break;

                    case "OrderDDesc":
                        orderDbResult = dbResult.OrderByDescending(all => all.OrderDd).ToPagedList(currentPage, pageSize);
                        break;
                    case "CustNameDesc":
                        orderDbResult = dbResult.OrderByDescending(all => all.CustName).ToPagedList(currentPage, pageSize);
                        break;
                    case "OrderdateStrDesc":
                        orderDbResult = dbResult.OrderByDescending(all => all.OrderDate).ToPagedList(currentPage, pageSize);
                        break;
                    case "ShippedDateStrDesc":
                        orderDbResult = dbResult.OrderByDescending(all => all.ShippedDate).ToPagedList(currentPage, pageSize);
                        break;
                }

                var index = 0;
                foreach (var tmp in orderDbResult)
                {
                    if (tmp.ShippedDate == null)
                    {
                        orderDbResult[index].ShippedDateStr = "";
                    }
                    else
                    {
                        orderDbResult[index].ShippedDateStr = ((DateTime)tmp.ShippedDate).ToString("yyyy/MM/dd");
                    }

                    orderDbResult[index].OrderdateStr = ((DateTime)tmp.OrderDate).ToString("yyyy/MM/dd");

                    index++;
                }


                //result.Add(new simpleSystem.ViewModels.Order() { CustId = "001", CustName = "叡揚資訊", EmpId = 1, ShipperName = "哈哈公司", ShipperId = 12, EmpName = "王小明", Orderdate = DateTime.Parse("2015/11/08"), ShippedDate = DateTime.Parse("2015/11/09") });
                //result.Add(new simpleSystem.ViewModels.Order() { CustId = "002", CustName = "網軟資訊", EmpId = 2, ShipperName="嘻嘻公司",ShipperId=14, EmpName = "李小華", Orderdate = DateTime.Parse("2015/11/01"), ShippedDate = DateTime.Parse("2015/11/09") });
                return orderDbResult;
            }
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
