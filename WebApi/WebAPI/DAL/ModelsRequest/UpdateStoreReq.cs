namespace DAL.ModelsRequest
{
    public class UpdateStoreReq
    {
        public int StoreID {  get; set; } 
        public string Name { get; set; }
        public string Img { get; set; }
        public string TimeOpen { get; set; }
        public string TimeClose { get; set; }
        public string Preferential { get; set; }
        public string Location { get; set; }
        public int WardID { get; set; }
        public int ContentID { get; set; }
        public string AdminName { get; set; }
        public int Status { get; set; }
    }
}
