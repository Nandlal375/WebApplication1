using DataAccessLayer;

namespace WebApplication1.Repository
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> Search(string name);
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployee(int id);
        Task<Employee> AddEmployee(Employee employee);
        Task<Employee> UpdateEmployee(Employee employee);
        Task<Employee> DeleteEmployee(int id);    
    }
}
