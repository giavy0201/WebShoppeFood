using DAL.Entities;

namespace DAL.Non_Repository.ProductRepo
{
    public interface IProductRepository
    {
        IEnumerable<CategoryProduct> ListCate();
        CategoryProduct GetCategoryByID(int CateID);
        ContentProduct GetContentProductByID(int ContentID);
        IEnumerable<ContentProduct> ListContentByCate(int CateID);
        IEnumerable<ContentProduct> ListContent();
    }
}
