using System;
class Task4
{
    static void Main()
    {
        Console.Write("1-ci qiyməti daxil edin: ");
        int q1 = int.Parse(Console.ReadLine());
        Console.Write("2-ci qiyməti daxil edin: ");
        int q2 = int.Parse(Console.ReadLine());
        Console.Write("3-cü qiyməti daxil edin: ");
        int q3 = int.Parse(Console.ReadLine());
        Console.Write("4-cü qiyməti daxil edin: ");
        int q4 = int.Parse(Console.ReadLine());
        Console.Write("5-ci qiyməti daxil edin: ");
        int q5 = int.Parse(Console.ReadLine());
        double orta = (q1 + q2 + q3 + q4 + q5) / 5.0;
        Console.WriteLine("Orta balınız: " + orta);
        if (orta < 51)
        {
            Console.WriteLine("Kəsildiniz");
        }
        else if (orta <= 90)
        {
            Console.WriteLine("Orta nəticə");
        }
        else
        {
            Console.WriteLine("Əla nəticə");
        }
    }
}
