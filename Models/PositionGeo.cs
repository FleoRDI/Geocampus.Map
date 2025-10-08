using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Geocampus.Map.API.Models;

[Table("positiongeo")]
public partial class PositionGeo : BaseModel
{
    [PrimaryKey("idpositiongeo")]
    public int Id { get; set; }

    [Column("latitude")]
    public double Latitude { get; set; }

    [Column("longitude")]
    public double Longitude { get; set; }

    //[Column("idcarte")]
    //public int IdCarte { get; set; }
}

