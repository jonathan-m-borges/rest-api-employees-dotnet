using RestApiEmployees.Domain.Models;
using System.Collections.Generic;

namespace RestApiEmployees.Domain.Services
{
    public interface IEmployeeService
    {
        void Add(Employee employee);

        Employee DeleteById(int id);

        Employee GetById(int id);

        List<Employee> ListAll();

        bool Update(Employee employee);
    }
}