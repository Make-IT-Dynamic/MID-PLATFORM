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
    public class SmTaskStatusController : ControllerBase
    {
        private readonly MIDPlatformContext _context;

        public SmTaskStatusController(MIDPlatformContext context)
        {
            _context = context;
        }

        //READ
        // GET: api/SmTaskStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SmTaskStatus>>> GetSmTaskStatuses()
        {
          if (_context.SmTaskStatuses == null)
          {
              return NotFound();
          }
            return await _context.SmTaskStatuses.ToListAsync();
        }

        //READ
        // GET: api/SmTaskStatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SmTaskStatus>> GetSmTaskStatus(int id)
        {
          if (_context.SmTaskStatuses == null)
          {
              return NotFound();
          }
            var smTaskStatus = await _context.SmTaskStatuses.FindAsync(id);

            if (smTaskStatus == null)
            {
                return NotFound();
            }

            return smTaskStatus;
        }

        //UPDATE
        // PUT: api/SmTaskStatus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSmTaskStatus(int id, SmTaskStatus smTaskStatus)
        {
            if (id != smTaskStatus.StatusId)
            {
                return BadRequest();
            }

            //_context.Entry(smTaskStatus).State = EntityState.Modified;

            SmTaskStatus modifiedSmTaskStatus = _context.SmTaskStatuses.FirstOrDefault(u => u.StatusId == id);
            if (modifiedSmTaskStatus == null)
            {
                return NotFound();
            }

            modifiedSmTaskStatus.Description = smTaskStatus.Description;
            modifiedSmTaskStatus.Closed = smTaskStatus.Closed;
            modifiedSmTaskStatus.Active = smTaskStatus.Active;


            try
            {
                _context.SmTaskStatuses.Update(modifiedSmTaskStatus);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SmTaskStatusExists(id))
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
        // POST: api/SmTaskStatus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SmTaskStatus>> PostSmTaskStatus(SmTaskStatus smTaskStatus)
        {
            if (_context.SmTaskStatuses == null)
            {
                return Problem("Entity set 'MIDPlatformContext.SmTaskStatuses'  is null.");
            }
            _context.SmTaskStatuses.Add(smTaskStatus);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return Problem(e.InnerException.ToString(), null, null, e.Message);
            }

            return CreatedAtAction("GetSmTaskStatus", new { id = smTaskStatus.StatusId }, smTaskStatus);
        }

        //DELETE
        // DELETE: api/SmTaskStatus/5
        [HttpDelete("{id},{disable}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSmTaskStatus(int id, bool disable = false)
        {
            if (_context.SmTaskStatuses == null)
            {
                return NotFound();
            }

            var smTaskStatus = await _context.SmTaskStatuses.FindAsync(id);
            if (smTaskStatus == null)
            {
                return NotFound();
            }

            if (disable)//se for true desativa, se for false apaga
            {
                smTaskStatus.Active = false;
                _context.SmTaskStatuses.Update(smTaskStatus);
            }
            else
            {
                _context.SmTaskStatuses.Remove(smTaskStatus);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                try
                {
                    smTaskStatus.Active = false;
                    _context.SmTaskStatuses.Update(smTaskStatus);

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

        private bool SmTaskStatusExists(int id)
        {
            return (_context.SmTaskStatuses?.Any(e => e.StatusId == id)).GetValueOrDefault();
        }
    }
}
