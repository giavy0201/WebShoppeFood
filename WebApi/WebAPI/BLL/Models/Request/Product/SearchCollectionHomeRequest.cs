using System.ComponentModel.DataAnnotations;

namespace BLL.Models.Request
{
    public class SearchCollectionHomeRequest
    {
        [Required(ErrorMessage = "Bắt Buộc Phải Có Mã Thành Phố")]
        public int? CityID { get; set; }
        [Required(ErrorMessage = "Bắt Buộc Phải Có Mã Loại Sản Phẩm")]
        public int? CateID { get; set; }
        public int? NumberOfItem { get; set; }
        public int? PageIndex { get; set; }
    }
}
