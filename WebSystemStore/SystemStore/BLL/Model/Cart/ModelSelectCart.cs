namespace BLL.Model.Cart
{
    public class ModelSelectCart
    {
        public int StoreID { get; set; }
        public int? CartID { get; set; }
        public DateTime? DayStart { get; set; }
        public DateTime? DayEnd { get; set; }
        public string? Delivery { get; set; }
        public int? Phone { get; set; }
        public int? StatusID { get; set; }
    }
}
