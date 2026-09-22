using System;
class Task2
{
    static void Main()
    {
        const double Pi = 3.14159;
        Console.Write("Radius daxil edin: ");
        double r = Convert.ToDouble(Console.ReadLine());
        double sahe = Pi * r * r;
        Console.WriteLine("Dairənin sahəsi: " + sahe);
    }
}