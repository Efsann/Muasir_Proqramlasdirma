using System;
class Program
{
    static void Main()
    {
        Random random = new Random();
        int gizliEded = random.Next(0, 101);
        int texmin;

        do
        {
            Console.Write("0-100 arasi eded daxil edin: ");
            texmin = Convert.ToInt32(Console.ReadLine());

            if (texmin > gizliEded)
            {
                Console.WriteLine("Daha kicik eded cehd edin");
            }
            else if (texmin < gizliEded)
            {
                Console.WriteLine("Daha boyuk eded cehd edin");
            }
            else
            {
                Console.WriteLine("Tebrikler!");
            }

        } while (texmin != gizliEded);
    }
}