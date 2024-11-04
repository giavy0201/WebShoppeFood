namespace DAL.ModelsRequest
{
    public class SearchProductReq
    {
        public int StoreID { get; set; }
        public string? Keyword { get; set; }
        public List<int>? Menu { get; set; }
        public double? PriceFirst { get; set; }
        public double? PriceEnd { get; set; }
        public int? DiscountPrice { get; set; }
        public int? StatusID { get; set; }
        public bool? SortPrice { get; set; }
    }

}
