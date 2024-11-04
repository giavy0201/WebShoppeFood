using System.ComponentModel.DataAnnotations;

namespace BLL.Models.Request
{
    public class StoreOfCityCateRequest
    {
        [Required(ErrorMessage = "Vui Lòng Nhập Mã Thành Phố")]
        public int? CityID { get; set; }
        [Required(ErrorMessage = "Vui Lòng Nhập Mã Sản Phẩm")]
        public int? CateID { get; set; }
        public List<int>? Districts { get; set; }
        public int? NumberOfItem { get; set; }
        public int? PageIndex { get; set; }
    }
}
