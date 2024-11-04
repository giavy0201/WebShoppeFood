using AutoMapper;
using BLL.IService;
using BLL.Models;
using BLL.Models.DTOs.ProductDtos;
using DAL.Non_Repository.ProductRepo;

namespace BLL.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepo;
        private readonly ICacheService _cacheService;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepo, IMapper mapper, ICacheService cacheService)
        {
            _productRepo = productRepo;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        /// <summary>
        /// list category
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CateDtos>> ListCategory()
        {
            //var ListCateCache = await _cacheService.GetCachedData<IEnumerable<CateDtos>>(GetKeyCache.ListCategory);
            //if (ListCateCache == null)
            //{
            //    var listCate = _productRepo.ListCate();
            //    var request = _mapper.Map<IEnumerable<CateDtos>>(listCate);
            //    await _cacheService.SetCachedData(GetKeyCache.ListCategory, request);
            //    return request;
            //}
            //return ListCateCache;

            var listCate = _productRepo.ListCate();
            var request = _mapper.Map<IEnumerable<CateDtos>>(listCate);
            return request;
        }

        /// <summary>
        /// select category by CateID
        /// </summary>
        /// <param name="CateID"></param>
        /// <returns></returns>
        public async Task<CateDtos> GetCategorById(int CateID)
        {
            var cate = _productRepo.GetCategoryByID(CateID);
            if (cate == null)
            {
                return null;
            }
            else return _mapper.Map<CateDtos>(cate);
        }

        /// <summary>
        /// list content of category by cateID
        /// </summary>
        /// <param name="CateID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ContentProductDtos>> ListContentOfCategory(int CateID)
        {
            //var stringKey = GetKeyCache.CreateKey(GetKeyCache.ListContentByCate, CateID.ToString());
            //var ListContentCache = await _cacheService.GetCachedData<IEnumerable<ContentProductDtos>>(stringKey);
            //if (ListContentCache == null)
            //{
            //    var listContent = _productRepo.ListContentByCate(CateID);
            //    var request = _mapper.Map<IEnumerable<ContentProductDtos>>(listContent);
            //    await _cacheService.SetCachedData(stringKey, request);
            //    return request;
            //}
            //return ListContentCache;

            var listContent = _productRepo.ListContentByCate(CateID);
            var request = _mapper.Map<IEnumerable<ContentProductDtos>>(listContent);
            return request;
        }

        public async Task<ContentProductDtos> GetContentById(int contentID)
        {
            var content = _productRepo.GetContentProductByID(contentID);
            if (content == null)
            {
                return null;
            }
            else return _mapper.Map<ContentProductDtos>(content);
        }

        public async Task<List<ContentProductDtos>> GetListContent()
        {
            //var ListContentCache = await _cacheService.GetCachedData<List<ContentProductDtos>>(GetKeyCache.ListContent);
            //if (ListContentCache == null)
            //{
            //    var listContent = _productRepo.ListContent();
            //    var request = _mapper.Map<List<ContentProductDtos>>(listContent);
            //    await _cacheService.SetCachedData(GetKeyCache.ListContent, request);
            //    return request;
            //}
            //return ListContentCache;

            var listContent = _productRepo.ListContent();
            var request = _mapper.Map<List<ContentProductDtos>>(listContent);
            return request;
        }
    }
}
