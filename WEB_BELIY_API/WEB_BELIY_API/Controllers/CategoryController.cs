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
    public class CategoryController : ControllerBase
    {
        private readonly MyDbContext Context;
        public CategoryController(MyDbContext context)
        {
            Context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var ListCategory = Context.Categories.ToList();

            return Ok(ListCategory);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var Category = Context.Categories.SingleOrDefault(c =>
            c.IDCat == id);
            if (Category != null)
            {
                return Ok(Category);
            }
            else
            {
                return NotFound();
            }
        }
       
        [HttpPost]
        public IActionResult Create(string Name, int IDParent)
        {
            var Category = new Category
            {
                Name = Name,
                IDParent = IDParent,
            };

            Context.Categories.Add(Category);
            Context.SaveChanges();

            return Ok(new
            {
                Success = true,
                Data = Category,
            });
        }
        [HttpPut("{id}")]
        public IActionResult Edit(int id, Category CategoryEdit)
        {
            try
            {
                var Category = Context.Categories.SingleOrDefault(c => c.IDCat == id);
                if
                    (Category == null)
                {
                    return NotFound();
                }
               
                if (id != Category.IDCat)
                {
                    return BadRequest();
                }

                Category.Name = Category.Name;
                Category.IDParent = Category.IDParent;

                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }
    }
}

