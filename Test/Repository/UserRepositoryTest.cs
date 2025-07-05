using API.Data;
using API.Models;
using API.Repository;
using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Repository
{
    public class UserRepositoryTest
    {
        private readonly UserRepository userRepository;

        public UserRepositoryTest()
        {
            var context = GetDatabaseContext().Result;
            userRepository = new UserRepository(context);
        }

        private async Task<UserDbContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<UserDbContext>()
                 .UseSqlite("Data Source= UsersDb.db")
                 .Options;
            var userDbContext = new UserDbContext(options);
            await userDbContext.Database.EnsureCreatedAsync(); // Ensure the database is created

            if (!userDbContext.Users.Any()) // Check if the Users table is empty
            {
                userDbContext.Users.Add(new User { Name = "Test User", Email = "Testuser@gmail.com" });
                await userDbContext.SaveChangesAsync(); // Save changes to the database
            }

            return userDbContext; // Return the context
        }

        [Fact]
        public async void UserRepository_Create_ReturnTrue()
        {
            // Arrange
            var user = A.Fake<User>();

            //act
            var result = await userRepository.CreateUserAsync(user);

            //Assert
            result.Should().BeTrue(); // Ensure the result is true  
        }




    }
}
