using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WEB_BELIY_API.DATA;
using WEB_BELIY_API.MODEL;

namespace WEB_BELIY_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly MyDbContext Context;
        public CustomerController(MyDbContext context)
        {
            Context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var ListCustomer = Context.Customers.ToList();

            return Ok(ListCustomer);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var Customer = Context.Customers.SingleOrDefault(c =>
            c.IdCus == id);
            if (Customer != null)
            {
                return Ok(Customer);
            }
            else
            {
                return NotFound();
            }
        }

        

        [HttpGet("{UserName},{Password}")]
        public IActionResult Login(string UserName, string Password)
        {
            var Cus = Context.Customers.SingleOrDefault(c =>
            c.Email.Equals(UserName) == true);

            if (Cus != null)
            {
                if(Customer.VerifyPassword(Cus.Password, UserName+Password) == true)
                {
                    return Ok(Cus);
                }    
                else
                {
                    return NotFound();
                }    
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public IActionResult Register(string Name, string Email, string Password, string PhoneNumber)
        {

            var Cus = Context.Customers.SingleOrDefault(c =>
            c.Email.Equals(Email) == true);

            if (Cus != null)
            {
                return BadRequest(new
                {
                    Data = "Email is exists",
                });
            }

            string HashPassword = Customer.HashPassword(Email + Password);

            var cus = new Customer
            {
                IdCus = Guid.NewGuid(),
                Name = Name,
                Email = Email,
                Password = HashPassword, 
                PhoneNumber = PhoneNumber, 
                
            };

            Context.Customers.Add(cus);
            Context.SaveChanges();

            return Ok(new
            {
                Success = true,
                Data = cus,
            });

        }
    }
}
