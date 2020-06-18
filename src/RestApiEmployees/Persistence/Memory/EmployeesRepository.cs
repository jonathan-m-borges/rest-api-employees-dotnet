using System.Collections.Generic;
using System.Linq;
using RestApiEmployees.Domain.Interfaces;
using RestApiEmployees.Domain.Models;

namespace RestApiEmployees.Persistence.Memory
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private static int idCount = 1;
        private Dictionary<int, Employee> employees = new Dictionary<int, Employee>();

        public List<Employee> ListAll()
        {
            return employees.Values.ToList();
        }

        public Employee GetById(int id)
        {
            if (employees.ContainsKey(id))
                return employees[id];
            else return null;
        }
        public void Add(Employee employee)
        {
            employee.Id = idCount++;
            employees.Add(employee.Id, employee);
        }

        public void Update(Employee employee)
        {
            if (employees.ContainsKey(employee.Id))
                employees[employee.Id] = employee;
        }

        public void DeleteById(int id)
        {
            if (employees.ContainsKey(id))
                employees.Remove(id);
        }
    }
}