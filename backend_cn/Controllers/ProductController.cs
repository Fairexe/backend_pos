using backend_cn.Repositories;
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

        [HttpPost]
        public IActionResult RemoveProduct(int id)
        {
            var products = productRepository.RemoveById(id);
            return new JsonResult(products);
        }

        [HttpPost]
        public IActionResult AddProduct(string code, string name, decimal price, int unitId)
        {
            var products = productRepository.Add(code, name, price, unitId);
            return new JsonResult(products);
        }

        [HttpPost]
        public IActionResult UpdateProduct(int id, string code, string name, decimal price, int unitId)
        {
            var products = productRepository.Update(id, code, name, price, unitId);
            return new JsonResult(products);
        }
    }
}
