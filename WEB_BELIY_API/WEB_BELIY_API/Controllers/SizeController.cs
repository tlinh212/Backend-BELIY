using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_BELIY_API.DATA;
using WEB_BELIY_API.MODEL;

namespace WEB_BELIY_API.Controllers
{
    [Route("size")]
    [ApiController]
    public class SizeController : Controller
    {
        private readonly MyDbContext Context;
        public SizeController(MyDbContext context)
        {
            Context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var List = Context.Sizes.ToList();

            return Ok(List);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var size = Context.Sizes.SingleOrDefault(s =>
            s.IDSize == Guid.Parse(id));

            if (size != null)
            {
                return Ok(size);
            }
            else
            {
                return NotFound();
            }
        }      

        [HttpPost]
        public IActionResult Create(Size size)
        {
            var Size = new Size
            {
                IDSize = Guid.NewGuid(),
                Name = size.Name,
                Description = size.Description,

            };

            Context.Sizes.Add(Size);
            Context.SaveChanges();

            return Ok(new
            {
                Success = true,
                Data = Size,
            });
        }
        [HttpPut]
        public IActionResult Edit(Size size)
        {
            try
            {
                var Size = Context.Sizes.SingleOrDefault(s=>s.IDSize == size.IDSize);
                
                if(Size == null)
                {
                    return NotFound();
                }

                Size.Name = size.Name;
                Size.Description = size.Description;

                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }
    }
}
