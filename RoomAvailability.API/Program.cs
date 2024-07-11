using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RoomAvailability.Core.Interfaces;
using RoomAvailability.Core.Interfaces.Infrastructure;
using RoomAvailability.Core.Services;
using RoomAvailability.Infrastructure;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<IRoomAvailabilityClient, RoomAvailabilityClient>(c =>
    c.BaseAddress = new Uri(builder.Configuration["RoomService:Url"]!)
);
builder.Services.AddScoped<IRoomAvailabilityService, RoomAvailabilityService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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

app.UseExceptionHandler(appError =>
    appError.Run(async context =>
    {
        var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
        var ex = errorFeature?.Error;
        if(ex != null)
        {
            context.Response.ContentType = "application/problem+json";
            var problem = new ProblemDetails
            {
                Status = 500,
                Title = "An error occured",
                Detail = app.Environment.IsDevelopment() ? ex.ToString() : null
            };

            var stream = context.Response.Body;
            await JsonSerializer.SerializeAsync(stream, problem);
        }
    })
);

app.Run();
