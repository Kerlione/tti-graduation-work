using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using tti_graduation_work.Application.Common.Interfaces;
using tti_graduation_work.Domain.Entities;

namespace tti_graduation_work.Infrastructure.Identity
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IConfiguration _configuration;

        public AuthRepository(IConfiguration configuration, IApplicationDbContext context)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Create a token for user
        /// </summary>
        /// <param name="profile">User data and profile info</param>
        /// <returns>JWT Token with following claims:
        /// NameIdentifier - EntityId (except Administrator - UserId)
        /// Name - Username
        /// Role - role name
        /// GivenName - First Name + Last Name (user Username for Administrator)
        /// Sid - UserId
        /// </returns>
        public string CreateToken(IProfileData profile)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, profile.ProfileId.ToString()),
                new Claim(ClaimTypes.Name, profile.User.Username),
                new Claim(ClaimTypes.Role, profile.User.Role.ToString()),
                new Claim(ClaimTypes.GivenName, profile.GivenName),
                new Claim(ClaimTypes.Sid, profile.User.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var validityDays = Convert.ToInt32(_configuration.GetSection("AppSettings:ValidityDays").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(validityDays),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
