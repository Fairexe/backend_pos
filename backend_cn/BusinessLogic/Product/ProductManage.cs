using backend_cn.APIResult;
using backend_cn.Constants;
using backend_cn.Repositories.Product;
using backend_cn.ViewModels;

namespace backend_cn.BusinessLogic.Product
{
    public class ProductManage : IAPIResult
    {
        readonly IProductRepository productRepository;

        public ProductManage(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public ApiResultViewModel Add(AddProductViewModel product)
        {
            try
            {
                if (string.IsNullOrEmpty(product.ProductCode) || string.IsNullOrEmpty(product.ProductName))
                    throw new Exception(ProductMessage.ISNULL);

                if (productRepository.CheckDuplicatedProductCode(product.ProductCode))
                    throw new Exception(ProductMessage.DUPLICATED);

                productRepository.Add(product);
                return this.ApiResultViewModel(APICode.Success, APIMessage.SUCCESSFUL);
            }
            catch (Exception ex)
            {
                return this.ApiResultViewModel(APICode.InvalidArgument, ex.ToString());
            }
        }

        public ApiResultViewModel RemoveById(int id)
        {
            try
            {
                if (productRepository.CheckProductIsUsed(id))
                {
                    return this.ApiResultViewModel(APICode.InvalidArgument, ProductMessage.ALREADY_USE);
                }
                productRepository.RemoveById(id);
                return this.ApiResultViewModel(APICode.Success, APIMessage.SUCCESSFUL);

            }
            catch (Exception ex)
            {
                return this.ApiResultViewModel(APICode.InvalidArgument, ex.ToString());
            }
        }

        public ApiResultViewModel Update(UpdateProductViewModel updateProduct)
        {
            try
            {
                var oldProduct = productRepository.GetProductById(updateProduct.Pid);
                if (oldProduct.ProductName == updateProduct.ProductName &&
                    oldProduct.ProductPrice == updateProduct.ProductPrice &&
                    oldProduct.UnitId == updateProduct.UnitId)
                {
                    return this.ApiResultViewModel(APICode.Success, APIMessage.SUCCESSFUL);
                }
                if (productRepository.CheckProductIsUsed(updateProduct.Pid))
                {
                    return this.ApiResultViewModel(APICode.InvalidArgument, ProductMessage.ALREADY_USE);
                }
                productRepository.Update(updateProduct);
                return this.ApiResultViewModel(APICode.Success, APIMessage.SUCCESSFUL);
            }
            catch (Exception ex)
            {
                return this.ApiResultViewModel(APICode.InvalidArgument, ex.ToString());
            }
        }
    }
}


