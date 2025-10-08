using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using ColumnAttribute = Supabase.Postgrest.Attributes.ColumnAttribute;
using TableAttribute = Supabase.Postgrest.Attributes.TableAttribute;

namespace GeoCampus.Models
{
    [Table("surface")]
    public class Surface : BaseModel
    {
        [PrimaryKey("idsurface")]
        public int Id { get; set; }

        [Column("nom")]
        public string Nom { get; set; } = string.Empty;

        [Column("description")]
        public string Description { get; set; } = string.Empty;

        [Column("idtypesurface")]
        public int IdTypeSurface { get; set; }

        [Column("idphotodestination")]
        public int IdPhotoDestination { get; set; }

        [Column("idpositiongeo")]
        public int IdPositionGeo { get; set; }
    }
}
