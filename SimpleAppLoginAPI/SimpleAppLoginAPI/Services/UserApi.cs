using Microsoft.EntityFrameworkCore;
using SimpleAppLoginAPI.Context;
using SimpleAppLoginAPI.Interface;
using SimpleAppLoginAPI.Models;
using SimpleAppLoginAPI.ViewModels;
using System.Linq;

namespace SimpleAppLoginAPI.Services
{
    public class UserApi : IUsers
    {
        private readonly AppDbContext _dbContext;

        public UserApi(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> Create(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;

        }

        public async Task<int> Delete(int id)
        {
            var user = await _dbContext.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            if(user != null)
            {
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();

                return user.Id;
            }
            else
            {
                throw new Exception("User not found.");
            }
        }

        public async Task<User> Get(int id)
        {
            return await _dbContext.Users.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<List<User>> GetAll()
        {
            return await _dbContext.Users.AsNoTracking().ToListAsync();
        }

        public async Task<User> GetByUserName(string userName)
        {
            return await _dbContext.Users.Where(x => x.UserName == userName).AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<User> Update(int id, User user)
        {
            var userFromDb = await _dbContext.Users.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
            if (userFromDb != null)
            {
                user.Id = userFromDb.Id;
                 _dbContext.Users.Update(user);
                await _dbContext.SaveChangesAsync();
                return user;
            }
            else
            {
                throw new Exception("User not found.");
            }
        }

        public async Task<LoginResponseModel> Login(UserLoginModel filter)
        {
            var response = new LoginResponseModel();
            var user = await GetByUserName(filter.UserName);
            if(user != null)
            {
                if(user.Password == filter.Password)
                {
                    response.User = user;
                    response.IsSuccess = true;
                    response.Response = "Success";
                    return response;
                }
            }
            response.IsSuccess = false;   
            response.Response = "Invalid Username or Password";
            return response;

        }
    }
}
