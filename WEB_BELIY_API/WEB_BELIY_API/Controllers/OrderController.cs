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
    [Route("order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly MyDbContext Context;
        public OrderController(MyDbContext context)
        {
            Context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        { List<Order> orders = (from order in Context.Orders
                       select order).ToList();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
           try
            {
                Order ord = Context.Orders.Where(o => o.IdOrder == id).FirstOrDefault();
                if (ord == null)
                    return NotFound();
                List<OrderDetail> list = Context.OrderDetails.Where(o => o.IDOrder == ord.IdOrder).ToList();
                List<Object> listPro = new List<Object>();
                for(int i = 0; i < list.Count;i++)
                {
                    ProductDetail pro = Context.ProductDetails.Where(p => p.IDProDetail == list[i].IDProDetail).FirstOrDefault();
                    Product product = Context.Products.Where(p => p.IDPro == pro.IDPro).FirstOrDefault();
                    Size s = Context.Sizes.Where(size => size.IDSize == pro.IDSize).FirstOrDefault();
                    listPro.Add(new
                    {
                        Name = product.NamePro,
                        Quantity = list[i].Quantity,
                        Size = s.Name,
                        Price = list[i].Price,
                    });
                }    
                return Ok(new
                {
                    order = new
                    {
                        IdOrder = ord.IdOrder,
                        OrderDate = ord.OrderDate,
                        Status = ord.Status,
                        PaymentMethod = ord.PaymentMethod,
                        PaymentDate = ord.PaymentDate,
                        TotalValue = ord.TotalValue,
                        Phone = ord.Phone,
                        Name = ord.Name,
                        Email = ord.Email,
                        IsCharge = ord.IsCharge,
                        NameStock = ord.NameStock,
                        DeliveryAddress = ord.DeliveryAddress,
                    },
                    products = listPro,
                });
            }
            catch
            {
                return BadRequest();
            }
        }

        public class AddProductOrder
        {
            public Guid IDPro { get; set; }
            public Guid IDSize { get; set; }
            public int QuantityOrder { get; set; }
            public int Price { get; set; }
        }

        public class AddOrder
        {
            public string Email { get; set; }
            public string Name { get; set; }        
            public string Phone{ get; set; }
            public string Province { get; set; }
            public string District { get; set; }
            public string Award { get; set; }
            public string AddressNumber { get; set; }
            
            public string Status { get; set; }
            public string PaymentMethods { get; set; }
            public Boolean IsCharge { get; set; }
            public List<AddProductOrder> Orders { get; set; }
        }
       public class StatusDTO
        {
            public bool IsCharge { get; set; }
            public Guid IDOrder { get; set; }

        }
        [HttpPut("change_status")]
        public IActionResult UpdateCharge(StatusDTO dto)
        { 
            var order = Context.Orders.FirstOrDefault(o => o.IdOrder == dto.IDOrder);
            if (order == null)
            {
                return BadRequest();
            }
            order.IsCharge = dto.IsCharge;
            Context.SaveChanges();
            return Ok(new { message="Đã cập nhật"});
        }
        public class StockInChargeDTO
        {
            public string NameStock { get; set; }


            public Guid IDOrder { get; set; }

        }
        [HttpPut("stock_incharge")]
        public IActionResult StockInCharge(StockInChargeDTO dto)
        {
            var order = Context.Orders.FirstOrDefault(o => o.IdOrder == dto.IDOrder);
            if (order == null)
            {
                return BadRequest();
            }
            order.NameStock = dto.NameStock;
            Context.SaveChanges();
            var listDetail = Context.OrderDetails.Where(o => o.IDOrder == dto.IDOrder).ToList();
            for(int i = 0; i < listDetail.Count;i++)
            {
                var productDetail = Context.ProductDetails.Where(p => p.IDProDetail == listDetail[i].IDProDetail).FirstOrDefault();
                if(productDetail==null)
                {
                    return NotFound();
                }
                productDetail.Quantity = productDetail.Quantity - listDetail[i].Quantity;
                Context.ProductDetails.Update(productDetail);
                Context.SaveChanges();
            }    
            return Ok(new { message = "Đã chỉ định kho" });
        }
        [HttpPost("create")]
        public IActionResult Create(AddOrder AddOrder)
        {
            Guid Id = Guid.NewGuid();

            double Total = 0;

            for(int i = 0; i < AddOrder.Orders.Count;i++)
            {
                Product product = Context.Products.Where(p => p.IDPro == AddOrder.Orders[i].IDPro).FirstOrDefault();
                if(product!=null)
                    Total += (product.SaleRate * (double)((100 - product.Discount) / 100)) * AddOrder.Orders[i].QuantityOrder;
            }

            List<string> address = new List<string>() { AddOrder.Award, AddOrder.District, AddOrder.Province };
                
            Order order = new Order
            {
                IdOrder = Id,
                OrderDate = System.DateTime.Now,
                Status = AddOrder.Status,
                Name =AddOrder.Name,
                Phone= AddOrder.Phone,
                IsCharge= false,
                Email = AddOrder.Email,
                PaymentMethod = AddOrder.PaymentMethods,
                PaymentDate = System.DateTime.Now,
                DeliveryAddress= AddOrder.AddressNumber+" " + String.Join(',',address),
                TotalValue = Total,
              
            };

            Context.Orders.Add(order);

            Context.SaveChanges();

            for(int i = 0; i < AddOrder.Orders.Count;i++)
            {
                ProductDetail pro = Context.ProductDetails.Where(p => p.IDPro == AddOrder.Orders[i].IDPro && p.IDSize == AddOrder.Orders[i].IDSize).FirstOrDefault();
                
                Product product = Context.Products.Where(p => p.IDPro == AddOrder.Orders[i].IDPro).FirstOrDefault();
                
                OrderDetail ord = new OrderDetail
                {
                    IDOrder = Id,
                    IDProDetail = pro.IDProDetail,
                    Price = product.Price * ((100-product.Discount)/100),
                    Quantity = AddOrder.Orders[i].QuantityOrder
                };

                Context.OrderDetails.Add(ord);

                Context.SaveChanges();
            }    

            return Ok();
        }
    }
}
