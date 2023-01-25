using Microsoft.AspNetCore.Identity;

namespace ZeroIdentity.Models
{
    public class ApplicationRole: IdentityRole
    {
        public string RoleId { get; set;} = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
