using Microsoft.AspNetCore.Mvc;
using RestApiEmployees.Models;
using RestApiEmployees.Services;
using System.Collections.Generic;

namespace RestApiEmployees.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository repository;

        public EmployeesController(IEmployeeRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/Employees
        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            var employees = repository.ListAll();
            return employees;
        }

        // GET: api/Employees/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<Employee> Get(int id)
        {
            var employee = repository.GetById(id);
            if (employee != null)
                return employee;

            return NotFound();
        }

        // POST: api/Employees
        [HttpPost]
        public Employee Post([FromBody] Employee employee)
        {
            repository.Add(employee);
            return employee;
        }

        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Employee employee)
        {
            var employeeFinded = repository.GetById(id);
            if (employeeFinded == null)
                return NotFound();

            employee.Id = id;
            repository.Update(employee);

            return Ok(employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var employeeFinded = repository.GetById(id);
            if (employeeFinded == null)
                return NotFound();

            repository.DeleteById(id);
            return Ok();
        }
    }
}