using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RestApiEmployees.Domain.Models;
using RestApiEmployees.Domain.Services;

namespace RestApiEmployees.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesService service;
        public EmployeesController(IEmployeesService service)
        {
            this.service = service;
        }

        // GET: api/Employees
        [HttpGet]
        public List<Employee> Get()
        {
            var employees = service.ListAll();
            return employees;
        }

        // GET: api/Employees/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<Employee> Get(int id)
        {
            var employee = service.GetById(id);
            if (employee != null)
                return employee;

            return NotFound();
        }

        // POST: api/Employees
        [HttpPost]
        public Employee Post([FromBody] Employee employee)
        {
            service.Add(employee);
            return employee;
        }

        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Employee employee)
        {
            var employeeFinded = service.GetById(id);
            if (employeeFinded == null)
                return NotFound();

            employee.Id = id;
            service.Update(employee);

            return Ok(employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var employeeFinded = service.GetById(id);
            if (employeeFinded == null)
                return NotFound();

            service.DeleteById(id);
            return Ok();
        }
    }
}