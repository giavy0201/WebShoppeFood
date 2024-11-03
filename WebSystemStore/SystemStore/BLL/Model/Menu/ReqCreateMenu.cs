using System.ComponentModel.DataAnnotations;

namespace BLL.Model.Menu
{
    public class ReqCreateMenu
    {
        public string MenuName { get; set; }
        public int StoreID { get; set; }
        public string? AdminName { get; set; }
        [Range(0, 2)]
        public int Status { get; set; }
    }
}
