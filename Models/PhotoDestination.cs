using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace GeoCampus.Models
{
    [Table("photodestination")]
    public partial class PhotoDestination : BaseModel
    {
        [PrimaryKey("idphotodestination")]
        public int Id { get; set; }

        [Column("cheminversphoto")]
        public string CheminVersDestination { get; set; } = string.Empty;
    }
}
