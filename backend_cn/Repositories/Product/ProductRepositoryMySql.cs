using backend_cn.Context;
using backend_cn.ViewModels;
using backend_cn.Models;

namespace backend_cn.Repositories.Product
{
    public class ProductRepositoryMySql : IProductRepository
    {
        readonly PosDbContext context;

        public ProductRepositoryMySql(PosDbContext context)
        {
            this.context = context;
        }

        public List<ProductViewModel> GetProducts()
        {
            using (var transaction = context.Database.BeginTransaction())
            {

                var products = (from p in context.Products
                                select new ProductViewModel
                                {
                                    Pid = p.ProductId,
                                    ProductCode = p.ProductCode,
                                    ProductName = p.ProductName,
                                    ProductPrice = p.Price,
                                    UnitId = p.UnitId
                                })
                             .OrderBy(x => x.Pid)
                             .ToList();

                return products;
            }
        }

        public ProductViewModel GetProductById(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                var product = (from p in context.Products
                               where p.ProductId == id
                               select new ProductViewModel
                               {
                                   Pid = p.ProductId,
                                   ProductCode = p.ProductCode,
                                   ProductName = p.ProductName,
                                   ProductPrice = p.Price,
                                   UnitId = p.UnitId
                               })
                             .Single();

                return product;
            }
        }

        public bool CheckDuplicatedProductCode(string code)
        {
            var duplicatename = context.Products.Any(item => item.ProductCode == code);
            return duplicatename;
        }

        public bool CheckProductIsUsed(int id)
        {
            var receipt = context.ReceiptDetails.Any(item => item.ProductId == id);
            return receipt;
        }
        public void Add(AddProductViewModel product)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    context.Products.Add(new Models.Product
                    {
                        ProductCode = product.ProductCode,
                        ProductName = product.ProductName,
                        Price = product.ProductPrice,
                        UnitId = product.UnitId
                    });
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void Update(UpdateProductViewModel updateProduct)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var product = context.Products.Single(item => item.ProductId == updateProduct.Pid);
                    product.ProductCode = updateProduct.ProductCode;
                    product.ProductName = updateProduct.ProductName;
                    product.Price = updateProduct.ProductPrice;
                    product.UnitId = updateProduct.UnitId;
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception(ex.ToString());
                }
            }
        }

        public void RemoveById(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var product = context.Products.Single(item => item.ProductId == id);
                    context.Products.Remove(product);
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

    }
}
