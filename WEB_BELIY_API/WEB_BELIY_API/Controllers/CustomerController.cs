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

        [HttpPost("login")]
        public IActionResult Login(string UserName, string Password)
        {
            var Cus = Context.Customers.SingleOrDefault(c =>
            c.Email.Equals(UserName) == true);

            if (Cus != null)
            {
                if (Customer.VerifyPassword(Cus.Password, UserName + Password) == true)
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
        [HttpPost("register")]
        public IActionResult Register(Customer customer)
        {
            var Cus = Context.Customers.SingleOrDefault(c =>
            c.Email.Equals(customer.Email) == true);

            if (Cus != null)
            {
                return BadRequest(new
                {
                    Data = "Email is exists",
                });
            }

            string HashPassword = Customer.HashPassword(customer.Email + customer.Password);

            var cus = new Customer
            {
                IDCus = Guid.NewGuid(),
                Name = customer.Name,
                Email = customer.Email,
                Password = HashPassword,
                PhoneNumber = customer.PhoneNumber,

            };

            Context.Customers.Add(cus);
            Context.SaveChanges();

            return Ok(new
            {
                Success = true,
            });
        }
    }
}
