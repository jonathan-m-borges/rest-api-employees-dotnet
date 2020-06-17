using RestApiEmployees.Domain.Models;
using RestApiEmployees.Domain.Repositories;
using System.Collections.Generic;

namespace RestApiEmployees.Domain.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            this.repository = repository;
        }

        public List<Employee> ListAll()
        {
            //Do anything more
            return repository.ListAll();
        }

        public Employee GetById(int id)
        {
            //Do anything more
            return repository.GetById(id);
        }

        public void Add(Employee employee)
        {
            //Do anything more
            repository.Add(employee);
        }

        public bool Update(Employee employee)
        {
            //Do anything more
            return repository.Update(employee);
        }

        public Employee DeleteById(int id)
        {
            //Do anything more
            return repository.DeleteById(id);
        }
    }
}