using backend_cn.BusinessLogic.Product;
using backend_cn.Repositories.Product;
using backend_cn.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Emit;

namespace backend_cn.Controllers
{
    [Route("[controller]/[action]")]
    public class ProductController : ControllerBase
    {
        readonly ProductManage productManage;
        readonly ProductDetail productDetail;

        public ProductController(ProductManage productManage, ProductDetail productDetail)
        {
            this.productManage = productManage;
            this.productDetail = productDetail;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = productDetail.GetProducts();
            return new JsonResult(products);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult RemoveProduct([FromRoute] int id)
        {
            var products = productManage.RemoveById(id);
            return new JsonResult(products);
        }

        [HttpPost]
        public IActionResult AddProduct([FromBody] AddProductViewModel product)
        {
            var products = productManage.Add(product);
            return new JsonResult(products);
        }

        [HttpPost]
        public IActionResult UpdateProduct([FromBody] UpdateProductViewModel updateProduct)
        {
            var product = productManage.Update(updateProduct);
            return new JsonResult(product);
        }
    }
}
