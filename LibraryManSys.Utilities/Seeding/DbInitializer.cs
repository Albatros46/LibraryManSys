using LibraryManSys.Models;
using LibraryManSys.Repositories.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryManSys.Utilities.Seeding
{
    public class DbInitializer : IDbInitializer
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private ApplicationDbContext _context;

        public DbInitializer(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public void Initialize()
        {
            try
            {
                if(_context.Database.GetPendingMigrations().Count() > 0)
                {//EGer migrations yapilmadiysa buradan initalizer ederek yapilsin
                    _context.Database.Migrate();
                }
            }
            catch (Exception)
            {
                throw;
            }
            if (!_roleManager.RoleExistsAsync(WebSiteRole.WebSite_Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(WebSiteRole.WebSite_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(WebSiteRole.WebSite_User)).GetAwaiter().GetResult();
                //_roleManager.CreateAsync(new IdentityRole(WebSiteRole.WebSite_Employee)).GetAwaiter().GetResult();
                //_roleManager.CreateAsync(new IdentityRole(WebSiteRole.WebSite_Student)).GetAwaiter().GetResult();

                _userManager.CreateAsync(new ApplicationUser {
                    UserName="admin@gmail.com",
                    Email="admin@gmail.com"
                },"admin12345").GetAwaiter().GetResult();

                var appUser=_userManager.Users.Where(x=>x.Email=="admin@gmail.com").FirstOrDefault();  
                if (appUser!=null)
                {
                    _userManager.AddToRoleAsync(appUser,WebSiteRole.WebSite_Admin).GetAwaiter().GetResult();
                }
            }
        }
    }
}
