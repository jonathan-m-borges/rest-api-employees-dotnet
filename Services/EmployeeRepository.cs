using RestApiEmployees.Models;
using System.Collections.Generic;
using System.Linq;

namespace RestApiEmployees.Services
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private static int idCount = 1;
        private Dictionary<int, Employee> employees = new Dictionary<int, Employee>();

        public void Add(Employee employee)
        {
            employee.Id = idCount++;
            employees.Add(employee.Id, employee);
        }

        public Employee DeleteById(int id)
        {
            Employee employee = null;

            if (employees.ContainsKey(id))
            {
                employee = employees[id];
                employees.Remove(id);
            }

            return employee;
        }

        public Employee GetById(int id)
        {
            if (employees.ContainsKey(id))
                return employees[id];
            else return null;
        }

        public List<Employee> ListAll()
        {
            return employees.Values.ToList();
        }

        public bool Update(Employee employee)
        {
            if (employees.ContainsKey(employee.Id))
            {
                employees[employee.Id] = employee;
                return true;
            }

            return false;
        }
    }
}