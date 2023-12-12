using System;
using Restaurant_Management.Model;

namespace Restaurant_Management.Interface
{
    public interface IEmployeeRepository
    {
        Employee? GetEmployee(int employeeId);
        List<Employee> GetAllEmployees();
        void AddEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(int employeeId);
    }
}
