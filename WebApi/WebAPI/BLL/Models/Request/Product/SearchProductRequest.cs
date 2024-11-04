namespace BLL.Models.Request
{
    public class SearchProductRequest
    {
        public int StoreID { get; set; }
        public string? Keyword { get; set; }
        public List<int>? Menu { get; set; }
        public double? PriceFirst { get; set; }
        public double? PriceEnd { get; set; }
        public int? DiscountID { get; set; }
        public int? StatusID { get; set; }
        public int? PriceID { get; set; }
    }
}
