using CircleApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircleApp.Data.Helpers
{
    public static class Dbinitializer
    {
        public static async Task SeedRolesAndUsersAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            // Creating Roles
            string[] roleNames = { "Admin", "User" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole<int>(roleName));
                }

            }

            // Creating  Users
            var adminEmail = "admin@example.com";
            var adminPassword = "Admin@123";
            var adminFullName = "Admin Admin";
            string userEmail = "user@gmail.com";
            string userPassword = "User@123";
            var userFullName = "User User";
            bool hasNormalUser = await userManager.Users.AnyAsync(u => !(string.IsNullOrEmpty(u.Email)));
            if (!hasNormalUser)
            {
                // Creating Admin User
                var adminUser = new User()
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    FullName = adminFullName
                };
                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    var savedUser = await userManager.Users.FirstOrDefaultAsync(u => u.Email == adminEmail);
                    Console.WriteLine(savedUser.Id);
                    Console.WriteLine(new string('*', 100));
                    if (savedUser != null)
                    {

                        await userManager.AddToRoleAsync(savedUser, "Admin");
                    }
                }

                // Creating Normal User

                var normalUser = new User()
                {
                    UserName = userEmail,
                    Email = userEmail,
                    EmailConfirmed = true,
                    FullName = userFullName
                };
                var resultNormalUser = await userManager.CreateAsync(normalUser, userPassword);
                if (resultNormalUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(normalUser, "User");
                }
            }
        }
        public static async Task InitializeAsync(ApplicationDbContext context)
        {
            await context.Database.MigrateAsync();


            if (!context.Users.Any() && !context.Posts.Any())
            {
                var User = new User()
                {
                    FullName = "Sagar Bhandari",
                    ProfilePictureUrl = "https://images.unsplash.com/photo-1542613172-4ac1d7351721?q=80&w=1887&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"


                };
                await context.Users.AddAsync(User);
                await context.SaveChangesAsync();

                var newPostWithoutImage = new Post()
                {
                    Content = "This is my first Post",
                    ImageUrl = "https://plus.unsplash.com/premium_photo-1667126445804-79202e10a28a?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    NrOfReports = 0,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    UserId = User.Id

                };
                var newPostWithImage = new Post()
                {
                    Content = "This is my second Post",
                    ImageUrl = "https://images.unsplash.com/photo-1556609894-0ae309cb8354?q=80&w=1887&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    NrOfReports = 0,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    UserId = User.Id
                };
                await context.Posts.AddRangeAsync(newPostWithoutImage, newPostWithImage);
                await context.SaveChangesAsync();


            }

        }
    }
}
