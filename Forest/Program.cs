using Forest.Data;
using Microsoft.AspNetCore.Identity;
using Forest.Data.Models.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;

var builder = WebApplication.CreateBuilder(args);

// Retrieve the connection string
//var connectionString = builder.Configuration.GetConnectionString("ForestContext") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
var connectionString = "Server=tcp:sqlserver-forest-001.database.windows.net,1433;Initial Catalog=sqldb-forest;Persist Security Info=False;User ID=Forest;Password=abcd@1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
var storageConnection = "DefaultEndpointsProtocol=https;AccountName=stforestsouthukdev001;AccountKey=jDABENqdM6AtsdyH8bRq1ir+N7OGz6ryOwvhG3aZhVqxbh5rDbvHmg1MI3y+rYB4oL5XomwWZUyY+ASt4sVYSA==;EndpointSuffix=core.windows.net";


builder.Services.AddAzureClients(azureBuilder =>
{
    azureBuilder.AddBlobServiceClient(storageConnection);
});
// Add DbContext for application-specific data
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Add DbContext for ASP.NET Core Identity
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Combine the identity setup into a single call
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseDeveloperExceptionPage(); // Include this to see detailed exceptions during development
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthentication(); // Ensure authentication is called before authorization
app.UseAuthorization();

app.MapControllerRoute(
    name: "genres",
    pattern: "{controller=Genre}/{action=GetGenres}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
