using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.Models.Dtos;
using MovieApi.Models.Entities;

namespace MovieApi.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieApiContext _context;

        public MoviesController(MovieApiContext context)
        {
            _context = context;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovie()
        {
            var movies = await _context.Movies
                .Include(m => m.MovieDetails) // Include related MovieDetails
                .Include(m => m.Reviews)       // Include related Reviews
                .Include(m => m.Actors)        // Include related Actors
                .ToListAsync(); 
            return movies;
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetMovie(int id)
        {
            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            var moveieDto = new MovieDto
            (
                Title: movie.Title,
                Year: movie.Year,
                Genre: movie.Genre,
                Duration: movie.Duration
            );

            return moveieDto;
        }
 
        // GET: api/Movies/5/details 
        [HttpGet("{id}/details")]
        public async Task<ActionResult<MovieDetailDto>> GetMovieDetails(int id)
        {
            var movie = await _context.Movies
                .Include(m => m.MovieDetails)
                .Include(m => m.Reviews)       // Include related Reviews
                .Include(m => m.Actors)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }
            var movieDetailDto = new MovieDetailDto
            (
                Title: movie.Title,
                Year: movie.Year,
                Genre: movie.Genre,
                Duration: movie.Duration,
                MovieDetails: new MovieDetailsDto
                (
                    Synopsis: movie.MovieDetails.Synopsis,
                    Language: movie.MovieDetails.Language,
                    Budget: movie.MovieDetails.Budget
                ),
                Reviews: movie.Reviews?
                .Select(r => new ReviewDto(r.ReviewerName,r.Comment, r.Rating))
                .ToList() ?? new List<ReviewDto>(),
                Actors: movie.Actors?
                .Select(a => new ActorDto(a.Name, a.BirthYear))
                .ToList() ?? new List<ActorDto>()
            );
            return movieDetailDto;
        }


        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostMovie(MovieCreateDto dto)
        {
            var movie = new Movie
            {
                Title = dto.Title,
                Genre = dto.Genre,
                Year = dto.Year,
                Duration = dto.Duration,
                MovieDetails = new MovieDetails
                {
                    Synopsis = dto.MovieDetails.Synopsis,
                    Language = dto.MovieDetails.Language,
                    Budget = dto.MovieDetails.Budget
                }
            };

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
