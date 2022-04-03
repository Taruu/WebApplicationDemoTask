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
                DateTime startTime = setDate.Date;
                DateTime endTime = setDate.Date.AddDays(1);
                 var valuesCounter =
                     _context.ElectricityValues.Where(v => v.CreateAt >= startTime && v.CreateAt < endTime);

                TimeSpan interval = new TimeSpan(0, 30, 0); // 30 minutes.
                var groupedTimes = from elVal in valuesCounter.AsEnumerable()
                    group elVal by elVal.CreateAt.Ticks / interval.Ticks
                    into g
                    select new {step = new DateTime(g.Key * interval.Ticks), Values = g};


                foreach (var value in groupedTimes)
                {
                    _logger.LogInformation($"{value.step} - {value.Values}");
                }

                return electricityCount;
            }
            else
            {
                return electricityCount;
            }
        }


        [HttpPost()]
        public async Task<ActionResult<ElectricityCount>> PostElectricityMeter(ElectricityCount newElectricityCount)
        {
            _context.ElectricityCount.Add(newElectricityCount);
            await _context.SaveChangesAsync();
            return newElectricityCount;
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<ElectricityCount>> PutElectricityMeter(long id,
            ElectricityCount electricityCount)
        {
            if (id != electricityCount.ElectricityCountId)
            {
                return BadRequest();
            }

            _context.Entry(electricityCount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ElectricityCountExists(id))
                {
                    return NotFound();
                }
                else throw;
            }

            return electricityCount;
        }

        private bool ElectricityCountExists(long id)
        {
            return _context.ElectricityCount.Any(e => e.ElectricityCountId == id);
        }
    }
}