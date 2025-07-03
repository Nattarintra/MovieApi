using System.Text.Json.Serialization;

namespace MovieApi.Models.Entities
{
    public class MovieDetails
    {
        public int Id { get; set; }
        public string Synopsis { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public int Budget { get; set; }

        // Forein Key
        public int MovieId { get; set; }

        // Navigation property
        [JsonIgnore]
        public Movie Movie { get; set; } = null!;
    }
}
