using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using TestAspDotNetCore.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Console.WriteLine(Environment.GetEnvironmentVariable("ConnectionStrings__SqlDBConnectionString"));
//Console.WriteLine(Environment.GetEnvironmentVariable(host.docker.internal));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddDbContext<ContactAPIDbContext>(options => options.UseInMemoryDatabase("ContactsDb"));
builder.Services.AddDbContext<ContactAPIDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ContactsApiConnectionString"), sql => sql.MigrationsAssembly(typeof(Program).Assembly.GetName().Name)));

//builder.Services.AddDbContext<ContactAPIDbContext>(options =>
//options.UseSqlServer(Environment.GetEnvironmentVariable("ConnectionStrings__SqlDBConnectionString"), sql => sql.MigrationsAssembly(typeof(Program).Assembly.GetName().Name)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowAnyMethod());
app.UseAuthorization();

app.MapControllers();

app.Run();
