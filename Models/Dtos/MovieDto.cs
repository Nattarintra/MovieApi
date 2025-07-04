using MovieApi.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace MovieApi.Models.Dtos
{
    public record MovieDto([Required]string Title, int Year,[Required]string Genre,int Duration);
   
}
