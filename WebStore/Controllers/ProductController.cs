using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using WebStore.Enum;
using WebStore.Models.ViewModel;
using WebStore.Repositories;
using WebStore.Service;

namespace WebStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _ProductService;
        private readonly LoggingService _logSvc;
        private string _signInManager;

        public ProductController()
        {
            var unitOfWork = new EFUnitOfWork();
            _ProductService = new ProductService(unitOfWork);
            _logSvc = new LoggingService(unitOfWork);
        }

        public string SignInManagerName
        {
            get
            {
                return _signInManager ?? HttpContext.User.Identity.Name.ToString();
            }
            private set
            {
                _signInManager = value;
            }
        }

        #region List - 產品列表

        [HttpGet]
        public ActionResult Index(ProductViewModel ViewModel, int page = 1)
        {
            ProductViewModel ResultViewModel = new ProductViewModel();
            ProductViewModel searchBlock = (ProductViewModel)TempData["ProductSelect"];
            if (searchBlock == null) /*空*/
            {
                ResultViewModel = _ProductService.GetProductViewModel(new ProductListHeaderViewModel(), page);
            }
            else
            {
                ResultViewModel = _ProductService.GetProductViewModel(searchBlock.Header, page);
            }
            return View(ResultViewModel);
        }

        [HttpPost]
        public ActionResult Index(ProductViewModel ViewModel)
        {
            if (ModelState.IsValid)
            {
                ProductViewModel ResultViewModel = _ProductService.GetProductViewModel(ViewModel.Header);
                TempData["ProductSelect"] = ResultViewModel;
                return View(ResultViewModel);
            }
            else return View(ViewModel);
        }

        #endregion List - 產品列表

        #region Main - 產品明細

        [HttpGet]
        public ActionResult ProductMain(Actions ActionType, string guid, string selectCreateTime,
            string selectProductName, string selectQty, int pages = 1)
        {
            TempData["Actions"] = ActionType;
            ProductDetailViewModel data = new ProductDetailViewModel();
            data = _ProductService.ReturnProductDetailViewModel(ActionType, guid);

            #region KeepSelectBlock

            pages = pages == 0 ? 1 : pages;
            TempData["ProductSelect"] = new ProductViewModel()
            {
                Header = new ProductListHeaderViewModel()
                {
                    CreateTime = selectCreateTime,
                    ProductName = selectProductName,
                    Qty = Convert.ToInt16(selectQty)
                },
                page = pages
            };

            #endregion KeepSelectBlock

            return View(data);
        }

        [HttpPost]
        public ActionResult ProductMain(Actions actions, ProductDetailViewModel ProductViewModel)
        {
            #region KeepSelectBlock

            TempData["Actions"] = actions;
            TempData["ProductSelect"] = (ProductViewModel)TempData["ProductSelect"];

            #endregion KeepSelectBlock

            if (ModelState.IsValid)
            {
                if (actions == Actions.Create) //建立資料
                {
                    ProductViewModel.ProductID = Guid.NewGuid().ToString().ToUpper();
                    TempData["message"] = _ProductService.Create(ProductViewModel, SignInManagerName);
                }
                else //更新資料
                {
                    TempData["message"] = _ProductService.Update(ProductViewModel, SignInManagerName);
                }

                _ProductService.Save();
            }

            // 顯示資料
            ProductViewModel = _ProductService.ReturnProductDetailViewModel(actions, ProductViewModel.ProductID);

            return View(ProductViewModel);
        }

        #endregion Main - 產品明細

        #region 回傳 刪除 日後可抽出

        [HttpPost]
        public JsonResult Delete(string StaticGuid, string guid, string actionTable)
        {
            string GetResult = "";
            //TableName actionTableS = (TableName)Enum.Parse(typeof(TableName), actionTable);

            GetResult = _ProductService.Delete(guid, SignInManagerName);

            _ProductService.Save();

            return Json(new { result = GetResult }, JsonRequestBehavior.AllowGet);
        }

        #endregion 回傳 刪除 日後可抽出
    }
}