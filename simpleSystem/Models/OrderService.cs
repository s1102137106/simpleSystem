using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using simpleSystem.Models;


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
        public  List<Shipper> GetShipper()
        {
            List<Shipper> result = new List<Shipper>();
            SimpleDB db = new SimpleDB();
           
            var tmpShipper =  db.Shippers.Select(x => new {CompanyName = x.CompanyName,ShipperID = x.ShipperID }).ToList();
            foreach (var Shipper in tmpShipper)
            {
                result.Add(new Shipper { CompanyName = Shipper.CompanyName, ShipperID = Shipper.ShipperID });
            }             return result;
        }

        /// <summary>
        ///  Order/index主畫面的 Emp預設下拉式選單
        /// </summary>
        /// <returns></returns>
         public  List<Employee> GetEmp(){
            List<Employee> result = new List<Employee>();
            SimpleDB db = new SimpleDB();
            var tmpEmp =  db.Employees.Select(x => new{EmployeeID = x.EmployeeID,FirstName = x.FirstName , LastName =x.LastName }).ToList();

            foreach (var emp in tmpEmp)
            {
                result.Add(new Employee { EmployeeID = emp.EmployeeID, FirstName = emp.FirstName, LastName = emp.LastName });
            } 
            return result;
         }

         


        /// <summary>
        /// 依照條件取得訂單資料
        /// </summary>
        /// <returns></returns>
        public List<simpleSystem.ViewModels.Order> GetOrderByCondtioin(simpleSystem.ViewModels.Order order)
        {
            List<simpleSystem.ViewModels.Order> result = new List<simpleSystem.ViewModels.Order>();

            DateTime reDate = (Convert.ToDateTime(order.RequireDdate)).Date;
            DateTime orDate = (Convert.ToDateTime(order.Orderdate)).Date;
            DateTime shDate = (Convert.ToDateTime(order.ShippedDate)).Date;
            
            using (SimpleDB db = new SimpleDB())
            {
               
                //依照給的order資訊下條件
                var dbResult = db.Orders.Join(db.Customers,
                                    orders => orders.CustomerID,
                                    customers => customers.CustomerID,
                                    (orders, customers) => new { Customers = customers, Orders = orders })
                                    .Where(y => (
                                        (order.OrderDd == y.Orders.OrderID  || order.OrderDd == 0)
                                        &&
                                        (order.EmpId == y.Orders.EmployeeID || order.EmpId == 0)
                                        &&
                                        (y.Customers.CompanyName.Contains(order.CustName) || order.CustName == null)
                                        &&
                                        (order.ShipperId == y.Orders.ShipperID || order.ShipperId == 0)
                                        &&//order.RequireDdate == y.Orders.RequiredDate
                                        (DbFunctions.TruncateTime(y.Orders.RequiredDate) == reDate || order.RequireDdate == null)
                                        &&// (order.Orderdate == y.Orders.OrderDate || order.Orderdate == null)
                                        (DbFunctions.TruncateTime(y.Orders.OrderDate) == orDate || order.Orderdate == null)
                                        &&
                                        (DbFunctions.TruncateTime(y.Orders.ShippedDate) == shDate || order.ShippedDate == null)
                                    ))

                                    .Select(x => new
                                    {
                                        OrderDd = x.Orders.OrderID,
                                        CustName = x.Customers.CompanyName,
                                        Orderdate = x.Orders.OrderDate ,
                                        ShippedDate = x.Orders.ShippedDate,
                                        
                                    })
                                    .OrderBy(all => all.OrderDd)
                                    .ToList();


                foreach (var tmp in dbResult)
                {
                   
                    String shipperDateStr;
                    if(tmp.ShippedDate == null){
                        shipperDateStr = "";
                    }else{
                        shipperDateStr = ((DateTime)tmp.ShippedDate).ToString("yyyy/MM/dd");
                    }
                    result.Add(new simpleSystem.ViewModels.Order() { CustId = tmp.OrderDd, CustName = tmp.CustName, OrderdateStr = tmp.Orderdate.ToString("yyyy/MM/dd"), ShippedDateStr = shipperDateStr });
                }
            }
            
            //result.Add(new simpleSystem.ViewModels.Order() { CustId = "001", CustName = "叡揚資訊", EmpId = 1, ShipperName = "哈哈公司", ShipperId = 12, EmpName = "王小明", Orderdate = DateTime.Parse("2015/11/08"), ShippedDate = DateTime.Parse("2015/11/09") });
            //result.Add(new simpleSystem.ViewModels.Order() { CustId = "002", CustName = "網軟資訊", EmpId = 2, ShipperName="嘻嘻公司",ShipperId=14, EmpName = "李小華", Orderdate = DateTime.Parse("2015/11/01"), ShippedDate = DateTime.Parse("2015/11/09") });
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
