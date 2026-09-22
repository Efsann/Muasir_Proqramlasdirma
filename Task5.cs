using System;
class Task5
{
    static void Main()
    {
        const double faiz = 0.12;
        Console.Write("İlkin məbləğ daxil edin: ");
        double ilkinMebleg = Convert.ToDouble(Console.ReadLine());
        Console.Write("Neçə il saxlanılacaq: ");
        int ilSayi = Convert.ToInt32(Console.ReadLine());
        double geleceksMebleg = ilkinMebleg * (1 + faiz * ilSayi);
        Console.WriteLine("Gələcək məbləğ: " + geleceksMebleg);
    }
}
