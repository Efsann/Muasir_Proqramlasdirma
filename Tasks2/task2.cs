using System;
class Program
{    static void Main()
    {
        Console.WriteLine("Hendesi Kalkulyator");
        Console.WriteLine("1. Daïre");
        Console.WriteLine("2. Duzbucaqli");
        Console.WriteLine("3. Ucbucaq");

        Console.Write("Fiqur secin: ");
        int secim = Convert.ToInt32(Console.ReadLine());
        double sahe = 0;

        switch (secim)
        {
            case 1:
                Console.Write("Radiusu daxil edin: ");
                double radius = Convert.ToDouble(Console.ReadLine());

                sahe = Math.PI * Math.Pow(radius, 2);

                Console.WriteLine("Sahe: " + Math.Round(sahe, 2));
                break;

            case 2:
                Console.Write("Uzunlugu daxil edin: ");
                double uzunluq = Convert.ToDouble(Console.ReadLine());

                Console.Write("Eni daxil edin: ");
                double en = Convert.ToDouble(Console.ReadLine());

                sahe = uzunluq * en;

                Console.WriteLine("Sahe: " + Math.Round(sahe, 2));
                break;

            case 3:
                Console.Write("1-ci terefi daxil edin: ");
                double a = Convert.ToDouble(Console.ReadLine());

                Console.Write("2-ci terefi daxil edin: ");
                double b = Convert.ToDouble(Console.ReadLine());

                Console.Write("3-cu terefi daxil edin: ");
                double c = Convert.ToDouble(Console.ReadLine());

                double p = (a + b + c) / 2;

                sahe = Math.Sqrt(p * (p - a) * (p - b) * (p - c));

                Console.WriteLine("Sahe: " + Math.Round(sahe, 2));
                break;

            default:
                Console.WriteLine("Yanlis secim!");
                break;
        }
    }
}
