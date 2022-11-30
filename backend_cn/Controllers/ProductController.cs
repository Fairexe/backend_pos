using backend_cn.Repositories;
using backend_cn.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Emit;

namespace backend_cn.Controllers
{
    [Route("[controller]/[action]")]
    public class ProductController : ControllerBase
    {
        readonly ProductRepository productRepository;

        public ProductController(ProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = productRepository.GetProducts();
            return new JsonResult(products);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult RemoveProduct([FromRoute] int id)
        {
            var products = productRepository.RemoveById(id);
            return new JsonResult(products);
        }

        [HttpPost]
        public IActionResult AddProduct([FromBody] AddProductViewModel product)
        {
            var products = productRepository.Add(product);
            return new JsonResult(products);
        }

        [HttpPost]
        public IActionResult UpdateProduct([FromBody] UpdateProductViewModel updateProduct)
        {
            var product = productRepository.Update(updateProduct);
            return new JsonResult(product);
        }
    }
}
