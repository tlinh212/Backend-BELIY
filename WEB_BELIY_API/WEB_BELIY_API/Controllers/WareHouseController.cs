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
    public class WareHouseController : ControllerBase
    {
        private readonly MyDbContext Context;
        public WareHouseController(MyDbContext context)
        {
            Context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var ListWareHouse = Context.WareHouses.ToList();

            return Ok(ListWareHouse);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var WareHouse = Context.WareHouses.SingleOrDefault(w => w.IDWare == id);
                if (WareHouse != null)
                {
                    return Ok(WareHouse);
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
        public IActionResult Create(string NameWare, string Address)
        {
            try
            {
                var WareHouse = new WareHouse
                {
                    IDWare = Guid.NewGuid(),
                    NameWare = NameWare,
                    Address = Address,
                };

                Context.WareHouses.Add(WareHouse);
                Context.SaveChanges();

                return Ok(new
                {
                    Success = true,
                    Data = WareHouse,
                });

            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public IActionResult Edit(string id, WareHouse warehouseedit)
        {
            try
            {
                var warehouse = Context.WareHouses.SingleOrDefault(w => w.IDWare == Guid.Parse(id));
                if (warehouse == null)
                {
                    return NotFound();
                }
                if (id != warehouse.IDWare.ToString())
                {
                    return BadRequest();
                }
                warehouse.NameWare = warehouse.NameWare;
                warehouse.Address = warehouse.Address;

                return Ok(warehouseedit);
            }
            catch
            {
                return BadRequest();
            }

        }

    }
}

