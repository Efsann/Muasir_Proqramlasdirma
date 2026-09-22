using System;
class Task3
{
    static void Main()
    {
        const double UsdRate = 1.7;
        const double EurRate = 1.82;
        Console.Write("Manat məbləğini daxil edin: ");
        string daxilEdilen = Console.ReadLine();
        double azn;
        bool ugurludur = double.TryParse(daxilEdilen, out azn);
        if (ugurludur)
        {
            double usd = azn / UsdRate;
            double eur = azn / EurRate;
            Console.WriteLine("Dollar: " + usd);
            Console.WriteLine("Avro: " + eur);
        }
        else
        {
            Console.WriteLine("Səhv məbləğ daxil etdiniz!");
        }
    }
}