using Highscore.Areas.API.Models.DTO;
using Highscore.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Highscore.Areas.API.Conroller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        public AuthController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        /// <summary>
        /// Uppdatera ett spel
        /// </summary>
        /// <param name="credentialDto">InloggningsUppgifter</param>


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task< ActionResult<AuthTokenDto>>CreateToken(CredentialDto credentialDto)
        {
            var user = await userManager.FindByNameAsync(credentialDto.UserName);
            var hasAccess = await userManager.CheckPasswordAsync(user , credentialDto.Password);
            if (!hasAccess)
            {
                return Unauthorized();//401
            }

            var token = GenerateToken(user);
            return Ok(token);
        }

        
        private AuthTokenDto GenerateToken(ApplicationUser user)
        {
            var signingKey = Convert.FromBase64String("tKE+pMd2rQAHBbOjXWTZqacLJRLqlrnTzZdmKRJEXLjtiGOnFY3w+vuUxPSgLdMFbbVXxPrFWNUd/yQyG5PsEg==");

            var claims = new List<Claim>();
            var roles = userManager.GetRolesAsync(user).Result;
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(signingKey),
                    SecurityAlgorithms.HmacSha256Signature),
                    Subject = new ClaimsIdentity(claims)
            };

            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var jwtSecurityToken = jwtTokenHandler
              .CreateJwtSecurityToken(tokenDescriptor);

            var authToken = new AuthTokenDto
            {
                Token = jwtTokenHandler.WriteToken(jwtSecurityToken)
            };

            return authToken;
            
        }
    }
}
