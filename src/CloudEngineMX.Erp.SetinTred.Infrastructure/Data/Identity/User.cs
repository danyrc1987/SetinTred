using CloudEngineMX.Erp.SetinTred.Core.Entities;

namespace CloudEngineMX.Erp.SetinTred.Infrastructure.Data.Identity
{
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        public UserCore UserCore { get; set; }
    }
}
