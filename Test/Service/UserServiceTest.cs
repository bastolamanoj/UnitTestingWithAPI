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


    }
}
