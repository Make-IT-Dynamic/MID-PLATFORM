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
    public class SmWorkRecordsController : ControllerBase
    {
        private readonly MIDPlatformContext _context;

        public SmWorkRecordsController(MIDPlatformContext context)
        {
            _context = context;
        }

        //READ
        // GET: api/SmWorkRecords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SmWorkRecord>>> GetSmWorkRecords()
        {
          if (_context.SmWorkRecords == null)
          {
              return NotFound();
          }
            return await _context.SmWorkRecords.ToListAsync();
        }

        //READ
        // GET: api/SmWorkRecords/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SmWorkRecord>> GetSmWorkRecord(int id)
        {
          if (_context.SmWorkRecords == null)
          {
              return NotFound();
          }
            var smWorkRecord = await _context.SmWorkRecords.FindAsync(id);

            if (smWorkRecord == null)
            {
                return NotFound();
            }

            return smWorkRecord;
        }

        //UPDATE
        // PUT: api/SmWorkRecords/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSmWorkRecord(int id, SmWorkRecord smWorkRecord)
        {
            if (id != smWorkRecord.WorkRecordId)
            {
                return BadRequest();
            }

            //_context.Entry(smWorkRecord).State = EntityState.Modified;

            SmWorkRecord modifiedSmWorkRecord = _context.SmWorkRecords.FirstOrDefault(u => u.WorkRecordId == id);
            if (modifiedSmWorkRecord == null)
            {
                return NotFound();
            }

            modifiedSmWorkRecord.Task = smWorkRecord.Task;
            modifiedSmWorkRecord.Type = smWorkRecord.Type;
            modifiedSmWorkRecord.Agent = smWorkRecord.Agent;
            modifiedSmWorkRecord.StartDate = smWorkRecord.StartDate;
            modifiedSmWorkRecord.EndDate = smWorkRecord.EndDate;
            modifiedSmWorkRecord.WorkedHours = smWorkRecord.WorkedHours;
            modifiedSmWorkRecord.BillableHours = smWorkRecord.BillableHours;
            modifiedSmWorkRecord.NonBillableHours = smWorkRecord.NonBillableHours;
            modifiedSmWorkRecord.Description = smWorkRecord.Description;
            modifiedSmWorkRecord.Active = smWorkRecord.Active;

            try
            {
                _context.SmWorkRecords.Update(modifiedSmWorkRecord);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SmWorkRecordExists(id))
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
        // POST: api/SmWorkRecords
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SmWorkRecord>> PostSmWorkRecord(SmWorkRecord smWorkRecord)
        {
            if (_context.SmWorkRecords == null)
            {
                return Problem("Entity set 'MIDPlatformContext.SmWorkRecords'  is null.");
            }
            _context.SmWorkRecords.Add(smWorkRecord);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return Problem(e.InnerException.ToString(), null, null, e.Message);
            }

            return CreatedAtAction("GetSmWorkRecord", new { id = smWorkRecord.WorkRecordId }, smWorkRecord);
        }

        //DELETE
        // DELETE: api/SmWorkRecords/5
        [HttpDelete("{id},{disable}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSmWorkRecord(int id, bool disable = false)
        {
            if (_context.SmWorkRecords == null)
            {
                return NotFound();
            }

            var smWorkRecord = await _context.SmWorkRecords.FindAsync(id);
            if (smWorkRecord == null)
            {
                return NotFound();
            }

            if (disable)//se for true desativa, se for false apaga
            {
                smWorkRecord.Active = false;
                _context.SmWorkRecords.Update(smWorkRecord);
            }
            else
            {
                _context.SmWorkRecords.Remove(smWorkRecord);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                try
                {
                    smWorkRecord.Active = false;
                    _context.SmWorkRecords.Update(smWorkRecord);

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

        private bool SmWorkRecordExists(int id)
        {
            return (_context.SmWorkRecords?.Any(e => e.WorkRecordId == id)).GetValueOrDefault();
        }
    }
}
