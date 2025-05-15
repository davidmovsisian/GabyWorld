using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace GabyWorld.Authentication
{
    public static class IdentityErrorExtensions
    {
        public static string AgAggregateErrors(this IEnumerable<IdentityError> errors)
        {
            return errors.Select(e => e.Description)
                .Aggregate((a, b) => $"{a}{Environment.NewLine}{b}");
        }
    }
}
