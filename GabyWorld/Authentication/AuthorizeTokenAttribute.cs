using Microsoft.AspNetCore.Authorization;

namespace GabyWorld.Authentication
{
    public class AuthorizeTokenAttribute : AuthorizeAttribute
    {
        public AuthorizeTokenAttribute()
        {
            AuthenticationSchemes = "Bearer";
        }
    }
}
