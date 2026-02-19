using backend.Data;
using backend.Domain.Entities;
using backend.Exceptions.Handlers;
using backend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Exception handlers

builder.Services.AddControllers();

builder.Services.AddSingleton<MongoDbService>();
builder.Services.AddSingleton(sp =>
{
    var db = sp.GetRequiredService<MongoDbService>().Database;
    return db.GetCollection<Trip>("trips");
});

builder.Services.AddScoped<TripService>();
builder.Services.AddScoped<LinkService>();
builder.Services.AddScoped<DayService>();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(_ => { });
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
