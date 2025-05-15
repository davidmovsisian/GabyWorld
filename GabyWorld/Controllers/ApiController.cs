using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using GabyWorld.IOC;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using GabyWorld.Authentication;
using Gaby.World.DAL.Data;
using Microsoft.AspNetCore.Identity;
using Gaby.World.Core.ApiModels.LoginRegister;
using Gaby.World.Core.ApiModels.Base;

namespace GabyWorld.Controllers
{
    //manages web api calls 
    public class ApiControllerm : Controller
    {
        #region members and constructor
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ApiControllerm(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        #endregion

        #region Register User
        [AllowAnonymous]
        [Route("api/register")]
        [HttpPost]
        public async Task<ApiResponse<RegisterResultApiModel>> RegisterAsync([FromBody] RegisterCredentialsApiModel model)
        {
            var errorMessage = "Please fill all requested fields";

            if(model == null || (model.UserName.IsNullOrEmpty() && model.Email.IsNullOrEmpty())) {
                return new ApiResponse<RegisterResultApiModel>
                {
                    ErrorMessage = errorMessage
                };
            }

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
            };

            //create user in DB
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var userIdentity = await _userManager.FindByNameAsync(model.UserName);

                return new ApiResponse<RegisterResultApiModel>
                {
                    Response = new RegisterResultApiModel
                    {
                        Email = model.Email,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        UserName = model.UserName,
                        Token = userIdentity.GenerateJwtToken(),
                    }
                };
            }

            return new ApiResponse<RegisterResultApiModel>
            {
                ErrorMessage = result.Errors.AgAggregateErrors()
            };
        }

        #endregion
        [Route("api/login")]
        public IActionResult LogIn(RegisterCredentialsApiModel model)
        {
            // TODO: Get users login information and check it is correct

            // For now set username
            var username = "DavidM`";

            // Set our tokens claims
            var claims = new[]
            {
                // Unique ID for this token
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),

                // The username using the Identity name so it fills out the HttpContext.User.Identity.Name value
                new Claim(ClaimsIdentity.DefaultNameClaimType, username),
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
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }

        [Route("api/private")]
        [AuthorizeToken]//JWT authorization
        public IActionResult Private()
        {
            var user = HttpContext.User;
            return Content($"Api private area-{user.Identity.Name}");
        }
    }
}
