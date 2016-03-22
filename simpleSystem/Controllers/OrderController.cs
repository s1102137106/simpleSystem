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
        public ActionResult Index()
        {
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
