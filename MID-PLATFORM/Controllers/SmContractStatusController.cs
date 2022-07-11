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
    public class SmContractStatusController : ControllerBase
    {
        private readonly MIDPlatformContext _context;

        public SmContractStatusController(MIDPlatformContext context)
        {
            _context = context;
        }

        //READ
        // GET: api/SmContractStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SmContractStatus>>> GetSmContractStatuses()
        {
          if (_context.SmContractStatuses == null)
          {
              return NotFound();
          }
            return await _context.SmContractStatuses.ToListAsync();
        }

        //READ
        // GET: api/SmContractStatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SmContractStatus>> GetSmContractStatus(int id)
        {
          if (_context.SmContractStatuses == null)
          {
              return NotFound();
          }
            var smContractStatus = await _context.SmContractStatuses.FindAsync(id);

            if (smContractStatus == null)
            {
                return NotFound();
            }

            return smContractStatus;
        }

        //UPDATE
        // PUT: api/SmContractStatus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSmContractStatus(int id, SmContractStatus smContractStatus)
        {
            if (id != smContractStatus.StatusId)
            {
                return BadRequest();
            }

            //_context.Entry(smContractStatus).State = EntityState.Modified;

            SmContractStatus modifiedSmContractStatus = _context.SmContractStatuses.FirstOrDefault(u => u.StatusId == id);
            if (modifiedSmContractStatus == null)
            {
                return NotFound();
            }

            modifiedSmContractStatus.Description = smContractStatus.Description;
            modifiedSmContractStatus.Closed = smContractStatus.Closed;
            modifiedSmContractStatus.Active = smContractStatus.Active;


            try
            {
                _context.SmContractStatuses.Update(modifiedSmContractStatus);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SmContractStatusExists(id))
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
        // POST: api/SmContractStatus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SmContractStatus>> PostSmContractStatus(SmContractStatus smContractStatus)
        {
            if (_context.SmContractStatuses == null)
            {
                return Problem("Entity set 'MIDPlatformContext.SmContractStatuses'  is null.");
            }
            _context.SmContractStatuses.Add(smContractStatus);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return Problem(e.InnerException.ToString(), null, null, e.Message);
            }

            return CreatedAtAction("GetSmContractStatus", new { id = smContractStatus.StatusId }, smContractStatus);
        }

        //DELETE
        // DELETE: api/SmContractStatus/5
        [HttpDelete("{id},{disable}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSmContractStatus(int id, bool disable = false)
        {
            if (_context.SmContractStatuses == null)
            {
                return NotFound();
            }

            var smContractStatus = await _context.SmContractStatuses.FindAsync(id);
            if (smContractStatus == null)
            {
                return NotFound();
            }

            if (disable)//se for true desativa, se for false apaga
            {
                smContractStatus.Active = false;
                _context.SmContractStatuses.Update(smContractStatus);
            }
            else
            {
                _context.SmContractStatuses.Remove(smContractStatus);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                try
                {
                    smContractStatus.Active = false;
                    _context.SmContractStatuses.Update(smContractStatus);

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

        private bool SmContractStatusExists(int id)
        {
            return (_context.SmContractStatuses?.Any(e => e.StatusId == id)).GetValueOrDefault();
        }
    }
}
