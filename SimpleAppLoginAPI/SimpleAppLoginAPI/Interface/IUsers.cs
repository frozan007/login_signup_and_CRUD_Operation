using SimpleAppLoginAPI.Models;
using SimpleAppLoginAPI.ViewModels;

namespace SimpleAppLoginAPI.Interface
{
    public interface IUsers
    {
        Task<User> Get(int id);
        Task<User> GetByUserName(string userName);
        Task<List<User>> GetAll();
        Task<User> Create(User user);
        Task<User> Update(int id, User user);
        Task<int> Delete(int id);
        Task<LoginResponseModel> Login(UserLoginModel filter);
    }
}
