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
    public class SmTasksController : ControllerBase
    {
        private readonly MIDPlatformContext _context;

        public SmTasksController(MIDPlatformContext context)
        {
            _context = context;
        }

        //READ
        // GET: api/SmTasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SmTask>>> GetSmTasks()
        {
          if (_context.SmTasks == null)
          {
              return NotFound();
          }
            return await _context.SmTasks.ToListAsync();
        }

        //READ
        // GET: api/SmTasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SmTask>> GetSmTask(int id)
        {
          if (_context.SmTasks == null)
          {
              return NotFound();
          }
            var smTask = await _context.SmTasks.FindAsync(id);

            if (smTask == null)
            {
                return NotFound();
            }

            return smTask;
        }

        //UPDATE
        // PUT: api/SmTasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSmTask(int id, SmTask smTask)
        {
            if (id != smTask.TaskId)
            {
                return BadRequest();
            }

            //_context.Entry(smTask).State = EntityState.Modified;

            SmTask modifiedSmTask = _context.SmTasks.FirstOrDefault(u => u.TaskId == id);
            if (modifiedSmTask == null)
            {
                return NotFound();
            }

            modifiedSmTask.Contract = smTask.Contract;
            modifiedSmTask.Type = smTask.Type;
            modifiedSmTask.Requester = smTask.Requester;
            modifiedSmTask.CreatedBy = smTask.CreatedBy;
            modifiedSmTask.AssignedTo = smTask.AssignedTo;
            modifiedSmTask.Subject = smTask.Subject;
            modifiedSmTask.Description = smTask.Description;
            modifiedSmTask.Priority = smTask.Priority;
            modifiedSmTask.Status = smTask.Status;
            modifiedSmTask.Category = smTask.Category;
            modifiedSmTask.CreationDate = smTask.CreationDate;
            modifiedSmTask.ReplyDate = smTask.ReplyDate;
            modifiedSmTask.ClosedDate = smTask.ClosedDate;
            modifiedSmTask.TotalHoursEstimated = smTask.TotalHoursEstimated;
            modifiedSmTask.RemainingHoursEstimaded = smTask.RemainingHoursEstimaded;
            modifiedSmTask.Active = smTask.Active;

            try
            {
                _context.SmTasks.Update(modifiedSmTask);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SmTaskExists(id))
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
        // POST: api/SmTasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SmTask>> PostSmTask(SmTask smTask)
        {
          if (_context.SmTasks == null)
          {
              return Problem("Entity set 'MIDPlatformContext.SmTasks'  is null.");
          }
            _context.SmTasks.Add(smTask);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return Problem(e.InnerException.ToString(), null, null, e.Message);
            }

            return CreatedAtAction("GetSmTask", new { id = smTask.TaskId }, smTask);
        }

        //DELETE
        // DELETE: api/SmTasks/5
        [HttpDelete("{id},{disable}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSmTask(int id, bool disable = false)
        {
            if (_context.SmTasks == null)
            {
                return NotFound();
            }
            var smTask = await _context.SmTasks.FindAsync(id);
            if (smTask == null)
            {
                return NotFound();
            }

            if (disable)//se for true desativa, se for false apaga
            {
                smTask.Active = false;
                _context.SmTasks.Update(smTask);
            }
            else
            {
                _context.SmTasks.Remove(smTask);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                try
                {
                    smTask.Active = false;
                    _context.SmTasks.Update(smTask);

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

        private bool SmTaskExists(int id)
        {
            return (_context.SmTasks?.Any(e => e.TaskId == id)).GetValueOrDefault();
        }
    }
}
