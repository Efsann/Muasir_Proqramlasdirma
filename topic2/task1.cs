
using System;
using System.Collections.Generic;
class Program
{
    static void Main()
    {
        Dictionary<int, string> telebeler = new Dictionary<int, string>()
        {
            { 1001, "Abbas" },
            { 1002, "Nihad" },
            { 1003, "Fuad" },
            { 1004, "Eyshan" },
            { 1005, "Aqshin" }
        };
        bool davamEt = true;

        while (davamEt)
        {
            Console.WriteLine("\n--- TELEBE MENYUSU ---");
            Console.WriteLine("1. Telebe elave et");
            Console.WriteLine("2. Telebeni ID ile axtar");
            Console.WriteLine("3. Butun telebeleri goster");
            Console.WriteLine("4. Cixis");
            Console.Write("Seciminizi daxil edin: ");

            int secim;

            if (!int.TryParse(Console.ReadLine(), out secim))
            {Console.WriteLine("Zehmet olmasa duzgun reqem daxil edin!");
                continue;}

            switch (secim)
            {
                case 1:
                    Console.Write("Telebenin ID-sini daxil edin: ");
                    int id;

                    if (!int.TryParse(Console.ReadLine(), out id))
                    {
                        Console.WriteLine("ID reqem olmalidir!");
                        break;
                    }

                    if (telebeler.ContainsKey(id))
                    {
                        Console.WriteLine("Bu ID artiq movcuddur!");
                        break;
                    }

                    Console.Write("Telebenin adini daxil edin: ");
                    string ad = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(ad))
                    {
                        Console.WriteLine("Ad bos ola bilmez!");
                        break;
                    }

                    telebeler.Add(id, ad);
                    Console.WriteLine("Telebe ugurla elave edildi!");
                    break;

                case 2:
                    Console.Write("Axtarilan telebenin ID-sini daxil edin: ");
                    int axtarilanID;

                    if (!int.TryParse(Console.ReadLine(), out axtarilanID))
                    {
                        Console.WriteLine("ID reqem olmalidir!");
                        break;
                    }

                    if (telebeler.TryGetValue(axtarilanID, out string telebeAdi))
                    {
                        Console.WriteLine("Telebenin adi: " + telebeAdi);
                    }
                    else
                    {
                        Console.WriteLine("Bu ID ile telebe tapilmadi!");
                    }
                    break;

                case 3:
                    Console.WriteLine("\n--- BUTUN TELEBELER ---");

                    foreach (var telebe in telebeler)
                    {
                        Console.WriteLine(
                            "ID: " + telebe.Key +
                            " | Ad: " + telebe.Value
                        );
                    }
                    break;

                case 4:
                    Console.WriteLine("Proqramdan cixilir...");
                    davamEt = false;
                    break;

                default:
                    Console.WriteLine("Yanlis secim! 1-4 arasi secin.");
                    break;
            }
        }
    }
}