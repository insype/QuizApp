using Microsoft.AspNetCore.Identity;

namespace QuizApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsAdmin { get; set; }
    }
}