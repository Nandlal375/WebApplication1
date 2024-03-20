using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DataContext;

namespace WebApplication1.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _Context;
        public EmployeeRepository(ApplicationDbContext Context)
        {
                _Context = Context;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            var result = await _Context.Employees.AddAsync(employee);
            await _Context.SaveChangesAsync();
            return result.Entity;

        }

        public async Task<Employee> DeleteEmployee(int id)
        {
            var result = await _Context.Employees.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (result != null) 
            {
                 _Context.Employees.Remove(result);
                await _Context.SaveChangesAsync();
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
           return await _Context.Employees.ToListAsync();
        }

        public async Task<Employee> GetEmployee(int id)
        {
            return await _Context.Employees.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
          var result = await _Context.Employees.FirstOrDefaultAsync(x => x.Id == employee.Id);
          if (result != null) 
            { 
              result.Name = employee.Name;
              result.City = employee.City;
              await _Context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<Employee>> Search(string name)
        {
            IQueryable<Employee> employees = _Context.Employees;
            if (!string.IsNullOrEmpty(name))
            {
                employees = employees.Where(x => x.Name.Contains(name));
            }
            return await employees.ToListAsync();  
        }
    }
}
