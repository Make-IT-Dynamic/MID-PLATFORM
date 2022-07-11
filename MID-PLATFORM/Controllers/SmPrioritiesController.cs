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
    public class SmPrioritiesController : ControllerBase
    {
        private readonly MIDPlatformContext _context;

        public SmPrioritiesController(MIDPlatformContext context)
        {
            _context = context;
        }

        //READ
        // GET: api/SmPriorities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SmPriority>>> GetSmPriorities()
        {
          if (_context.SmPriorities == null)
          {
              return NotFound();
          }
            return await _context.SmPriorities.ToListAsync();
        }

        //READ
        // GET: api/SmPriorities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SmPriority>> GetSmPriority(int id)
        {
          if (_context.SmPriorities == null)
          {
              return NotFound();
          }
            var smPriority = await _context.SmPriorities.FindAsync(id);

            if (smPriority == null)
            {
                return NotFound();
            }

            return smPriority;
        }

        //UPDATE
        // PUT: api/SmPriorities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSmPriority(int id, SmPriority smPriority)
        {
            if (id != smPriority.PriorityId)
            {
                return BadRequest();
            }

            //_context.Entry(smPriority).State = EntityState.Modified;

            SmPriority modifiedSmPriority = _context.SmPriorities.FirstOrDefault(u => u.PriorityId == id);
            if (modifiedSmPriority == null)
            {
                return NotFound();
            }

            modifiedSmPriority.Description = smPriority.Description;
            modifiedSmPriority.Active = smPriority.Active;



            try
            {
                _context.SmPriorities.Update(modifiedSmPriority);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SmPriorityExists(id))
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
        // POST: api/SmPriorities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SmPriority>> PostSmPriority(SmPriority smPriority)
        {
            if (_context.SmPriorities == null)
            {
                return Problem("Entity set 'MIDPlatformContext.SmPriorities'  is null.");
            }
            _context.SmPriorities.Add(smPriority);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return Problem(e.InnerException.ToString(), null, null, e.Message);
            }

            return CreatedAtAction("GetSmPriority", new { id = smPriority.PriorityId }, smPriority);
        }

        //DELETE
        // DELETE: api/SmPriorities/5
        [HttpDelete("{id},{disable}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSmPriority(int id, bool disable = false)
        {
            if (_context.SmPriorities == null)
            {
                return NotFound();
            }

            var smPriority = await _context.SmPriorities.FindAsync(id);
            if (smPriority == null)
            {
                return NotFound();
            }

            if (disable)//se for true desativa, se for false apaga
            {
                smPriority.Active = false;
                _context.SmPriorities.Update(smPriority);
            }
            else
            {
                _context.SmPriorities.Remove(smPriority);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                try
                {
                    smPriority.Active = false;
                    _context.SmPriorities.Update(smPriority);

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

        private bool SmPriorityExists(int id)
        {
            return (_context.SmPriorities?.Any(e => e.PriorityId == id)).GetValueOrDefault();
        }
    }
}
