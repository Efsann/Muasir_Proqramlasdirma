using System;
using System.Collections.Generic;

class Vehicle
{
    public string Brand { get; set; }
    public double Speed { get; set; }
    public Vehicle(string brand, double speed)
    {
        Brand = brand;
        Speed = speed;
    }
    public virtual void MakeSound()
    {
        Console.WriteLine("Nəqliyyat vasitəsi səs çıxarır.");
    }
}

class Car : Vehicle
{
    public Car(string brand, double speed) : base(brand, speed) { }
    public override void MakeSound() => Console.WriteLine($"{Brand} (Avtomobil): Bi-bip!");
}

class Motorcycle : Vehicle
{
    public Motorcycle(string brand, double speed) : base(brand, speed) { }
    public override void MakeSound() => Console.WriteLine($"{Brand} (Motosiklet): Vrrr-vrrr!");
}

class Bicycle : Vehicle
{
    public Bicycle(string brand, double speed) : base(brand, speed) { }
    public override void MakeSound() => Console.WriteLine($"{Brand} (Velosiped): Zəng səsi!");
}

class Program{
    static void Main()
    {
        List<Vehicle> vehicles = new List<Vehicle>
        {
            new Car("BMW", 220),
            new Motorcycle("Yamaha", 180),
            new Bicycle("Giant", 30)
        };
        foreach (var vehicle in vehicles)
        {
            vehicle.MakeSound();
        }
    }
}