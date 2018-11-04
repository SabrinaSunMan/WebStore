using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebStore.Enum;
using WebStore.Interface;
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
    }
}