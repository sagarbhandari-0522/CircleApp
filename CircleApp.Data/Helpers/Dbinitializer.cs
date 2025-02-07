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
                    FullName = "Sagar Bhandari",
                    ProfilePictureUrl= "https://images.unsplash.com/photo-1542613172-4ac1d7351721?q=80&w=1887&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"


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
