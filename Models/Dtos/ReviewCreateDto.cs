using System.ComponentModel.DataAnnotations;

namespace MovieApi.Models.Dtos
{
    public record ReviewCreateDto([Required] string ReviewerName, [Required] string Comment, [Range(1,5)] int Rating);
}
