using SimpleAppLoginAPI.Models;

namespace SimpleAppLoginAPI.Interface
{
    public interface IEmployee
    {
        Task<Employee>Get(int id);
        Task<List<Employee>> GetAll();
        Task<Employee> Create(Employee employee);
        Task<Employee> Update(int id, Employee employee);
        Task<Employee> Delete(int id);

    }
}
 