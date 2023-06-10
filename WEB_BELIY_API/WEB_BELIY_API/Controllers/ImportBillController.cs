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
    [Route("imp")]
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
        public IActionResult GetById(string id)
        {
            try
            {
                var ImportBill = Context.ImportBills.SingleOrDefault(i => i.IDImp == Guid.Parse(id));
                
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
        public IActionResult Create(ImportBill import)
        {
            try
            {
                var ImportBill = new ImportBill
                {
                    IDImp = Guid.NewGuid(),
                    NameSupplier = import.NameSupplier,
                    TotalMoney = 0,
                    DateImport = import.DateImport

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
        public IActionResult Edit(string id, ImportBill importBillEdit)
        {
            try
            {
                var importbill = Context.ImportBills.SingleOrDefault(i => i.IDImp == Guid.Parse(id));
                
                if (importbill == null)
                {
                    return NotFound();
                }

                importbill.NameSupplier = importBillEdit.NameSupplier;
                importbill.TotalMoney = importBillEdit.TotalMoney;
                importbill.DateImport = importBillEdit.DateImport;

                Context.SaveChanges();

                return Ok(importbill);
            }
            catch
            {
                return BadRequest();
            }

        }


    }
}

