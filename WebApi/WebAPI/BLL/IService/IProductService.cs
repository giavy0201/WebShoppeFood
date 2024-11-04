using BLL.Models.DTOs.ProductDtos;

namespace BLL.IService
{
    public interface IProductService
    {
        Task<IEnumerable<CateDtos>> ListCategory();
        Task<IEnumerable<ContentProductDtos>> ListContentOfCategory(int id);
        Task<CateDtos> GetCategorById(int id);
        Task<ContentProductDtos> GetContentById(int contentID);
        Task<List<ContentProductDtos>> GetListContent();
    }
}
