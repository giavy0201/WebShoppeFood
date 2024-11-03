namespace BLL.Model.Food
{
    public class ModelSearchProduct
    {
        public int StoreID { get; set; }
        public string? Keyword { get; set; }
        public List<int>? Menu { get; set; }
        public double? PriceFirst { get; set; }
        public double? PriceEnd { get; set; }
        public int? DiscountPrice { get; set; }
        public int? StatusID { get; set; }
        public string SortPrice { get; set; }
    }
}
