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
    [Route("stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly MyDbContext Context;
        public StockController(MyDbContext context)
        {
            Context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var List = Context.Stocks.ToList();

            return Ok(List);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                var stock = Context.Stocks.SingleOrDefault(st => st.IDStock == Guid.Parse(id));
                
                if (stock != null)
                {
                    return Ok(stock);
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
        public IActionResult Create(Stock stock)
        {
            try
            {
                var st = new Stock
                {
                    IDStock = Guid.NewGuid(),
                    Name = stock.Name,
                    Address = stock.Address,
                };

                Context.Stocks.Add(st);
                Context.SaveChanges();

                return Ok();

            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut]
        public IActionResult Edit(Stock stockEdit)
        {
            try
            {
                var st = Context.Stocks.SingleOrDefault(s => s.IDStock == stockEdit.IDStock);
                
                if (st == null)
                {
                    return NotFound();
                }

                st.Name = stockEdit.Name;
                st.Address = stockEdit.Address;

                return Ok(st);
            }
            catch
            {
                return BadRequest();
            }

        }

    }
}

