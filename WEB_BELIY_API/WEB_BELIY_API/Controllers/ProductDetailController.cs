using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_BELIY_API.DATA;
using WEB_BELIY_API.MODEL;

namespace WEB_BELIY_API.Controllers
{
    [Route("productdetail")]
    [ApiController]
    public class ProductDetailController : Controller
    {
        private readonly MyDbContext Context;
        public ProductDetailController(MyDbContext context)
        {
            Context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var ListProduct = Context.ProductDetails.ToList();

            return Ok(ListProduct);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var Product = Context.ProductDetails.SingleOrDefault(p => p.IDPro == id);
                
                if (Product != null)
                {
                    return Ok(Product);
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Create(ProductDetail productDetail)
        {
            var productAdd = new ProductDetail
            {
                IDProDetail = Guid.NewGuid(),
                IDPro = productDetail.IDPro,
                IDSize = productDetail.IDSize,
            };

            Context.ProductDetails.Add(productAdd);
            Context.SaveChanges();

            return Ok(new
            {
                Success = true,
            });
        }
    }
}
