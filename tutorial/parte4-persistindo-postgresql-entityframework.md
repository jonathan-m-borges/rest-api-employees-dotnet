# Criando uma WebAPI com dotnet core e C#

## Parte 4 - Persistindo os dados no banco de dados PostgreSQL - com EntityFramework

Nesta etapa vamos persistir os dados dos empregados no banco de dados PostgreSQL, utilizando o framework ORM **EntityFramework**.

Para mais detalhes consulte as [referencias](#referencias).

Será implementado uma nova classe para a interface ```IEmployeesRepository``` que salva os dados no Postgre.


---
### Adicionando o pacote do Npgsql

- Na linha de comando no diretório raiz da aplicação e execute:
  ```console
  dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
  ```
  Aguarde a instalação.



---
### Criando a classe EmployeesContext

- Organizando diretórios da aplicação:
  ```console
  mkdir Persistence\PostgreEF
  ```
- Crie a interface EmployeesContext dentro do diretório Persistence\PostgreEF:
  ```csharp
  using Microsoft.EntityFrameworkCore;
  using RestApiEmployees.Domain.Models;
  namespace RestApiEmployees.Persistence.PostgreEF
  {
      public class EmployeesContext : DbContext
      {
          public DbSet<Employee> Employees { get; set; }
          public EmployeesContext(DbContextOptions options) : base(options)
          {
          }

          protected override void OnModelCreating(ModelBuilder modelBuilder)
          {
              base.OnModelCreating(modelBuilder);

              modelBuilder.UseSerialColumns();

              modelBuilder.Entity<Employee>().ToTable("employees");
              modelBuilder.Entity<Employee>().Property(x => x.Id).HasColumnName("id");
              modelBuilder.Entity<Employee>().Property(x => x.Name).HasColumnName("name");
              modelBuilder.Entity<Employee>().Property(x => x.Salary).HasColumnName("salary");
              modelBuilder.Entity<Employee>().Property(x => x.Age).HasColumnName("age");
              modelBuilder.Entity<Employee>().Property(x => x.ProfileImage).HasColumnName("profile_image");
          }
      }
  }
  ```



---
### Criando a classe EmployeesRepositoryEF

- Organizando diretórios da aplicação:
  ```console
  mkdir Persistence\PostgreEF
  ```
- Crie a interface EmployeesRepositoryEF dentro do diretório Persistence\PostgreEF:
  ```csharp
  using System.Collections.Generic;
  using System.Linq;
  using RestApiEmployees.Domain.Interfaces;
  using RestApiEmployees.Domain.Models;
  namespace RestApiEmployees.Persistence.PostgreEF
  {
      public class EmployeeRepositoryEF : IEmployeesRepository
      {
          private readonly EmployeesContext context;
          public EmployeeRepositoryEF(EmployeesContext context)
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
  ```


#### Ajustando a classe Startup.cs, registrando as classes na injeção de dependência

- Altere o método ```ConfigureServices(IServiceCollection services)``` da classe ```Startup.cs```:
  ```csharp
  public void ConfigureServices(IServiceCollection services)
  {
      services.AddScoped<IEmployeesService, EmployeesService>();
      services.AddDbContext<EmployeesContext>(options =>
          options.UseNpgsql(Configuration.GetConnectionString("postgresql")));
      services.AddScoped<IEmployeesRepository, EmployeesRepositoryEF>();
      services.AddControllers();
  }
  ```

#### Execute a aplicação

Nesta etapa vamos compilar e executar a aplicação:

- Para executar a aplicação, basta executar na linha de comando:
  ```csharp
  dotnet run
  ```

#### Testando a aplicação com o Postman

Para testar os endpoints da aplicação, vamos utilizar o Postman.

Em paralelo, execute os comandos SQL no Postird para verificar que os dados foram persistidos no banco.



---
### Referências
 - [EntityFrameWork](https://docs.microsoft.com/pt-br/ef/core/)
 - [PostgreSQL com dotnet e EntityFrameWork](https://www.npgsql.org/efcore/index.html)
 