using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DataContext;

namespace WebApplication1.Repository
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly ApplicationDbContext _context;
        public RegistrationRepository(ApplicationDbContext context)
        {
                _context = context;
        }

        public async Task<IEnumerable<Registration>> GetRegistrationList()
        {
           return await _context.registrations.ToListAsync();
        }

        public async Task<Registration> AddRegistrationList(Registration registration)
        {
          var addregistration_detail = await _context.registrations.AddAsync(registration);
            _context.SaveChanges();
            return addregistration_detail.Entity;
        }

        public async Task<Registration> GetRegistrationListById(int id)
        {
            return await _context.registrations.FirstOrDefaultAsync(x => x.Pid == id);
        }

        public async Task<Registration> UpdateRegistrationList(Registration registration)
        {
            var result = await _context.registrations.FirstOrDefaultAsync(x => x.Pid == registration.Pid);
            if (result != null)
            {
                result.Pname = registration.Pname;
                result.Pemail = registration.Pemail;
                result.Paddress = registration.Paddress;
                result.Pmobile = registration.Pmobile;
                result.Pusername = registration.Pusername;
                result.Ppassword = registration.Ppassword;
                _context.SaveChanges();
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<Registration> DeleteUser(int id)
        {
            var deleterecord = await _context.registrations.FirstOrDefaultAsync(x => x.Pid == id);
            if (deleterecord != null) 
            {
               _context.registrations.Remove(deleterecord);
               await _context.SaveChangesAsync();
               return deleterecord;
            }
            else
            {
                return null;
            }
        }

        public async Task<Registration> CheckCredential(LoginTbl loginTbl)
        {
            var matchrecord = await _context.registrations
            .FirstOrDefaultAsync(x => x.Pusername == loginTbl.username && x.Ppassword ==  loginTbl.password);

            if (matchrecord != null) 
            {
                return matchrecord;
            }
            else 
            {
                return null;
            }
        }
    }
}
