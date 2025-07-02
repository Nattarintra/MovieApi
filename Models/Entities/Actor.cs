using System.ComponentModel.DataAnnotations;

namespace MovieApi.Models.Entities
{
    public class Actor
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; } = string.Empty;
        // The birth year of the actor
        public int BirthYear { get; set; }



    }
}
