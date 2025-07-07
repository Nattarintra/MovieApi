using System.ComponentModel.DataAnnotations;

namespace MovieApi.Models.Dtos
{
    public record MovieDetailDto(
        [Required] string Title,
        int Year,
        [Required] string Genre,
        int Duration,
        MovieDetailsDto MovieDetails,
        List<ReviewDto> Reviews,
        List<ActorDto> Actors

        );
}
