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