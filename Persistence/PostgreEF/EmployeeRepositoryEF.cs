using RestApiEmployees.Domain.Models;
using RestApiEmployees.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace RestApiEmployees.Persistence.PostgreEF
{
    public class EmployeeRepositoryEF : IEmployeeRepository
    {
        private readonly EmployeesContext context;

        public EmployeeRepositoryEF(EmployeesContext context)
        {
            this.context = context;
        }

        public void Add(Employee employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();
        }

        public Employee DeleteById(int id)
        {
            var employee = context.Employees.SingleOrDefault(x => x.Id == id);
            if (employee == null)
                return null;

            context.Employees.Remove(employee);
            context.SaveChanges();
            return employee;
        }

        public Employee GetById(int id)
        {
            var employee = context.Employees.SingleOrDefault(x => x.Id == id);
            return employee;
        }

        public List<Employee> ListAll()
        {
            return context.Employees.ToList();
        }

        public bool Update(Employee employee)
        {
            var persisted = context.Employees.SingleOrDefault(x => x.Id == employee.Id);
            if (persisted == null)
                return false;

            persisted.Name = employee.Name;
            persisted.Salary = employee.Salary;
            persisted.Age = employee.Age;
            persisted.ProfileImage = employee.ProfileImage;

            context.SaveChanges();
            return true;
        }
    }
}