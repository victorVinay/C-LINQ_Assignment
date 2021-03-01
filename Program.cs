using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_LINQ_Assignment
{
    public enum SalaryType
    {
        Monthly,
        Performance,
        Bonus
    }

    public class Employee
    {
        public int EmployeeID { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public int Age { get; set; }
    }

    public class Salary
    {
        public int EmployeeID { get; set; }
        public int Amount { get; set; }
        public SalaryType Type { get; set; }
    }
    public class Program
    {
        List<Salary> salaryList;
        IList<Employee> employeeList;
        public int item_no = 2;


        public Program()
        {
            employeeList = new List<Employee>() {
                new Employee(){ EmployeeID = 1, EmployeeFirstName = "Rajiv", EmployeeLastName = "Desai", Age = 49},
                new Employee(){ EmployeeID = 2, EmployeeFirstName = "Karan", EmployeeLastName = "Patel", Age = 32},
                new Employee(){ EmployeeID = 3, EmployeeFirstName = "Sujit", EmployeeLastName = "Dixit", Age = 28},
                new Employee(){ EmployeeID = 4, EmployeeFirstName = "Mahendra", EmployeeLastName = "Suri", Age = 26},
                new Employee(){ EmployeeID = 5, EmployeeFirstName = "Divya", EmployeeLastName = "Das", Age = 20},
                new Employee(){ EmployeeID = 6, EmployeeFirstName = "Ridhi", EmployeeLastName = "Shah", Age = 60},
                new Employee(){ EmployeeID = 7, EmployeeFirstName = "Dimple", EmployeeLastName = "Bhatt", Age = 53}
            };

            salaryList = new List<Salary>() {
            new Salary(){ EmployeeID = 1, Amount = 1000, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 1, Amount = 500, Type = SalaryType.Performance},
            new Salary(){ EmployeeID = 1, Amount = 100, Type = SalaryType.Bonus},
            new Salary(){ EmployeeID = 2, Amount = 3000, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 2, Amount = 1000, Type = SalaryType.Bonus},
            new Salary(){ EmployeeID = 3, Amount = 1500, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 4, Amount = 2100, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 5, Amount = 2800, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 5, Amount = 600, Type = SalaryType.Performance},
            new Salary(){ EmployeeID = 5, Amount = 500, Type = SalaryType.Bonus},
            new Salary(){ EmployeeID = 6, Amount = 3000, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 6, Amount = 400, Type = SalaryType.Performance},
            new Salary(){ EmployeeID = 7, Amount = 4700, Type = SalaryType.Monthly}
        };
        }
        static void Main(string[] args)
        {
            Program program = new Program();

            program.Task1();

            program.Task2();

            program.Task3();
        }
        public void Task1()
        {
            var emp = (from Employee in employeeList
                       join Salary in salaryList on Employee.EmployeeID equals Salary.EmployeeID
                       group new { Salary, Employee } by Employee.EmployeeFirstName into SalaryGroup
                       orderby SalaryGroup.Sum(x => x.Salary.Amount)
                       select new { Name = SalaryGroup.Key, s = SalaryGroup.Sum(x => x.Salary.Amount) }
                       );
            foreach (var sal in emp)
            {
                Console.WriteLine($"{sal.Name}:{sal.s}");

            }
        }

        public void Task2()
        {
            var result1 = (from emp in employeeList
                           join sal in salaryList
                           on emp.EmployeeID equals sal.EmployeeID into egroup
                           orderby emp.Age descending
                           select new
                           {
                               id = emp.EmployeeID,
                               fname = emp.EmployeeFirstName,
                               lname = emp.EmployeeLastName,
                               age = emp.Age,
                               salary = egroup.Sum(x => x.Amount)
                           }).ToList();
            Console.WriteLine("2nd Oldest Employee :");
            Console.WriteLine("Employee ID:" + result1[item_no - 1].id);
            Console.WriteLine("Employee Name :" + result1[item_no - 1].fname + result1[item_no - 1].lname);
            Console.WriteLine("Employee Age :" + result1[item_no - 1].age);
            Console.WriteLine("Employee Salary:" + result1[item_no - 1].salary);

        }

        public void Task3()
        {
            Console.WriteLine("Mean Salary Of Employees Whose Age Is Greater Than 30:");
            var result2 = from e in employeeList
                          where e.Age > 30
                          join salary in salaryList on e.EmployeeID equals salary.EmployeeID into grp
                          select new
                          {
                              mean = grp.Average(x => x.Amount)
                          };
            foreach (var e in result2)
            {
                Console.WriteLine(e.mean);
            }
        }

    }
}
