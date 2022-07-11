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
    public class CompaniesController : ControllerBase
    {
        private readonly MIDPlatformContext _context;

        public CompaniesController(MIDPlatformContext context)
        {
            _context = context;
        }

        //READ
        // GET: api/Companies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompanies()
        {
          if (_context.Companies == null)
          {
              return NotFound();
          }
            return await _context.Companies.ToListAsync();
        }

        //READ
        // GET: api/Companies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompany(int id)
        {
          if (_context.Companies == null)
          {
              return NotFound();
          }
            var company = await _context.Companies.FindAsync(id);

            if (company == null)
            {
                return NotFound();
            }

            return company;
        }

        //UPDATE
        // PUT: api/Companies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(int id, Company company)
        {
            if (id != company.CompanyId)
            {
                return BadRequest();
            }

            //_context.Entry(company).State = EntityState.Modified;

            Company modifiedCompany = _context.Companies.FirstOrDefault(c => c.CompanyId == id);
            if (modifiedCompany == null)
            {
                return NotFound();
            }

            modifiedCompany.Code = company.Code;
            modifiedCompany.Name = company.Name;
            modifiedCompany.Country = company.Country;
            modifiedCompany.Active = company.Active;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //CREATE
        // POST: api/Companies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Company>> PostCompany([FromBody]Company company)
        {
            if (_context.Companies == null)
            {
                return Problem("Entity set 'MIDPlatformContext.Companies'  is null.");
            }
            _context.Companies.Add(company);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception e)
            {
                return Problem(e.InnerException.ToString(),null,null,e.Message);
            }

            return CreatedAtAction("GetCompany", new { id = company.CompanyId }, company);
        }

        //DELETE
        // DELETE: api/Companies/5
        [HttpDelete("{id},{realDelete}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id, bool disable = false)
        {
            if (_context.Companies == null)
            {
                return NotFound();
            }

            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            if (disable)//se for true desativa, se for false apaga
            {
                company.Active = false;
                _context.Companies.Update(company);
            }
            else
            {
                _context.Companies.Remove(company);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                try
                {
                    company.Active = false;
                    _context.Companies.Update(company);

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

        private bool CompanyExists(int id)
        {
            return (_context.Companies?.Any(e => e.CompanyId == id)).GetValueOrDefault();
        }
    }
}
