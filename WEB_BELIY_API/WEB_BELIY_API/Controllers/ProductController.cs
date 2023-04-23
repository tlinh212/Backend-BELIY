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
    [Route("api/[controller]")]
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
        public IActionResult GeById( Guid id )
        {
            try
            {
                var Product = Context.Products.SingleOrDefault(p =>p.IDPro == id);
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
        public IActionResult Create(string NamePro, int IDCat, string IDImp, double Price, int Quantity, string Description, double Discount, double SaleRate)
        {
            var Product = new Product
            {
                IDPro = Guid.NewGuid(),
                NamePro = NamePro,
                IDCat = IDCat,
                IDImp = Guid.Parse(IDImp),            
                Price = Price,
                Quantity = Quantity,
                Description = Description,
                Discount = Discount,
                SaleRate = SaleRate

            };

            Context.Products.Add(Product);
            Context.SaveChanges();

            return Ok(new
            {
                Success = true,
                Data = Product,
            });

        }
       
        [HttpPut("{id}")]
        public IActionResult Edit(string id, Product productedit  )
        {
            try
            {
                var product = Context.Products.SingleOrDefault(pp => pp.IDPro == Guid.Parse(id));
                if (product == null)
                {
                    return NotFound();
                }
                /////update 
                if (id !=product.IDPro.ToString())
                {
                    return BadRequest();
                }
          
                product.NamePro = product.NamePro;
                product.IDCat = product.IDCat;
                product.IDImp = product.IDImp;                
                product.Price = product.Price;
                product.Quantity = product.Quantity;
                product.Description = product.Description;
                product.Discount = product.Discount;
                product.SaleRate = product.SaleRate;

                return Ok(productedit);
            }
            catch
            {
                return BadRequest();
            }

        }


    }
}
