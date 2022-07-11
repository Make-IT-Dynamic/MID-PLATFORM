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
    public class SmWorkRecordTypesController : ControllerBase
    {
        private readonly MIDPlatformContext _context;

        public SmWorkRecordTypesController(MIDPlatformContext context)
        {
            _context = context;
        }

        //READ
        // GET: api/SmWorkRecordTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SmWorkRecordType>>> GetSmWorkRecordTypes()
        {
          if (_context.SmWorkRecordTypes == null)
          {
              return NotFound();
          }
            return await _context.SmWorkRecordTypes.ToListAsync();
        }

        //READ
        // GET: api/SmWorkRecordTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SmWorkRecordType>> GetSmWorkRecordType(int id)
        {
          if (_context.SmWorkRecordTypes == null)
          {
              return NotFound();
          }
            var smWorkRecordType = await _context.SmWorkRecordTypes.FindAsync(id);

            if (smWorkRecordType == null)
            {
                return NotFound();
            }

            return smWorkRecordType;
        }

        //UPDATE
        // PUT: api/SmWorkRecordTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSmWorkRecordType(int id, SmWorkRecordType smWorkRecordType)
        {
            if (id != smWorkRecordType.WorkRecordTypeId)
            {
                return BadRequest();
            }

            //_context.Entry(smWorkRecordType).State = EntityState.Modified;

            SmWorkRecordType modifiedSmWorkRecordType = _context.SmWorkRecordTypes.FirstOrDefault(u => u.WorkRecordTypeId == id);
            if (modifiedSmWorkRecordType == null)
            {
                return NotFound();
            }

            modifiedSmWorkRecordType.Description = smWorkRecordType.Description;
            modifiedSmWorkRecordType.Billable = smWorkRecordType.Billable;
            modifiedSmWorkRecordType.Active = smWorkRecordType.Active;


            try
            {
                _context.SmWorkRecordTypes.Update(modifiedSmWorkRecordType);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SmWorkRecordTypeExists(id))
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
        // POST: api/SmWorkRecordTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SmWorkRecordType>> PostSmWorkRecordType(SmWorkRecordType smWorkRecordType)
        {
            if (_context.SmWorkRecordTypes == null)
            {
                return Problem("Entity set 'MIDPlatformContext.SmWorkRecordTypes'  is null.");
            }
            _context.SmWorkRecordTypes.Add(smWorkRecordType);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return Problem(e.InnerException.ToString(), null, null, e.Message);
            }

            return CreatedAtAction("GetSmWorkRecordType", new { id = smWorkRecordType.WorkRecordTypeId }, smWorkRecordType);
        }

        //DELETE
        // DELETE: api/SmWorkRecordTypes/5
        [HttpDelete("{id},{disable}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSmWorkRecordType(int id, bool disable = false)
        {
            if (_context.SmWorkRecordTypes == null)
            {
                return NotFound();
            }

            var smWorkRecordType = await _context.SmWorkRecordTypes.FindAsync(id);
            if (smWorkRecordType == null)
            {
                return NotFound();
            }

            if (disable)//se for true desativa, se for false apaga
            {
                smWorkRecordType.Active = false;
                _context.SmWorkRecordTypes.Update(smWorkRecordType);
            }
            else
            {
                _context.SmWorkRecordTypes.Remove(smWorkRecordType);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                try
                {
                    smWorkRecordType.Active = false;
                    _context.SmWorkRecordTypes.Update(smWorkRecordType);

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

        private bool SmWorkRecordTypeExists(int id)
        {
            return (_context.SmWorkRecordTypes?.Any(e => e.WorkRecordTypeId == id)).GetValueOrDefault();
        }
    }
}
