using Microsoft.EntityFrameworkCore;
using SimpleAppLoginAPI.Context;
using SimpleAppLoginAPI.Interface;
using SimpleAppLoginAPI.Models;

namespace SimpleAppLoginAPI.Services
{
    public class ProfileApi : IProfile
    {
        AppDbContext _profileApi;

        public ProfileApi(AppDbContext profileApi)
        {
            _profileApi = profileApi;
        }
    
        public async Task<Profile> Create(Profile profile)
        {
            await _profileApi.Profile.AddAsync(profile);
            await _profileApi.SaveChangesAsync();
            return profile;
        }

        public async Task<int> Delete(int id)
        {
            var profile = await _profileApi.Profile.Where(x => x.Id == id).FirstOrDefaultAsync();
            if(profile != null)
            {
                _profileApi.Profile.Remove(profile);
                await _profileApi.SaveChangesAsync();
                return profile.Id;
            }
            else
            {
                throw new Exception("Profile not found.");
            }
        }

        public async Task<Profile> Get(int id)
        {
            return await _profileApi.Profile.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<List<Profile>> GetAll()
        {
            return await _profileApi.Profile.AsNoTracking().ToListAsync();
        }

        public async Task<Profile> Update(int id, Profile profile)
        {
            var profileDb = await _profileApi.Profile.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
            if(null != profileDb)
            {
                profile.Id = profileDb.Id;
                _profileApi.Profile.Update(profile);
                await _profileApi.SaveChangesAsync();
                return profile;
            }
            else
            {
                throw new Exception("Profile not found.");
            }
        }

        public async Task<Profile> GetBase64ProfilePicture(int empId)
        {
            return await _profileApi.Profile.Where(x => x.EmpId == empId).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<Profile> GetProfilePicture(int empId)
        {
            return await _profileApi.Profile.Where(x => x.EmpId == empId).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<Profile> AddOrUpdate(Profile profile)
        {
            var profileFromDb = await _profileApi.Profile.Where(x => x.EmpId == profile.EmpId).AsNoTracking().FirstOrDefaultAsync();
            if(profileFromDb != null)
            {
                profile.EmpId = profileFromDb.EmpId;
                _profileApi.Profile.Update(profile);
                await _profileApi.SaveChangesAsync();
                return profile;
            }
            else
            {
                await _profileApi.Profile.AddAsync(profile);
                await _profileApi.SaveChangesAsync();
                return profile;
            }
        }
    }
}
