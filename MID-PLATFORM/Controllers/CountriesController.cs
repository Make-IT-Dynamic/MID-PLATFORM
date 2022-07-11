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
    public class CountriesController : ControllerBase
    {
        private readonly MIDPlatformContext _context;

        public CountriesController(MIDPlatformContext context)
        {
            _context = context;
        }

        //READ
        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
        {
            if (_context.Countries == null)
            {
                return NotFound();
            }
            return await _context.Countries.ToListAsync();
        }

        //READ
        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetCountry(int id)
        {
            if (_context.Countries == null)
            {
                return NotFound();
            }
            var country = await _context.Countries.FindAsync(id);

            if (country == null)
            {
                return NotFound();
            }

            return country;
        }

        //UPDATE
        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, Country country)
        {
            if (id != country.CountryId)
            {
                return BadRequest();
            }

            //_context.Entry(country).State = EntityState.Modified;

            Country modifiedCountry = _context.Countries.FirstOrDefault(c => c.CountryId == id);
            if(modifiedCountry == null)
            {
                return NotFound();
            }

            modifiedCountry.CountryCode = country.CountryCode;
            modifiedCountry.Name = country.Name;
            modifiedCountry.Active = country.Active;

            try
            {
                _context.Countries.Update(modifiedCountry);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id))
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
        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry(Country country)
        {
            if (_context.Countries == null)
            {
                return Problem("Entity set 'MIDPlatformContext.Countries'  is null.");
            }
            _context.Countries.Add(country);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return Problem(e.InnerException.ToString(), null, null, e.Message);
            }

            return CreatedAtAction("GetCountry", new { id = country.CountryId }, country);
        }

        //DELETE
        // DELETE: api/Countries/5
        [HttpDelete("{id},{disable}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id, bool disable = false)
        {
            if (_context.Countries == null)
            {
                return NotFound();
            }

            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            if (disable)//se for true desativa, se for false apaga
            {
                country.Active = false;
                _context.Countries.Update(country);
            }
            else
            {
                _context.Countries.Remove(country);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                try
                {
                    country.Active = false;
                    _context.Countries.Update(country);

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

        private bool CountryExists(int id)
        {
            return (_context.Countries?.Any(e => e.CountryId == id)).GetValueOrDefault();
        }
    }
}
