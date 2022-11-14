using bookstore.Auth.Model;
using Microsoft.AspNetCore.Identity;

namespace bookstore.Data
{
    public class AuthDbSeeder
    {
        private readonly UserManager<BookstoreUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AuthDbSeeder(UserManager<BookstoreUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            await AddDefaultRoles();
            await AddAdminUser();
        }

        private async Task AddAdminUser()
        {
            var newAdminUser = new BookstoreUser
            {
                //cia gali buti kad truksta fieldu
                UserName = "admin"
                //Email = "admin@admin.com",
            };

            var adminExist = await userManager.FindByNameAsync(newAdminUser.UserName);

            if(adminExist == null)
            {
                //cia gali buti, kad blogas passwordas
                var createAdminResult = await userManager.CreateAsync(newAdminUser, "admin123");
                if (createAdminResult.Succeeded)
                {
                    await userManager.AddToRolesAsync(newAdminUser, BookstoreRoles.All);
                }
            }
        }

        private async Task AddDefaultRoles()
        {
            foreach (var role in BookstoreRoles.All)
            {
                var roleExist = await roleManager.RoleExistsAsync(role);
                if (!roleExist)
                    await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}
