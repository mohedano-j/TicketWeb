using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using IssuesManagement.Services.Data;
using IssuesManagement.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add db context
var connectionString = builder.Configuration.GetConnectionString("IssuesSystemDb");
builder.Services.AddDbContext<IssuesDbContext>(options =>
    options.UseSqlServer(connectionString));

//TODO Handle Authentication
/*
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<TicketSystemContext>();

builder.Services.AddIdentityServer()
    .AddApiAuthorization<ApplicationUser, TicketSystemContext>();

builder.Services.AddAuthentication()
    .AddIdentityServerJwt();
*/

//Dependency Injection

//repos
builder.Services.AddTransient<IIssuesRepository, IssuesRepository>();
builder.Services.AddTransient<IUsersRepository, UsersRepository>();

//services
builder.Services.AddTransient<IIssuesService, IssuesService>();
builder.Services.AddTransient<IUsersService, UsersService>();

//End of Dependency Injection

//Json options 
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles); ;
    
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add SPA entry point
builder.Services.AddSpaStaticFiles(config => config.RootPath = "ClientApp/build");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

//app.UseAuthentication(); TODO Add Authentication

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();

app.UseSpaStaticFiles();

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
		name: "default",
		pattern: "{controller}/{action=Index}/{id?}");
});

app.UseSpa(spa =>
{
    spa.Options.SourcePath = "ClientApp";
});

app.Run();