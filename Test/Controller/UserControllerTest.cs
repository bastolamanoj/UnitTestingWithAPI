using API.Controllers;
using API.Models;
using API.Repository;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;                   

namespace Test.Controller
{
    public class UserControllerTest
    {
        private readonly IUserRepository userRepository;
        private readonly UserController userController;
        public UserControllerTest()
        {
            //setup dependencies using FakeItEasy
            this.userRepository = A.Fake<IUserRepository>();

            //SUT (System Under Test)   
            this.userController = new UserController(userRepository);
        }

        private static User CreateFakeUser() => A.Fake<User>();

        [Fact]
        public async void UserControler_Create_ReturnCreated()
        {
            // Arrange
            var user = CreateFakeUser();   
            
            // Act
            A.CallTo(() => userRepository.CreateUserAsync(user)).Returns(Task.FromResult(true)); // Simulate successful creation
            var result = (CreatedAtActionResult)await userController.CreateUser(user);

            //Assert
            result.StatusCode.Should().Be(201); // 201 Created
            result.Value.Should().NotBeNull(); // Ensure the user is returned
        }

        // Read All
        //This method return Ok(success) | NotFound(fails) if no users are found
        [Fact]
        public async void UserController_GetUsers_ReturnOk()
        {
            // Arrange
            var users = A.Fake<List<User>>();
            users.Add(new User { Name ="John Doe", Email="john@example.com"});
            users.Add(new User { Name = "Johnny Cash", Email="jogny@example.com"});
            users.Add(new User { Name = "Johnny doe", Email="jogny@example.com"});
            
            // Act
            A.CallTo(() => userRepository.GetAllUsersAsync()).Returns(Task.FromResult(users.AsEnumerable()));
            var result = (OkObjectResult)await userController.GetAllUsers();
            // Assert
            result.StatusCode.Should().Be(200); // 200 OK
            result.Value.Should().NotBeNull(); // Ensure the users are returned
            result.Value.Should().BeEquivalentTo(users); // Ensure the returned users match the expected ones
        }

        [Theory]
        [InlineData(1)]
        public async void UserController_GetUserById_ReturnOk(int id)
        {
            // Arrange
            var user = CreateFakeUser();
            user.Id = id; // Set the ID for the fake user
            // Act
            A.CallTo(() => userRepository.GetUserByIdAsync(id)).Returns(Task.FromResult(user));
            var result = (OkObjectResult)await userController.GetUserById(id);
            // Assert
            result.StatusCode.Should().Be(200);
            result.Value.Should().NotBeNull(); 
            result.Value.Should().BeEquivalentTo(user); // Ensure the returned user matches the expected one
        }

        [Fact]
        public async void UserController_UpdateUser_ReturnOk()
        {
            // Arrange
            var user = CreateFakeUser();
            user.Id = 1; // Set the ID for the fake user
            user.Name = "John Doe"; user.Email = "john@example.com";
            // Act
            A.CallTo(() => userRepository.UpdateUserAsync(user)).Returns(true);
            var result = (OkResult)await userController.UpdateUser(user);

            // Assert
            result.StatusCode.Should().Be(200); // 200 OK
            result.Should().NotBeNull(); // Ensure the result is not null

        }

        [Fact]
        public async void UserController_DeleteUser_ReturnOk()
        {
            // Arrange
            int userId = 1; // Example user ID to delete
            // Act
            A.CallTo(() => userRepository.DeleteUserAsync(userId)).Returns(Task.FromResult(true)); // Simulate successful deletion
            var result = (NoContentResult)await userController.DeleteUser(userId);
            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status204NoContent); // 200 OK
            result.Should().NotBeNull(); // Ensure the result is not null
        }

        [Fact]
        public async void UserController_SearchUserByName_ReturnOK()
        {
            // Arrange
            string searchName = "John";
            var users = new List<User>
            {
                new User { Id = 1, Name = "John Doe", Email = "" },
                new User { Id = 2, Name = "Jane Doe", Email = "" } ,
                new User { Id = 3, Name = "Johnny Cash", Email = "" } ,
            };

            // Act
            A.CallTo(() => userRepository.SearchUserByName(searchName))
                .Returns(Task.FromResult(users.Where(u => u.Name.Contains(searchName, StringComparison.OrdinalIgnoreCase))));
            var result = (OkObjectResult)await userController.SearchUserByName(searchName);

            // Assert
            result.StatusCode.Should().Be(200); // 200 OK
        }

    }
}
