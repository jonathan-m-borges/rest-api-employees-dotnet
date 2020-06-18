# Criando uma WebAPI com dotnet core e C#

## Parte 2 - Criando a WebAPI

Nesta etapa será implementada uma WebAPI simples, utilizando dotnet core e C#.


---
### Resultado esperado

Será criada uma WebAPI muito simples para cadastro de Empregados.

As operações esperadas na WebAPI são:

- GET /api/employees -> lista todos os empregados cadastrados
- GET /api/employees/:id -> busca um empregado cadastrado, pelo id
- POST /api/employees -> cadastra um novo empregado
- PUT  /api/employees/:id -> atualiza os dados de um empregado já cadastrado
- DELETE /api/employees/:id -> excluir um empregado cadastrado

**[Acesse a documentação completa da API, para mais detalhes](https://employees38.docs.apiary.io/)**


---
### Criando o projeto

#### Criando a estrutura básica do projeto

- Crie um novo diretório para o seu projeto e abra o VSCode neste diretório:
  Pela linha de comando ficaria:
  ```console
  cd c:\temp
  mkdir RestApiEmployees
  cd RestApiEmployees
  code .
  ```
- Na linha de comando, digite os comando para criar uma nova WebAPI com dotnet core:
  ```console
  dotnet new webapi
  ```
- Os arquivos do projeto serão criados automaticamente:
  > ![Comando dotnet new webapi](./assets/vscode-new-web-api.png)
- As classes WeatherForecastController.cs e WeatherForecast.cs podem ser excluídas
  ```console
  del Controllers\WeatherForecastController.cs
  del WeatherForecast.cs
  ```

#### Criando a entidade Employee
 
- Organizando diretórios da aplicação:
  ```console
  mkdir Domain
  mkdir Domain\Models
   ```
- Crie a classe Employees.cs no diretório Domain\Models com o seguinte código:
  ```csharp
  namespace RestApiEmployees.Domain.Models
  {
      public class Employee
      {
          public int Id { get; set; }
          public string Name { get; set; }
          public decimal? Salary { get; set; }
          public int? Age { get; set; }
          public string ProfileImage { get; set; }

          public Employee()
          {
          }

          public Employee(int id, string name, decimal? salary, int? age, string profileImage)
          {
              Id = id;
              Name = name;
              Salary = salary;
              Age = age;
              ProfileImage = profileImage;
          }
      }
  }
  ```

#### Criando a interface IEmployeesService.cs

Foi utilizado na aplicação o padrão de projeto de Camada de Serviço/ServiceLayer. Consute as [referências](#referencias) para mais informações.

- Organizando diretórios da aplicação:
  ```console
  mkdir Domain\Services
  ```
- Crie a interface IEmployeesService dentro do diretório Domain\Services:
  ```csharp
  using System.Collections.Generic;
  using RestApiEmployees.Domain.Models;
  namespace RestApiEmployees.Domain.Services
  {
      public interface IEmployeesService
      {
          List<Employee> ListAll();
          Employee GetById(int id);
          void Add(Employee employee);
          void Update(Employee employee);
          void DeleteById(int id);
      }
  }
  ```

#### Criando o controller EmployeesController.cs
- Crie o controller EmployeesController.cs dentro do diretório Controllers
  ```csharp
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
  ```

#### Criando a interface IEmployeesRepository

Foi utilizado na aplicação o padrão de projeto Repository para abstrair a camada de dados. Consute as [referências](#referencias) para mais informações.

- Organizando diretórios da aplicação:
  ```console
  mkdir Domain\Interfaces
  ```
- Crie a interface IEmployeesRepository dentro do diretório Domain\Interfaces:
  ```csharp
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
  ```


#### Implementando a interface IEmployeesService - com a casse EmployeesService

> Por se tratar de uma aplicação simples não há regras de negócio, mas caso houvessem, estariam aqui.
> Nesta camada de serviços ficam as regras de negócio. Esta camanda de serviço utiliza os Repositórios para tratar dos dados (buscar, salvar, atualizar, deletar) e também contêm as regras de negócio da aplicação.

- Crie a classe EmployeesService, dentro do diretório Domain\Services
  ```csharp
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
  ```


#### Implementando a interface IEmployeesRepository - com a casse EmployeesRepository

Esta camada abstrai o banco de dados.

Neste exemplo, vamos apenas salvar os dados na memória da aplicação. Posteriormente vamos persistir em banco de dados.

- Organizando diretórios da aplicação:
  ```console
  mkdir Persistence\Memory
  ```
- Crie a classe EmployeesRepository dentro do diretório Persistence\Memory:
  ```csharp
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
              else
                  return null;
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
  ```


#### Ajustando a classe Startup.cs, registrando as classes na injeção de dependência

Consute as [referencias](#referencias) para saber mais sobre Inversão de Controle e Injeção de Dependência.

- Altere o método ```ConfigureServices(IServiceCollection services)``` da classe ```Startup.cs```:
  ```csharp
  public void ConfigureServices(IServiceCollection services)
  {
      services.AddScoped<IEmployeesService, EmployeesService>();
      services.AddSingleton<IEmployeesRepository, EmployeesRepository>();
      services.AddControllers();
  }
  ```

#### Execute a aplicação

Nesta etapa vamos compilar e executar a aplicação:

- Para executar a aplicação, basta executar na linha de comando:
  ```csharp
  dotnet run
  ```
  Isto já irá compilar a aplicação (que tbm pode ser compilado com ```dotnet build```) e executá-la.

- Você pode pedir ao dotnet já atualizar a aplicação, caso alguma classe seja alterada, rodando a aplicação da seguinte forma:
  ```csharp
  dotnet watch run
  ```

#### Testando a aplicação com o Postman

Para testar os endpoints da aplicação, vamos utilizar o Postman

- Instale o Postman caso ainda não tenha instalado. [Site Postman](https://postman.com/) e consuma os endpoints da aplicação.


---
### Referências
 
 **Padrões de projeto**

 - [ServiceLayer](https://trailhead.salesforce.com/pt-BR/content/learn/modules/apex_patterns_sl/apex_patterns_sl_learn_sl_principles)
 - [ServiceLayer](https://martinfowler.com/eaaCatalog/serviceLayer.html)
 - [Inversão de dependência e injeção de controle](https://docs.microsoft.com/pt-br/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.1)
 - [Inversão de dependência e injeção de controle](https://medium.com/@gustavoosantoos95/entendendo-invers%C3%A3o-de-controle-e-inje%C3%A7%C3%A3o-de-depend%C3%AAncias-nativas-no-net-core-26f9a3f6895d#:~:text=%5BASP.NET%20Core%5D%20Entendendo%20invers%C3%A3o%20de%20controle,e%20inje%C3%A7%C3%A3o%20de%20depend%C3%AAncias%20nativa&text=Um%20dos%20grandes%20problemas%20no,encarregar%20de%20finalizar%20esse%20objeto)
 
 
 **Outros**
 
 - [Documentando APIs com apiary.io](https://apiary.io/)
 
 
