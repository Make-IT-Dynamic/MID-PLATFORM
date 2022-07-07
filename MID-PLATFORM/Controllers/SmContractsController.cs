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
    public class SmContractsController : ControllerBase
    {
        private readonly MIDPlatformContext _context;

        public SmContractsController(MIDPlatformContext context)
        {
            _context = context;
        }

        //READ
        // GET: api/SmContracts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SmContract>>> GetSmContracts()
        {
          if (_context.SmContracts == null)
          {
              return NotFound();
          }
            return await _context.SmContracts.ToListAsync();
        }

        //READ
        // GET: api/SmContracts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SmContract>> GetSmContract(int id)
        {
          if (_context.SmContracts == null)
          {
              return NotFound();
          }
            var smContract = await _context.SmContracts.FindAsync(id);

            if (smContract == null)
            {
                return NotFound();
            }

            return smContract;
        }

        //UPDATE
        // PUT: api/SmContracts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSmContract(int id, SmContract smContract)
        {
            if (id != smContract.ContractId)
            {
                return BadRequest();
            }

            //_context.Entry(smContract).State = EntityState.Modified;

            SmContract modifiedSMcontract = _context.SmContracts.FirstOrDefault(u => u.ContractId == id);
            if (modifiedSMcontract == null)
            {
                return NotFound();
            }

            modifiedSMcontract.Code = smContract.Code;
            modifiedSMcontract.Instance = smContract.Instance;
            modifiedSMcontract.Type = smContract.Type;
            modifiedSMcontract.Company = smContract.Company;
            modifiedSMcontract.ContactPerson = smContract.ContactPerson;
            modifiedSMcontract.Date = smContract.Date;
            modifiedSMcontract.Name = smContract.Name;
            modifiedSMcontract.Category = smContract.Category;
            modifiedSMcontract.Description = smContract.Description;
            modifiedSMcontract.AllowExceededHours = smContract.AllowExceededHours;
            modifiedSMcontract.BillableExceededHours = smContract.BillableExceededHours;
            modifiedSMcontract.Status = smContract.Status;
            modifiedSMcontract.Active = smContract.Active;

            try
            {
                _context.SmContracts.Update(modifiedSMcontract);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SmContractExists(id))
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
        // POST: api/SmContracts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SmContract>> PostSmContract(SmContract smContract)
        {
            if (_context.SmContracts == null)
            {
                return Problem("Entity set 'MIDPlatformContext.SmContracts'  is null.");
            }
            _context.SmContracts.Add(smContract);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return Problem(e.InnerException.ToString(), null, null, e.Message);
            }

            return CreatedAtAction("GetSmContract", new { id = smContract.ContractId }, smContract);
        }

        //DELETE
        // DELETE: api/SmContracts/5
        [HttpDelete("{id},{disable}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSmContract(int id, bool disable = false)
        {
            if (_context.SmContracts == null)
            {
                return NotFound();
            }
            var smContract = await _context.SmContracts.FindAsync(id);
            if (smContract == null)
            {
                return NotFound();
            }
            if (disable)//se for true desativa, se for false apaga
            {
                smContract.Active = false;
                _context.SmContracts.Update(smContract);
            }
            else
            {
                _context.SmContracts.Remove(smContract);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                try
                {
                    smContract.Active = false;
                    _context.SmContracts.Update(smContract);

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

        private bool SmContractExists(int id)
        {
            return (_context.SmContracts?.Any(e => e.ContractId == id)).GetValueOrDefault();
        }
    }
}
