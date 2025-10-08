using Geocampus.Map.API.Services;
using GeoCampus.Map.API.Services;
using Supabase;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500") // Allowed origin(s)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


var options = new SupabaseOptions
{
    AutoConnectRealtime = true
};

var url = builder.Configuration["Supabase:Url"] ?? throw new InvalidOperationException("Supabase URL is not configured.");
var key = builder.Configuration["Supabase:Key"] ?? throw new InvalidOperationException("Supabase Key is not configured.");

SupaDataBase supa = new(new Client(url, key, options));

var app = builder.Build();

var port = Environment.GetEnvironmentVariable("PORT") ?? "10000";

app.Urls.Add($"http://0.0.0.0:{port}");

// Use CORS(Cross-Origin Request ) middleware
app.UseCors("AllowSpecificOrigins");

TileService TileS = new(builder.Configuration);

//double latitudeCentre = 7.417283519316341;
//double longitudeCentre = 13.546677129528522;


//Give all geo positions from db
app.MapGet("/points", async () =>
{
    var points = await supa.GetPointsOfInterests();

    if (points == null || points.Count == 0)
        return Results.NotFound("No points found"); 

    return Results.Ok(points);
});


//Give map tiles from db
app.MapGet("tiles/{z:int}/{x:int}/{y:int}.png", async (int z, int x, int y, HttpResponse response) =>
{
    try
    {
        var result = TileS.GetTile(z, x, y);

        if (result == null)
            return Results.NotFound("Tile not found");

        response.ContentType = "image/png";
        response.Headers.CacheControl = "public, max-age=31536000, immutable";
        response.Headers.ETag = $"\"{z}-{x}-{y}-{result.Length}\"";

        await response.Body.WriteAsync(result);

        return Results.Ok();
    }
    catch
    {
        return Results.StatusCode(500);
    }
});

app.Run();
