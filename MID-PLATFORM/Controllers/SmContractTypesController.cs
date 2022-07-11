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
    public class SmContractTypesController : ControllerBase
    {
        private readonly MIDPlatformContext _context;

        public SmContractTypesController(MIDPlatformContext context)
        {
            _context = context;
        }

        //READ
        // GET: api/SmContractTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SmContractType>>> GetSmContractTypes()
        {
          if (_context.SmContractTypes == null)
          {
              return NotFound();
          }
            return await _context.SmContractTypes.ToListAsync();
        }

        //READ
        // GET: api/SmContractTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SmContractType>> GetSmContractType(int id)
        {
          if (_context.SmContractTypes == null)
          {
              return NotFound();
          }
            var smContractType = await _context.SmContractTypes.FindAsync(id);

            if (smContractType == null)
            {
                return NotFound();
            }

            return smContractType;
        }

        //UPDATE
        // PUT: api/SmContractTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSmContractType(int id, SmContractType smContractType)
        {
            if (id != smContractType.ContractTypeId)
            {
                return BadRequest();
            }

            //_context.Entry(smContractType).State = EntityState.Modified;

            SmContractType modifiedContractType = _context.SmContractTypes.FirstOrDefault(u => u.ContractTypeId == id);
            if (modifiedContractType == null)
            {
                return NotFound();
            }

            modifiedContractType.Description = smContractType.Description;
            modifiedContractType.AllowExeedHours = smContractType.AllowExeedHours;
            modifiedContractType.BillableExceedHours = smContractType.BillableExceedHours;
            modifiedContractType.Active = smContractType.Active;

            try
            {
                _context.SmContractTypes.Update(modifiedContractType);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SmContractTypeExists(id))
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
        // POST: api/SmContractTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SmContractType>> PostSmContractType(SmContractType smContractType)
        {
            if (_context.SmContractTypes == null)
            {
                return Problem("Entity set 'MIDPlatformContext.SmContractTypes'  is null.");
            }
            _context.SmContractTypes.Add(smContractType);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return Problem(e.InnerException.ToString(), null, null, e.Message);
            }

            return CreatedAtAction("GetSmContractType", new { id = smContractType.ContractTypeId }, smContractType);
        }

        //DELETE
        // DELETE: api/SmContractTypes/5
        [HttpDelete("{id},{disable}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSmContractType(int id, bool disable = false)
        {
            if (_context.SmContractTypes == null)
            {
                return NotFound();
            }

            var smContractType = await _context.SmContractTypes.FindAsync(id);
            if (smContractType == null)
            {
                return NotFound();
            }

            if (disable)//se for true desativa, se for false apaga
            {
                smContractType.Active = false;
                _context.SmContractTypes.Update(smContractType);
            }
            else
            {
                _context.SmContractTypes.Remove(smContractType);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                try
                {
                    smContractType.Active = false;
                    _context.SmContractTypes.Update(smContractType);

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

        private bool SmContractTypeExists(int id)
        {
            return (_context.SmContractTypes?.Any(e => e.ContractTypeId == id)).GetValueOrDefault();
        }
    }
}
