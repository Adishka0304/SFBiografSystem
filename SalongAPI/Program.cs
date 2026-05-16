var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// API-nyckel middleware
app.Use(async (context, next) =>
{
    if (!context.Request.Path.StartsWithSegments("/swagger"))
    {
        var apiKey = context.Request.Headers["X-Api-Key"].FirstOrDefault()
                     ?? context.Request.Query["apikey"].FirstOrDefault();

        var validKey = builder.Configuration["ApiKey"];

        if (apiKey == null || apiKey != validKey)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Ogiltig API-nyckel");
            return;
        }
    }
    await next();
});

app.UseAuthorization();
app.MapControllers();
app.Run();