using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using StaskoTravel.Models.Entities;
using System.Security.Claims;

namespace StaskoTravel.Factories
{
    public class CustomClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, IdentityRole<Guid>>
    {
        public CustomClaimsPrincipalFactory(UserManager<User> userManager, 
                                            RoleManager<IdentityRole<Guid>> roleManager, 
                                            IOptions<IdentityOptions> options) 
                                            : base(userManager, roleManager, options)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("HomeCurrency", user.HomeCurrency));

            return identity;
        }
    }
}