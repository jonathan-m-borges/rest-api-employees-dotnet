using Microsoft.Extensions.Configuration;
using Npgsql;
using RestApiEmployees.Domain.Models;
using RestApiEmployees.Domain.Repositories;
using System;
using System.Collections.Generic;

namespace RestApiEmployees.Persistence.PostgreSQL
{
    public class EmployeeRepositoryPG : IEmployeeRepository
    {
        private string connString;

        public EmployeeRepositoryPG(IConfiguration config)
        {
            connString = config.GetConnectionString("postgresql");
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
            using var conn = Connection();

            try
            {
                using var cmd = new NpgsqlCommand("SELECT id, name, salary, age, profile_image FROM employees", conn);
                using var reader = cmd.ExecuteReader();

                var result = new List<Employee>();
                while (reader.Read())
                {
                    var id = reader.GetInt32(0);
                    var name = reader.GetString(1);
                    var salary = reader.GetValue(2) as decimal?;
                    var age = reader.GetValue(3) as int?;
                    var profileImage = reader.GetValue(4) as string;
                    result.Add(new Employee(id, name, salary, age, profileImage));
                }

                return result;
            }
            finally
            {
                conn.Close();
            }
        }

        public bool Update(Employee employee)
        {
            throw new NotImplementedException();
        }

        private NpgsqlConnection Connection()
        {
            var conn = new NpgsqlConnection(connString);
            conn.Open();
            return conn;
        }
    }
}