using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.Models.Dtos;
using MovieApi.Models.Entities;

namespace MovieApi.Controllers
{
    [ApiController]
    [Route("api/movies/{movieId}/actors")]
    public class MovieActorsController : ControllerBase
    {
        private readonly MovieApiContext _context;

        public MovieActorsController(MovieApiContext context)
        {
            _context = context;
        }
        // GET: api/movies/{movieId}/actors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Actor>>> GetActorsForMovie(int movieId)
        {
            var movie = await _context.Movies.Include(m => m.Actors).FirstOrDefaultAsync(m => m.Id == movieId);
            if (movie == null) return NotFound($"Movie {movieId} not found.");

            return Ok(movie.Actors);
        }
        // POST: api/movies/{movieId}/actors/{actorId}

        [HttpPost("{actorId}")]
        public async Task<ActionResult<ActorDto>> AddActorToMovie(int movieId, int actorId)
        {
            var movie = await _context.Movies.Include(m => m.Actors).FirstOrDefaultAsync(m => m.Id == movieId);
            if (movie == null) return NotFound($"Movie {movieId} not found.");

            var actor = await _context.Actors.FindAsync(actorId);
            if (actor == null) return NotFound($"Actor {actorId} not found.");

            if (movie.Actors.Any(a => a.Id == actorId))
                return Conflict($"Actor {actorId} already in Movie {movieId}.");

            movie.Actors.Add(actor);
            await _context.SaveChangesAsync();

            return Ok($"Added Actor {actorId} to Movie {movieId}.");
        }
    }
}
