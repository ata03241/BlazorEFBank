using Microsoft.AspNetCore.Identity;
using BlazorEFIdentity.Model;

namespace BlazorEFIdentity.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Handle { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string Personnummer { get; set; } = Guid.NewGuid().ToString();  
        public List<Account> Accounts { get; set; } 
    }
}

