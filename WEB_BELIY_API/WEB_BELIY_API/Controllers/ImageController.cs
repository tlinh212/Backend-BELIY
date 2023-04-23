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
    public class ImageController : ControllerBase
    {
        private readonly MyDbContext Context;
        public ImageController(MyDbContext context)
        {
            Context = context;
        }
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var ListImage = Context.Images.ToList().Where(i => i.IDPro == id);
                if (ListImage != null)
                {
                    return Ok(ListImage);
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
        public IActionResult Create(Guid IDPro, string LinkImage)
        {
            try
            {
                var Image = new Image
                {
                    IDImage = Guid.NewGuid(),
                    IDPro = IDPro,                  
                    LinkImage = LinkImage,
                };

                Context.Images.Add(Image);
                Context.SaveChanges();

                return Ok(new
                {
                    Success = true,
                    Data = Image,
                });

            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public IActionResult Edit(string id, Image ImageEdit)
        {
            try
            {
                var image = Context.Images.SingleOrDefault(i => i.IDImage == Guid.Parse(id));

                if (image == null)
                {
                    return NotFound();
                }

                image.LinkImage = image.LinkImage;
               
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
                var image = Context.Images.SingleOrDefault(i => i.IDImage == Guid.Parse(id));

                if (image == null)
                {
                    return NotFound();
                }

                Context.Images.Remove(image);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
