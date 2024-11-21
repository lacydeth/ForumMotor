using Microsoft.AspNetCore.Identity;

namespace ForumMotor_13BC_H.Models
{
    public class User : IdentityUser
    {
        public string? LastName { get; set; }
        public string? FirstName {  get; set; }
        public DateTime BirthDate { get; set; }
    }
}
