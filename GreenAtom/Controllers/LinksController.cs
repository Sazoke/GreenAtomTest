using GreenAtom.Data;
using GreenAtom.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GreenAtom.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LinksController : Controller
    {
        private ILinksRepository _linksRepository;
        
        public LinksController(OrdersContext context)
        {
            _linksRepository = new LinksRepository(context);
        }
        
        [HttpGet]
        public IActionResult Index([FromQuery]int orderId, [FromQuery]int productId)
        {
            return Ok(_linksRepository.GetLinkByIds(orderId, productId));
        }

        [HttpPost]
        public IActionResult CreateLink([FromQuery]int orderId, [FromQuery]int productId, [FromQuery]int count)
        {
            var link = _linksRepository.GetLinkByIds(orderId, productId);
            if (link is null)
                return BadRequest();
            _linksRepository.CreateLink(orderId, productId, count);
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteLink([FromQuery]int orderId, [FromQuery]int productId)
        {
            var link = _linksRepository.GetLinkByIds(orderId, productId);
            if (link is null)
                return NotFound();
            _linksRepository.DeleteLink(orderId, productId);
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateLink([FromQuery]int orderId, [FromQuery]int productId, [FromQuery]int count)
        {
            var link = _linksRepository.GetLinkByIds(orderId, productId);
            if (link is null)
                return NotFound();
            _linksRepository.UpdateLink(orderId, productId, count);
            return Ok();
        }
    }
}