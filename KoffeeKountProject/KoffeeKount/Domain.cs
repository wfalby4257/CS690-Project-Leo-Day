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

public class Reminder {
    public DateTime dateAdded;
    public string triggerDate;
    public string triggerTime;
    public int recurCount;
    public int alertIntrvl;
    public string note;

    public Reminder(string triggerDate, string triggerTime, int recurCount = 0, int alertIntrvl = 0, string note = "") {
        this.triggerDate = triggerDate;
        this.triggerTime = triggerTime;        
        this.recurCount = recurCount;
        this.alertIntrvl = alertIntrvl;
        this.note = note;

        this.dateAdded = DateTime.Now;
    }

    public void showReminder() {
        Console.WriteLine("triggerDate = " + triggerDate);
        Console.WriteLine("triggerTime = " + triggerTime);
        Console.WriteLine("dateAdded = " + dateAdded);
        Console.WriteLine("recurCount = " + recurCount);
        Console.WriteLine("alertIntrvl = " + alertIntrvl);

        if (!String.IsNullOrEmpty(note)) {
            Console.WriteLine("note = " + note);
        }
    }
}