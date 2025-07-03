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
       
        public ICollection<Review> Reviews { get; set; } = new List<Review>(); // 1:M
        
        public ICollection<Actor> Actors { get; set; } = new List<Actor>(); // N:M relationship with Actor



    }
}
