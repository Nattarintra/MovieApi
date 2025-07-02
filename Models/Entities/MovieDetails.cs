namespace MovieApi.Models.Entities
{
    public class MovieDetails
    {
        public int Id { get; set; }
        public string Synopsis { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public decimal Budget { get; set; }

        // Forein Key
        public int MovieId { get; set; }

        // Navigation property
        public Movie Movie { get; set; } = null!;
    }
}
