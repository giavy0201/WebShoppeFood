using AutoMapper;
using BLL.Models.Authentication;
using BLL.Models.DTOs;
using BLL.Models.DTOs.AccountStore;
using BLL.Models.DTOs.AddressDtos;
using BLL.Models.DTOs.Cart;
using BLL.Models.DTOs.CollectionDtos;
using BLL.Models.DTOs.Food;
using BLL.Models.DTOs.ListMenu;
using BLL.Models.DTOs.ProductDtos;
using BLL.Models.DTOs.Comment;
using BLL.Models.Request;
using BLL.Models.Request.Comment;
using BLL.Models.Request.Food;
using BLL.Models.Request.ListMenu;
using BLL.Models.Request.Store;

using DAL.Entities;
using DAL.ModelsRequest;
using DAL.ModelsRequest.MenuRequest;
using DAL.Non_Repository.CollectionRepo;
using DAL.Non_Repository.MenuRepo;
using DAL.Non_Repository.StoreRepo;

namespace BLL.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Address
            CreateMap<City, CityDtos>().ReverseMap();
            CreateMap<District, DistrictDtos>().ReverseMap();
            CreateMap<Ward, WardDtos>().ReverseMap();
            //product
            CreateMap<CategoryProduct, CateDtos>().ReverseMap();
            CreateMap<ContentProduct, ContentProductDtos>().ReverseMap();
            //Menu
            CreateMap<ListMenu, ListMenuDtos>().ReverseMap();
            CreateMap<Food, FoodDtos>().ReverseMap();
            CreateMap<ViewFoodDtos, FoodDtos>().ReverseMap();
            CreateMap<Food, ListMenuFoodOfStoreDtos>().ReverseMap();
            //store
            CreateMap<ViewStore, StoreDtos>().ReverseMap();
            CreateMap<Store, StoreDtos>().ReverseMap();
            CreateMap<Store, StoreSystem>().ReverseMap();
            //collection
            CreateMap<Collection, CollecDtos>().ReverseMap();
            CreateMap<ViewListStoreOfCollection, ListStoreOfCollectionDtos>().ReverseMap();
            //customer
            CreateMap<Customer, SignUpModel>().ReverseMap()
                    .ForMember(x => x.UserName, y => y.MapFrom(y => y.Email));
            // login store
            CreateMap<ViewLoginStore, ViewLogin>().ReverseMap();
            //cart 
            CreateMap<DetailCart, DetailCartDtos>().ReverseMap();
            //comment
            CreateMap<Comment,CommentDtos>().ReverseMap();
            //mapper request
            CreateMap<SearchProductReq, SelectProductRequest>().ReverseMap();
            CreateMap<SearchCartReq, SearchCartRequest>().ReverseMap();
            CreateMap<StoreOfCityCateReq, StoreOfCityCateRequest>().ReverseMap();
            CreateMap<SearchStoreReq, SearchStoreRequest>().ReverseMap();
            CreateMap<SearchStoreBotReq, StoreMenuBotRequest>().ReverseMap();
            CreateMap<ListCollectionByCityCate, SearchCollectionHomeRequest>().ReverseMap();
            CreateMap<ListStoreOfCollection, StoreCollectionRequest>().ReverseMap();
            //request CRUD
            CreateMap<AddMenuReq, AddMenuRequest>().ReverseMap();
            CreateMap<UpdateMenuReq, UpdateMenuRequest>().ReverseMap();
            CreateMap<AddFoodReq, AddFoodRequest>().ReverseMap();
            CreateMap<UpdateFoodReq, UpdateFoodRequest>().ReverseMap();
            CreateMap<UpdateStoreReq, UpdateStoreRequest>().ReverseMap();
            CreateMap<AddCommentReq, AddCommentRequest>().ReverseMap();
            CreateMap<AddCommentReq, UpdateCommentRequest>().ReverseMap();
            //System
            CreateMap<Food, FoodSystem>().ReverseMap();
            CreateMap<Cart, CartSystem>().ReverseMap();
            CreateMap<DetailCart, SelectCart>().ReverseMap();
            CreateMap<ListMenu, ListMenuSystem>().ReverseMap();
        }
    }
}
