using Domain.Entities;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Presentation.Services;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers(options =>
                { 
                    options.ReturnHttpNotAcceptable = true; 
                    options.RespectBrowserAcceptHeader = false;
                })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                })
                .AddXmlSerializerFormatters();

builder.Services.AddCors( options =>
{
    options.AddPolicy("AllowAll",builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CodeTeasersConnectionString")));

builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ProblemRepository>();
builder.Services.AddScoped<ProblemService>();
builder.Services.AddScoped<CategoryRepository>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 2 * 1024 * 1024; // Limit file size to 2 MB sent [FromForm]
});

var app = builder.Build();

app.UseCors("AllowAll");
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
