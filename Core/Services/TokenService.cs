using Application.ApplicationUser.Queries.GetToken;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICurrentUserService _currentUserService;

        public TokenService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, ICurrentUserService currentUserService)
        {
            _configuration = configuration;
            _currentUserService = currentUserService;
            _httpContextAccessor = httpContextAccessor;
        }
        bool ITokenService.ValidateToken()
        {
            var identity =_currentUserService.ClaimsPrincipal.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userclaims = identity.Claims;
                var Name = userclaims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault().ToString();
                var Username = userclaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value;
                var Role = userclaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value;


            }//  return AuthorizationResult.Succeed(Username, Role, Name);
                return true;
        }
        string ITokenService.CreateJwtSecurityToken(string AuthToken)
        {
            string Name = "Rohit";
            string Role = "User"; //Get Roles from DB
            var authClaims = new List<Claim>
                {
                    //new Claim(ClaimTypes.NameIdentifier, userAuth.Email),
                    new Claim(ClaimTypes.NameIdentifier, AuthToken),
                    new Claim(ClaimTypes.Name, Name),
                    new Claim(ClaimTypes.Role, Role),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token =  new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(90),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
