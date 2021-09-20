using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CloudEngineMX.Erp.SetinTred.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CloudEngineMX.Erp.SetinTred.Web.Extensions.Identity
{
    public class CustomClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, IdentityRole>
    {
        public CustomClaimsPrincipalFactory(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> optionAccesor)
            : base(userManager, roleManager, optionAccesor)
        {

        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("Email", user.Email ?? ""));
            identity.AddClaim(new Claim("FirstName", GetFirstName(user) ?? ""));
            identity.AddClaim(new Claim("FullName", GetFullName(user) ?? ""));
            identity.AddClaim(new Claim("AreaName", GetAreaName(user) ?? ""));

            return identity;
        }

        private string GetFullName(User us)
        {
            var user = this.UserManager.Users.Where(x => x.Id == us.Id).Include(x => x.UserCore).FirstOrDefault();
            return $"{user.UserCore.FirstName} {user.UserCore.LastName}";
        }

        private string GetFirstName(User us)
        {
            var user = this.UserManager.Users.Where(x => x.Id == us.Id).Include(x => x.UserCore).FirstOrDefault();
            return user.UserCore.FirstName;
        }

        private string GetAreaName(User us)
        {
            var user = this.UserManager.Users.Where(x => x.Id == us.Id).Include(x => x.UserCore).ThenInclude(x => x.Area).FirstOrDefault();
            return user.UserCore.Area.AreaName;
        }
    }
}
