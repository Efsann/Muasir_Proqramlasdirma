using System;
using System.Collections.Generic;

abstract class Employee
{
    public string Name { get; set; }
    private double baseSalary;
    public Employee(string name, double baseSalary)
    {
        Name = name;
        SetSalary(baseSalary);
    }
    public double GetSalary() => baseSalary;
    public void SetSalary(double value)
    {
        if (value >= 0) baseSalary = value;
    }
    public abstract double CalculateBonus();
    public double GetTotalSalary() => GetSalary() + CalculateBonus();
}

class Manager : Employee
{
    public Manager(string name, double baseSalary) : base(name, baseSalary) { }
    public override double CalculateBonus() => GetSalary() * 0.20;
}

class Intern : Employee
{
    public Intern(string name, double baseSalary) : base(name, baseSalary) { }
    public override double CalculateBonus() => 100;
}

class Program
{
    static void Main()
    {
        List<Employee> employees = new List<Employee>
        {
            new Manager("Əli Məmmədov", 2000),
            new Manager("Aysel Əliyeva", 2500),
            new Intern("Rəşad Həsənov", 500),
            new Intern("Leyla Qasımova", 600)
        };
        foreach (var emp in employees)
        {
            Console.WriteLine($"Ad: {emp.Name} | Vəzifə: {emp.GetType().Name} | Yekun Maaş: {emp.GetTotalSalary()} AZN");
        }
    }
}