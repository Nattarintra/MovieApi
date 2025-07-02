using System.ComponentModel.DataAnnotations;

namespace MovieApi.Models.Entities
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        public required string Title { get; set; } = string.Empty;
        public int Year { get; set; }

        [Required]
        public required string Genre { get; set; } = string.Empty;
        public int Duration { get; set; }

        // Navigation property
        public MovieDetails MovieDetails { get; set; } = null!;



    }
}
