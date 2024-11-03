using System.ComponentModel.DataAnnotations;

namespace BLL.Model.Menu
{
    public class ReqUpdateMenu
    {
        public int MenuID { get; set; }
        public string MenuName { get; set; }
        public string AdminName { get; set; }
        public int Status { get; set; }
    }
}
