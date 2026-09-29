
using System;
class Task6
{
    static void Main()
    {
        Console.Write("Məsafəni km ilə daxil edin:");
        double mesafe = Convert.ToDouble(Console.ReadLine());
        Console.Write("Yanacağı litr ilə daxil edin:");
        double yanacaq = Convert.ToDouble(Console.ReadLine());
        if (mesafe <= 0 || yanacaq <= 0)
        {
            Console.WriteLine("Daxil edilən məlumat yanlışdır");
        }
        else
        {
            double serf = (yanacaq / mesafe) * 100;
            Console.WriteLine("100 km üçün sərfiyyat:" + serf + " litr");
        }
    }
}