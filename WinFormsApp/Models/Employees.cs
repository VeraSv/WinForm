using System;
using System.Collections.Generic;
using System.Text;

namespace WinFormsApp.Models
{
    public class Employee
    {
        private int count = 1;
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Car Car { get; set; }
        public Employee(string name, int age, Car car)
        {
            
            Name = name;
            Age = age;
            Car = car;
            Id = GetHashCode();
        }

        public Employee()
        {

        }
        public override int GetHashCode()
        {
            return count;
        }
    }
}
