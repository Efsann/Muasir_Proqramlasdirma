using System;
using System.Collections.Generic;

interface IShape
{
    double CalculateArea();
    double CalculatePerimeter();
}

class Circle : IShape
{
    public double Radius { get; set; }
    public Circle(double radius) => Radius = radius;
    public double CalculateArea() => Math.PI * Radius * Radius;
    public double CalculatePerimeter() => 2 * Math.PI * Radius;
}

class Rectangle : IShape
{
    public double Width { get; set; }
    public double Height { get; set; }
    public Rectangle(double width, double height)
    {
        Width = width;
        Height = height;
    }
    public double CalculateArea() => Width * Height;
    public double CalculatePerimeter() => 2 * (Width + Height);
}

class Triangle : IShape
{
    public double SideA { get; set; }
    public double SideB { get; set; }
    public double SideC { get; set; }
    public Triangle(double a, double b, double c)
    {
        SideA = a;
        SideB = b;
        SideC = c;
    }
    public double CalculatePerimeter() => SideA + SideB + SideC;
    public double CalculateArea()
    {
        double s = CalculatePerimeter() / 2;
        return Math.Sqrt(s * (s - SideA) * (s - SideB) * (s - SideC));
    }
}

class ShapeHelper
{
    public double CalculateArea(double radius) => Math.PI * radius * radius;
    public double CalculateArea(double width, double height) => width * height;
}

class Program
{
    static void Main()
    {
        List<IShape> shapes = new List<IShape>
        {
            new Circle(5),
            new Rectangle(4, 6),
            new Triangle(3, 4, 5)
        };
        foreach (var shape in shapes)
        {
            Console.WriteLine($"Fiqur: {shape.GetType().Name}");
            Console.WriteLine($"Sahə: {shape.CalculateArea():F2}");
            Console.WriteLine($"Perimetr: {shape.CalculatePerimeter():F2}");
        }
    }
}