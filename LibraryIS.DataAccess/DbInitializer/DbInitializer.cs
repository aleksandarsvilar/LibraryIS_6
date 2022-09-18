using LibraryIS.Models;
using LibraryIS.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryIS.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _applicationDbContext;

        public DbInitializer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext applicationDbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _applicationDbContext = applicationDbContext;
        }

        public void Initialize()
        {
            try
            {
                if(_applicationDbContext.Database.GetPendingMigrations().Count() > 0)
                {
                    _applicationDbContext.Database.Migrate();
                }
            }
            catch
            {
            }

            if (!_roleManager.RoleExistsAsync(UserRoles.Role_Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(UserRoles.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(UserRoles.Role_Guest)).GetAwaiter().GetResult();

                _userManager.CreateAsync(new ApplicationUser 
                { 
                    UserName = "admin@libraryis.com",
                    Email = "admin@libraryis.com",
                    Name = "Admin",
                    PhoneNumber = "0111234567"
                }, "Test123!").GetAwaiter().GetResult();

                ApplicationUser user = _applicationDbContext.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@libraryis.com");
                _userManager.AddToRoleAsync(user, UserRoles.Role_Admin).GetAwaiter().GetResult();
            }
        }

        public void SeedDatabase()
        {
            throw new NotImplementedException();
        }
    }
}
