using Microsoft.AspNet.Identity.EntityFramework;

namespace Task5.Models
{
    public class UserDbContext : IdentityDbContext
    {
        public UserDbContext() : base("UserDbConnString") { }

        public System.Data.Entity.DbSet<Task5.Models.ApplicationUser> IdentityUsers { get; set; }
    }
}