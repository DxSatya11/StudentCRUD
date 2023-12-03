using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using StudentAdminPortal.Data;
using StudentAdminPortal.Repository;
using System;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<StudentDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
}, ServiceLifetime.Scoped);
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    // Configure Newtonsoft.Json settings here
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; // Handle circular references
}); ;


builder.Services.AddFluentValidation(option => option.RegisterValidatorsFromAssemblyContaining<Program>());
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
//builder.Services.AddCors(

//    );

builder.Services.AddCors(option =>
{
    option.AddPolicy("angularApplication", (builder) =>
    {
        builder.WithOrigins("http://localhost:4200", "https://studentdevfrontend.azurewebsites.net")
        .AllowAnyHeader()
        .WithMethods("GET", "POSt", "PUT", "DELETE")
        .WithExposedHeaders("*");
    });
});


var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();


string connectionString = configuration.GetConnectionString("AzureBlobStorage");

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (true)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("angularApplication");
app.UseHttpsRedirection();
//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "StudentsResorces")),
//    RequestPath = "/StudentsResorces"
//});

app.UseAuthorization();


app.MapControllers();

app.Run();
