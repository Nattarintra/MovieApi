using MovieApi.Data;
using System.Diagnostics;

namespace MovieApi.Extensions
{
    public class WebApplicationExtensions
    {
        //public static object SeedData { get; private set; }

        public static async Task SeedDataAsynce(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var context = serviceProvider.GetRequiredService<MovieApiContext>();

                await context.Database.EnsureCreatedAsync();
                // await context.Database.EnsureDeletedAsync();

                try
                {
                    await SeedData.InitAsync(context);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    throw;
                }
            }

           


        }




    }
}
