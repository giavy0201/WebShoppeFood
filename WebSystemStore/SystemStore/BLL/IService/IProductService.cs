using BLL.Model.Product;

namespace BLL.IService
{
    public interface IProductService
    {
        Task<ContentDtos> GetContentByID(int ContentID);
        Task<List<ContentDtos>> ListContents();

    }
}
