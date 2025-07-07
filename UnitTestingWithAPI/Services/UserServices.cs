using API.Models;
using API.Repository;

namespace API.Services
{
    public class UserServices : IUserService
    {
        private HttpClient _httpClient;
        public UserServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5288/"); // Set the base address for the API

            // Initialize any required services or dependencies here
        }
        public async Task<bool> CreateUserAsync(User user)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/user/CreateUser", user);
            if (result.IsSuccessStatusCode) return true;
            else return false; // 

        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var result = await _httpClient.DeleteAsync($"/api/user/DeleteUser/{id}");
            if (result.IsSuccessStatusCode) return true;
            else return false; // If the deletion fails, return false
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync() 
            => await _httpClient.GetFromJsonAsync<IEnumerable<User>>("/api/user/GetAllUsers");


        public async Task<User> GetUserByIdAsync(int id)
            => await _httpClient.GetFromJsonAsync<User>($"/api/user/GetUserById/{id}");
        public async Task<bool> UpdateUserAsync(User user)
        {
           var result = await _httpClient.PutAsJsonAsync("/api/user/UpdateUser", user);
            if (result.IsSuccessStatusCode) return true;
            else return false; // If the update fails, return false
        }
    }
}
