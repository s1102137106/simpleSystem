using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace eSale.Controllers
{
    public class OrderController : Controller
    {

        /// <summary>
        /// 訂單管理首頁
        /// </summary>
        /// <returns></returns>

        [HttpGet()]
        public ActionResult Index()
        {
            Models.OrderService orderService = new Models.OrderService();
            List<simpleSystem.ViewModels.Order> orderList = new List<simpleSystem.ViewModels.Order>();
            simpleSystem.ViewModels.Order nullCondition = new simpleSystem.ViewModels.Order();
            //查詢條件設定成空的
            orderList = orderService.GetOrderByCondtioin(nullCondition);

            var empList = orderService.GetEmp();
            var shipperList = orderService.GetShipper();

            ViewBag.Data = orderList;


            List<SelectListItem> ShipperId = new List<SelectListItem>();//出貨公司代號
            List<SelectListItem> Emp = new List<SelectListItem>();//value:empId text:  empName

           
            System.Text.StringBuilder fullName = new StringBuilder();
            
            foreach (var emp in empList)
            {
                fullName.Append(emp.FirstName);
                fullName.Append(" ");
                fullName.Append(emp.LastName);
                Emp.Add(new SelectListItem
                {
                    Text = fullName.ToString(),
                    Value = emp.EmployeeID.ToString()
                });
                fullName.Clear();
            }

            foreach (var shipper in shipperList)
            {
                ShipperId.Add(new SelectListItem
                {
                    Text = shipper.CompanyName.ToString(),
                    Value = shipper.ShipperID.ToString()
                });
            }
            
            ViewBag.Emps = Emp;
            ViewBag.ShipperIds = ShipperId;
            return View();
        }

        /// <summary>
        /// 有查詢條件
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        [HttpPost()]
        public ActionResult Index(simpleSystem.ViewModels.Order condition)
        {
            Models.OrderService orderService = new Models.OrderService();
            List<simpleSystem.ViewModels.Order> orderList = new List<simpleSystem.ViewModels.Order>();
            simpleSystem.ViewModels.Order nullCondition = new simpleSystem.ViewModels.Order();
            //查詢條件設定成空的
            orderList = orderService.GetOrderByCondtioin(condition);

            var empList = orderService.GetEmp();
            var shipperList = orderService.GetShipper();

            ViewBag.Data = orderList;


            List<SelectListItem> ShipperId = new List<SelectListItem>();//出貨公司代號
            List<SelectListItem> Emp = new List<SelectListItem>();//value:empId text:  empName


            System.Text.StringBuilder fullName = new StringBuilder();

            foreach (var emp in empList)
            {
                fullName.Append(emp.FirstName);
                fullName.Append(" ");
                fullName.Append(emp.LastName);
                Emp.Add(new SelectListItem
                {
                    Text = fullName.ToString(),
                    Value = emp.EmployeeID.ToString()
                });
                fullName.Clear();
            }

            foreach (var shipper in shipperList)
            {
                ShipperId.Add(new SelectListItem
                {
                    Text = shipper.CompanyName.ToString(),
                    Value = shipper.ShipperID.ToString()
                });
            }

            ViewBag.Emps = Emp;
            ViewBag.ShipperIds = ShipperId;
            return View();
        }

        /// <summary>
        /// 訂單管理明細
        /// </summary>
        /// <returns></returns>
        public ActionResult Detial()
        {

            return View();
        }
        /// <summary>
        /// 加入訂單
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult GetSysData()
        {
            return PartialView("_SysDatePartial");
        }

        [HttpPost()]
        public ActionResult InsertOrder(simpleSystem.Models.Order order)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Hello = "ViewBag歡迎光臨";
                TempData["Hello"] = "TempData歡迎光臨";
                return RedirectToAction("Index");

            }
            return View(order);
            //return View();
        }

        public JsonResult TestJsonResult()
        {
            Models.OrderService service = new Models.OrderService();
            return this.Json(service.GetOrderById("123"), JsonRequestBehavior.AllowGet);
        }
    }
}
