using AutoMapper;
using BLL.IService;
using BLL.Models.DTOs;
using BLL.Models.DTOs.Cart;
using BLL.Models.Request;
using DAL;
using DAL.Entities;
using DAL.ModelsRequest;
using DAL.Non_Repository;
using DAL.Non_Repository.CartRepo;
using Microsoft.EntityFrameworkCore;
using System;
using static BLL.Models.Validate.EnumNumber;

namespace BLL.Service
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepo;
        private readonly IMapper _mapper;
        private readonly CartDtos _cartDtos;

        public CartService(ICartRepository cartRepo, IMapper mapper, CartDtos cartDtos)
        {
            _cartRepo = cartRepo;
            _mapper = mapper;
            _cartDtos = cartDtos;
        }
        public async Task<Cart> InsertCart(CartRequest reqCart)
        {
            try
            {
                var req = await _cartRepo.AddCart((int)reqCart.StoreId, reqCart.CustomerId, reqCart.Delivery, reqCart.PhoneNumber, 0);
                var cartID = req.Id;
                foreach (var detailcart in reqCart.Details)
                {
                    await _cartRepo.AddDetailCart(detailcart.FoodId, detailcart.Quantity, detailcart.Price, cartID);
                }
                return req;
            }
            catch
            {
                return null;
            }
        }

        public async Task<double> TotalMoneyToday(int StoreID)
        {
            var cart = await _cartRepo.GetCartTotalToday(StoreID);
            if (cart == null)
            {
                return 0;
            }
            else return cart;
        }

        public async Task<ModelApiRequest> UpdateCartOrder(SetStatusCartReq request)
        {
            var req = new ModelApiRequest();
            var cart = await _cartRepo.UpdateStatusOrder(request.CartID, request.Status);
            if (cart == null)
            {
                return null;
            }
            req.ID = cart.Id;
            return req;
        }

        public async Task<List<CartSystem>> ListCartByStore(SearchCartRequest model)
        {
            if (model.StatusID != null && (!Enum.IsDefined(typeof(CartStatus), model.StatusID)))
            {
                return null;
            }
            var req = _mapper.Map<SearchCartReq>(model);
            var listCart = await _cartRepo.ListCartByStore(req);
            var cartSystem = _mapper.Map<List<CartSystem>>(listCart);
            return cartSystem;
        }

        public async Task<CartSystem> CartByID(int CartID)
        {
            var cart = await _cartRepo.SelectCartById(CartID);
            var cartSystem = _mapper.Map<CartSystem>(cart);
            return cartSystem;
        }

        

        public async Task<double> TotalMoneyForMonth(int storeID, int month, int year)
        {
            double totalMoney = 0;

            for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
            {
                // Lấy danh sách chi tiết giỏ hàng cho mỗi ngày trong tháng
                var dailyDetailCarts = await _cartRepo.GetDetailCartsForDate(storeID, new DateTime(year, month, i));

                // Tính tổng doanh thu cho ngày đó và cộng vào tổng doanh thu của tháng
                var dailyMoney = dailyDetailCarts.Sum(dc => dc.Price * dc.Quantity);
                totalMoney += dailyMoney;
            }

            return totalMoney;
        }
        public async Task<List<double>> TotalMoneyForYear(int storeID, int year)
        {
            var monthlyRevenues = new List<double>();

            for (int month = 1; month <= 12; month++)
            {
                double totalMoney = 0;

                for (int day = 1; day <= DateTime.DaysInMonth(year, month); day++)
                {
                    // Lấy danh sách chi tiết giỏ hàng cho mỗi ngày trong tháng
                    var dailyDetailCarts = await _cartRepo.GetDetailCartsForDate(storeID, new DateTime(year, month, day));

                    // Tính tổng doanh thu cho ngày đó và cộng vào tổng doanh thu của tháng
                    var dailyMoney = dailyDetailCarts.Sum(dc => dc.Price * dc.Quantity);
                    totalMoney += dailyMoney;
                }

                monthlyRevenues.Add(totalMoney);
            }

            return monthlyRevenues;
        }

        public async Task<List<int>> GetCartIdByEmail(string email)
        {
            var carts = await _cartRepo.GetCartIdsByEmail(email);
            if (carts == null || !carts.Any())
            {
                // Nếu không tìm thấy giỏ hàng cho email đã cho, bạn có thể trả về null hoặc danh sách rỗng
                return new List<int>(); // hoặc return null;
            }

            return carts;
        }
        //public async Task<List<CartSystem>> GetOrdersByStoreAndMonth(int storeId, int month)
        //{
        //    var orders = await _cartRepo.GetOrdersByStoreAndMonth(storeId, month);
        //    var cartSystems = _mapper.Map<List<CartSystem>>(orders);
        //    return cartSystems;
        //}
    }
}
