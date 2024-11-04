using System.ComponentModel.DataAnnotations;

namespace BLL.Models.Request
{
    public class StoreCollectionRequest
    {
        [Required(ErrorMessage = "Bắt Buộc Phải Có Mã Bộ Sưu Tập")]
        public int? CollectionID { get; set; }
        public int? NumberOfItem { get; set; }
        public int? PageIndex { get; set; }
    }
}
