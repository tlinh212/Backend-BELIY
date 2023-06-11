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
    [Route("product")]
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
            List<Product> listProduct = Context.Products.ToList();

            List<Object> list = new List<Object>();

            for(int i = 0; i < listProduct.Count; i++)
            {
                List<Image> listImage = Context.Images.Where(img => img.IDPro == listProduct[i].IDPro).ToList();
                List<String> listUrl = new List<String>();
                for (int j = 0; j < listImage.Count; j++)
                {
                    listUrl.Add(listImage[j].LinkImage);
                }
                List<ProductDetail> listProDetail = Context.ProductDetails.Where(p => p.IDPro == listProduct[i].IDPro).ToList();
                List<Object> listSize = new List<Object>();
                for(int j = 0; j < listProDetail.Count; j ++)
                {
                    Size s = Context.Sizes.Where(s => s.IDSize == listProDetail[j].IDSize).FirstOrDefault();
                    listSize.Add( new {
                        IDSize = s.IDSize,
                        Name = s.Name,
                    });
                }

                Category category = Context.Categories.Where(c => c.IDCat == listProduct[i].IDCat).FirstOrDefault();
                Category parentCate = Context.Categories.Where(c => c.IDCat == category.IDParent).FirstOrDefault();

                list.Add(new { 
                    product = listProduct[i],
                    sizes = listSize,
                    category = new
                    {
                        idCat = category.IDCat,
                        nameCat = category.Name,
                        Gender = parentCate.Name
                    },
                    images = listUrl,
                });
            }    

            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var Product = Context.Products.SingleOrDefault(p => p.IDPro == id);
                if (Product != null)
                {
                    List<Image> listImage = Context.Images.Where(img => img.IDPro == Product.IDPro).ToList();
                    List<String> listUrl = new List<String>();
                    for (int j = 0; j < listImage.Count; j++)
                    {
                        listUrl.Add(listImage[j].LinkImage);
                    }
                    List<ProductDetail> listProDetail = Context.ProductDetails.Where(p => p.IDPro == Product.IDPro).ToList();
                    List<Object> listSize = new List<Object>();
                    for (int j = 0; j < listProDetail.Count; j++)
                    {
                        Size s = Context.Sizes.Where(s => s.IDSize == listProDetail[j].IDSize).FirstOrDefault();
                        listSize.Add(new
                        {
                            IDSize = s.IDSize,
                            Name = s.Name,
                        });
                    }

                    Category category = Context.Categories.Where(c => c.IDCat == Product.IDCat).FirstOrDefault();
                    Category parentCate = Context.Categories.Where(c => c.IDCat == category.IDParent).FirstOrDefault();

                    return Ok(new
                    {
                        product = Product,
                        sizes = listSize,
                        category = new
                        {
                            idCat = category.IDCat,
                            nameCat = category.Name,
                            Gender = parentCate.Name
                        },
                        images = listUrl,
                    });
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

        [HttpGet("cat/{id}")]
        public IActionResult GetByIdCat(int id)
        {
            try
            {
                var listProduct = Context.Products.Where(p => p.IDCat == id).ToList();
                 
                if (listProduct != null)
                {
                    List<Object> list = new List<Object>();

                    for (int i = 0; i < listProduct.Count; i++)
                    {
                        List<Image> listImage = Context.Images.Where(img => img.IDPro == listProduct[i].IDPro).ToList();
                        List<String> listUrl = new List<String>();
                        for (int j = 0; j < listImage.Count; j++)
                        {
                            listUrl.Add(listImage[j].LinkImage);
                        }
                        List<ProductDetail> listProDetail = Context.ProductDetails.Where(p => p.IDPro == listProduct[i].IDPro).ToList();
                        List<Object> listSize = new List<Object>();
                        for (int j = 0; j < listProDetail.Count; j++)
                        {
                            Size s = Context.Sizes.Where(s => s.IDSize == listProDetail[j].IDSize).FirstOrDefault();
                            listSize.Add(new
                            {
                                IDSize = s.IDSize,
                                Name = s.Name,
                            });
                        }
                        Category category = Context.Categories.Where(c => c.IDCat == listProduct[i].IDCat).FirstOrDefault();
                        Category parentCate = Context.Categories.Where(c => c.IDCat == category.IDParent).FirstOrDefault();
                        list.Add(new
                        {
                            product = listProduct[i],
                            sizes = listSize,
                            category = new
                            {
                                idCat = category.IDCat,
                                nameCat = category.Name,
                                Gender = parentCate.Name
                            },
                            images = listUrl,
                        });
                    }

                    return Ok(list);
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

        [HttpGet("parent/{id}")]
        public IActionResult GetByIdParent(int id)
        {
            try
            {
                List<Category> listCat = Context.Categories.Where(c => c.IDParent == id).ToList();

                List<Object> list = new List<Object>();

                for(int i = 0; i < listCat.Count; i++)
                {
                    var listProduct = Context.Products.Where(p => p.IDCat == listCat[i].IDCat).ToList();

                    if (listProduct != null)
                    {
                        for (int j = 0; j < listProduct.Count; j++)
                        {
                            List<Image> listImage = Context.Images.Where(img => img.IDPro == listProduct[j].IDPro).ToList();

                            List<String> listUrl = new List<String>();

                            for (int k = 0; k < listImage.Count; k++)
                            {
                                listUrl.Add(listImage[k].LinkImage);
                            }

                            List<ProductDetail> listProDetail = Context.ProductDetails.Where(p => p.IDPro == listProduct[j].IDPro).ToList();

                            List<Object> listSize = new List<Object>();

                            for (int k = 0; k < listProDetail.Count; k++)
                            {
                                Size s = Context.Sizes.Where(s => s.IDSize == listProDetail[k].IDSize).FirstOrDefault();
                                listSize.Add(new
                                {
                                    IDSize = s.IDSize,
                                    Name = s.Name,
                                });
                            }
                            
                            Category parentCate = Context.Categories.Where(c => c.IDCat == listCat[i].IDParent).FirstOrDefault();

                            list.Add(new
                            {
                                product = listProduct[j],
                                sizes = listSize,
                                category = new
                                {
                                    idCat = listCat[i].IDCat,
                                    nameCat = listCat[i].Name,
                                    Gender = parentCate.Name
                                },
                                images = listUrl,
                            });
                        }
                    }
                }    

                return Ok(list);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("create")]
        public IActionResult Create(Product product)
        {
            var productAdd = new Product
            {
                IDPro = Guid.NewGuid(),
                NamePro = product.NamePro,
                IDCat = product.IDCat,
                Price = product.Price,
                Description = product.Description,
                Discount = product.Discount,
                SaleRate = product.SaleRate,
            };

            Context.Products.Add(productAdd);
            Context.SaveChanges();

            return Ok(new
            {
                Success = true,
                Data = productAdd,
            });

        }

        [HttpPut("update")]
        public IActionResult Edit(Product productedit)
        {
            try
            {
                var product = Context.Products.SingleOrDefault(p => p.IDPro == productedit.IDPro);
                
                if (product == null)
                {
                    return NotFound();
                }

                product.NamePro = productedit.NamePro;
                product.IDCat = productedit.IDCat;
                product.Price = productedit.Price;
                product.Description = productedit.Description;
                product.Discount = productedit.Discount;
                product.SaleRate = productedit.SaleRate;

                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }

        public class CheckProductQuantity
        {
            public string IDPro { get; set; }

            public string IDSize { get; set; }
            public int QuantityOrder { get; set; }
        }

        public class DataCheck
        {
            public List<CheckProductQuantity> Orders { get; set; }
        }

        [HttpPost("checkquantity")]
        public IActionResult CheckQuantity(DataCheck Data)
        {
            for(int i = 0; i < Data.Orders.Count;i++)
            {           
                ProductDetail ProDetail = Context.ProductDetails.Where(p => p.IDPro == Guid.Parse(Data.Orders[i].IDPro) && p.IDSize == Guid.Parse(Data.Orders[i].IDSize)).FirstOrDefault();

                if(ProDetail==null)
                {
                    return NotFound(Data.Orders[i]);
                }    

                List<ProductStock> listProStock = Context.ProductStocks.Where(p => p.IDProDetail == ProDetail.IDProDetail).ToList();
                
                int Quantity = 0;
                
                for (int k = 0; k < listProStock.Count; k++)
                {
                    Quantity += listProStock[k].Quantity;
                }

                if (Quantity < Data.Orders[i].QuantityOrder)
                {
                    Product product = Context.Products.Where(p => p.IDPro == Guid.Parse(Data.Orders[i].IDPro)).FirstOrDefault();
                    String Error = "Sản phẩm "+ product.NamePro +" chỉ còn " + Quantity + " sản phẩm";
                    return Ok(Error);
                }    
                   
            }    
            return Ok();
        }
    }
}
