using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using MongoDB.Driver;
using Serilog;
using TicketSystem_API.Models;
using TicketSystem_API.services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddControllers()
    .AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.Configure<BookingStoreDatabaseSettings>(
                builder.Configuration.GetSection(nameof(BookingStoreDatabaseSettings)));

builder.Services.AddSingleton<IBookingStoreDatabaseSettings>(sp =>
    sp.GetRequiredService<IOptions<BookingStoreDatabaseSettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(s =>
        new MongoClient(builder.Configuration.GetValue<string>("BookingStoreDatabaseSettings:ConnectionString")));

builder.Services.AddScoped<IBookingService, BookingService>();


builder.Services.Configure<ScheduleStoreDatabaseSettings>(
                builder.Configuration.GetSection(nameof(ScheduleStoreDatabaseSettings)));

builder.Services.AddSingleton<IScheduleStoreDatabaseSettings>(sp =>
    sp.GetRequiredService<IOptions<ScheduleStoreDatabaseSettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(s =>
        new MongoClient(builder.Configuration.GetValue<string>("ScheduleStoreDatabaseSettings:ConnectionString")));

builder.Services.AddScoped<IScheduleService, ScheduleService>();

builder.Services.Configure<UserStoreDatabaseSettings>(
                builder.Configuration.GetSection(nameof(UserStoreDatabaseSettings)));

builder.Services.AddSingleton<IUserStoreDatabaseSettings>(sp =>
    sp.GetRequiredService<IOptions<UserStoreDatabaseSettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(s =>
        new MongoClient(builder.Configuration.GetValue<string>("UserStoreDatabaseSettings:ConnectionString")));


builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add(HeaderNames.Accept);
    logging.RequestHeaders.Add(HeaderNames.ContentType);
    logging.RequestHeaders.Add(HeaderNames.ContentDisposition);
    logging.RequestHeaders.Add(HeaderNames.ContentEncoding);
    logging.RequestHeaders.Add(HeaderNames.ContentLength);

    logging.MediaTypeOptions.AddText("application/json");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseHttpLogging();

app.UseAuthorization();

app.MapControllers();

app.Run();
