using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class UserRepository(UserDbContext context) : IUserRepository
    {
        public async Task<bool> CreateUserAsync(User user)
        {
            context!.Users.Add(user);    
            var result = await context.SaveChangesAsync();
            return result > 0; // Returns true if at least one record was affected

        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await context!.Users.FindAsync(id);
            if (user != null)
            {
               context.Users.Remove(user);  
               var result = await context.SaveChangesAsync();   
               return result > 0;
            }
            return false; // User not found
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var users = await context!.Users.ToListAsync();
            return users; // Returns a Task<IEnumerable<User>>
        }

        public async Task<User> GetUserByIdAsync(int id) => await context!.Users.FirstOrDefaultAsync(_ => _.Id == id); 

        public async Task<bool> UpdateUserAsync(User user)
        {
            var getUser = await context!.Users.FirstOrDefaultAsync(_ => _.Id == user.Id);  
            if (getUser != null)
            {
               getUser.Name = user.Name;
               getUser.Email = user.Email;
               var result = await context.SaveChangesAsync();
               return result > 0; // Returns true if at least one record was affected
            }
            return false; // User not found
        }


    }
}
