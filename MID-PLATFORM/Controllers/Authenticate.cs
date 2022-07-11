using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MID_PLATFORM.Models;
using MID_PLATFORM.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MID_PLATFORM.Controllers
{
    [Route("API/")]
    [ApiController]
    [Authorize]
    public class Authenticate : ControllerBase
    {
        private readonly IJWTManagerRepository JWTManagerRepository;
        public Authenticate(IJWTManagerRepository JWTManagerRepository)
        {
            this.JWTManagerRepository = JWTManagerRepository;
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public IActionResult Get(User user)
        {
            var token = JWTManagerRepository.Authenticate(user);

            if (token == null)
                return Unauthorized();

            return Ok(token);
        }

        // GET: api/<Authenticate>
        /*[HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<Authenticate>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Authenticate>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<Authenticate>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Authenticate>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}
