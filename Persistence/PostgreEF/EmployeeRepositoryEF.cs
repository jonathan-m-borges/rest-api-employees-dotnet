using RestApiEmployees.Domain.Models;
using RestApiEmployees.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestApiEmployees.Persistence.PostgreEF
{
    public class EmployeeRepositoryEF : IEmployeeRepository
    {
        private readonly EmployeesDbContext context;

        public EmployeeRepositoryEF(EmployeesDbContext context)
        {
            this.context = context;
        }

        public void Add(Employee employee)
        {
            throw new NotImplementedException();
        }

        public Employee DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public Employee GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Employee> ListAll()
        {
            return context.Employees.ToList();
        }

        public bool Update(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}