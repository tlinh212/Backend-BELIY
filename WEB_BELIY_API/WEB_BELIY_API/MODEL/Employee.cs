using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_BELIY_API.MODEL
{
    public class Employee
    {
        [Key]      
        public Guid IDEmp { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        [MaxLength(10)]
        public string Phone { get; set; }
        public string Role { get; set; }

    }
}
