using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_BELIY_API.MODEL;
using WEB_BELIY_API.DATA;

namespace WEB_BELIY_API.Controllers
{
    [Route("product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly MyDbContext Context;
        public ProductController(MyDbContext context)
        {
            Context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var ListProduct = Context.Products.ToList();

            return Ok(ListProduct);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var Product = Context.Products.SingleOrDefault(p => p.IDPro == id);
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

        [HttpGet("cat/{id}")]
        public IActionResult GetByIdCat(int id)
        {
            try
            {
                var Product = Context.Products.Where(p => p.IDCat == id).ToList();

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
        public IActionResult Create(Product product)
        {
            var productAdd = new Product
            {
                IDPro = Guid.NewGuid(),
                NamePro = product.NamePro,
                IDCat = product.IDCat,
                Price = product.Price,
                Description = product.Description,
                Discount = product.Discount,
                SaleRate = product.SaleRate,
            };

            Context.Products.Add(productAdd);
            Context.SaveChanges();

            return Ok(new
            {
                Success = true,
                Data = productAdd,
            });

        }

        [HttpPut()]
        public IActionResult Edit(Product productedit)
        {
            try
            {
                var product = Context.Products.SingleOrDefault(p => p.IDPro == productedit.IDPro);
                
                if (product == null)
                {
                    return NotFound();
                }

                product.NamePro = productedit.NamePro;
                product.IDCat = productedit.IDCat;
                product.Price = productedit.Price;
                product.Description = productedit.Description;
                product.Discount = productedit.Discount;
                product.SaleRate = productedit.SaleRate;

                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }


    }
}
