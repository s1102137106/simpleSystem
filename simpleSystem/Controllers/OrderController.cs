using System;
using System.Collections.Generic;
using System.Linq;
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
            List<Models.Order> orderList = new List<Models.Order>();
            Models.Order nullCondition = new Models.Order();
            //查詢條件設定成空的
            orderList = orderService.GetOrderByCondtioin(nullCondition);
            ViewBag.Data = orderList;

            List<SelectListItem> EmpId = new List<SelectListItem>();//業務(員工)代號
            List<SelectListItem> ShipperId = new List<SelectListItem>();//出貨公司代號

            foreach (Models.Order order in orderList)
            {
                EmpId.Add(new SelectListItem
                {
                    Text = order.EmpId.ToString(),
                    Value = order.EmpId.ToString()
                });

                ShipperId.Add(new SelectListItem
                {
                    Text = order.ShipperId.ToString(),
                    Value = order.ShipperId.ToString()
                });
            }
            ViewBag.EmpIds = EmpId;
            ViewBag.ShipperIds = ShipperId;

            return View();
        }

        [HttpPost()]
        public ActionResult Index(Models.Order condition)
        {
            Models.OrderService orderService = new Models.OrderService();
            List<Models.Order> orderList = new List<Models.Order>();
            orderList = orderService.GetOrderByCondtioin(condition);
            ViewBag.Data = orderList;
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

        [HttpGet()]
        public ActionResult InsertOrder()
        {
            return View();
        }

        public ActionResult GetSysData()
        {
            return PartialView("_SysDatePartial");
        }

        [HttpPost()]
        public ActionResult InsertOrder(Models.Order order)
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
