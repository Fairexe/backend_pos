using backend_cn.Context;
using backend_cn.Models;
using backend_cn.ViewModels;

namespace backend_cn.Repositories
{
    public class ProductRepository
    {
        readonly PosDbContext context;

        public ProductRepository(PosDbContext context)
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
                                 pid = p.ProductId,
                                 productCode = p.ProductCode,
                                 productName = p.ProductName,
                                 productPrice = p.Price,
                                 unitId = p.UnitId
                             })
                             .OrderBy(x => x.pid)
                             .ToList();

                return products;
            }
        }

        public string Add(string code, string name, decimal price, int unitId)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {

                    context.Products.Add(new Product { ProductCode = code, ProductName = name, Price = price, UnitId = unitId });
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
                return "Add Product " + name + " Successful";
            }
        }

        public string Update(int id, string code, string name, decimal price, int unitId)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var product = context.Products.Single(item => item.ProductId == id);
                    product.ProductCode = code;
                    product.ProductName = name;
                    product.Price = price;
                    product.UnitId = unitId;
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
                return "Update Unit " + id + " Successful";
            }
        }

        public string RemoveById(int id)
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
                }
                return "Remomve Product Id:" + id + " Successful";
            }
        }

    }
}
