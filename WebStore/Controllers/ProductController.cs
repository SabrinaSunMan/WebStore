using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStore.Service;

namespace WebStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _StaticHtmlService;
        private readonly LoggingService _logSvc;

        #region List - 產品列表

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        #endregion List - 產品列表
    }
}