using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace GeoCampus.Models
{
    [Table("salle")]
    public class Salle : BaseModel
    {
        [PrimaryKey("idsalle")]
        public int Id { get; set; }

        [Column("nom")]
        public string Nom { get; set; } = string.Empty;

        [Column("description")]
        public string Description { get; set; } = string.Empty;

        [Column("propriétaire")]
        public string? Proprio { get; set; }

        [Column("contenance")]
        public int Contenance { get; set; }

        [Column("disponibilite")]
        public bool Disponibilite { get; set; }

        [Column("tag")]
        public string Tag { get; set; } = "n/a";

        [Column("idphotodestination")]
        public int IdPhotoDestination { get; set; }

        [Column("idpositiongeo")]
        public int IdPositionGeo { get; set; }

    }
}
