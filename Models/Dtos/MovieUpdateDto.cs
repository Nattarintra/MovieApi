using System.ComponentModel.DataAnnotations;

namespace MovieApi.Models.Dtos
{
    public record MovieUpdateDto
    (
        [Required] string Title,
        [Range(1995, 2025)] int Year,
        [Required] string Genre, 
        int Duration,
        MovieDetailsDto MovieDetails

    );
    
}
