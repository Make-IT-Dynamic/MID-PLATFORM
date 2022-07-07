using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MID_PLATFORM.Models;

namespace MID_PLATFORM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmContractLimitsController : ControllerBase
    {
        private readonly MIDPlatformContext _context;

        public SmContractLimitsController(MIDPlatformContext context)
        {
            _context = context;
        }

        //READ
        // GET: api/SmContractLimits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SmContractLimit>>> GetSmContractLimits()
        {
          if (_context.SmContractLimits == null)
          {
              return NotFound();
          }
            return await _context.SmContractLimits.ToListAsync();
        }

        //READ
        // GET: api/SmContractLimits/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SmContractLimit>> GetSmContractLimit(int id)
        {
          if (_context.SmContractLimits == null)
          {
              return NotFound();
          }
            var smContractLimit = await _context.SmContractLimits.FindAsync(id);

            if (smContractLimit == null)
            {
                return NotFound();
            }

            return smContractLimit;
        }

        //UPDATE
        // PUT: api/SmContractLimits/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSmContractLimit(int id, SmContractLimit smContractLimit)
        {
            if (id != smContractLimit.ContractLimitsId)
            {
                return BadRequest();
            }

            //_context.Entry(smContractLimit).State = EntityState.Modified;

            SmContractLimit modifiedSMcontractLimit = _context.SmContractLimits.FirstOrDefault(u => u.ContractLimitsId == id);
            if (modifiedSMcontractLimit == null)
            {
                return NotFound();
            }

            modifiedSMcontractLimit.Contract = smContractLimit.Contract;
            modifiedSMcontractLimit.Date = smContractLimit.Date;
            modifiedSMcontractLimit.Quantity = smContractLimit.Quantity;
            modifiedSMcontractLimit.Value = smContractLimit.Value;
            modifiedSMcontractLimit.Document = smContractLimit.Document;
            modifiedSMcontractLimit.Description = smContractLimit.Description;
            modifiedSMcontractLimit.Active = smContractLimit.Active;

            try
            {
                _context.SmContractLimits.Update(modifiedSMcontractLimit);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SmContractLimitExists(id))
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
        // POST: api/SmContractLimits
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SmContractLimit>> PostSmContractLimit(SmContractLimit smContractLimit)
        {
          if (_context.SmContractLimits == null)
          {
              return Problem("Entity set 'MIDPlatformContext.SmContractLimits'  is null.");
          }
            _context.SmContractLimits.Add(smContractLimit);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return Problem(e.InnerException.ToString(), null, null, e.Message);
            }

            return CreatedAtAction("GetSmContractLimit", new { id = smContractLimit.ContractLimitsId }, smContractLimit);
        }

        //DELETE
        // DELETE: api/SmContractLimits/5
        [HttpDelete("{id},{disable}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSmContractLimit(int id, bool disable = false)
        {
            if (_context.SmContractLimits == null)
            {
                return NotFound();
            }

            var smContractLimit = await _context.SmContractLimits.FindAsync(id);
            if (smContractLimit == null)
            {
                return NotFound();
            }

            if (disable)//se for true desativa, se for false apaga
            {
                smContractLimit.Active = false;
                _context.SmContractLimits.Update(smContractLimit);
            }
            else
            {
                _context.SmContractLimits.Remove(smContractLimit);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                try
                {
                    smContractLimit.Active = false;
                    _context.SmContractLimits.Update(smContractLimit);

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

        private bool SmContractLimitExists(int id)
        {
            return (_context.SmContractLimits?.Any(e => e.ContractLimitsId == id)).GetValueOrDefault();
        }
    }
}
