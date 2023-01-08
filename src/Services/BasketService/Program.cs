using BasketService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddActors(options =>
{
    options.Actors.RegisterActor<BasketActor>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapActorsHandlers();

app.Run();

