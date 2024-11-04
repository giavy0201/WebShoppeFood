using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Model.Cart
{
    public class CartDtos
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public string CustomerId { get; set; }
        public string Delivery { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime TimeOrder { get; set; }
        public double Status { get; set; }
        public List<CartItemDtos> DetailCarts { get; set; }
        public string GetStatusLabel()
        {
            return Status switch
            {
                0 => "Đang Đợi",
                1 => "Chuẩn Bị",
                2 => "Hủy Đơn",
                3 => "Đã Giao",
                _ => "Không xác định"
            };
        }

        public string GetStatusClass()
        {
            return Status switch
            {
                0 => "text-success",
                1 => "text-success",
                2 => "text-danger",
                3 => "text-success",
                _ => "text-muted"
            };
        }
    }

    public class CartItemDtos
    {
        public int Id { get; set; }
        public int FoodId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int CartID { get; set; }
    }
}
