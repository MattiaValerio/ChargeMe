using Microsoft.AspNetCore.Identity;

namespace ChargeMe.Shared.Models
{
    public class User : IdentityUser 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        
    }
}
