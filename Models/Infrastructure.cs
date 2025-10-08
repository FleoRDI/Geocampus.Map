using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using ColumnAttribute = Supabase.Postgrest.Attributes.ColumnAttribute;
using TableAttribute = Supabase.Postgrest.Attributes.TableAttribute;

namespace GeoCampus.Models
{
    [Table("infrastructure")]
    public partial class Infrastructure : BaseModel
    {
        [PrimaryKey("idinfrastructure")]
        public int Id { get; set; }

        [Column("nom")]
        public string Nom { get; set; } = string.Empty;

        [Column("description")]
        public string Description { get; set; } = string.Empty;

        [Column("datecreation")]
        public string? DateCreationEcole { get; set; }

        [Column("nomdirigeant")]
        public string? NomDirigeant { get; set; }

        [Column("idphotodestination")]
        public int IdPhotoDestination { get; set; } = 1;

        [Column("idpositiongeo")]
        public int IdPositionGeo { get; set; } = 1;
    }
    
    
}
