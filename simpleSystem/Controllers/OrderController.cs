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
            simpleSystem.ViewModels.Order nullCondition = new simpleSystem.ViewModels.Order();//查詢條件設定成空的
            nullCondition.OrderBy = new simpleSystem.ViewModels.OrderBy { OrderByid = OrderByID[0], OrderByStrings = OrderByStrings[0] };
            nullCondition.OrderByid = OrderByID[0];
            IPagedList<simpleSystem.ViewModels.Order> orderList = orderService.GetOrderByCondtion(nullCondition);
            

            ViewBag.condition = nullCondition;//空白的查詢條件
            ViewBag.Data = orderList;//傳回搜尋結果



            #region 員工選單
            List<SelectListItem> Emp = new List<SelectListItem>();//value:empId text:  empName
            var empList = orderService.GetEmp();
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
            ViewBag.Emps = Emp;
            #endregion

            #region 出貨公司選單
            var shipperList = orderService.GetShipper();
            List<SelectListItem> ShipperId = new List<SelectListItem>();//出貨公司代號
            foreach (var shipper in shipperList)
            {
                ShipperId.Add(new SelectListItem
                {
                    Text = shipper.CompanyName.ToString(),
                    Value = shipper.ShipperID.ToString()
                });
            }
            ViewBag.ShipperIds = ShipperId;
            #endregion

            #region 排序條件字串


            List<simpleSystem.ViewModels.OrderBy> OrderByList = new List<simpleSystem.ViewModels.OrderBy>();
            for (int i = 0; i < OrderByStrings.Length; i++)
            {
                OrderByList.Add(new simpleSystem.ViewModels.OrderBy
                {
                    OrderByid =  OrderByID[i],
                    OrderByStrings = OrderByStrings[i],
                });
            }

            List<SelectListItem> OrderBySelect = new List<SelectListItem>();//value:empId text:  empName
            foreach (var orderBy in OrderByList)
            {

                OrderBySelect.Add(new SelectListItem
                {
                    Text = orderBy.OrderByStrings.ToString(),
                    Value = orderBy.OrderByid.ToString()
                });
                ViewBag.OrderBySelect = OrderBySelect;


            }
            #endregion

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
            IPagedList<simpleSystem.ViewModels.Order> orderList = orderService.GetOrderByCondtion(condition, condition.Page);
           
            ViewBag.Data = orderList;
            

            #region 員工選單
            List<SelectListItem> Emp = new List<SelectListItem>();//value:empId text:  empName
            System.Text.StringBuilder fullName = new StringBuilder();
            var empList = orderService.GetEmp();
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
            ViewBag.Emps = Emp;
            #endregion

            #region 出貨公司
            var shipperList = orderService.GetShipper();
            List<SelectListItem> ShipperId = new List<SelectListItem>();//出貨公司代號
            foreach (var shipper in shipperList)
            {
                ShipperId.Add(new SelectListItem
                {
                    Text = shipper.CompanyName.ToString(),
                    Value = shipper.ShipperID.ToString()
                });
            }
            ViewBag.ShipperIds = ShipperId;
            #endregion

            #region 搜尋條件相關日期
            if (condition.OrderDate != null)
            {
                ViewBag.OrderDate = ((DateTime)condition.OrderDate).ToString("yyyy-MM-dd");
            }
            else
            {
                ViewBag.OrderDate = null;
            }

            if (condition.RequireDdate != null)
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
            }
            else
            {
                ViewBag.ShippedDate = null;
            }

            #endregion

            #region 排序條件字串
            List<simpleSystem.ViewModels.OrderBy> OrderByList = new List<simpleSystem.ViewModels.OrderBy>();
            for (int i = 0; i < OrderByStrings.Length; i++)
            {
                OrderByList.Add(new simpleSystem.ViewModels.OrderBy
                {
                    OrderByid = OrderByID[i],
                    OrderByStrings = OrderByStrings[i],
                });
            }

            List<SelectListItem> OrderBySelect = new List<SelectListItem>();//value:empId text:  empName
            foreach (var orderBy in OrderByList)
            {

                OrderBySelect.Add(new SelectListItem
                {
                    Text = orderBy.OrderByStrings.ToString(),
                    Value = orderBy.OrderByid.ToString()
                });
                ViewBag.OrderBySelect = OrderBySelect;


            }
            #endregion

            return View(orderList);
        }


        public List<simpleSystem.ViewModels.OrderBy> OrderByList { get; set; }

        /// <summary>
        /// 定義排序顯示自串
        /// </summary>
        private string[] OrderByStrings = new string[8]
        {   
            "依照訂單編號由小到大",
            "依照客戶名稱由小到大",
            "依照訂單日期由小到大",
            "依照出貨日期由小到大",
            "依照訂單編號由大到小",
            "依照客戶名稱由大到小",
            "依照訂單日期由大到小",
            "依照出貨日期由大到小",
         };

         /// <summary>
        /// 定義排序顯示欄位
        /// </summary>
         private string[] OrderByID = new string[8]
         { "OrderDd",
            "CustName",
            "OrderdateStr",
            "ShippedDateStr",
            "OrderDDesc",
            "CustNameDesc",
            "OrderdateStrDesc",
            "ShippedDateStrDesc",
        };
        
        

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
