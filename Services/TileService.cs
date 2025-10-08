using Microsoft.Data.Sqlite;

namespace Geocampus.Map.API.Services;

public class TileService
{
    private readonly string? dbPath;

    public TileService(IConfiguration config)
    {
        dbPath = config.GetConnectionString("MapDB");
    }

    public byte[]? GetTile(int z, int x, int y)
    {
        // MBTiles stores Y coordinate flipped
        int flippedY = (int)(Math.Pow(2, z) - 1 - y); 

        using var connection = new SqliteConnection($"{dbPath}");
        connection.Open();

        using var cmd = connection.CreateCommand();
        cmd.CommandText = "SELECT tile_data FROM tiles WHERE zoom_level = @z AND tile_column = @x AND tile_row = @y LIMIT 1";
        cmd.Parameters.AddWithValue("@z", z);
        cmd.Parameters.AddWithValue("@x", x);
        cmd.Parameters.AddWithValue("@y", flippedY);

        var result = cmd.ExecuteScalar();
        return result as byte[];
    }


}