using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace Task5.Models
{
    
    public class ApplicationUser : IdentityUser
    {
        public Pets Preferences { get; set; }
    }
}