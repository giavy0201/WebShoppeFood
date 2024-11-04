using BLL.Model;
using BLL.Model.ModelRequest;
using BLL.Model.ProductDtos;

namespace BLL.IService
{
    public interface IProductService
    {
        Task<List<CategoryDtos>> GetListCategory();
        Task<ApiResponse<List<CollectionDtos>>> ListCollection(ReqCollectionHome reqCollection);
        Task<CollectionDtos> DetailCollection(int CollectionID);
        Task<List<ContentDtos>> GetListContentByCate(int CateID);
    }

}
