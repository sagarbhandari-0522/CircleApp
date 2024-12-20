using CircleApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircleApp.Data.Helpers
{
    public static class Dbinitializer
    {
        public static async Task InitializeAsync(ApplicationDbContext context)
        {
            await context.Database.MigrateAsync();
        
      
            if (!context.Users.Any() && !context.Posts.Any())
            {
                var User = new User()
                {
                    FullName = "Sagar Bhandari"

                };
                await context.Users.AddAsync(User);
                await context.SaveChangesAsync();
                var newPostWithoutImage = new Post()
                {
                    Content = "This is my first Post",
                    ImageUrl = "imageurl",
                    NrOfReports = 0,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    UserId = User.Id

                };
                var newPostWithImage = new Post()
                {
                    Content = "This is my second Post",
                    ImageUrl = "imageurl",
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
