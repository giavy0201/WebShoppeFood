namespace DAL.ModelsRequest.MenuRequest
{
    public class ListStoreOfCollection
    {
        public int CollectionID { get; set; }
        public int? NumberOfItem { get; set; }
        public int? PageIndex { get; set; }
    }
}
