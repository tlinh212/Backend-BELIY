using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WEB_BELIY_API.DATA;
using WEB_BELIY_API.MODEL;

namespace WEB_BELIY_API.Controllers
{
    [Route("customer")]
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
        public class LoginForm
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
        [HttpPost("login")]
        public IActionResult Login(LoginForm customer)
        {
            var Cus = Context.Customers.SingleOrDefault(c =>
            c.Username.Equals(customer.Username) == true);

            if (Cus != null)
            {
                if (Customer.VerifyPassword(Cus.Password, customer.Username + customer.Password) == true)
                {
                    return Ok( new {
                        IDCus = Cus.IDCus,
                        Username = Cus.Username,
                        Email = Cus.Email,
                     
                    });
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
        [HttpPost("register")]
        public IActionResult Register(Customer customer)
        {
            var Cus = Context.Customers.SingleOrDefault(c =>
            c.Username.Equals(customer.Username) == true);

            if (Cus != null)
            {
                return BadRequest(new
                {
                    Data = "Username is exists",
                });
            }

            string HashPassword = Customer.HashPassword(customer.Username + customer.Password);

            var cus = new Customer
            {
                IDCus = Guid.NewGuid(),
                Username = customer.Username,
                Email = customer.Email,
                Password = HashPassword,

            };
                
            Context.Customers.Add(cus);
            Context.SaveChanges();

            return Ok(new
            {
                Success = true,
            });
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(string id)
        {
            var cus = Context.Customers.Where(c=>c.IDCus == Guid.Parse(id)).FirstOrDefault();
            if (cus == null)
            {
                return NotFound();
            }

            Context.Customers.Remove(cus);

            Context.SaveChanges();

            return Ok();
        }
    }
}
