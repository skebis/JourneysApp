using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using solita_assignment.Classes;
using solita_assignment.Models;

namespace solita_assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JourneysController : ControllerBase
    {
        private readonly JourneyContext _context;

        public JourneysController(JourneyContext context)
        {
            _context = context;
        }

        // GET: api/Journeys?Page=1&PageSize=20
        [HttpGet]
        public async Task<ActionResult<PagedResponse<Journey>>> GetJourneys([FromQuery] Pagination pag)
        {
            // Return bad request if pagination is incorrect.
            if (pag.Page < 0 || pag.PageSize < 1 || pag.PageSize > 20)
            {
                return BadRequest();
            }
            if (_context.Journeys == null)
            {
                return NotFound();
            }
            // DB can contain millions of data so return only a chunk of it
            // and count so front end can still show pagination pages.
            return Ok(new PagedResponse<Journey>{
                DataCount = await _context.Journeys.CountAsync(),
                Data = await _context.Journeys
                .Skip(pag.PageSize * pag.Page)
                .Take(pag.PageSize)
                .ToListAsync()
            });
        }

        // GET: api/Journeys/11223344-5566-7788-99AA-BBCCDDEEFF00
        [HttpGet("{id}")]
        public async Task<ActionResult<Journey>> GetJourney(Guid id)
        {
            if (_context.Journeys == null)
            {
                return NotFound();
            }
            var journey = await _context.Journeys.FindAsync(id);

            if (journey == null)
            {
                return NotFound();
            }

            return journey;
        }

        // PUT: api/Journeys/11223344-5566-7788-99AA-BBCCDDEEFF00
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJourney(Guid id, Journey journey)
        {
            if (id != journey.JourneyId)
            {
                return BadRequest();
            }

            _context.Entry(journey).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JourneyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Journeys
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Journey>> PostJourney(JourneyDto journeyDto)
        {
            if (_context.Journeys == null)
            {
                return Problem("Entity set 'JourneyContext.Journeys'  is null.");
            }
            var journey = new Journey
            {
                JourneyId = Guid.NewGuid(),
                CoveredDistance = journeyDto.CoveredDistance,
                Departure = journeyDto.Departure,
                DepartureStationId = journeyDto.DepartureStationId,
                DepartureStationName = journeyDto.DepartureStationName,
                Duration = journeyDto.Duration,
                Return = journeyDto.Return,
                ReturnStationId = journeyDto.ReturnStationId,
                ReturnStationName = journeyDto.ReturnStationName
            };
            _context.Journeys.Add(journey);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetJourney), new { id = journey.JourneyId }, journey);
        }

        // DELETE: api/Journeys/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJourney(Guid id)
        {
            if (_context.Journeys == null)
            {
                return NotFound();
            }
            var journey = await _context.Journeys.FindAsync(id);
            if (journey == null)
            {
                return NotFound();
            }

            _context.Journeys.Remove(journey);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JourneyExists(Guid id)
        {
            return (_context.Journeys?.Any(e => e.JourneyId == id)).GetValueOrDefault();
        }
    }
}
