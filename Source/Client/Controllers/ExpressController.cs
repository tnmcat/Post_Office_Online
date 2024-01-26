using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostOffice.API.Data.Context;
using PostOffice.Client.Models;

namespace PostOffice.Client.Controllers
{
    public class ExpressController : Controller
    {
        private readonly AppDbContext _context;
        public ExpressController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult ExpressService()
        {
            var prices = from p in _context.ServicePrices
            join w in _context.WeightScopes on p.scope_weight_id equals w.id
                         join z in _context.ZoneTypes on p.zone_type_id equals z.id
                         join s in _context.ParcelServices on p.service_id equals s.service_id
                         where w.id == p.scope_weight_id && z.id == p.zone_type_id
                         orderby s.service_id == 2, z.zone_description
                         select new ServiceOrderExpress
                         {
                             Name = s.name,
                             ServicePrice = p.service_price,
                             WeightScope = w.description,
                             ZoneDescription = z.zone_description
                         };

            return View(prices.ToList());
        }
    }
}
