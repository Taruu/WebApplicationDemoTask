using Microsoft.AspNetCore.Mvc;
using WebApplicationGuide.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationGuide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElectricityValueController : Controller
    {
        private readonly ElectricityСountContext _context; //Тип контекста
        private readonly ILogger _logger;

        public ElectricityValueController(ElectricityСountContext context,
            ILogger<ElectricityMeterController> logger) //Мы его передали?
        {
            _context = context;
            _logger = logger;
        }

        // GET
        public IActionResult Index()
        {
            return Content("test");
        }

        [HttpPost()]
        public async Task<ActionResult<ElectricityValue>> PostElectricityMeter(ElectricityValue newElectricityValue)
        {
            //Проверку на ключ можно сделать в объекте но я так и не понял как это правильно делать : /
            var electricityCount =
                await _context.ElectricityCount.FindAsync(newElectricityValue.ElectricityCountForeignKey);
            _logger.LogInformation($" el -{newElectricityValue.CreateAt}");
            if (!ModelState.IsValid || (electricityCount == null)) return BadRequest(ModelState);
            if (newElectricityValue.CreateAt.ToString() == "01.01.0001 00:00:00")

                _context.ElectricityValues.Add(newElectricityValue);
            await _context.SaveChangesAsync();
            return newElectricityValue;
        }
    }
}