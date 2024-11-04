using DAL.Entities;
using DAL.Repository;

namespace DAL.Non_Repository.ProductRepo
{
    public class ProductRepository : IProductRepository
    {
        private readonly IRepository<CategoryProduct> _categoryProductRepo;
        private readonly IRepository<ContentProduct> _contentProductRepo;

        public ProductRepository(IRepository<CategoryProduct> categoryProductRepo, IRepository<ContentProduct> contentProductRepo)
        {
            _categoryProductRepo = categoryProductRepo;
            _contentProductRepo = contentProductRepo;
        }

        public CategoryProduct GetCategoryByID(int CateID)
        {
            return _categoryProductRepo.GetById(CateID);
        }

        public ContentProduct GetContentProductByID(int ContentID)
        {
            return _contentProductRepo.GetById(ContentID);
        }

        public IEnumerable<CategoryProduct> ListCate()
        {
            return _categoryProductRepo.GetAll().ToList();
        }

        public IEnumerable<ContentProduct> ListContent()
        {
            return _contentProductRepo.GetAll();
        }

        public IEnumerable<ContentProduct> ListContentByCate(int CateID)
        {
            return _contentProductRepo.GetAll().Where(x => x.CategoryID == CateID && x.Status == 1).ToList();
        }
    }
}
