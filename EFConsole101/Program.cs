using DapperUsageConsole.Repositories;
using EFConsole101.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DapperUsageConsole
{
    class Program
    {
        static async Task Main()
        {
            var services = ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();


            // Example user ID to search for
            int userIdToFind = 10; // You can change this to test different IDs

            // Search and display user
            await SearchAndDisplayUser(serviceProvider.GetRequiredService<UserRepository>(), userIdToFind);

            
            //// Example user ID to delete
            int userIdToDelete = 12; // Change this ID based on your data

            //// Delete user and display result
            await DeleteUserAndDisplayResult(serviceProvider.GetRequiredService<UserRepository>(), userIdToDelete);


            // Add a new random user and display the result
            await AddNewRandomUserAndDisplayResult(serviceProvider.GetRequiredService<UserRepository>());


            // Example user ID and new email to update
            int userIdToUpdate = 2; // Specify the user ID you want to update
            string newUserEmail = "newemail@example.com"; // Specify the new email

            // Update user email and display result
            await UpdateUserEmailAndDisplayResult(serviceProvider.GetRequiredService<UserRepository>(),userIdToUpdate, newUserEmail); // Example user ID and new email


            // Listing all users
            // Retrieve UserRepository from the service provider and list all users
            await ListAllUsersAsync(serviceProvider.GetRequiredService<UserRepository>());

        }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            services.AddDbContext<ErDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("MyDbConnection")));

            services.AddScoped<UserRepository>();

            return services;
        }

        private static async Task ListAllUsersAsync(UserRepository userRepository)
        {
            // Call GetAllUsersAsync and write output to console
            var users = await userRepository.GetAllUsersAsync();
            foreach (var user in users)
            {
                Console.WriteLine($"ID: {user.Id}, Username: {user.Username}, Email: {user.Email}, CreatedDate: {user.CreatedDate}");
            }
        }

        private static async Task SearchAndDisplayUser(UserRepository userRepository, int userId)
        {
            var user = await userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                Console.WriteLine($"Found User: ID: {user.Id}, Username: {user.Username}, Email: {user.Email}, CreatedDate: {user.CreatedDate}");
            }
            else
            {
                Console.WriteLine($"User with ID, {userId}, not found!");
            }
        }

        private static async Task DeleteUserAndDisplayResult(UserRepository userRepository, int userId)
        {
            var userExists = await userRepository.GetByIdAsync(userId);
            if (userExists != null)
            {
                var deleted = await userRepository.DeleteByIdAsync(userId);
                if (deleted)
                {
                    Console.WriteLine($"User with ID: {userId} has been deleted.");
                }
                else
                {
                    // This block might not be reached since we already check if the user exists.
                    Console.WriteLine("An error occurred while trying to delete the user.");
                }
            }
            else
            {
                Console.WriteLine($"User with ID: {userId} was not found.");
            }
        }

        private static async Task AddNewRandomUserAndDisplayResult(UserRepository userRepository)
        {
            // Generate a new random user
            var newUser = User.GenerateRandomUser();

            // Add the new user to the database and get their ID
            newUser = await userRepository.AddAsync(newUser);

            Console.WriteLine($"New User Added: ID: {newUser.Id}, Username: {newUser.Username}, Email: {newUser.Email}");
        }

        private static async Task UpdateUserEmailAndDisplayResult(UserRepository userRepository, int userId, string newEmail)
        {
            bool updated = await userRepository.UpdateEmailAsync(userId, newEmail);
            if (updated)
            {
                Console.WriteLine($"User with ID: {userId} had their email updated to: {newEmail}");
            }
            else
            {
                Console.WriteLine($"User with ID: {userId} was not found.");
            }
        }
    }
}
