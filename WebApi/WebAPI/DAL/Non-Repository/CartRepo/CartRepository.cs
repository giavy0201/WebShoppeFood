using DAL.Entities;
using DAL.ModelsRequest;
using DAL.Repository;
using Microsoft.EntityFrameworkCore;
using Unidecode.NET;

namespace DAL.Non_Repository.CartRepo
{

    public class CartRepository : ICartRepository
    {
        private readonly IRepository<Cart> _cartRepositoty;
        private readonly IRepository<DetailCart> _detailCartRepositoty;
        private readonly DataContext _dataContext;

        public CartRepository(IRepository<Cart> cartRepositoty, IRepository<DetailCart> detailCartRepositoty, DataContext dataContext)
        {
            _cartRepositoty = cartRepositoty;
            _detailCartRepositoty = detailCartRepositoty;
            _dataContext = dataContext;
        }
        public async Task<Cart> AddCart(int StoreID, string CustomerID, string DeliveryAddress, int Phone, double Status)
        {
            var cart = new Cart
            {
                StoreId = StoreID,
                CustomerId = CustomerID,
                TimeOrder = DateTime.Now,
                Delivery = DeliveryAddress,
                DeliveryNoDiacritic = DeliveryAddress.Unidecode(),
                PhoneNumber = Phone,
                Status = Status
            };
            _cartRepositoty.Insert(cart);
            _cartRepositoty.Save();
            return cart;
        }

        public async Task<DetailCart> AddDetailCart(int FoodID, int Quantity, double Price, int CartID)
        {
            var detailCart = new DetailCart
            {
                FoodId = FoodID,
                Quantity = Quantity,
                Price = Price,
                CartID = CartID
            };
            _detailCartRepositoty.Insert(detailCart);
            _detailCartRepositoty.Save();
            return detailCart;
        }

        public async Task<double> GetCartTotalToday(int StoreID)
        {
            var today = DateTime.Now.Date;
            var cart = _dataContext.Carts;
            var detailcart = _dataContext.DetailCarts;
            var result = (from x in cart
                          join y in detailcart on x.Id equals y.CartID
                          where x.StoreId == StoreID && x.TimeOrder.HasValue && x.TimeOrder.Value.Date == today && x.Status == ValueOrder.Done
                          select y.Price * y.Quantity)
                          .Sum();
            return result;
        }

        public async Task<Cart> UpdateStatusOrder(int CartID,int Status)
        {
            var cart = _cartRepositoty.GetById(CartID);
            if (cart == null)
            {
                return null;
            }

            switch (Status)
            {
                case ValueOrder.Order:
                case ValueOrder.Cancel:
                case ValueOrder.Done:
                    cart.Status = Status;
                    break;
                default:
                    return null;
            }
            _cartRepositoty.Update(cart);
            _cartRepositoty.Save();
            return cart;
        }

        public async Task<List<Cart>> ListCartByStore(SearchCartReq Request)
        {
            if (!string.IsNullOrEmpty(Request.Delivery))
            {
                Request.Delivery = Request.Delivery.Unidecode().ToLower();
            }
            var cart = _dataContext.Carts.Where(x =>
                 x.StoreId == Request.StoreID &&
                 (Request.CartID == null || x.Id == Request.CartID) &&
                 (Request.DayStart == null || x.TimeOrder >= Request.DayStart) &&
                 (Request.DayEnd == null || x.TimeOrder <= Request.DayEnd) &&
                 (Request.Delivery == null || x.DeliveryNoDiacritic.ToLower().Contains(Request.Delivery)) &&
                 (Request.Phone == null || x.PhoneNumber == Request.Phone) &&
                 (Request.StatusID == null || x.Status == Request.StatusID)
                ).ToList();
            cart = cart.OrderByDescending(x=>x.TimeOrder).ToList();
            return cart;
        }

        public async Task<Cart> SelectCartById(int CartID)
        {
            var cart = _dataContext.Carts.Include(x=>x.DetailCarts).FirstOrDefault(x => x.Id == CartID);
            return cart;
        }

        //public async Task<List<Cart>> GetOrdersByStoreAndMonth(int StoreID, int month)
        //{
        //    var orders = await _dataContext.Carts
        //.Where(x => x.StoreId == StoreID && x.TimeOrder.HasValue && x.TimeOrder.Value.Month == month)
        //.Include(x => x.DetailCarts) // Bao gồm chi tiết đơn hàng
        //.ToListAsync();

        //    return orders;
        //}
        public async Task<double> GetCartTotalForMonth(int storeID, int month, int year)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            var cart = _dataContext.Carts;
            var detailcart = _dataContext.DetailCarts;

            var result = (from x in cart
                          join y in detailcart on x.Id equals y.CartID
                          where x.StoreId == storeID &&
                                x.TimeOrder.HasValue &&
                                x.TimeOrder.Value.Date >= startDate &&
                                x.TimeOrder.Value.Date <= endDate &&
                                x.Status == ValueOrder.Done
                          select y.Price * y.Quantity)
                          .Sum();

            return result;
        }
        public async Task<List<object>> GetMonthlyRevenue(int storeID, int year)
        {
            var result = await (from cart in _dataContext.Carts
                                join detail in _dataContext.DetailCarts on cart.Id equals detail.CartID
                                where cart.StoreId == storeID &&
                                      cart.TimeOrder.HasValue &&
                                      cart.TimeOrder.Value.Year == year &&
                                      cart.Status == ValueOrder.Done
                                group detail by cart.TimeOrder.Value.Month into g
                                select new
                                {
                                    Month = g.Key,
                                    TotalRevenue = g.Sum(detail => detail.Price * detail.Quantity)
                                }).ToListAsync();

            return result.Cast<object>().ToList();
        }

        public async Task<List<DetailCart>> GetDetailCartsForDate(int storeID, DateTime date)
        {
            var detailCarts = await _dataContext.DetailCarts
             .Include(dc => dc.Cart) // Bao gồm thông tin giỏ hàng liên quan
                .Where(dc => dc.Cart.StoreId == storeID &&
                     dc.Cart.TimeOrder.HasValue &&
                     dc.Cart.TimeOrder.Value.Date == date.Date &&
                     dc.Cart.Status == ValueOrder.Done)
            .ToListAsync();

            return detailCarts;
        }
        
        public async Task<List<int>> GetCartIdsByEmail(string email)
        {
            var cartIds = await _dataContext.Carts
                                            .Where(c => c.Customer.Email == email)
                                            .Select(c => c.Id)
                                            .ToListAsync();
            return cartIds;
        }


    }
}
