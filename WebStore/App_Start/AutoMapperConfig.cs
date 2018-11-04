using AutoMapper;
using WebStore.Models;
using WebStore.Models.ViewModel;

namespace WebStore.App_Start
{
    public static class AutoMapperConfig
    {
        public static MapperConfiguration InitializeAutoMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                // [功能名稱].List/Main.Get/Post. DBModel TO ViewModel (縮寫 D To V 或是 V To D即可)

                cfg.AddProfile<ProductProfile>();               //[產品].List.Get.D To V
                cfg.AddProfile<ProductViewModelProfile>();      //[產品].Main.Post.V To D

                /*etc...*/
            });
            config.AssertConfigurationIsValid();//←證驗應對
            return config;
        }

        #region 產品

        /// <summary>
        /// [產品].List.Get.D To V
        /// </summary>
        /// <seealso cref="AutoMapper.Profile" />
        public class ProductProfile : Profile
        {
            public override string ProfileName => base.ProfileName;

            //這邊負責確認是否兩個欄位有不同名稱
            public ProductProfile()
            {
                CreateMap<Product, ProductDetailViewModel>()

                    .ForMember(dest => dest.ProductID, opt => opt.MapFrom(src => src.ProductID))
                    .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
                    .ForMember(dest => dest.Qty, opt => opt.MapFrom(src => src.Qty))

                    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                    .ForMember(dest => dest.CreateTime, opt => opt.MapFrom(src => src.CreateTime))
                    .ForMember(dest => dest.CreateUser, opt => opt.MapFrom(src => src.CreateUser))
                    .ForMember(dest => dest.sort, opt => opt.MapFrom(src => src.sort))
                    .ForMember(dest => dest.UpdateTime, opt => opt.MapFrom(src => src.UpdateTime))
                    .ForMember(dest => dest.UpdateUser, opt => opt.MapFrom(src => src.UpdateUser));
            }
        }

        /// <summary>
        /// [產品].Main.Post.V To D
        /// </summary>
        /// <seealso cref="AutoMapper.Profile" />
        public class ProductViewModelProfile : Profile
        {
            public override string ProfileName => base.ProfileName;

            //這邊負責確認是否兩個欄位有不同名稱
            public ProductViewModelProfile()
            {
                CreateMap<ProductDetailViewModel, Product>()

                   .ForMember(dest => dest.ProductID, opt => opt.MapFrom(src => src.ProductID))
                    .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
                    .ForMember(dest => dest.Qty, opt => opt.MapFrom(src => src.Qty))

                    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                    .ForMember(dest => dest.CreateTime, opt => opt.MapFrom(src => src.CreateTime))
                    .ForMember(dest => dest.CreateUser, opt => opt.MapFrom(src => src.CreateUser))
                    .ForMember(dest => dest.sort, opt => opt.MapFrom(src => src.sort))
                    .ForMember(dest => dest.UpdateTime, opt => opt.MapFrom(src => src.UpdateTime))
                    .ForMember(dest => dest.UpdateUser, opt => opt.MapFrom(src => src.UpdateUser));
            }
        }

        #endregion 產品
    }
}