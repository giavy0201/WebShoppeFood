using BLL.Models.DTOs;
using BLL.Models.DTOs.Cart;
using BLL.Models.Request;
using DAL.Entities;

namespace BLL.IService
{
    public interface ICartService
    {
       
        Task<Cart> InsertCart(CartRequest reqCart);
        Task<double> TotalMoneyToday(int StoreID);
        Task<ModelApiRequest> UpdateCartOrder(SetStatusCartReq request);
        Task<List<CartSystem>> ListCartByStore(SearchCartRequest model);
        Task<CartSystem> CartByID(int CartID);

        
        Task<double> TotalMoneyForMonth(int storeID, int month, int year);

        Task<List<double>> TotalMoneyForYear(int storeID, int year);
        Task<List<int>> GetCartIdByEmail(string email);
        //Task<List<CartSystem>> GetOrdersByStoreAndMonth(int storeId, int month);
    }
}
