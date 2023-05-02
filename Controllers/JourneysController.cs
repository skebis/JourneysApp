using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

        // GET: api/Journeys
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Journey>>> GetJourneys()
        {
          if (_context.Journeys == null)
          {
              return NotFound();
          }
            return await _context.Journeys.ToListAsync();
        }

        // GET: api/Journeys/5
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

        // PUT: api/Journeys/5
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
        public async Task<ActionResult<Journey>> PostJourney(Journey journey)
        {
          if (_context.Journeys == null)
          {
              return Problem("Entity set 'JourneyContext.Journeys'  is null.");
          }
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
