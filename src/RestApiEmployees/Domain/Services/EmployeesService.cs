using System.Collections.Generic;
using RestApiEmployees.Domain.Interfaces;
using RestApiEmployees.Domain.Models;

namespace RestApiEmployees.Domain.Services
{
    public class EmployeesService : IEmployeesService
    {
        private readonly IEmployeesRepository repository;
        public EmployeesService(IEmployeesRepository repository)
        {
            this.repository = repository;
        }

        public void Add(Employee employee)
        {
            //TODO: regras de negócio, se tiver
            //Exemplo: enviar email para o RH com os dados do empregado adicionado
            repository.Add(employee);
        }

        public void DeleteById(int id)
        {
            //TODO: regras de negócio, se tiver
            repository.DeleteById(id);
        }

        public Employee GetById(int id)
        {
            //TODO: regras de negócio, se tiver
            return repository.GetById(id);
        }

        public List<Employee> ListAll()
        {
            //TODO: regras de negócio, se tiver
            return repository.ListAll();
        }

        public void Update(Employee employee)
        {
            //TODO: regras de negócio, se tiver
            repository.Update(employee);
        }
    }
}