using RavenApiV25.Model;
using System.Linq;
using System.Web.Http;

namespace RavenApiV25.Controllers
{

    public class OrdersController : ApiController
    {
        public IHttpActionResult Get()
        {
            using (var session = Startup.Store.OpenSession())
            {
                var orders = session.Query<Order>()
                                .OrderByDescending(o => o.OrderedAt)
                                .Take(100)
                                .ToList();

                return Ok(orders);
            }
        }
    }
}
