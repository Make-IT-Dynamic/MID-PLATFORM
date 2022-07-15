using MID_PLATFORM.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using MID_PLATFORM.Controllers;

namespace MID_PLATFORM.Repository
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
        private readonly IConfiguration configuration;

        private readonly MIDPlatformContext _context;

        public JWTManagerRepository(IConfiguration configuration, MIDPlatformContext context)
        {
            this.configuration = configuration;
            _context = context;
        }

        public Tokens Authenticate(User user)
        {
            User verify = _context.Users.FindAsync(user.Username).Result;
            if (verify == null || verify.Password != user.Password)
                return null;

            //else o user é valido gera se token
            var tokenhandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(configuration["JWT:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Username)
                    }),
                //Expires=DateTime.UtcNow.AddMinutes(15),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenhandler.CreateToken(tokenDescriptor);

            return new Tokens { Token = tokenhandler.WriteToken(token) };
        }
    }
}
