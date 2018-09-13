using System;

namespace CompanyNetCore.Model
{
    public class Employee
    {
        public Employee(int id, string name, DateTime birthdate, decimal salary, string gender) {
            this.Id = id;
            this.Name = name;
            this.Birthdate = birthdate;
            this.Salary = salary;
            this.Gender = gender;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public decimal Salary { get; set; }
        public string Gender { get; set; }
    }
}
