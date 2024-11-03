namespace BLL.Model.Food
{
    public class FoodDtos
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public int Discount { get; set; }
        public string Description { get; set; }
        public int MenuID { get; set; }
        public string AdminName { get; set; }
        public DateTime AdminTime { get; set; }
        public int Status { get; set; }
    }
}
