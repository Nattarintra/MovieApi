using Bogus;
using Microsoft.EntityFrameworkCore;
using MovieApi.Models.Entities;

namespace MovieApi.Data
{
    internal class SeedData
    {
        public static Faker faker = new Faker("en_US");
        internal static async Task InitAsync(MovieApiContext context)
        {
            if (await context.Movies.AnyAsync()) return;

            var movies = GenerateMovies(50);
            await context.Movies.AddRangeAsync(movies);

            await context.SaveChangesAsync();
        }

        private static IEnumerable<Movie> GenerateMovies(int numberOfMovies)
        {
          
            var movies = new List<Movie>(numberOfMovies);

            for (int i = 0; i < numberOfMovies; i++)
            {
                var movie = new Movie
                {
                    Title = faker.Lorem.Sentence(2, 3), // ชื่อเรื่อง
                    Year = faker.Date.Past(30).Year,     // ภายใน 30 ปีที่ผ่านมา
                    Genre = faker.PickRandom(
                        "Action", 
                        "Drama", 
                        "Comedy", 
                        "Sci-Fi", 
                        "Horror", 
                        "Documentary"
                        ),
                    Duration = faker.Random.Int(80, 180), // นาที
                    MovieDetails = new MovieDetails
                    {
                        Synopsis = faker.Lorem.Paragraph(),
                        Language = faker.PickRandom(
                            "English", 
                            "Swedish", 
                            "French", 
                            "German", 
                            "Japanese", 
                            "Korean"
                            ),
                        Budget = faker.Random.Int(1_000_000, 200_000_000)
                    }
                };

                movies.Add(movie);
            }
            return movies;

        }
    }
}