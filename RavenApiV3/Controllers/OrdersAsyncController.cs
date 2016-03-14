﻿using Raven.Client;
using RavenApiV3.Model;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace RavenApiV3.Controllers
{
    public class OrdersAsyncController : ApiController
    {
        public async Task<IHttpActionResult> Get()
        {
            using (var session = Startup.Store.OpenAsyncSession())
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
