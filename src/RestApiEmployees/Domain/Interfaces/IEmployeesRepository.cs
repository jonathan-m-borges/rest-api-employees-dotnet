using System.Collections.Generic;
using RestApiEmployees.Domain.Models;

namespace RestApiEmployees.Domain.Interfaces
{
    public interface IEmployeesRepository
    {
        List<Employee> ListAll();
        Employee GetById(int id);
        void Add(Employee employee);
        void Update(Employee employee);
        void DeleteById(int id);
    }
}