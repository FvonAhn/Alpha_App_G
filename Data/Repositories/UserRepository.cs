using Data.Context;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Data.Repositories
{
    public class UserRepository(DataContext context)
    {
        private readonly DataContext _context = context;

        // CREATE

        public virtual async Task<UserEntity> CreateUserAsync(UserEntity entity)
        {
            try
            {
                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex) 
            {
                Debug.WriteLine(ex.Message);
                return null!;
            }
        }

        // READ

        public virtual async Task<IEnumerable<UserEntity>> GetAllUsersAsync()
        {
            try
            {
                return await _context.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return []; 
            }
        }

        public virtual async Task<UserEntity?> GetUserById(int id)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null!;
            }
        }

        // UPDATE

        public virtual async Task<bool> UpdateUserAsync(UserEntity updatedUser)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == updatedUser.Id);
            if (existingUser == null)
            {
                return false;
            }

            try
            {
                existingUser.FullName = updatedUser.FullName;
                existingUser.Email = updatedUser.Email;
                existingUser.Password = updatedUser.Password;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) 
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        // DELETE

        public virtual async Task<bool> DeleteUserAsync(UserEntity deletedUser)
        {
            var existingUser = _context.Users.FirstOrDefault(x => x.Id == deletedUser.Id);
            if (existingUser == null) 
            {
                return false;
            }

            try
            {
                _context.Users.Remove(existingUser);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) 
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
