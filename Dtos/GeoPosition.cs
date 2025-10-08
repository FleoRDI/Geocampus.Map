namespace Geocampus.Map.API.Models;

public record class GeoPosition
{
    public int Id { get; set; }
    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string ImageUrl { get; set; } = string.Empty;

    public GeoPosition(int id, double lat, double lon, string name = "", string description = "", string imageUrl = "")
    {
        Id = id;
        Latitude = lat;
        Longitude = lon;
        Name = name;
        Description = description;
        ImageUrl = imageUrl;
    }
}

