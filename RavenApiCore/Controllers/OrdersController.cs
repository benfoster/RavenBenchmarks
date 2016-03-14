using Microsoft.AspNet.Mvc;
using Raven.Client;
using RavenApiCore.Model;
using System.Linq;
using System.Threading.Tasks;

namespace RavenApiCore.Controllers
{
    [Route("[controller]")]
    public class OrdersController : Controller
    {
        private readonly IDocumentStore store;

        public OrdersController(IDocumentStore store)
        {
            this.store = store;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            using (var session = store.OpenAsyncSession())
            {
                var orders = await session.Query<Order>()
                                .OrderByDescending(o => o.OrderedAt)
                                .Take(100)
                                .ToListAsync();

                return Ok(orders);
            }
        }
    }
}
