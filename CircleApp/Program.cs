using CircleApp.Data;
using CircleApp.Data.Helpers;
using CircleApp.Services;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IPostsService, PostsService>();
builder.Services.AddScoped<IHashtagService, HashtagService>();
builder.Services.AddScoped<IStoryService, StoryService>();
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

//Seed the database with the initial data
using(var scope=app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await Dbinitializer.InitializeAsync(dbContext);

}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
