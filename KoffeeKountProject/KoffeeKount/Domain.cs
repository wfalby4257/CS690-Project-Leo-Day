namespace KoffeeKount;
using System;
public class Koffee {
    public DateTime dateAdded;
    public double price;
    public int count;
    public string type;
    public int dayNumber;

    public Koffee(double price, int count, string type) {
        this.price = price;
        this.count = count;
        this.type = type;

        this.dateAdded = DateTime.Now;
        this.dayNumber = this.dateAdded.DayOfYear;
    }

    public void showKoffee() {
        Console.WriteLine("price = " + price);
        Console.WriteLine("count = " + count);
        Console.WriteLine("type = " + type);
        Console.WriteLine("dateAdded = " + dateAdded);
        Console.WriteLine("dayNumber = " + dayNumber);
    }
    
}