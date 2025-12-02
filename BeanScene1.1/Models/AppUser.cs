using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BeanScene1._1.Models
{
    public class AppUser : IdentityUser
    {
        
        public string? AppUserName { get; set; }
        
        public string? Password { get; set; }
    }
}
