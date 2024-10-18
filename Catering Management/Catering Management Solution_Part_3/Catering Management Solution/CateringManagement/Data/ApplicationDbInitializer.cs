using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CateringManagement.Data
{
    public static class ApplicationDbInitializer
    {
        public static async void Seed(IApplicationBuilder applicationBuilder)
        {
            ApplicationDbContext context = applicationBuilder.ApplicationServices.CreateScope()
                .ServiceProvider.GetRequiredService<ApplicationDbContext>();
            try
            {
                //Create the database if it does not exist and apply the Migration
                context.Database.Migrate();

                //Create Roles
                var RoleManager = applicationBuilder.ApplicationServices.CreateScope()
                    .ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                string[] roleNames = { "Admin", "Security", "Supervisor", "Staff" };
                IdentityResult roleResult;
                foreach (var roleName in roleNames)
                {
                    var roleExist = await RoleManager.RoleExistsAsync(roleName);
                    if (!roleExist)
                    {
                        roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }
                //Create Users
                var userManager = applicationBuilder.ApplicationServices.CreateScope()
                    .ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                if (userManager.FindByEmailAsync("Admin@outlook.com").Result == null)
                {
                    IdentityUser user = new IdentityUser
                    {
                        UserName = "admin@outlook.com",
                        Email = "admin@outlook.com",
                        EmailConfirmed = true
                    };

                    IdentityResult result = userManager.CreateAsync(user, "Pa55w@rd").Result;

                    if (result.Succeeded)
                    {
                        await userManager.AddToRolesAsync(user, new[] { "Admin", "Security" });
                    }
                }
                if (userManager.FindByEmailAsync("security@outlook.com").Result == null)
                {
                    IdentityUser user = new IdentityUser
                    {
                        UserName = "security@outlook.com",
                        Email = "security@outlook.com",
                        EmailConfirmed = true
                        
                    };

                    IdentityResult result = userManager.CreateAsync(user, "Pa55w@rd").Result;

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "Security");
                    }
                }
                if (userManager.FindByEmailAsync("supervisor@outlook.com").Result == null)
                {
                    IdentityUser user = new IdentityUser
                    {
                        UserName = "supervisor@outlook.com",
                        Email = "supervisor@outlook.com",
                        EmailConfirmed = true
                    };

                    IdentityResult result = userManager.CreateAsync(user, "Pa55w@rd").Result;

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "Supervisor");
                    }
                }
                if (userManager.FindByEmailAsync("staff@outlook.com").Result == null)
                {
                    IdentityUser user = new IdentityUser
                    {
                        UserName = "staff@outlook.com",
                        Email = "staff@outlook.com",
                        EmailConfirmed = true
                    };

                    IdentityResult result = userManager.CreateAsync(user, "Pa55w@rd").Result;

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "Staff");
                    }
                }
                if (userManager.FindByEmailAsync("user@outlook.com").Result == null)
                {
                    IdentityUser user = new IdentityUser
                    {
                        UserName = "user@outlook.com",
                        Email = "user@outlook.com",
                        EmailConfirmed = true
                    };

                    IdentityResult result = userManager.CreateAsync(user, "Pa55w@rd").Result;
                    //Not in any role
                }
                if (userManager.FindByEmailAsync("jkaluba@niagaracollege.ca").Result == null)
                {
                    IdentityUser user = new IdentityUser
                    {
                        UserName = "jkaluba@niagaracollege.ca",
                        Email = "jkaluba@niagaracollege.ca",
                        EmailConfirmed = true
                    };

                    IdentityResult result = userManager.CreateAsync(user, "Pa55w@rd").Result;

                    if (result.Succeeded)
                    {
                        await userManager.AddToRolesAsync(user, new[] { "Admin", "Security" });
                    }
                }

                if (userManager.FindByEmailAsync("dstovell@niagaracollege.ca").Result == null)
                {
                    IdentityUser user = new IdentityUser
                    {
                        UserName = "dstovell@niagaracollege.ca",
                        Email = "dstovell@niagaracollege.ca",
                        EmailConfirmed = true
                    };

                    IdentityResult result = userManager.CreateAsync(user, "Pa55w@rd").Result;

                    if (result.Succeeded)
                    {
                        await userManager.AddToRolesAsync(user, new[] { "Admin", "Security" });
                    }
                }

                if (userManager.FindByEmailAsync("spatel315@ncstudents.niagaracollege.ca").Result == null)
                {
                    IdentityUser user = new IdentityUser
                    {
                        UserName = "spatel315@ncstudents.niagaracollege.ca",
                        Email = "spatel315@ncstudents.niagaracollege.ca",
                        EmailConfirmed = true
                    };

                    IdentityResult result = userManager.CreateAsync(user, "Pa55w@rd").Result;

                    if (result.Succeeded)
                    {
                        await userManager.AddToRolesAsync(user, new[] { "Admin", "Security" });
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetBaseException().Message);
            }
        }
    }

}
