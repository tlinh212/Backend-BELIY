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
    [Route("employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly MyDbContext Context;
        public EmployeeController(MyDbContext context)
        {
            Context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var List = Context.Employees.ToList();
            return Ok(List);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var emp = Context.Employees.Where(e=>e.IDEmp == Guid.Parse(id)).FirstOrDefault();
            if(emp==null)
            {
                return NotFound();
            }    
            return Ok(emp);
        }
        [HttpPost("create")]
        public IActionResult Create(Employee Emp)
        {
            try
            {
                Employee employee = Context.Employees.Where(e => e.Email == Emp.Email).FirstOrDefault();
                
                if(employee!=null)
                {
                    return BadRequest(new
                    {
                        Data = "Email is exists",
                    });
                }
                
                Guid Id = Guid.NewGuid();
                Emp.IDEmp = Id;
                Context.Employees.Add(Emp);
                Context.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        public class UpdateRole
        {
            public Guid IDEmp { get; set; }
            public string Role { get; set; }
        }
        [HttpPut("updaterole")]
        public IActionResult Update(UpdateRole EmpEdit)
        {
            try
            {
                Employee Emp = Context.Employees.Where(e => e.IDEmp == EmpEdit.IDEmp).FirstOrDefault();
                if(Emp == null)
                {
                    return NotFound();
                }
                Emp.Role = EmpEdit.Role;
                Context.Update(Emp);
                Context.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        public class LoginFormEmp
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        [HttpPost("login")]
        public IActionResult Login(LoginFormEmp Emp)
        {
            var employee = Context.Employees.SingleOrDefault(e =>
            e.Email.Equals(Emp.Email) == true);

            if (employee != null)
            {
                if(employee.Password == Emp.Password)
                {
                    return Ok(new
                    {
                        IDEmp = employee.IDEmp,
                        Username = employee.Email,
                        Email = employee.Email,
                        Role = employee.Role
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        Data = "Password is incorrect",
                    });
                }    
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(string id)
        {
            var emp = Context.Employees.Where(e => e.IDEmp == Guid.Parse(id)).FirstOrDefault();
            if (emp == null)
            {
                return NotFound();
            }
            Context.Employees.Remove(emp);
            Context.SaveChanges();

            return Ok();
        }
    }
}
