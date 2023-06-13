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
    [Route("category")]
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
        [HttpGet("parent/{id}")]
        public IActionResult GetByIdParent(int id)
        {
            var Category = Context.Categories.Where(p => p.IDParent == id).ToList();
            if (Category != null)
            {
                return Ok(Category);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost("create")]
        public IActionResult Create(Category cate)
        {
            var Category = new Category
            {
                Name = cate.Name,
                IDParent = cate.IDParent,
            };

            Context.Categories.Add(Category);

            Context.SaveChanges();

            return Ok(new
            {
                Success = true,
            });
        }
        [HttpPut("update")]
        public IActionResult Edit(Category CategoryEdit)
        {
            try
            {
                var Category = Context.Categories.SingleOrDefault(c => c.IDCat == CategoryEdit.IDCat);
                
                if(Category == null)
                {
                    return NotFound();
                }
               
                Category.Name = Category.Name;

                Category.IDParent = Category.IDParent;

                Context.Update(Category);

                Context.SaveChanges();

                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var cat = Context.Categories.Where(c => c.IDCat == id).FirstOrDefault();
            if (cat == null || cat.IDParent == 0)
            {
                return NotFound();
            }
            
            var listProduct = Context.Products.Where(p => p.IDCat == id).ToList();
            for(int i = 0; i < listProduct.Count;i++)
            {
                listProduct[i].IDCat = 0;
                Context.Products.Update(listProduct[i]);
                Context.SaveChanges();
            }
            Context.Categories.Remove(cat);
            Context.SaveChanges();

            return Ok();
        }
    }
}

