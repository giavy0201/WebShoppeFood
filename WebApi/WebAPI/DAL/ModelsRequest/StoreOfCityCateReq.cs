namespace DAL.ModelsRequest
{
    public class StoreOfCityCateReq
    {
        public int CityID { get; set; }
        public int CateID { get; set; }
        public List<int>? Districts { get; set; }
        public int? NumberOfItem { get; set; }
        public int? PageIndex { get; set; }
    }
}
