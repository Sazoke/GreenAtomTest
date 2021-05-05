using GreenAtom.Data;
using GreenAtom.Models;
using GreenAtom.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GreenAtom.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : Controller
    {
        private IProductsRepository _productsRepository;
        
        public ProductsController(OrdersContext context)
        {
            _productsRepository = new ProductsRepository(context);
        }

        [HttpGet]
        public IActionResult Index([FromQuery] int id)
        {
            return Ok(_productsRepository.GetProductById(id));
        }

        [HttpPost]
        public IActionResult CreateProduct([FromQuery] string name)
        {
            var product = new Product() {Name = name};
            _productsRepository.InsertProduct(product);
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteOrder([FromQuery] int id)
        {
            var product = _productsRepository.GetProductById(id);
            if (product is null)
                return NotFound();
            _productsRepository.DeleteProduct(id);
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateOrder([FromQuery] int id, [FromQuery] string name)
        {
            _productsRepository.UpgradeProduct(id, new Product() {Id = id, Name = name});
            return Ok();
        }
    }
}