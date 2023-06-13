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

            return Ok();
        }
    }
}
