using backend_cn.ViewModels;

namespace backend_cn.Repositories.Product
{
    public interface IProductRepository
    {
        List<ProductViewModel> GetProducts();
        bool CheckDuplicatedProductCode(string coede);
        void Add(AddProductViewModel product);
        void Update(UpdateProductViewModel updateProduct);
        void RemoveById(int id);
        bool CheckProductIsUsed(int id);
        ProductViewModel GetProductById(int id);
    }
}
