namespace BLL.Models.DTOs.ListMenu
{
    public class ListMenuSystem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StoreID { get; set; }
        public string? AdminName { get; set; }
        public DateTime? AdminTime { get; set; }
        public int? Status { get; set; }
    }
}
