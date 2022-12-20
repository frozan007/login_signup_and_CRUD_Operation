using Microsoft.EntityFrameworkCore;
using SimpleAppLoginAPI.Context;
using SimpleAppLoginAPI.Interface;
using SimpleAppLoginAPI.Models;
using SimpleAppLoginAPI.ViewModels;

namespace SimpleAppLoginAPI.Services
{
    public class SignupApi : ISignup
    {
        private readonly AppDbContext _dbContext;

        public SignupApi(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Signup> Create(Signup signUpUser)
        {
            await _dbContext.Signup.AddAsync(signUpUser);
            await _dbContext.SaveChangesAsync();
            return signUpUser;
        }

        public async Task<int> Delete(int id)
        {
            var signUpUser = await _dbContext.Signup.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (signUpUser != null)
            {
                _dbContext.Signup.Remove(signUpUser);
                await _dbContext.SaveChangesAsync();
                return signUpUser.Id;
            }
            else
            {
                throw new Exception("User not found.");
            }
        }

        public async Task<List<Signup>> GetAll()
        {
            return await _dbContext.Signup.AsNoTracking().ToListAsync();
        }

        public async Task<Signup> GetById(int id)
        {
            return await _dbContext.Signup.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<Signup> GetByUserId(int userId)
        {
            return await _dbContext.Signup.Where(x => x.UserId == userId).AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<Signup> Update(int id, Signup signUpUser)
        {
            var SignUpUserFromDb = await _dbContext.Signup.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
            if (SignUpUserFromDb != null)
            {
                signUpUser.Id = SignUpUserFromDb.Id;
                _dbContext.Signup.Update(signUpUser);
                await _dbContext.SaveChangesAsync();
                return signUpUser;
            }
            else
            {
                throw new Exception("User not found.");
            }
        }
    }
}
