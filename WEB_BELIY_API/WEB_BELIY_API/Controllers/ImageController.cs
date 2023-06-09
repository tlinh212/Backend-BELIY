﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_BELIY_API.MODEL;
using WEB_BELIY_API.DATA;

namespace WEB_BELIY_API.Controllers
{
    [Route("image")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly MyDbContext Context;
        public ImageController(MyDbContext context)
        {
            Context = context;
        }
        [HttpGet("idpro/{id}")]
        public IActionResult GetByProductId(Guid id)
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
        public IActionResult Create(Image image)
        {
            try
            {
                var Image = new Image
                {
                    IDImage = Guid.NewGuid(),
                    IDPro = image.IDPro,                  
                    LinkImage = image.LinkImage,
                };

                Context.Images.Add(Image);
                Context.SaveChanges();

                return Ok(new
                {
                    Success = true,
                });

            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult Edit(Image ImageEdit)
        {
            try
            {
                var image = Context.Images.SingleOrDefault(i => i.IDImage == ImageEdit.IDImage);

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
