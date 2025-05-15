using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Gaby.World.DAL.Data;
using GabyWorld.IOC;
using Microsoft.IdentityModel.Tokens;

namespace GabyWorld.Authentication
{
    public static class JwtTokenExtensionMethods
    {
        public static string GenerateJwtToken(this ApplicationUser user)
        {
            // Set our tokens claims
            var claims = new[]
            {
                // Unique ID for this token
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),

                // The username using the Identity name so it fills out the HttpContext.User.Identity.Name value
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),

                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            // Create the credentials used to generate the token
            var credentials = new SigningCredentials(
                // Get the secret key from configuration
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(IoCContainer.Configuration["Jwt:SecretKey"])),
                // Use HS256 algorithm
                SecurityAlgorithms.HmacSha256);

            // Generate the Jwt Token
            var token = new JwtSecurityToken(
                issuer: IoCContainer.Configuration["Jwt:Issuer"],
                audience: IoCContainer.Configuration["Jwt:Audience"],
                claims: claims,
                signingCredentials: credentials,
                // Expire if not used for 3 months
                expires: DateTime.Now.AddMonths(3)
                );

            // Return token to user
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
