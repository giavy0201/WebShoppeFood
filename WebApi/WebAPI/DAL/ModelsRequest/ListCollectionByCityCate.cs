namespace DAL.ModelsRequest
{
    public class ListCollectionByCityCate
    {
        public int CityID { get; set; }
        public int CateID { get; set; }
        public int? NumberOfItem { get; set; }
        public int? PageIndex { get; set; }
    }
}
