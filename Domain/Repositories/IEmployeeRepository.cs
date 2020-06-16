using RestApiEmployees.Domain.Models;
using System.Collections.Generic;

namespace RestApiEmployees.Domain.Repositories
{
    public interface IEmployeeRepository
    {
        List<Employee> ListAll();

        Employee GetById(int id);

        void Add(Employee employee);

        bool Update(Employee employee);

        Employee DeleteById(int id);
    }
}