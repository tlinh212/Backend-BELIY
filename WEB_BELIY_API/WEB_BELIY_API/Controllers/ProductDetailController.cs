using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_BELIY_API.DATA;
using WEB_BELIY_API.MODEL;

namespace WEB_BELIY_API.Controllers
{
    [Route("productdetail")]
    [ApiController]
    public class ProductDetailController : Controller
    {
        private readonly MyDbContext Context;
        public ProductDetailController(MyDbContext context)
        {
            Context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var listProduct = Context.ProductDetails.ToList();

            if(listProduct==null)
            {
                return NotFound();
            }    

            List<Object> list = new List<Object>();

            for (int i = 0; i < listProduct.Count; i++)
            {
                List<Image> listImage = Context.Images.Where(img => img.IDPro == listProduct[i].IDPro).ToList();
                
                List<String> listUrl = new List<String>();

                for (int j = 0; j < listImage.Count; j++)
                {
                    listUrl.Add(listImage[j].LinkImage);
                }

                var product = Context.Products.Where(p => p.IDPro == listProduct[i].IDPro).FirstOrDefault();

                var size = Context.Sizes.Where(s => s.IDSize == listProduct[i].IDSize).FirstOrDefault();

                Category category = Context.Categories.Where(c => c.IDCat == product.IDCat).FirstOrDefault();
                
                if (category == null)
                {
                    list.Add(new
                    {
                        idDetail = listProduct[i].IDProDetail,
                        product = product,
                        size = size,
                        category = 0,
                        images = listUrl,
                        quantity = listProduct[i].Quantity,
                    });
                }
                else
                {
                    Category parentCate = Context.Categories.Where(c => c.IDCat == category.IDParent).FirstOrDefault();

                    list.Add(new
                    {
                        idDetail = listProduct[i].IDProDetail,
                        product = product,
                        size = size,
                        category = new
                        {
                            idCat = category.IDCat,
                            nameCat = category.Name,
                            Gender = parentCate.Name
                        },
                        images = listUrl,
                        quantity = listProduct[i].Quantity,
                    });
                }
            }

            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var Product = Context.ProductDetails.SingleOrDefault(p => p.IDPro == id);
                
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
        public IActionResult Create(ProductDetail productDetail)
        {
            var productAdd = new ProductDetail
            {
                IDProDetail = Guid.NewGuid(),
                IDPro = productDetail.IDPro,
                IDSize = productDetail.IDSize,
            };

            Context.ProductDetails.Add(productAdd);
            Context.SaveChanges();

            return Ok(new
            {
                Success = true,
            });
        }
    }
}
