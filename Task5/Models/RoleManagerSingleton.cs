using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Task5.Models
{
    public class RoleManagerSingleton
    {
        private static Lazy<RoleManagerSingleton> _lazy
            = new Lazy<RoleManagerSingleton>(() => new RoleManagerSingleton());

        public RoleManager<IdentityRole> Manager { get; private set; }

        private RoleManagerSingleton()
        {
            Manager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new UserDbContext()));
        }
        public static RoleManagerSingleton GetInstance()
        {
            return _lazy.Value;
        }
    }
}