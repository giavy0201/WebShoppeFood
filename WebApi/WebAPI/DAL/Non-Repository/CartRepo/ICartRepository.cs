using DAL.Entities;
using DAL.ModelsRequest;

namespace DAL.Non_Repository.CartRepo
{
    public interface ICartRepository
    {
        
        Task<Cart> AddCart(int StoreID,string CustomerID,string DeliveryAddress,int Phone,double Status);
        Task<DetailCart> AddDetailCart(int FoodID, int Quantity, double Price,int CartID);
        Task<double> GetCartTotalToday(int StoreID);
        Task<Cart> UpdateStatusOrder(int CartID, int Status);
        Task<List<Cart>> ListCartByStore(SearchCartReq Request);
        Task<Cart> SelectCartById(int CartID);

        Task<List<DetailCart>> GetDetailCartsForDate(int storeID, DateTime date);
        Task<double> GetCartTotalForMonth(int storeID, int month, int year);
        Task<List<object>> GetMonthlyRevenue(int storeID, int year);
        Task<List<int>> GetCartIdsByEmail(string email);

        //Task<double> GetCartTotalToday(int storeID, DateTime dateTime);
        //Task<List<Cart>> GetOrdersByStoreAndMonth(int StoreID, int month);
    }
}
