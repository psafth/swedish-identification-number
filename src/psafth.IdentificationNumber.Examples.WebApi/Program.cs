using Microsoft.EntityFrameworkCore;
using psafth.IdentificationNumber.Examples.WebApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(opts => opts.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=IdentificationNumber_WebApi;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;"));

//builder.Services.AddControllers()
//    .AddJsonOptions(options =>
//    {
//        options.JsonSerializerOptions.Converters.Add(new IdentificationNumberJsonConverter());
//    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
