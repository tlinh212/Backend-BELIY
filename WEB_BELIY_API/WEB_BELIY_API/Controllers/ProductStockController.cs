using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_BELIY_API.DATA;
using WEB_BELIY_API.MODEL;

namespace WEB_BELIY_API.Controllers
{
    [Route("productstock")]
    [ApiController]
    public class ProductStockController : ControllerBase
    {
        private readonly MyDbContext Context;
        public ProductStockController(MyDbContext context)
        {
            Context = context;
        }

        [HttpPost]
        public IActionResult Create(ProductStock productStock)
        {
            var productAdd = new ProductStock
            {
                IDProStock = Guid.NewGuid(),
                IDProDetail = productStock.IDProDetail,
                IDStock = productStock.IDStock,
            };

            Context.ProductStocks.Add(productAdd);
            Context.SaveChanges();

            return Ok(new
            {
                Success = true,
            });
        }
    }
}
