using Microsoft.AspNetCore.Identity;

namespace crpt.Models
{
    public class Writer : IdentityUser<int>
    {
        public bool isAdmin { get; set; }
    }
}