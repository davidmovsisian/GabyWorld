using Microsoft.AspNetCore.Authorization;

namespace GabyWorld.Custom
{
    public class AuthorizeTokenAttribute : AuthorizeAttribute
    {
        public AuthorizeTokenAttribute()
        {
            AuthenticationSchemes = "Bearer";
        }
    }
}
