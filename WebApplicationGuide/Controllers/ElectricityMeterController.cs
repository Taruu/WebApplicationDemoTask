using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApplicationGuide.Models;


namespace WebApplicationGuide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElectricityMeterController : ControllerBase
    {
        private readonly ElectricityСountContext _context; //Тип контекста
        private readonly ILogger _logger;

        public ElectricityMeterController(ElectricityСountContext context,
            ILogger<ElectricityMeterController> logger) //Мы его передали?
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/ElectricityMeter
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ElectricityCount>>> GetElectricityMeters()
        {
            return await _context.ElectricityCount.ToListAsync();
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ElectricityCount>> GetElectricityMeters(long id)
        {
            DateTime setDate;
            _logger.LogInformation($"Id {id} - {HttpContext.Request.Query["date"].GetType()}");
            Boolean dateInQuery = DateTime.TryParse(HttpContext.Request.Query["date"], out setDate);
            _logger.LogInformation($"setDate {dateInQuery}, {setDate}, - {HttpContext.Request.Query["date"]}");

            
            var electricityCount = await _context.ElectricityCount.FindAsync(id); 
            
            if (dateInQuery)
            {
                
                return Content("max date");
            }
            else
            {
                return electricityCount;
            }
        }


        [HttpPost()]
        public async Task<ActionResult<ElectricityCount>> PostElectricityMeter(ElectricityCount electricityCount)
        {
            return Content("test");
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<ElectricityCount>> PutElectricityMeter(ElectricityCount electricityCount)
        {
            return Content("test");
        }
    }
}