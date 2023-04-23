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
    [Route("api/[controller]")]
    [ApiController]
    public class ImportBillController : ControllerBase
    {
        private readonly MyDbContext Context;

        public Guid IDSupp { get; private set; }

        public ImportBillController(MyDbContext context)
        {
            Context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var ListProduct = Context.ImportBills.ToList();

            return Ok(ListProduct);
        }

        [HttpGet("{id}")]
        public IActionResult GeById(Guid id)
        {
            try
            {
                var ImportBill = Context.ImportBills.SingleOrDefault(i => i.IDImp == id);
                if (ImportBill != null)
                {
                    return Ok(ImportBill);
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
        public IActionResult Create(string IDSupp, string IDWare, double Price, int Quantity, double TotalMoney, DateTime DateImport)
        {
            try
            {
                var ImportBill = new ImportBill
                {
                    IDImp = Guid.NewGuid(),
                    IDSupp = Guid.Parse(IDSupp),
                    IDWare = Guid.Parse(IDWare),
                    Price = Price,
                    Quantity = Quantity,
                    TotalMoney = TotalMoney,
                    DateImport = DateImport

                };

                Context.ImportBills.Add(ImportBill);
                Context.SaveChanges();

                return Ok(new
                {
                    Success = true,
                    Data = ImportBill,
                });
            }
            catch
            {
                return BadRequest();
            } 
            

        }

        [HttpPut("{id}")]
        public IActionResult Edit(string id, ImportBill importbilledit)
        {
            try
            {
                var importbill = Context.ImportBills.SingleOrDefault( i=>i.IDImp == Guid.Parse(id));
                if (importbill == null)
                {
                    return NotFound();
                }
                /////update 
                if (id != importbill.IDImp.ToString())
                {
                    return BadRequest();
                }

                importbill.IDSupp = importbill.IDSupp;
                importbill.IDWare = importbill.IDWare;
                importbill.Price = importbill.Price;
                importbill.Quantity = importbill.Quantity;
                importbill.TotalMoney = importbill.TotalMoney;
                importbill.DateImport = importbill.DateImport;

                return Ok(importbill);
            }
            catch
            {
                return BadRequest();
            }

        }


    }
}

