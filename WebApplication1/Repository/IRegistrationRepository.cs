using DataAccessLayer;

namespace WebApplication1.Repository
{
    public interface IRegistrationRepository
    {
        Task<IEnumerable<Registration>> GetRegistrationList();
        Task<Registration> AddRegistrationList(Registration registration);

        Task<Registration> GetRegistrationListById(int id);
        Task<Registration> UpdateRegistrationList(Registration registration);
        Task<Registration> CheckCredential(LoginTbl loginTbl);

        Task<Registration> DeleteUser(int id);
    }
}
