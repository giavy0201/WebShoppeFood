namespace DAL.ModelsRequest.MenuRequest
{
    public class UpdateFoodReq
    {
        public int FoodID { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }
        public double Price { get; set; }
        public int Discount { get; set; }
        public string Description { get; set; }
        public int MenuID { get; set; }
        public string AdminName { get; set; }
        public int Status { get; set; }
    }
}
