namespace DAL.ModelsRequest
{
    public class SearchStoreReq
    {
        public int CityID { get; set; }
        public int CateID { get; set; }
        public List<int>? Districts { get; set; }
        public List<int>? Contents { get; set; }
        public string? KeyWord { get; set; }
        public int? NumberOfItem { get; set; }
        public int? PageIndex { get; set; }
    }
}
