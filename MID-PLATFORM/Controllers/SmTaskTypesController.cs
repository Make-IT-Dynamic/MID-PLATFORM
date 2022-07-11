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
    public class SmTaskTypesController : ControllerBase
    {
        private readonly MIDPlatformContext _context;

        public SmTaskTypesController(MIDPlatformContext context)
        {
            _context = context;
        }

        //READ
        // GET: api/SmTaskTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SmTaskType>>> GetSmTaskTypes()
        {
          if (_context.SmTaskTypes == null)
          {
              return NotFound();
          }
            return await _context.SmTaskTypes.ToListAsync();
        }

        //READ
        // GET: api/SmTaskTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SmTaskType>> GetSmTaskType(int id)
        {
          if (_context.SmTaskTypes == null)
          {
              return NotFound();
          }
            var smTaskType = await _context.SmTaskTypes.FindAsync(id);

            if (smTaskType == null)
            {
                return NotFound();
            }

            return smTaskType;
        }

        //UPDATE
        // PUT: api/SmTaskTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSmTaskType(int id, SmTaskType smTaskType)
        {
            if (id != smTaskType.TaskTypeId)
            {
                return BadRequest();
            }

            //_context.Entry(smTaskType).State = EntityState.Modified;

            SmTaskType modifiedSmTaskType = _context.SmTaskTypes.FirstOrDefault(u => u.TaskTypeId == id);
            if (modifiedSmTaskType == null)
            {
                return NotFound();
            }

            modifiedSmTaskType.Description = smTaskType.Description;
            modifiedSmTaskType.Active = smTaskType.Active;



            try
            {
                _context.SmTaskTypes.Update(modifiedSmTaskType);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SmTaskTypeExists(id))
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
        // POST: api/SmTaskTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SmTaskType>> PostSmTaskType(SmTaskType smTaskType)
        {
            if (_context.SmTaskTypes == null)
            {
                return Problem("Entity set 'MIDPlatformContext.SmTaskTypes'  is null.");
            }
            _context.SmTaskTypes.Add(smTaskType);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return Problem(e.InnerException.ToString(), null, null, e.Message);
            }

            return CreatedAtAction("GetSmTaskType", new { id = smTaskType.TaskTypeId }, smTaskType);
        }

        //DELETE
        // DELETE: api/SmTaskTypes/5
        [HttpDelete("{id},{disable}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSmTaskType(int id, bool disable = false)
        {
            if (_context.SmTaskTypes == null)
            {
                return NotFound();
            }

            var smTaskType = await _context.SmTaskTypes.FindAsync(id);
            if (smTaskType == null)
            {
                return NotFound();
            }

            if (disable)//se for true desativa, se for false apaga
            {
                smTaskType.Active = false;
                _context.SmTaskTypes.Update(smTaskType);
            }
            else
            {
                _context.SmTaskTypes.Remove(smTaskType);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                try
                {
                    smTaskType.Active = false;
                    _context.SmTaskTypes.Update(smTaskType);

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

        private bool SmTaskTypeExists(int id)
        {
            return (_context.SmTaskTypes?.Any(e => e.TaskTypeId == id)).GetValueOrDefault();
        }
    }
}
