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
        public async Task<ActionResult<Object>> GetElectricityMeters(long id)
        {
            DateTime setDate;

            Boolean dateInQuery = DateTime.TryParse(HttpContext.Request.Query["date"], out setDate);


            setDate = setDate.ToUniversalTime(); // Вот за что я люблю unixtime там такой шизы нет. цифирки значит цифирки
            var electricityCount = await _context.ElectricityCount.FindAsync(id);


            DateTime startTime = setDate.Date;
            DateTime endTime = setDate.Date.AddDays(1);

            var valuesCounter =
                _context.ElectricityValues.Where(v =>
                    (v.ElectricityCountForeignKey == electricityCount.ElectricityCountId) &&
                    (v.CreateAt >= startTime && v.CreateAt < endTime));

            ElectricityValue newestValue = _context.ElectricityValues.Where(v =>
                    (v.ElectricityCountForeignKey == electricityCount.ElectricityCountId))
                .OrderByDescending(value => value.CreateAt)
                .FirstOrDefault();

            ElectricityValue oldestValue = _context.ElectricityValues.Where(v =>
                    (v.ElectricityCountForeignKey == electricityCount.ElectricityCountId))
                .OrderBy(value => value.CreateAt)
                .FirstOrDefault();

            var RangeDates = new Object();


            if ((newestValue is ElectricityValue) && (oldestValue is ElectricityValue))
            {
                RangeDates = new
                    {maxDate = newestValue.CreateAt.ToString("o"), minDate = oldestValue.CreateAt.ToString("o")};
            }
            else
            {
                RangeDates = new { };
            }
            
            if (dateInQuery)
            {
                TimeSpan interval = new TimeSpan(0, 30, 0); // 30 minutes.

                //Нужно потом разобрать как это работает в стиле функций
                var electricityCountValues = (from elVal in valuesCounter.AsEnumerable()
                        group elVal by elVal.CreateAt.Ticks / interval.Ticks
                        into g
                        select new {step = new DateTime(g.Key * interval.Ticks).ToString("o"), Values = g.ToList()})
                    .ToDictionary(b => b.step, b => b.Values);
                
                var result = new
                {
                    electricityCountId = electricityCount.ElectricityCountId, name = electricityCount.Name,
                    serialNumber = electricityCount.SerialNumber, RangeDates, electricityCountValues
                };

                return result;
            }
            else
            {
                var result = new
                {
                    electricityCountId = electricityCount.ElectricityCountId, name = electricityCount.Name,
                    serialNumber = electricityCount.SerialNumber, RangeDates, electricityCountValues = new { }
                };
                return result;
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