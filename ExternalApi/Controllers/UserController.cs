using API.Repository;
using API.Models;   
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserRepository userRepository) : ControllerBase
    {
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await userRepository.GetAllUsersAsync();
            if (users == null || !users.Any())
            {
                return NotFound("No users found");
            }
            return Ok(users);
        }

        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User cannot be null");
            }
            var result = await userRepository.CreateUserAsync(user);
            if (result)
            {
                return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
            }
            return BadRequest("Failed to create user");
        }
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            if (user == null || user.Id <= 0)
            {
                return BadRequest("Invalid user data");
            }
            var result = await userRepository.UpdateUserAsync(user);
            if (result)
            {
                return Ok();
            }
            return NotFound("User not found");
        }
        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await userRepository.DeleteUserAsync(id);
            if (result)
            {
                return NoContent();
            }
            return NotFound("User not found");
        }
        
        [HttpGet("SearchUserByName/{name}")]
        public async Task<IActionResult> SearchUserByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Name cannot be empty");
            }
            var users = await userRepository.SearchUserByName(name);
            if (users == null || !users.Any())
            {
                return NotFound("No users found with the given name");
            }
            return Ok(users);
        }

    }

}
