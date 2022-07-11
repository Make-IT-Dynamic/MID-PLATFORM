using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MID_PLATFORM.Models;
using Microsoft.AspNetCore.Authorization;

namespace MID_PLATFORM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PeriodsController : ControllerBase
    {
        private readonly MIDPlatformContext _context;

        public PeriodsController(MIDPlatformContext context)
        {
            _context = context;
        }

        //READ
        // GET: api/Periods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Period>>> GetPeriods()
        {
          if (_context.Periods == null)
          {
              return NotFound();
          }
            return await _context.Periods.ToListAsync();
        }

        //READ
        // GET: api/Periods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Period>> GetPeriod(int id)
        {
          if (_context.Periods == null)
          {
              return NotFound();
          }
            var period = await _context.Periods.FindAsync(id);

            if (period == null)
            {
                return NotFound();
            }

            return period;
        }

        //UPDATE
        // PUT: api/Periods/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPeriod(int id, Period period)
        {
            if (id != period.PeriodId)
            {
                return BadRequest();
            }

            //_context.Entry(period).State = EntityState.Modified;

            Period modifiedPeriod = _context.Periods.FirstOrDefault(u => u.PeriodId == id);
            if (modifiedPeriod == null)
            {
                return NotFound();
            }

            modifiedPeriod.Code = period.Code;
            modifiedPeriod.StartDate = period.StartDate;
            modifiedPeriod.EndDate = period.EndDate;
            modifiedPeriod.ActiveForSm = period.ActiveForSm;
            modifiedPeriod.Active = period.Active;

            try
            {
                _context.Periods.Update(modifiedPeriod);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PeriodExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        //CREATE
        // POST: api/Periods
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Period>> PostPeriod(Period period)
        {
            if (_context.Periods == null)
            {
                return Problem("Entity set 'MIDPlatformContext.Periods'  is null.");
            }
            _context.Periods.Add(period);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return Problem(e.InnerException.ToString(), null, null, e.Message);
            }

            return CreatedAtAction("GetPeriod", new { id = period.PeriodId }, period);
        }

        //DELETE
        // DELETE: api/Periods/5
        [HttpDelete("{id},{disable}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePeriod(int id, bool disable = false)
        {
            if (_context.Periods == null)
            {
                return NotFound();
            }
            var period = await _context.Periods.FindAsync(id);
            if (period == null)
            {
                return NotFound();
            }

            if (disable)//se for true desativa, se for false apaga
            {
                period.ActiveForSm = false;
                _context.Periods.Update(period);
            }
            else
            {
                _context.Periods.Remove(period);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                try
                {
                    period.ActiveForSm = false;
                    _context.Periods.Update(period);

                    return Ok(ex.InnerException);
                }
                catch (Exception e)
                {
                    return Problem(e.InnerException.ToString(), null, null, e.Message);
                }
            }
            catch (Exception e)
            {
                return Problem(e.InnerException.ToString(), null, null, e.Message);
            }

            return Ok();
        }

        private bool PeriodExists(int id)
        {
            return (_context.Periods?.Any(e => e.PeriodId == id)).GetValueOrDefault();
        }
    }
}
