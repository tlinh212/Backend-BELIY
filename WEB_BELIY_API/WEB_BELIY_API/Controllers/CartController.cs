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
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly MyDbContext Context;
        public CartController(MyDbContext context)
        {
            Context = context;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var ListCart = Context.Carts.ToList().Where(i => i.IDCus == id);
                if (ListCart != null)
                {
                    return Ok(ListCart);
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
        public IActionResult Create(Guid IDPro, Guid IDCus)
        {
            try
            {
                var cart = new Cart
                {
                    IdCart = Guid.NewGuid(),
                    IDPro = IDPro,
                    IDCus = IDCus,
                    Quantity = 1
                };

                Context.Carts.Add(cart);

                Context.SaveChanges();

                return Ok(new
                {
                    Success = true,
                    Data = cart,
                });

            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public IActionResult Edit(string id, int quantity)
        {
            try
            {
                var cart = Context.Carts.SingleOrDefault(c => c.IdCart == Guid.Parse(id));

                if (cart == null)
                {
                    return NotFound();
                }

                cart.Quantity = quantity;

                Context.SaveChanges();

                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                var cart = Context.Carts.SingleOrDefault(i => i.IdCart == Guid.Parse(id));

                if (cart == null)
                {
                    return NotFound();
                }

                Context.Carts.Remove(cart);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
