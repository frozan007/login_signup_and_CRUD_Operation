using SimpleAppLoginAPI.Models;

namespace SimpleAppLoginAPI.Interface
{
    public interface IProfile
    {
        Task<Profile> Get(int id);
        Task<List<Profile>> GetAll();
        Task<Profile> GetBase64ProfilePicture(int empId);
        Task<Profile> GetProfilePicture(int empId);
        Task<Profile> AddOrUpdate(Profile profile);
        Task<int> Delete(int id);
    }
}
