using SimpleAppLoginAPI.Models;
using SimpleAppLoginAPI.ViewModels;

namespace SimpleAppLoginAPI.Interface
{
    public interface ISignup
    {
        Task<Signup> GetByUserId(int userId);
        Task<Signup> GetById(int id);
        Task<List<Signup>> GetAll();
        Task<Signup> Create(Signup SignUpUser);
        Task<Signup> Update(int id, Signup SignUpUser);
        Task<int> Delete(int id);
    }
}
