using Microsoft.EntityFrameworkCore;
using SimpleAppLoginAPI.Context;
using SimpleAppLoginAPI.Interface;
using SimpleAppLoginAPI.Models;

namespace SimpleAppLoginAPI.Services
{
    public class EmployeeApi : IEmployee
    {
        private readonly AppDbContext _dbContext;

        public EmployeeApi(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Employee> Create(Employee employee)
        {
            await _dbContext.Employees.AddAsync(employee);
            await _dbContext.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> Delete(int id)
        {
            var emp = await _dbContext.Employees.Where(x => x.Id == id).FirstOrDefaultAsync();
            if(emp != null)
            {
                emp.Status = StatusEnum.Deleted;
                _dbContext.Employees.Update(emp);
                await _dbContext.SaveChangesAsync();
                return emp;
            }
            else
            {
                throw new Exception("Employee not found.");
            }
        }

        public async Task<Employee> Get(int id)
        {
            return await _dbContext.Employees.Where(x => x.Id == id && x.Status == StatusEnum.Active).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<List<Employee>> GetAll()
        {
            return await _dbContext.Employees.AsNoTracking().ToListAsync();
            //return await _dbContext.Employees.Where(x => x.Status == StatusEnum.Active).AsNoTracking().ToListAsync(); 
        }

        public async Task<Employee> Update(int id, Employee employee)
        {
            var empFromDb = await _dbContext.Employees.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
            if(empFromDb != null)
            {  
                employee.Id = empFromDb.Id;
                _dbContext.Employees.Update(employee);
                await _dbContext.SaveChangesAsync();  
                return employee;
            }
            else
            {
                throw new Exception("Employee not Found");
            }
        }
    }
}
