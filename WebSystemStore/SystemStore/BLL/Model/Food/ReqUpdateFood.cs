using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BLL.Model.Food
{
    public class ReqUpdateFood
    {
        public int FoodId { get; set; }
        public string Name { get; set; }
        public string? Img { get; set; }
        public double Price { get; set; }
        public int Discount { get; set; }
        public string? Description { get; set; }
        public int MenuID { get; set; }
        public string? AdminName { get; set; }
        [Range(0, 2)]
        public int Status { get; set; }
        public IFormFile formFile { get; set; }
    }
}
