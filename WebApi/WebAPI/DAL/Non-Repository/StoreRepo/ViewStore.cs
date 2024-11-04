namespace DAL.Non_Repository.StoreRepo
{
    public class ViewStore
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public string? Preferential { get; set; }
        public string Address { get; set; }
        public string? AddressLocation { get; set; }
        public int DistrictID { get; set; }
        public int ContentID { get; set; }
    }
}
