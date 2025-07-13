using API.Models;
using API.Services;
using FakeItEasy;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Service
{
    public class UserServiceTest
    {
        //Create
        [Fact]
        public async void UserService_CreateUser_ReturnTrue()
        {
            // Arrange
            var user = A.Fake<User>();

            // Act
            var client = Helper.CustomFakeHttpClient.FakeHttpClient(true);
            var userService = new UserServices(client);
            var result = await userService.CreateUserAsync(user);

            // Assert
            result.Should().BeTrue(); // Ensure the result is true  
        }

        [Fact]
        public async void UserService_GetAllUsers_ReturnsListOfUsers()
        {
            //Arrange
            var users = A.Fake<IEnumerable<User>>();
            // Act
            var client = Helper.CustomFakeHttpClient.FakeHttpClient(users);
            var userService = new UserServices(client);
            var result = await userService.GetAllUsersAsync();
            // Assert
            result.Should().BeEquivalentTo(users); // Ensure the result matches the fake data
        }

        [Theory]
        [InlineData(3)]
        public async void UserService_GetUserById_ReturnsUser(int id)
        {
            // Arrange
            var user = A.Fake<User>();
            // Act
            var client = Helper.CustomFakeHttpClient.FakeHttpClient(user);
            var userService = new UserServices(client);
            var result = await userService.GetUserByIdAsync(id);
            // Assert
            result.Should().BeEquivalentTo(user); // Ensure the result matches the fake data
        }

        [Theory]
        [InlineData(3)]
        public async void UserService_UpdateUser_ReturnsTrue(int id)
        {
            // Arrange

            // Act 
            var client = Helper.CustomFakeHttpClient.FakeHttpClient(true);
            var userService = new UserServices(client); 
            var user = new User { Id = id, Name = "Test User", Email = "example@gmail.com" };    
            var result = await userService.UpdateUserAsync(user);

            // Assert   
            result.Should().BeTrue(); // Ensure the result is true
        }

        [Theory]
        [InlineData(1)]
        public async void UserService_DeleteUser_ReturnsTrue(int Id)
        {
            // Arrange
            // Act
            var client = Helper.CustomFakeHttpClient.FakeHttpClient(true);
            var userService = new UserServices(client); 
            var result = await userService.DeleteUserAsync(Id);
            // Assert
            result.Should().BeTrue(); // Ensure the result is true
        }
    }
}
