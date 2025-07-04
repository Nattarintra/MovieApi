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
                            "Korean"
                            ),
                        Budget = faker.Random.Int(1_000_000, 200_000_000)
                    }
                };

                // Add some random actors to the movie
                for (int a = 0; a < faker.Random.Int(1, 3); a++)
                {
                    movie.Actors.Add(new Actor
                    {
                        Name = faker.Name.FullName(),
                        BirthYear = faker.Date.Past(30).Year
                    });

                }

                // Add some random reviews to the movie
                for (int r = 0; r < faker.Random.Int(1, 3); r++)
                {
                    movie.Reviews.Add(new Review
                    {
                        ReviewerName = faker.Name.FullName(),
                        Rating = faker.Random.Int(1, 5),
                        Comment = faker.Lorem.Sentence(5,10)
                    });
                }

                movies.Add(movie);


            }
            return movies;

        }
    }
}