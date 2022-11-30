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

        public ApiResultViewModel<List<ProductViewModel>> GetProducts()
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

                return new ApiResultViewModel<List<ProductViewModel>> { StatusCode = 0, Message = "Successful", Data = products};
            }
        }

        public ApiResultViewModel Add(AddProductViewModel product)
        {
            if(product.ProductName == "")
            {
                return new ApiResultViewModel { StatusCode = 1, Message = " Name cannot be null" };
            }
            if (product.ProductCode == "")
            {
                return new ApiResultViewModel { StatusCode = 1, Message = " Code cannot be null" };
            }
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var duplicatename =  context.Products.Any(item => item.ProductCode == product.ProductCode);
                    if (duplicatename)
                    {
                        return new ApiResultViewModel { StatusCode = 1, Message = " duplicated code" };
                    }

                    context.Products.Add(new Product { ProductCode = product.ProductCode, ProductName = product.ProductName, Price = product.ProductPrice, UnitId = product.UnitId });
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
                return new ApiResultViewModel { StatusCode = 0, Message = "Successful" };
            }
        }

        public ApiResultViewModel Update(UpdateProductViewModel updateProduct)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var oldProduct = context.Products.Single(item => item.ProductId == updateProduct.Pid);
                    if(oldProduct.ProductName == updateProduct.ProductName &&
                        oldProduct.Price == updateProduct.ProductPrice &&
                        oldProduct.UnitId == updateProduct.UnitId) 
                    {
                        return new ApiResultViewModel { StatusCode = 0, Message = "Not update" };
                    }
                    var receipt = context.ReceiptDetails.Any(item => item.ProductId == updateProduct.Pid);
                    if (receipt)
                    {
                        return new ApiResultViewModel { StatusCode = 1, Message = "Product " + updateProduct.ProductCode + " already use in receipt"};
                    }

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
                }
                return new ApiResultViewModel { StatusCode = 0, Message = "Successful"};
            }
        }

        public ApiResultViewModel RemoveById(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var receipt = context.ReceiptDetails.Any(item => item.ProductId == id);
                    if (receipt)
                    {
                        return new ApiResultViewModel { StatusCode = 1, Message = "cant delete product already in use by receipt" };
                    }
                    var product = context.Products.Single(item => item.ProductId == id);
                    context.Products.Remove(product);
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
                return new ApiResultViewModel { StatusCode = 0, Message = "Successful" };
            }
        }

    }
}
