using PagedList;
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
         
            simpleSystem.ViewModels.Order nullCondition = new simpleSystem.ViewModels.Order();
            //查詢條件設定成空的
            IPagedList<simpleSystem.ViewModels.Order> orderList = orderService.GetOrderByCondtion(nullCondition);
            ViewBag.condition = nullCondition;//空白的查詢條件
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
            return View(orderList);
        }

        /// <summary>
        /// 有查詢條件
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        [HttpPost()]
        public ActionResult Index(simpleSystem.ViewModels.Order condition)
        {
            ViewBag.condition = condition;//將查詢結果傳回給前端

            Models.OrderService orderService = new Models.OrderService();
            simpleSystem.ViewModels.Order nullCondition = new simpleSystem.ViewModels.Order();
            //查詢條件設定成空的
            IPagedList<simpleSystem.ViewModels.Order> orderList = orderService.GetOrderByCondtion(condition, condition.Page);
          
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


            
            if(condition.OrderDate!=null){
                ViewBag.OrderDate = ((DateTime)condition.OrderDate).ToString("yyyy-MM-dd");
            }else{
                ViewBag.OrderDate = null;
            }

            if (condition.RequireDdate!=null)
            {
                ViewBag.RequireDdate = ((DateTime)condition.RequireDdate).ToString("yyyy-MM-dd");
            }
            else
            {
                ViewBag.RequireDdate = null;
            }

            if (condition.ShippedDate != null)
            {
                ViewBag.ShippedDate = ((DateTime)condition.ShippedDate).ToString("yyyy-MM-dd");
            }else{
                ViewBag.ShippedDate = null;
            }
            
            

            ViewBag.Emps = Emp;
            ViewBag.ShipperIds = ShipperId;
            return View(orderList);
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
