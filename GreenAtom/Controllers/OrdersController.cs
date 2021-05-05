using System.Collections.Generic;
using System.Linq;
using GreenAtom.Data;
using GreenAtom.Models;
using GreenAtom.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GreenAtom.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : Controller
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IProductsRepository _productsRepository;
        private readonly ILinksRepository _linksRepository;
        
        public OrdersController(OrdersContext context)
        {
            _ordersRepository = new OrdersRepository(context);
            _productsRepository = new ProductsRepository(context);
            _linksRepository = new LinksRepository(context);
        }

        [HttpGet]
        public IActionResult Index([FromQuery] int? id)
        {
            if (id is null)
                return  Ok(_ordersRepository.GetOrders());
            var order = _ordersRepository.GetOrderById((int)id);
            var products = _linksRepository.GetProductIds(order.Id)
                .Select(p => _productsRepository.GetProductById(p));
            return Ok(new {order, products});
        }

        [HttpPost]
        public IActionResult CreateOrder([FromQuery] string name)
        {
            var order = new Order() {Name = name};
            _ordersRepository.InsertOrder(order);
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteOrder([FromQuery] int id)
        {
            var order = _ordersRepository.GetOrderById(id);
            if (order is null)
                return NotFound();
            _ordersRepository.DeleteOrder(id);
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateOrder([FromQuery] int id, [FromQuery] string name)
        {
            _ordersRepository.UpdateOrder(id, new Order() {Id = id, Name = name});
            return Ok();
        }
    }
}