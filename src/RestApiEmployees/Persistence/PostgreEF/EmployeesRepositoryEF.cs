using System.Collections.Generic;
using System.Linq;
using RestApiEmployees.Domain.Interfaces;
using RestApiEmployees.Domain.Models;

namespace RestApiEmployees.Persistence.PostgreEF
{
    public class EmployeesRepositoryEF : IEmployeesRepository
    {
        private readonly EmployeesContext context;

        public EmployeesRepositoryEF(EmployeesContext context)
        {
            this.context = context;
        }

        public List<Employee> ListAll()
        {
            return context.Employees.ToList();
        }

        public Employee GetById(int id)
        {
            var employee = context.Employees.SingleOrDefault(x => x.Id == id);
            return employee;
        }

        public void Add(Employee employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();
        }

        public void DeleteById(int id)
        {
            var employee = context.Employees.SingleOrDefault(x => x.Id == id);
            if (employee == null)
                return;
            context.Employees.Remove(employee);
            context.SaveChanges();
        }

        public void Update(Employee employee)
        {
            var persisted = context.Employees.SingleOrDefault(x => x.Id == employee.Id);
            if (persisted == null)
                return;
            persisted.Name = employee.Name;
            persisted.Salary = employee.Salary;
            persisted.Age = employee.Age;
            persisted.ProfileImage = employee.ProfileImage;
            context.SaveChanges();
        }
    }
}