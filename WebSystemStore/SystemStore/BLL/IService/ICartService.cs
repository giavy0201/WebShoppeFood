using BLL.Model.Cart;
using Microsoft.AspNetCore.Http;

namespace BLL.IService
{
    public interface ICartService
    {
        Task<CartDtos> GetCartByID(int CartID);

        Task<double> GetRevenueForMonth(int storeID, int month, int year);

    }
}
