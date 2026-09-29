using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<int> ededler = new List<int>();

        for (int i = 0; i < 10; i++)
        {
            Console.Write((i + 1) + "-ci ededi daxil edin: ");
            int eded = Convert.ToInt32(Console.ReadLine());

            ededler.Add(eded);
        }

        int enBoyuk = ededler[0];
        int enKicik = ededler[0];
        int cutSay = 0;
        int tekSay = 0;

        foreach (int eded in ededler)
        {
            enBoyuk = Math.Max(enBoyuk, eded);
            enKicik = Math.Min(enKicik, eded);

            if (eded % 2 == 0)
            {
                cutSay++;
            }
            else
            {
                tekSay++;
            }
        }

        Console.WriteLine("En boyuk eded: " + enBoyuk);
        Console.WriteLine("En kicik eded: " + enKicik);
        Console.WriteLine("Cut ededlerin sayi: " + cutSay);
        Console.WriteLine("Tek ededlerin sayi: " + tekSay);
    }
}