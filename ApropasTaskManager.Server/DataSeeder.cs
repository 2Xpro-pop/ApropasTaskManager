
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
                UserName = "director"
            };
            var result = await userManager.CreateAsync(user, "Apr@12345");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Director");
            }
        }
        if (await userManager.FindByNameAsync("projectManager") == null)
        {
            var user = new User
            {
                UserName = "projectManager",
            };
            var result = await userManager.CreateAsync(user, "Apr@12345");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "ProjectManager");
            }
        }
        if (await userManager.FindByNameAsync("employee") == null)
        {
            var user = new User
            {
                UserName = "employee",
            };
            var result = await userManager.CreateAsync(user, "Apr@12345");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Employee");
            }
        }
    }
}