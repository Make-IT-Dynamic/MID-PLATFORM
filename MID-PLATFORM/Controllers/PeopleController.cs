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
    public class PeopleController : ControllerBase
    {
        private readonly MIDPlatformContext _context;

        public PeopleController(MIDPlatformContext context)
        {
            _context = context;
        }

        //READ
        // GET: api/People
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPeople()
        {
          if (_context.People == null)
          {
              return NotFound();
          }
            return await _context.People.ToListAsync();
        }

        //READ
        // GET: api/People/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
          if (_context.People == null)
          {
              return NotFound();
          }
            var person = await _context.People.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

        //UPDATE
        // PUT: api/People/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(int id, Person person)
        {
            if (id != person.PersonId)
            {
                return BadRequest();
            }

            //_context.Entry(person).State = EntityState.Modified;

            Person modifiedPerson = _context.People.FirstOrDefault(u => u.PersonId == id);
            if (modifiedPerson == null)
            {
                return NotFound();
            }

            modifiedPerson.Company = person.Company;
            modifiedPerson.Name = person.Name;
            modifiedPerson.Email = person.Email;
            modifiedPerson.Active = person.Active;

            try
            {
                _context.People.Update(modifiedPerson);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
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
        // POST: api/People
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {
          if (_context.People == null)
          {
              return Problem("Entity set 'MIDPlatformContext.People'  is null.");
          }
            _context.People.Add(person);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return Problem(e.InnerException.ToString(), null, null, e.Message);
            }

            return CreatedAtAction("GetPerson", new { id = person.PersonId }, person);
        }

        //DELETE
        // DELETE: api/People/5
        [HttpDelete("{id},{disable}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id, bool disable = false)
        {
            if (_context.People == null)
            {
                return NotFound();
            }

            var person = await _context.People.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            if (disable)//se for true desativa, se for false apaga
            {
                person.Active = false;
                _context.People.Update(person);
            }
            else
            {
                _context.People.Remove(person);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                try
                {
                    person.Active = false;
                    _context.People.Update(person);

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

        private bool PersonExists(int id)
        {
            return (_context.People?.Any(e => e.PersonId == id)).GetValueOrDefault();
        }
    }
}
