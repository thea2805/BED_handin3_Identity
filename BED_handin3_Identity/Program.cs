using BED_handin3_Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(
        "CanEnterReception",
        policyBuilder => policyBuilder.RequireClaim("Reception", "True"));

    options.AddPolicy(
        "CanEnterRestaurant",
        policyBuilder => policyBuilder.RequireClaim("Restaurant", "True"));
    options.AddPolicy(
        "CanEnterKitchen",
        policyBuilder => policyBuilder.RequireClaim("IsWorker", "True"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();
    SeedData.SeedDBData(context);

    var userManager = services.GetService<UserManager<IdentityUser>>();
    if (userManager != null)
    {
        SeedData.SeedUsers(userManager);
    }
    else
    {
        throw new Exception("Unable to get userManager.");
    }
}

app.Run();
