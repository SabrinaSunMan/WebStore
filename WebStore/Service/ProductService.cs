using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebStore.App_Start;
using WebStore.Enum;
using WebStore.Helper;
using WebStore.Interface;
using WebStore.Models;
using WebStore.Models.ViewModel;
using WebStore.Repositories;

namespace WebStore.Service
{
    public class ProductService
    {
        private readonly ProductRepository _ProductRep;

        private readonly IUnitOfWork _unitOfWork;

        private readonly int pageSize = (int)BackPageListSize.commonSize;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _ProductRep = new ProductRepository(unitOfWork);
        }

        /// <summary>
        /// Gets the product view model.
        /// </summary>
        /// <param name="selectModel">The select model.</param>
        /// <param name="nowpage">The nowpage.</param>
        /// <returns></returns>
        public ProductViewModel GetProductViewModel(ProductListHeaderViewModel selectModel, int nowpage = 1)
        {
            ProductViewModel returnSystemRolesListViewModel = new ProductViewModel();
            returnSystemRolesListViewModel.Header = selectModel; /*表頭*/

            IEnumerable<ProductListContentViewModel> GetStaticHtmlListViewModelResult = GetAllProductListViewModel(selectModel);
            int currentPage = (nowpage < 1) && GetStaticHtmlListViewModelResult.Count() >= 1 ? 1 : nowpage;
            returnSystemRolesListViewModel.Content_List = GetStaticHtmlListViewModelResult.ToPagedList(currentPage, pageSize);/*內容*/

            return returnSystemRolesListViewModel;
        }

        /// <summary>
        /// Gets all static HTML ListView model.
        /// </summary>
        private IEnumerable<ProductListContentViewModel> GetAllProductListViewModel(ProductListHeaderViewModel selectModel)
        {
            //ProductListContentViewModel = 網頁要顯示的欄位抓取
            //此動作目的在於不顯示過多的資訊至網頁上，進行欄位Mapping動作
            IEnumerable<ProductListContentViewModel> ReturnList = _ProductRep.GetAll().
                Where(s => (!string.IsNullOrEmpty(selectModel.ProductName) ?
            s.ProductName.Contains(selectModel.ProductName) : s.ProductName == s.ProductName)

            && s.Qty == s.Qty

            && (!string.IsNullOrWhiteSpace(selectModel.CreateTime) ?
            s.CreateTime.ToString() == selectModel.CreateTime : s.CreateTime == s.CreateTime
            )).Select(s => new ProductListContentViewModel()
            {
                ProductName = s.ProductName,
                CreateTime = s.CreateTime.ToString(),
                Status = s.Status.ToString()
            });
            return ReturnList;
        }

        /// <summary>
        /// Returns the product detail view model.
        /// </summary>
        /// <param name="ActionType">Type of the action.</param>
        /// <param name="guid">The unique identifier.</param>
        /// <returns></returns>
        public ProductDetailViewModel ReturnProductDetailViewModel(Actions ActionType, string guid)
        {
            ProductDetailViewModel DetailViewModel = new ProductDetailViewModel();
            Product StaticHtmlDBViewModel = _ProductRep.GetSingle(s => s.ProductID == guid);
            if (StaticHtmlDBViewModel == null) StaticHtmlDBViewModel = new Product();
            var mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            DetailViewModel = mapper.Map<ProductDetailViewModel>(StaticHtmlDBViewModel);
            return DetailViewModel;
        }

        /// <summary>
        /// Creates the specified actity.
        /// </summary>
        /// <param name="productDetail">The actity.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public string Create(ProductDetailViewModel productDetail, string userName)
        {
            try
            {
                Product ProductDBViewModel = new Product();
                var mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
                ProductDBViewModel = mapper.Map<Product>(productDetail);

                ProductDBViewModel.CreateTime = DateTime.Now;
                ProductDBViewModel.CreateUser = userName;
                ProductDBViewModel.UpdateTime = DateTime.Now;
                ProductDBViewModel.UpdateUser = userName;

                _ProductRep.Create(ProductDBViewModel);

                return EnumHelper.GetEnumDescription(DataAction.CreateScuess);
            }
            catch
            {
                return EnumHelper.GetEnumDescription(DataAction.CreateFail);
            }
        }

        /// <summary>
        /// Updates the specified product detail.
        /// </summary>
        /// <param name="productDetail">The product detail.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public string Update(ProductDetailViewModel productDetail, string userName)
        {
            try
            {
                Product ProductDBViewModel = new Product();
                Product StaticHtmlDBViewModel = _ProductRep.GetSingle(s => s.ProductID == productDetail.ProductID);

                var mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
                ProductDBViewModel = mapper.Map<Product>(productDetail);

                ProductDBViewModel.UpdateTime = DateTime.Now;
                ProductDBViewModel.UpdateUser = userName;

                return EnumHelper.GetEnumDescription(DataAction.UpdateScuess);
            }
            catch
            {
                return EnumHelper.GetEnumDescription(DataAction.UpdateFail);
            }
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="ID">The identifier.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public string Delete(string ID, string userName)
        {
            try
            {
                Product product = _ProductRep.GetSingle(s => s.ProductID.Equals(ID));
                _ProductRep.Delete(product);
                return EnumHelper.GetEnumDescription(DataAction.DeleteScuess);
            }
            catch
            {
                return EnumHelper.GetEnumDescription(DataAction.DeleteFail);
            }
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save()
        {
            _unitOfWork.Save();
        }
    }
}