namespace MovieApi.Models.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string ReviewerName { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public int Rating { get; set; } // rating scale of 1-5 

        // Foreign Key
        public int MovieId { get; set; } // M:1 relationship with Movie

        // Navigation property
        public Movie Movie { get; set; } = null!; 
    }
}
