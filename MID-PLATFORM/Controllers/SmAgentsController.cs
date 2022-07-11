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
    public class SmAgentsController : ControllerBase
    {
        private readonly MIDPlatformContext _context;

        public SmAgentsController(MIDPlatformContext context)
        {
            _context = context;
        }

        //READ
        // GET: api/SmAgents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SmAgent>>> GetSmAgents()
        {
          if (_context.SmAgents == null)
          {
              return NotFound();
          }
            return await _context.SmAgents.ToListAsync();
        }

        //READ
        // GET: api/SmAgents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SmAgent>> GetSmAgent(int id)
        {
          if (_context.SmAgents == null)
          {
              return NotFound();
          }
            var smAgent = await _context.SmAgents.FindAsync(id);

            if (smAgent == null)
            {
                return NotFound();
            }

            return smAgent;
        }

        //UPDATE
        // PUT: api/SmAgents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSmAgent(int id, SmAgent smAgent)
        {
            if (id != smAgent.AgentId)
            {
                return BadRequest();
            }

            //_context.Entry(smAgent).State = EntityState.Modified;

            SmAgent modifiedSMAgent = _context.SmAgents.FirstOrDefault(u => u.AgentId == id);
            if (modifiedSMAgent == null)
            {
                return NotFound();
            }

            modifiedSMAgent.Code = smAgent.Code;
            modifiedSMAgent.Username = smAgent.Username;
            modifiedSMAgent.Name = smAgent.Name;
            modifiedSMAgent.Email = smAgent.Email;
            modifiedSMAgent.HourCost = smAgent.HourCost;
            modifiedSMAgent.Active = smAgent.Active;

            try
            {
                _context.SmAgents.Update(modifiedSMAgent);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SmAgentExists(id))
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
        // POST: api/SmAgents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SmAgent>> PostSmAgent(SmAgent smAgent)
        {
            if (_context.SmAgents == null)
            {
                return Problem("Entity set 'MIDPlatformContext.SmAgents'  is null.");
            }
            _context.SmAgents.Add(smAgent);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return Problem(e.InnerException.ToString(), null, null, e.Message);
            }

            return CreatedAtAction("GetSmAgent", new { id = smAgent.AgentId }, smAgent);
        }

        //DELETE
        // DELETE: api/SmAgents/5
        [HttpDelete("{id},{disable}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSmAgent(int id, bool disable = false)
        {
            if (_context.SmAgents == null)
            {
                return NotFound();
            }

            var smAgent = await _context.SmAgents.FindAsync(id);
            if (smAgent == null)
            {
                return NotFound();
            }

            if (disable)//se for true desativa, se for false apaga
            {
                smAgent.Active = false;
                _context.SmAgents.Update(smAgent);
            }
            else
            {
                _context.SmAgents.Remove(smAgent);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                try
                {
                    smAgent.Active = false;
                    _context.SmAgents.Update(smAgent);

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

        private bool SmAgentExists(int id)
        {
            return (_context.SmAgents?.Any(e => e.AgentId == id)).GetValueOrDefault();
        }
    }
}
