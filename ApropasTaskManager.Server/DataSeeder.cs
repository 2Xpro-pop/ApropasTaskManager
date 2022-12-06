
using ApropasTaskManager.Shared;
using Microsoft.AspNetCore.Identity;

namespace ApropasTaskManager.Server;

public static class DataSeeder
{
    public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        // Создание ролей
        foreach (var role in Enum.GetNames<UserRoles>())
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
        // Создание пользователей
        if (await userManager.FindByNameAsync("director") == null)
        {
            var user = new User
            {
                UserName = "director",
                Role = UserRoles.Director,
                Profile = new UserProfile
                {
                    Name = "MyLord"
                }
            };
            var result = await userManager.CreateAsync(user, "Apr@12345");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, Enum.GetName(UserRoles.Director));
            }
        }
        if (await userManager.FindByNameAsync("projectManager") == null)
        {
            var user = new User
            {
                UserName = "projectManager",
                Role = UserRoles.ProjectManager,
                Profile = new UserProfile
                {
                    Name = "Stan"
                }
            };
            var result = await userManager.CreateAsync(user, "Apr@12345");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, Enum.GetName(UserRoles.ProjectManager));
            }
        }
        if (await userManager.FindByNameAsync("employee") == null)
        {
            var user = new User
            {
                UserName = "employee",
                Role = UserRoles.Employee,
                Profile = new UserProfile
                {
                    Name = "Steve"
                }
            };
            var result = await userManager.CreateAsync(user, "Apr@12345");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, Enum.GetName(UserRoles.Employee));
            }
        }
    }
}