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
            using var conn = Connection();

            try
            {
                using var cmd = new NpgsqlCommand("insert into employees (name, salary, age, profile_image) values (@name, @salary, @age, @profile_image) RETURNING id", conn);
                cmd.Parameters.AddWithValue("name", employee.Name);
                cmd.Parameters.AddWithValue("salary", GetValueOrDBNull(employee.Salary));
                cmd.Parameters.AddWithValue("age", GetValueOrDBNull(employee.Age));
                cmd.Parameters.AddWithValue("profile_image", GetValueOrDBNull(employee.ProfileImage));
                using var reader = cmd.ExecuteReader();

                var result = new List<Employee>();
                while (reader.Read())
                {
                    var id = reader.GetInt32(0);
                    employee.Id = id;
                }
            }
            finally
            {
                conn.Close();
            }
        }

        public Employee DeleteById(int id)
        {
            using var conn = Connection();

            try
            {
                var employee = GetById(id, conn);

                if (employee != null)
                {
                    using var cmd = new NpgsqlCommand("delete from employees where id=@id", conn);
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.ExecuteNonQuery();
                }

                return employee;
            }
            finally
            {
                conn.Close();
            }
        }

        public Employee GetById(int id)
        {
            using var conn = Connection();

            try
            {
                return GetById(id, conn);
            }
            finally
            {
                conn.Close();
            }
        }

        private Employee GetById(int id, NpgsqlConnection conn)
        {
            using var cmd = new NpgsqlCommand("select id, name, salary, age, profile_image from employees where id=@id", conn);
            cmd.Parameters.AddWithValue("id", id);
            using var reader = cmd.ExecuteReader();

            var result = new List<Employee>();
            if (reader.Read())
                return GetEmployee(reader);

            return null;
        }

        public List<Employee> ListAll()
        {
            using var conn = Connection();

            try
            {
                using var cmd = new NpgsqlCommand("select id, name, salary, age, profile_image from employees", conn);
                using var reader = cmd.ExecuteReader();

                var result = new List<Employee>();
                while (reader.Read())
                {
                    var employee = GetEmployee(reader);
                    result.Add(employee);
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
            using var conn = Connection();

            try
            {
                var employeePersited = GetById(employee.Id, conn);

                if (employeePersited == null)
                    return false;

                using var cmd = new NpgsqlCommand("update employees set name=@name, salary=@salary, age=@age, profile_image=@profile_image where id=@id", conn);
                cmd.Parameters.AddWithValue("id", employee.Id);
                cmd.Parameters.AddWithValue("name", employee.Name);
                cmd.Parameters.AddWithValue("salary", GetValueOrDBNull(employee.Salary));
                cmd.Parameters.AddWithValue("age", GetValueOrDBNull(employee.Age));
                cmd.Parameters.AddWithValue("profile_image", GetValueOrDBNull(employee.ProfileImage));
                cmd.ExecuteNonQuery();

                return true;
            }
            finally
            {
                conn.Close();
            }
        }

        private NpgsqlConnection Connection()
        {
            var conn = new NpgsqlConnection(connString);
            conn.Open();
            return conn;
        }

        private object GetValueOrDBNull(object value)
        {
            return value ?? DBNull.Value;
        }

        private Employee GetEmployee(NpgsqlDataReader reader)
        {
            var id = reader.GetInt32(0);
            var name = reader.GetString(1);
            var salary = reader.GetValue(2) as decimal?;
            var age = reader.GetValue(3) as int?;
            var profileImage = reader.GetValue(4) as string;
            var employee = new Employee(id, name, salary, age, profileImage);
            return employee;
        }
    }
}