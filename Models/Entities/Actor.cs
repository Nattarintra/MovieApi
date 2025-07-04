using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MovieApi.Models.Entities
{
    public class Actor
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; } = string.Empty;
       
        public int BirthYear { get; set; }

        // Navigation property
        [JsonIgnore]
        public ICollection<Movie> Movies { get; set; } = new List<Movie>(); // N:M relationship with Movie



    }
}
