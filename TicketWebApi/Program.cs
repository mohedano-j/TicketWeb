using Microsoft.EntityFrameworkCore;
using Tickets.Services.Data;
using Tickets.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("TicketSystemDb");
builder.Services.AddDbContext<TicketSystemContext>(options =>
    options.UseSqlServer(connectionString));

//repos
builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddTransient<IProjectRepository, ProjectRepository>();
builder.Services.AddTransient<IStatusRepository, StatusRepository>();
builder.Services.AddTransient<ITicketRepository, TicketRepository>();

//services
builder.Services.AddTransient<IEmployeesService, EmployeesService>();
builder.Services.AddTransient<IProjectsService, ProjectsService>();
builder.Services.AddTransient<IStatusService, StatusService>();
builder.Services.AddTransient<ITicketsService, TicketsService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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