using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.Models.Dtos;
using MovieApi.Models.Entities;

namespace MovieApi.Controllers
{
    [ApiController]
    [Route("api/movies/{movieId}/reviews")]
    public class MovieReviewsController : ControllerBase
    {
        private readonly MovieApiContext _context;

        public MovieReviewsController(MovieApiContext context)
        {
            _context = context;
        }
        // GET: api/movies/{movieId}/reviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviewsForMovie(int movieId)
        {
            var movie = await _context.Movies.FindAsync(movieId);
            if (movie == null) return NotFound($"Movie with ID {movieId} not found.");

            var reviews = await _context.Reviews
                .Where(r => r.MovieId == movieId)
                .Select(r => new ReviewDto(r.Id, r.ReviewerName, r.Comment, r.Rating))
                .ToListAsync(); 
            return Ok(reviews);
        }
        // POST: api/movies/{movieId}/reviews
        [HttpPost]
        public async Task<ActionResult<ReviewDto>> AddReviewToMovie(int movieId, ReviewCreateDto dto)
        {
            var movie = await _context.Movies.FindAsync(movieId);
            if (movie == null) return NotFound($"Movie with ID {movieId} not found.");

            var reviewDto = new Review
            {   ReviewerName = dto.ReviewerName,
                Comment = dto.Comment,
                Rating = dto.Rating,
                MovieId = movieId
            };

            _context.Reviews.Add(reviewDto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReviewsForMovie", new { movieId }, reviewDto);

        }
    }
}
