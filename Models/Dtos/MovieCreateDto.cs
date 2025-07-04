using MovieApi.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace MovieApi.Models.Dtos
{
    public record MovieCreateDto
        (
        [Required] string Title,
        [Required] string Genre, 
        [Range(1995, 2025)] int Year,
        int Duration,
        MovieDetailsDto MovieDetails

        );

    public record MovieDetailsDto(string Synopsis,string Language,int Budget);
}
