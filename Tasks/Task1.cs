using System;
class Task1
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.Write("Adınızı daxil edin: ");
        string ad = Console.ReadLine();
        Console.Write("Yaşınızı daxil edin: ");
        int yas = int.Parse(Console.ReadLine());
        Console.Write("Boyunuzu daxil edin (məsələn, 1.80): ");
        double boy = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine($"\nSalam, mənim adım {ad}dir. Mən {yas} yaşım var, boyum {boy} m-dir.");
    }
}