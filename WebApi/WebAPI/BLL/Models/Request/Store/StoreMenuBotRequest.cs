using System.ComponentModel.DataAnnotations;

namespace BLL.Models.Request
{
    public class StoreMenuBotRequest
    {
        [Required(ErrorMessage = "Vui Lòng Nhập Mã Thành Phố")]
        public int? CityID { get; set; }
        [Required(ErrorMessage = "Vui Lòng Nhập Mã Sản Phẩm")]
        public int? CateID { get; set; }
        public int? DistrictID { get; set; }
        public int? NumberOfItem { get; set; }
        public int? PageIndex { get; set; }
    }
}
