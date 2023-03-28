using backend_cn.APIResult;
using backend_cn.Constants;
using backend_cn.Repositories.Product;
using backend_cn.ViewModels;

namespace backend_cn.BusinessLogic.Product
{
    public class ProductDetail: IAPIResult
    {
        readonly IProductRepository productRepository;

        public ProductDetail(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public ApiResultViewModel<List<ProductViewModel>> GetProducts()
        {

            var products = productRepository.GetProducts();
            return this.ApiResultViewModel(APICode.Success, APIMessage.SUCCESSFUL, products);
        }
    }
}
