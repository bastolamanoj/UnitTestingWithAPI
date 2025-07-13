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
                userDbContext.Users.Add(new User {Id=1, Name = "Test User", Email = "Testuser@gmail.com" });
                await userDbContext.SaveChangesAsync(); // Save changes to the database
            }

            return userDbContext; // Return the context
        }

        [Fact]
        public async void UserRepository_Create_ReturnTrue()
        {
            // Arrange
            var user = A.Fake<User>();
           
            user.Name="Test User";
            user.Email = "Testemail";

            //act
            var result = await userRepository.CreateUserAsync(user);

            //Assert
            result.Should().BeTrue(); // Ensure the result is true  
        }
        [Fact]
        public async void UserRepository_GetAllUsers_ReturnListOfUsers()
        {
            // Arrange

            // Act
            var users = await userRepository.GetAllUsersAsync();

            // Assert
            users.Should().NotBeNull(); // Ensure the users list is not null
            users.Should().BeOfType<List<User>>(); // Ensure the users are of type List<User>
        }

        [Theory]
        [InlineData(2)]
        public async void UserRepository_GetUserById_ReturnUser(int id) 
        {
            //Arrange
            //var user = new User { Name = "Test User", Email = "Testemail" };

            //Act
            var user = await userRepository.GetUserByIdAsync(id);
            //Assert
            user.Should().NotBeNull(); // Ensure the user is not null
            user.Should().BeOfType<User>(); // Ensure the user is of type User  
        }


        [Theory]
        [InlineData(5)]
        public async void UserRepository_UpdateUser_ReturnTrue(int id)
        {
            //Arrange

            //Act
            var user  = await userRepository.GetUserByIdAsync(id);
            user.Name = "Updated Name"; // Update the user's name   
            var result = await userRepository.UpdateUserAsync(user);
            //Assert
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData(4)]
        public async void UserRepository_DeleteUser_ReturnTrue(int id) {
            //Arrange

            //Act
            var result = await userRepository.DeleteUserAsync(id); 
            //
            result.Should().BeTrue(); // Ensure the result is true

        }
    }
}
