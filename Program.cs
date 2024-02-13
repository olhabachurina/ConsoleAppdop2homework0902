using ConsoleAppdop2homework0902.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConsoleAppdop2homework0902;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<LibraryContext>();
                context.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while creating the database.");
                Console.WriteLine(ex.Message);
            }
        }

        var serviceProvider = host.Services;

        using (var scope = serviceProvider.CreateScope())
        {
            var libraryService = scope.ServiceProvider.GetRequiredService<LibraryService>();

            //  добавления книги
            var bookToAdd = new Book { Title = "Example Book", Author = "John Doe", YearPublished = 2022 };
            await libraryService.AddBook(bookToAdd);
            Console.WriteLine("Book added successfully.");

            //  обновления книги
            var bookToUpdate = await libraryService.GetBookById(1); 
            if (bookToUpdate != null)
            {
                bookToUpdate.Title = "Updated Title";
                await libraryService.UpdateBook(bookToUpdate);
                Console.WriteLine("Book updated successfully.");
            }

            //  удаления книги
            await libraryService.DeleteBook(1); 
            Console.WriteLine("Book deleted successfully.");
        }

        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                services.AddDbContext<LibraryContext>(options =>
                {
                    options.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=BookChange;Trusted_Connection=True;MultipleActiveResultSets=true");
                });

                services.AddTransient<LibraryService>();
            });
}