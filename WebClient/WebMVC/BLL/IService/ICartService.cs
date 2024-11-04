using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Model.Cart;
using BLL.Model.Customer;

namespace BLL.IService
{
    public interface ICartService
    {
        Task<List<int>> GetOrCreateCartIdByEmail(string? email);
        Task<CartDtos> GetOrderHistory(int CartId);
        Task<List<CartDtos>> GetOrderHistoryByEmail(string email);
    }
}
